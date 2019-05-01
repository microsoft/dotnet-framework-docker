// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class ImageTests
    {
        private const string WSC_LTSC2016 = "windowsservercore-ltsc2016";
        private const string WSC_1803 = "windowsservercore-1803";
        private const string WSC_LTSC2019 = "windowsservercore-ltsc2019";

        private static bool IsLocalRun = Environment.GetEnvironmentVariable("LOCAL_RUN") != null;
        private static string OSFilter => Environment.GetEnvironmentVariable("IMAGE_OS_FILTER");
        public static string RepoPrefix { get; } = Environment.GetEnvironmentVariable("REPO_PREFIX") ?? string.Empty;
        public static string Registry { get; } = Environment.GetEnvironmentVariable("REGISTRY") ?? GetManifestRegistry();
        private static string VersionFilter => Environment.GetEnvironmentVariable("IMAGE_VERSION_FILTER");

        private static ImageDescriptor[] TestData = new ImageDescriptor[]
            {
                new ImageDescriptor { RuntimeVersion = "3.5", BuildVersion = "3.5", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "3.5", BuildVersion = "3.5", OsVariant = WSC_1803 },
                new ImageDescriptor { RuntimeVersion = "3.5", BuildVersion = "3.5", OsVariant = WSC_LTSC2019 },
                new ImageDescriptor { RuntimeVersion = "4.6.2", BuildVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.6.2", BuildVersion = "4.8", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7", BuildVersion = "4.7.1", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7", BuildVersion = "4.8", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.1", BuildVersion = "4.7.1", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.1", BuildVersion = "4.8", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", BuildVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", BuildVersion = "4.7.2", OsVariant = WSC_1803 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", BuildVersion = "4.7.2", OsVariant = WSC_LTSC2019 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", BuildVersion = "4.8", OsVariant = WSC_LTSC2019 },
                new ImageDescriptor { RuntimeVersion = "4.8", BuildVersion = "4.8", OsVariant = WSC_1803 },
                new ImageDescriptor { RuntimeVersion = "4.8", BuildVersion = "4.8", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.8", BuildVersion = "4.8", OsVariant = WSC_LTSC2019 },
            };

        private DockerHelper DockerHelper { get; set; }

        public ImageTests(ITestOutputHelper outputHelper)
        {
            DockerHelper = new DockerHelper(outputHelper);
        }

        public static IEnumerable<object[]> GetVerifyImagesData()
        {
            string versionFilterPattern = VersionFilter != null ? GetFilterRegexPattern(VersionFilter) : null;
            string osFilterPattern = OSFilter != null ? GetFilterRegexPattern(OSFilter) : null;

            // Filter out test data that does not match the active os and version filters.
            return TestData
                .Where(imageDescriptor => OSFilter == null
                    || Regex.IsMatch(imageDescriptor.OsVariant, osFilterPattern, RegexOptions.IgnoreCase))
                .Where(imageDescriptor => VersionFilter == null
                    || Regex.IsMatch(imageDescriptor.RuntimeVersion, versionFilterPattern, RegexOptions.IgnoreCase))
                .Select(imageDescriptor => new object[] { imageDescriptor });
        }

        private static string GetFilterRegexPattern(string filter)
        {
            return $"^{Regex.Escape(filter).Replace(@"\*", ".*").Replace(@"\?", ".")}$";
        }

        [Theory]
        [MemberData(nameof(GetVerifyImagesData))]
        public void VerifyImagesWithApps(ImageDescriptor imageDescriptor)
        {
            VerifyImages(imageDescriptor, "dotnetapp", "", true);
        }

        [Theory]
        [MemberData(nameof(GetVerifyImagesData))]
        public void VerifyImagesWithWebApps(ImageDescriptor imageDescriptor)
        {
            VerifyImages(imageDescriptor, "webapp", "powershell -command \"dir ./bin/SimpleWebApplication.dll\"", false);
        }

        private void VerifyImages(ImageDescriptor imageDescriptor, string appDescriptor, string runCommand, bool includeRuntime)
        {
            string baseBuildImage = GetImage("sdk", imageDescriptor.BuildVersion, imageDescriptor.OsVariant);

            List<string> appBuildArgs = new List<string> { $"BASE_BUILD_IMAGE={baseBuildImage}"};
            if (includeRuntime)
            {
                string baseRuntimeImage = GetImage("runtime", imageDescriptor.RuntimeVersion, imageDescriptor.OsVariant);
                appBuildArgs.Add($"BASE_RUNTIME_IMAGE={baseRuntimeImage}");
            }

            string appId = $"{appDescriptor}-{DateTime.Now.ToFileTime()}";
            string workDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "projects",
                $"{appDescriptor}-{imageDescriptor.RuntimeVersion}");

            try
            {
                DockerHelper.Build(
                    tag: appId,
                    dockerfile: Path.Combine(workDir, "Dockerfile"),
                    buildContextPath: workDir,
                    buildArgs: appBuildArgs);

                DockerHelper.Run(image: appId, containerName: appId, command: runCommand);
            }
            finally
            {
                DockerHelper.DeleteImage(appId);
            }
        }

        private string GetImage(string imageType, string version, string osVariant)
        {
            string image = $"{Registry}/{RepoPrefix}dotnet/framework/{imageType}:{version}-{osVariant}";

            // Ensure image exists locally
            if (IsLocalRun)
            {
                Assert.True(DockerHelper.ImageExists(image), $"`{image}` could not be found on disk.");
            }
            else
            {
                DockerHelper.Pull(image);
            }

            return image;
        }

        private static string GetManifestRegistry()
        {
            string manifestJson = File.ReadAllText("manifest.json");
            JObject manifest = JObject.Parse(manifestJson);
            return (string)manifest["registry"];
        }
    }
}
