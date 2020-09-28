// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    [Trait("Category", "wcf")]
    public class WcfImageTests : ImageTests
    {
        private static readonly ImageDescriptor[] s_imageData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "4.6.2", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.1", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_1903 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_1909 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_2004 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_2009 },
        };

        public WcfImageTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected override string ImageType => "wcf";

        [SkippableTheory("3.5")]
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            VerifyCommonEnvironmentVariables(AspnetImageTests.GetEnvironmentVariables(imageDescriptor), imageDescriptor);
        }

        // Skip the test if it's for 3.5 to avoid empty MemberData (see https://github.com/xunit/xunit/issues/1113)
        [SkippableTheory("3.5")]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithApps(ImageDescriptor imageDescriptor)
        {
            List<string> appBuildArgs = new List<string> { };

            string baseWCFImage = ImageTestHelper.GetImage("wcf", imageDescriptor.Version, imageDescriptor.OsVariant);
            appBuildArgs.Add($"BASE_WCF_IMAGE={baseWCFImage}");

            ImageTestHelper.BuildAndTestImage(
                imageDescriptor: imageDescriptor,
                buildArgs: appBuildArgs,
                appDescriptor: "wcf",
                runCommand: "",
                testUrl: "/Service1.svc"
                );
        }

        // Skip the test if it's for 3.5 to avoid empty MemberData (see https://github.com/xunit/xunit/issues/1113)
        [SkippableTheory("3.5")]
        [MemberData(nameof(GetImageData))]
        public void VerifyNgenQueuesAreEmpty(ImageDescriptor imageDescriptor)
        {
            VerifyCommmonNgenQueuesAreEmpty(imageDescriptor);
        }

        [SkippableTheory("3.5")]
        [MemberData(nameof(GetImageData))]
        public void VerifyShell(ImageDescriptor imageDescriptor)
        {
            string expectedShellValue;
            if (imageDescriptor.OsVariant == OsVersion.WSC_LTSC2016 ||
                imageDescriptor.OsVariant == OsVersion.WSC_LTSC2019 ||
                imageDescriptor.OsVariant == OsVersion.WSC_1903 ||
                imageDescriptor.OsVariant == OsVersion.WSC_1909 ||
                imageDescriptor.OsVariant == OsVersion.WSC_2004)
            {
                expectedShellValue = ShellValue_PowerShell;
            }
            else
            {
                expectedShellValue = ShellValue_Default;
            }

            VerifyCommonShell(imageDescriptor, expectedShellValue);
        }

        public static IEnumerable<object[]> GetImageData()
        {
            return ImageTestHelper.ApplyImageDataFilters(s_imageData);
        }
    }
}
