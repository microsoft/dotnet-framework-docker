// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class RuntimeSdkImageTests
    {
        private static RuntimeImageDescriptor[] ImageData = new RuntimeImageDescriptor[]
        {
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_1903 },
            new RuntimeImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_1909 },
            new RuntimeImageDescriptor { Version = "4.6.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.1", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.7.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_1903 },
            new RuntimeImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_1909 },
        };

        private readonly ImageTestHelper imageTestHelper;

        public RuntimeSdkImageTests(ITestOutputHelper outputHelper)
        {
            imageTestHelper = new ImageTestHelper(outputHelper);
        }

        [Theory]
        [Trait("Category", "runtime")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithApps(RuntimeImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "dotnetapp", "", true);
        }

        [Theory]
        [Trait("Category", "runtime")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithWebApps(RuntimeImageDescriptor imageDescriptor)
        {
            VerifyFxImages(imageDescriptor, "webapp", "powershell -command \"dir ./bin/SimpleWebApplication.dll\"", false);
        }

        public static IEnumerable<object[]> GetImageData()
        {
            return ImageTestHelper.ApplyImageDataFilters(ImageData);
        }

        private void VerifyFxImages(RuntimeImageDescriptor imageDescriptor, string appDescriptor, string runCommand, bool includeRuntime)
        {
            string baseBuildImage = imageTestHelper.GetImage("sdk", imageDescriptor.SdkVersion, imageDescriptor.OsVariant);

            List<string> appBuildArgs = new List<string> { $"BASE_BUILD_IMAGE={baseBuildImage}" };
            if (includeRuntime)
            {
                string baseRuntimeImage = imageTestHelper.GetImage("runtime", imageDescriptor.Version, imageDescriptor.OsVariant);
                appBuildArgs.Add($"BASE_RUNTIME_IMAGE={baseRuntimeImage}");
            }

            imageTestHelper.BuildAndTestImage(
                imageDescriptor: imageDescriptor,
                buildArgs: appBuildArgs,
                appDescriptor: appDescriptor,
                runCommand: runCommand,
                testUrl: ""
                );
        }
    }
}
