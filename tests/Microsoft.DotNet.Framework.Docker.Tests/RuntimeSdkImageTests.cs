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
    [Trait("Category", "runtime")]
    [Trait("Category", "sdk")]
    public class RuntimeSdkImageTests
    {
        private static readonly RuntimeImageDescriptor[] s_imageData = new RuntimeImageDescriptor[]
        {
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_2004 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_20H2 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_LTSC2022 },
            new RuntimeImageDescriptor { Version = "4.6.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.1", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_2004 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_20H2 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2022 },
        };

        private readonly ImageTestHelper _imageTestHelper;

        public RuntimeSdkImageTests(ITestOutputHelper outputHelper)
        {
            _imageTestHelper = new ImageTestHelper(outputHelper);
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithApps(RuntimeImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "dotnetapp", "", true);
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithWebApps(RuntimeImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "webapp", "powershell -command \"dir ./bin/SimpleWebApplication.dll\"", false);
        }

        [SkippableTheory(
            "3.5", "4.6.2", "4.7", "4.7.1", "4.7.2",
            SkipOnOsVersions = new string[]
            {
                OsVersion.WSC_LTSC2016,
                OsVersion.WSC_2004,
                OsVersion.WSC_20H2
            })]
        [MemberData(nameof(GetImageData))]
        public void ContainerLimits(RuntimeImageDescriptor imageDescriptor)
        {
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

        public static IEnumerable<object[]> GetImageData()
        {
            return ImageTestHelper.ApplyImageDataFilters(s_imageData);
        }

        private string VerifyFxImages(RuntimeImageDescriptor imageDescriptor, string appDescriptor, string runCommand, bool includeRuntime, string optionalRunArgs = null)
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
