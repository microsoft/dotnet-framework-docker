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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class ImageTestHelper
    {
        private static Lazy<JArray> ImageInfoData;

        public DockerHelper DockerHelper { get; }
        private ITestOutputHelper _outputHelper;

        public ImageTestHelper(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            DockerHelper = new DockerHelper(outputHelper);
        }

        static ImageTestHelper()
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

        public static IEnumerable<object[]> ApplyImageDataFilters(IEnumerable<ImageDescriptor> imageDescriptors)
        {
            string versionFilterPattern =
                Config.VersionFilter != null ? Config.GetFilterRegexPattern(Config.VersionFilter) : null;
            string osFilterPattern =
                Config.OSFilter != null ? Config.GetFilterRegexPattern(Config.OSFilter) : null;

            // Filter out test data that does not match the active os and version filters.
            return imageDescriptors
                .Where(imageDescriptor => Config.OSFilter == null
                    || Regex.IsMatch(imageDescriptor.OsVariant, osFilterPattern, RegexOptions.IgnoreCase))
                .Where(imageDescriptor => Config.VersionFilter == null
                    || Regex.IsMatch(imageDescriptor.Version, versionFilterPattern, RegexOptions.IgnoreCase))
                .Select(imageDescriptor => new object[] { imageDescriptor });
        }

        public string BuildAndTestImage(
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

            string output;

            try
            {
                DockerHelper.Build(
                    tag: appId,
                    dockerfile: Path.Combine(workDir, "Dockerfile"),
                    contextDir: workDir,
                    buildArgs: buildArgs.ToArray());

                output = DockerHelper.Run(image: appId, name: appId, command: runCommand, detach: !string.IsNullOrEmpty(testUrl));
                if (!string.IsNullOrEmpty(testUrl))
                {
                    VerifyHttpResponseFromContainer(appId, testUrl);
                }
            }
            finally
            {
                DockerHelper.DeleteContainer(appId, !string.IsNullOrEmpty(testUrl));
                DockerHelper.DeleteImage(appId);
            }

            return output;
        }

        public string GetImage(string imageType, string version, string osVariant)
        {
            string repo = $"dotnet/framework/{imageType}";
            string tag = $"{version}-{osVariant}";
            string registry = GetRegistryNameWithRepoPrefix(repo, tag);
            string image = $"{registry}{repo}:{tag}";

            // Ensure image exists locally
            if (Config.PullImages)
            {
                DockerHelper.Pull(image);
            }
            else
            {
                Assert.True(DockerHelper.ImageExists(image), $"`{image}` could not be found on disk.");
            }

            return image;
        }

        private static string GetRegistryNameWithRepoPrefix(string repo, string tag)
        {
            bool isUsingCustomRegistry = true;

            // In the case of running this in a local development environment, there would likely be no image info file
            // provided. In that case, the assumption is that the images exist in the custom configured location.

            if (ImageTestHelper.ImageInfoData.Value != null)
            {
                JObject repoInfo = (JObject)ImageTestHelper.ImageInfoData.Value
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

            return isUsingCustomRegistry ? $"{Config.Registry}/{Config.RepoPrefix}" : $"{Config.GetManifestRegistry()}/";
        }

        private void VerifyHttpResponseFromContainer(string containerName, string urlPath)
        {
            var retries = 30;

            // Can't use localhost when running inside containers or Windows.
            var url = $"http://{DockerHelper.GetContainerAddress(containerName)}" + urlPath;

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
