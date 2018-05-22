// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class ImageTests
    {
        private const string WSC_LTSC2016 = "windowsservercore-ltsc2016";
        private const string WSC_1709 = "windowsservercore-1709";
        private const string WSC_1803 = "windowsservercore-1803";

        private static string OSFilter => Environment.GetEnvironmentVariable("IMAGE_OS_FILTER");
        private static string RepoOwner => Environment.GetEnvironmentVariable("REPO_OWNER") ?? "microsoft";
        private static string VersionFilter => Environment.GetEnvironmentVariable("IMAGE_VERSION_FILTER");

        private static ImageDescriptor[] TestData = new ImageDescriptor[]
            {
                new ImageDescriptor { RuntimeVersion = "3.5", BuildVersion = "3.5", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "3.5", BuildVersion = "3.5", OsVariant = WSC_1709 },
                new ImageDescriptor { RuntimeVersion = "3.5", BuildVersion = "3.5", OsVariant = WSC_1803 },
                new ImageDescriptor { RuntimeVersion = "4.6.2", BuildVersion = "4.7.1", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.6.2", BuildVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7", BuildVersion = "4.7.1", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7", BuildVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.1", BuildVersion = "4.7.1", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.1", BuildVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.1", BuildVersion = "4.7.1", OsVariant = WSC_1709 },
                new ImageDescriptor { RuntimeVersion = "4.7.1", BuildVersion = "4.7.2", OsVariant = WSC_1709 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", BuildVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", BuildVersion = "4.7.2", OsVariant = WSC_1709 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", BuildVersion = "4.7.2", OsVariant = WSC_1803 },
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
            string baseBuildImage = $"{RepoOwner}/dotnet-framework:{imageDescriptor.BuildVersion}-sdk-{imageDescriptor.OsVariant}";
            VerifyImageExist(baseBuildImage);

            List<string> appBuildArgs = new List<string> { $"BASE_BUILD_IMAGE={baseBuildImage}"};
            if (includeRuntime)
            {
                string baseRuntimeImage = $"{RepoOwner}/dotnet-framework:{imageDescriptor.RuntimeVersion}-{imageDescriptor.OsVariant}";
                VerifyImageExist(baseRuntimeImage);
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

        private void VerifyImageExist(string image)
        {
            Assert.True(DockerHelper.ImageExists(image), $"`{image}` could not be found on disk.");
        }
    }
}
