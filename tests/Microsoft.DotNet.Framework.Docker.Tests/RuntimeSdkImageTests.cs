// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    [Trait("Category", ImageTypes.Runtime)]
    [Trait("Category", ImageTypes.Sdk)]
    public class RuntimeSdkImageTests
    {
        private readonly ImageTestHelper _imageTestHelper;

        public RuntimeSdkImageTests(ITestOutputHelper outputHelper)
        {
            _imageTestHelper = new ImageTestHelper(outputHelper);
        }

        public static IEnumerable<object[]> GetImageData()
        {
            return ImageTestHelper.ApplyImageDataFilters(TestData.GetImageData(), ImageTypes.Runtime);
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithApps(ImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "dotnetapp", "", true);
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithWebApps(ImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "webapp", "powershell -command \"dir ./bin/SimpleWebApplication.dll\"", false);
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void ContainerLimits(ImageDescriptor imageDescriptor)
        {
            // Container limits are only supported on 4.8 for Server 2019 and 2022.
            if (imageDescriptor.Version != "4.8" ||
                (imageDescriptor.OsVariant != OsVersion.WSC_LTSC2019 &&
                imageDescriptor.OsVariant != OsVersion.WSC_LTSC2022))
            {
                _imageTestHelper.OutputHelper.WriteLine("Test skipped due to unsupported version.");
                return;
            }

            string appId = $"container-limits-{DateTime.Now.ToFileTime()}";
            string workDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "projects",
                $"dotnetapp-{imageDescriptor.Version}");

            string baseBuildImage = _imageTestHelper.GetImage("sdk", imageDescriptor.SdkVersion, imageDescriptor.OsVariant);
            string baseRuntimeImage = _imageTestHelper.GetImage("runtime", imageDescriptor.Version, imageDescriptor.OsVariant);
            List<string> appBuildArgs = new ()
            {
                $"BASE_BUILD_IMAGE={baseBuildImage}",
                $"BASE_RUNTIME_IMAGE={baseRuntimeImage}"
            };

            string runCommand = "getProcessorCount";

            try
            {
                _imageTestHelper.DockerHelper.Build(
                    tag: appId,
                    dockerfile: Path.Combine(workDir, "Dockerfile"),
                    contextDir: workDir,
                    buildArgs: appBuildArgs.ToArray());

                string output = _imageTestHelper.DockerHelper.Run(image: appId, name: appId, command: runCommand, optionalRunArgs: "--cpus 0.5");
                Assert.Equal("1", output);

                string processorCount = "20";
                output = _imageTestHelper.DockerHelper.Run(image: appId, name: appId, command: runCommand, optionalRunArgs: $"--cpus 0.5 -e COMPLUS_PROCESSOR_COUNT={processorCount}");
                Assert.Equal(processorCount, output);
            }
            finally
            {
                _imageTestHelper.DockerHelper.DeleteContainer(appId);
                _imageTestHelper.DockerHelper.DeleteImage(appId);
            }
        }

        private string VerifyFxImages(ImageDescriptor imageDescriptor, string appDescriptor, string runCommand, bool includeRuntime, string optionalRunArgs = null)
        {
            string baseBuildImage = _imageTestHelper.GetImage("sdk", imageDescriptor.SdkVersion, imageDescriptor.OsVariant);

            List<string> appBuildArgs = new List<string> { $"BASE_BUILD_IMAGE={baseBuildImage}" };
            if (includeRuntime)
            {
                string baseRuntimeImage = _imageTestHelper.GetImage("runtime", imageDescriptor.Version, imageDescriptor.OsVariant);
                appBuildArgs.Add($"BASE_RUNTIME_IMAGE={baseRuntimeImage}");
            }

            return _imageTestHelper.BuildAndTestImage(
                imageDescriptor: imageDescriptor,
                buildArgs: appBuildArgs,
                appDescriptor: appDescriptor,
                runCommand: runCommand,
                testUrl: "",
                optionalRunArgs
                );
        }
    }
}
