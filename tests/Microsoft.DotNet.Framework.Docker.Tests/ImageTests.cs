// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
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
        private static RuntimeImageDescriptor[] RuntimeTestData = new RuntimeImageDescriptor[]
        {
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = WSC_1803 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = WSC_1903 },
            new RuntimeImageDescriptor { Version = "4.6.2", SdkVersion = "4.8", OsVariant = WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7", SdkVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.1", SdkVersion = "4.7.1", OsVariant = WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.2", SdkVersion = "4.7.2", OsVariant = WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.2", SdkVersion = "4.7.2", OsVariant = WSC_1803 },
            new RuntimeImageDescriptor { Version = "4.7.2", SdkVersion = "4.7.2", OsVariant = WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = WSC_1803 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = WSC_1903 },
        };

        private static ImageDescriptor[] SdkTestData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "3.5", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "3.5", OsVariant = WSC_1803 },
            new ImageDescriptor { Version = "3.5", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { Version = "3.5", OsVariant = WSC_1903 },
            new ImageDescriptor { Version = "4.7.1", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_1803 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_1803 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_1903 },
        };

        private static ImageDescriptor[] AspnetTestData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "3.5", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "3.5", OsVariant = WSC_1803 },
            new ImageDescriptor { Version = "3.5", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { Version = "3.5", OsVariant = WSC_1903 },
            new ImageDescriptor { Version = "4.6.2", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.1", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_1803 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_1903 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_1803 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8",  OsVariant = WSC_1903 },
        };

        private static ImageDescriptor[] WcfTestData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "4.6.2", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.1", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_1803 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_1803 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = WSC_1903 },
        };

        private static Lazy<JArray> ImageInfoData;

        private DockerHelper _dockerHelper;
        private ITestOutputHelper _outputHelper;

        public ImageTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _dockerHelper = new DockerHelper(outputHelper);
        }

        static ImageTests()
        {
            ImageInfoData = new Lazy<JArray>(() =>
            {
                string imageInfoPath = Environment.GetEnvironmentVariable("IMAGE_INFO_PATH");
                if (!String.IsNullOrEmpty(imageInfoPath))
                {
                    string imageInfoContents = File.ReadAllText(imageInfoPath);
                    return JsonConvert.DeserializeObject<JArray>(imageInfoContents);
                }

                return null;
            });
        }

        public static IEnumerable<object[]> GetVerifyRuntimeImagesData()
        {
            return GetVerifyImagesData(RuntimeTestData);
        }

        public static IEnumerable<object[]> GetVerifySdkImagesData()
        {
            return GetVerifyImagesData(SdkTestData);
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
                    || Regex.IsMatch(imageDescriptor.Version, versionFilterPattern, RegexOptions.IgnoreCase))
                .Select(imageDescriptor => new object[] { imageDescriptor });
        }

        private static string GetFilterRegexPattern(string filter)
        {
            return $"^{Regex.Escape(filter).Replace(@"\*", ".*").Replace(@"\?", ".")}$";
        }

        [Theory]
        [Trait("Category", "runtime")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetVerifyRuntimeImagesData))]
        public void VerifyImagesWithApps(RuntimeImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "dotnetapp", "", true);
        }

        [Theory]
        [Trait("Category", "runtime")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetVerifyRuntimeImagesData))]
        public void VerifyImagesWithWebApps(RuntimeImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "webapp", "powershell -command \"dir ./bin/SimpleWebApplication.dll\"", false);
        }

        /// <summary>
        /// Verifies the SDK images contain the expected targeting packs.
        /// </summary>
        [SkippableTheory("4.6.2", "4.7")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetVerifySdkImagesData))]
        public void VerifyTargetingPacks(ImageDescriptor imageDescriptor)
        {
            Version[] allFrameworkVersions = new Version[]
            {
                new Version("4.0"),
                new Version("4.5"),
                new Version("4.5.1"),
                new Version("4.5.2"),
                new Version("4.6"),
                new Version("4.6.1"),
                new Version("4.6.2"),
                new Version("4.7"),
                new Version("4.7.1"),
                new Version("4.7.2"),
                new Version("4.8")
            };

            string baseBuildImage = GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"targetingpacks-{DateTime.Now.ToFileTime()}";
            string command = @"cmd /c dir /B ""C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework""";
            string output = _dockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            IEnumerable<Version> actualVersions = output.Split(Environment.NewLine)
                .Select(name => new Version(name.Substring(1))); // Trim the first character (v4.0 => 4.0)

            Version buildVersion = new Version(imageDescriptor.Version);

            IEnumerable<Version> expectedVersions = allFrameworkVersions;
            if (imageDescriptor.Version != "3.5")
            {
                expectedVersions = allFrameworkVersions.Where(version => version <= buildVersion);
            }

            Assert.Equal(expectedVersions, actualVersions);
        }

        [Theory]
        [Trait("Category", "aspnet")]
        [MemberData(nameof(GetVerifyAspnetImagesData))]
        public void VerifyAspnetImagesWithApps(ImageDescriptor imageDescriptor)
        {
            VerifyAspnetImages(imageDescriptor);
        }

        // Skip the test if it's for 3.5 to avoid empty MemberData (see https://github.com/xunit/xunit/issues/1113)
        [SkippableTheory("3.5")]
        [Trait("Category", "wcf")]
        [MemberData(nameof(GetVerifyWcfImagesData))]
        public void VerifyWcfImagesWithApps(ImageDescriptor imageDescriptor)
        {
            VerifyWcfImages(imageDescriptor);
        }

        private void VerifyFxImages(RuntimeImageDescriptor imageDescriptor, string appDescriptor, string runCommand, bool includeRuntime)
        {
            string baseBuildImage = GetImage("sdk", imageDescriptor.SdkVersion, imageDescriptor.OsVariant);

            List<string> appBuildArgs = new List<string> { $"BASE_BUILD_IMAGE={baseBuildImage}" };
            if (includeRuntime)
            {
                string baseRuntimeImage = GetImage("runtime", imageDescriptor.Version, imageDescriptor.OsVariant);
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

        private void VerifyAspnetImages(ImageDescriptor imageDescriptor)
        {
            List<string> appBuildArgs = new List<string> { };

            string baseAspnetImage = GetImage("aspnet", imageDescriptor.Version, imageDescriptor.OsVariant);
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
            List<string> appBuildArgs = new List<string> { };

            string baseWCFImage = GetImage("wcf", imageDescriptor.Version, imageDescriptor.OsVariant);
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
                $"{appDescriptor}-{imageDescriptor.Version}");

            try
            {
                _dockerHelper.Build(
                    tag: appId,
                    dockerfile: Path.Combine(workDir, "Dockerfile"),
                    contextDir: workDir,
                    buildArgs: buildArgs.ToArray());

                _dockerHelper.Run(image: appId, name: appId, command: runCommand, detach: !string.IsNullOrEmpty(testUrl));
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
            string repo = $"dotnet/framework/{imageType}";
            string tag = $"{version}-{osVariant}";
            string registry = GetRegistryNameWithRepoPrefix(repo, tag);
            string image = $"{registry}{repo}:{tag}";

            // Ensure image exists locally
            if (IsLocalRun)
            {
                Assert.True(DockerHelper.ImageExists(image), $"`{image}` could not be found on disk.");
            }
            else
            {
                _dockerHelper.Pull(image);
            }

            return image;
        }

        private static string GetRegistryNameWithRepoPrefix(string repo, string tag)
        {
            bool isUsingCustomRegistry = true;

            // In the case of running this in a local development environment, there would likely be no image info file
            // provided. In that case, the assumption is that the images exist in the custom configured location.

            if (ImageTests.ImageInfoData.Value != null)
            {
                JObject repoInfo = (JObject)ImageTests.ImageInfoData.Value
                    .FirstOrDefault(imageInfoRepo => imageInfoRepo["repo"].ToString() == repo);

                if (repoInfo["images"] != null)
                {
                    isUsingCustomRegistry = repoInfo["images"]
                        .Cast<JProperty>()
                        .Any(imageInfo => imageInfo.Value["simpleTags"].Any(imageTag => imageTag.ToString() == tag));
                }
                else
                {
                    isUsingCustomRegistry = false;
                }
            }

            return isUsingCustomRegistry ? $"{Registry}/{RepoPrefix}" : $"{GetManifestRegistry()}/";
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

        private class SkippableTheoryAttribute : TheoryAttribute
        {
            public SkippableTheoryAttribute(params string[] skipOnRuntimeVersions)
            {
                if (VersionFilter != "*")
                {
                    string versionFilterPattern = VersionFilter != null ? GetFilterRegexPattern(VersionFilter) : null;
                    foreach (string skipOnRuntimeVersion in skipOnRuntimeVersions)
                    {
                        if (Regex.IsMatch(skipOnRuntimeVersion, versionFilterPattern, RegexOptions.IgnoreCase))
                        {
                            Skip = $"{skipOnRuntimeVersion} is unsupported";
                            break;
                        }
                    }
                }
            }
        }
    }
}
