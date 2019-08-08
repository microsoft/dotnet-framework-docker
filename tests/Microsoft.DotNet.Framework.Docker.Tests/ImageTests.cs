// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
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
        private const string WSC_1903 = "windowsservercore-1903";

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
                new ImageDescriptor { RuntimeVersion = "3.5", BuildVersion = "3.5", OsVariant = WSC_1903 },
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
                new ImageDescriptor { RuntimeVersion = "4.8", BuildVersion = "4.8", OsVariant = WSC_1903 },
            };

        private static ImageDescriptor[] AspnetTestData = new ImageDescriptor[]
            {
                new ImageDescriptor { RuntimeVersion = "3.5", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "3.5", OsVariant = WSC_1803 },
                new ImageDescriptor { RuntimeVersion = "3.5", OsVariant = WSC_LTSC2019 },
                new ImageDescriptor { RuntimeVersion = "3.5", OsVariant = WSC_1903 },
                new ImageDescriptor { RuntimeVersion = "4.6.2", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.1", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", OsVariant = WSC_1803 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", OsVariant = WSC_LTSC2019 },
                new ImageDescriptor { RuntimeVersion = "4.7.2", OsVariant = WSC_1903 },
                new ImageDescriptor { RuntimeVersion = "4.8", OsVariant = WSC_1803 },
                new ImageDescriptor { RuntimeVersion = "4.8", OsVariant = WSC_LTSC2016 },
                new ImageDescriptor { RuntimeVersion = "4.8", OsVariant = WSC_LTSC2019 },
                new ImageDescriptor { RuntimeVersion = "4.8",  OsVariant = WSC_1903 },
            };
        private static ImageDescriptor[] WcfTestData = new ImageDescriptor[]
          {
            new ImageDescriptor { RuntimeVersion = "4.6.2", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { RuntimeVersion = "4.7", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { RuntimeVersion = "4.7.1", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { RuntimeVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { RuntimeVersion = "4.7.2", OsVariant = WSC_1803 },
            new ImageDescriptor { RuntimeVersion = "4.7.2", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { RuntimeVersion = "4.8", OsVariant = WSC_1803 },
            new ImageDescriptor { RuntimeVersion = "4.8", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { RuntimeVersion = "4.8", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { RuntimeVersion = "4.8", OsVariant = WSC_1903 },
          };

        private DockerHelper _dockerHelper;
        private ITestOutputHelper _outputHelper;

        public ImageTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _dockerHelper = new DockerHelper(outputHelper);
        }

        public static IEnumerable<object[]> GetVerifyRuntimeImagesData()
        {
            return GetVerifyImagesData(TestData);
        }

        public static IEnumerable<object[]> GetVerifyAspnetImagesData()
        {
            return GetVerifyImagesData(AspnetTestData);
        }
      
        public static IEnumerable<object[]> GetVerifyWcfImagesData()
        {
            return GetVerifyImagesData(WcfTestData);
        }

        public static IEnumerable<object[]> GetVerifyImagesData(IEnumerable<ImageDescriptor> imageDescriptors)
        {
            string versionFilterPattern = VersionFilter != null ? GetFilterRegexPattern(VersionFilter) : null;
            string osFilterPattern = OSFilter != null ? GetFilterRegexPattern(OSFilter) : null;

            // Filter out test data that does not match the active os and version filters.
            return imageDescriptors
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
        [Trait("Category", "Runtime")]
        [MemberData(nameof(GetVerifyRuntimeImagesData))]
        public void VerifyImagesWithApps(ImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "dotnetapp", "", true);
        }

        [Theory]
        [Trait("Category", "Runtime")]
        [MemberData(nameof(GetVerifyRuntimeImagesData))]
        public void VerifyImagesWithWebApps(ImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "webapp", "powershell -command \"dir ./bin/SimpleWebApplication.dll\"", false);
        }

        [Theory]
        [Trait("Category", "ASPNET")]
        [MemberData(nameof(GetVerifyAspnetImagesData))]
        public void VerifyAspnetImagesWithApps(ImageDescriptor imageDescriptor)
        {
            VerifyAspnetImages(imageDescriptor, "");
        }


        [Theory]
        [Trait("Category", "WCF")]
        [MemberData(nameof(GetVerifyWcfImagesData))]
        public void VerifyWcfImagesWithApps(ImageDescriptor imageDescriptor)
        {
            VerifyWcfImages(imageDescriptor);
        }

        private void VerifyFxImages(ImageDescriptor imageDescriptor, string appDescriptor, string runCommand, bool includeRuntime)
        {
            string baseBuildImage = GetImage("sdk", imageDescriptor.BuildVersion, imageDescriptor.OsVariant);

            List<string> appBuildArgs = new List<string> { $"BASE_BUILD_IMAGE={baseBuildImage}" };
            if (includeRuntime)
            {
                string baseRuntimeImage = GetImage("runtime", imageDescriptor.RuntimeVersion, imageDescriptor.OsVariant);
                appBuildArgs.Add($"BASE_RUNTIME_IMAGE={baseRuntimeImage}");
            }

            VerifyImages(
                imageDescriptor: imageDescriptor,
                buildArgs: appBuildArgs,
                appDescriptor: appDescriptor,
                runCommand: runCommand,
                testUrl: ""
                );
        }
        private void VerifyAspnetImages(ImageDescriptor imageDescriptor, string runCommand)
        {
            List<string> appBuildArgs = new List<string> {  };

            string baseAspnetImage = GetImage("aspnet", imageDescriptor.RuntimeVersion, imageDescriptor.OsVariant);
            appBuildArgs.Add($"BASE_ASPNET_IMAGE={baseAspnetImage}");

            VerifyImages(
                imageDescriptor: imageDescriptor,
                buildArgs: appBuildArgs,
                appDescriptor: "aspnet",
                runCommand: "",
                testUrl: "/hello-world.aspx"
                );
        }

        private void VerifyWcfImages(ImageDescriptor imageDescriptor)
        {
            List<string> appBuildArgs = new List<string> {  };

            string baseWCFImage = GetImage("wcf", imageDescriptor.RuntimeVersion, imageDescriptor.OsVariant);
            appBuildArgs.Add($"BASE_WCF_IMAGE={baseWCFImage}");

            VerifyImages(
                imageDescriptor: imageDescriptor,
                buildArgs: appBuildArgs,
                appDescriptor: "wcf",
                runCommand: "",
                testUrl: "/Service1.svc"
                );

        }

        private void VerifyImages(
            ImageDescriptor imageDescriptor,
            IEnumerable<string> buildArgs,
            string appDescriptor, 
            string runCommand, 
            string testUrl)
        {
            string appId = $"{appDescriptor}-{DateTime.Now.ToFileTime()}";
            string workDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "projects",
                $"{appDescriptor}-{imageDescriptor.RuntimeVersion}");

            try
            {
                _dockerHelper.Build(
                    tag: appId,
                    dockerfile: Path.Combine(workDir, "Dockerfile"),
                    buildContextPath: workDir,
                    buildArgs: buildArgs);

                _dockerHelper.Run(image: appId, containerName: appId, command: runCommand, detach: !string.IsNullOrEmpty(testUrl));
                if (!string.IsNullOrEmpty(testUrl))
                {
                    VerifyHttpResponseFromContainer(appId, testUrl);
                }
            }
            finally
            {
                _dockerHelper.DeleteContainer(appId, !string.IsNullOrEmpty(testUrl));
                _dockerHelper.DeleteImage(appId);
            }
        }

        private string GetImage(string imageType, string version, string osVariant)
        {
            string image = $"{Registry}/{RepoPrefix}dotnet/framework/{imageType}:{version}-{osVariant}";

            // Ensure image exists locally
            if (IsLocalRun)
            {
                Assert.True(_dockerHelper.ImageExists(image), $"`{image}` could not be found on disk.");
            }
            else
            {
                _dockerHelper.Pull(image);
            }

            return image;
        }

        private static string GetManifestRegistry()
        {
            string manifestJson = File.ReadAllText("manifest.json");
            JObject manifest = JObject.Parse(manifestJson);
            return (string)manifest["registry"];
        }
  
        private void VerifyHttpResponseFromContainer(string containerName, string urlPath)
        {
            var retries = 30;

            // Can't use localhost when running inside containers or Windows.
            var url = $"http://{_dockerHelper.GetContainerAddress(containerName)}" + urlPath;

            using (HttpClient client = new HttpClient())
            {
                while (retries > 0)
                {
                    retries--;
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    try
                    {
                        using (HttpResponseMessage result = client.GetAsync(url).Result)
                        {
                            result.EnsureSuccessStatusCode();
                        }

                        _outputHelper.WriteLine($"Successfully accessed {url}");
                        return;
                    }
                    catch (Exception ex)
                    {
                        _outputHelper.WriteLine($"Request to {url} failed - retrying: {ex.ToString()}");
                    }
                }
            }
            throw new TimeoutException($"Timed out attempting to access the endpoint {url} on container {containerName}");
        }
    }
}
