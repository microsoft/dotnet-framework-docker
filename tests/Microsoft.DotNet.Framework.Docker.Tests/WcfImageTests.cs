﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    [Trait("Category", ImageTypes.Wcf)]
    public class WcfImageTests : ImageTests
    {
        public WcfImageTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected override string ImageType => ImageTypes.Wcf;

        public static IEnumerable<object[]> GetImageData()
        {
            IEnumerable<object[]> imageData =
                ImageTestHelper.ApplyImageDataFilters(TestData.GetImageData(), ImageTypes.Wcf);
            if (!imageData.Any())
            {
                Assert.NotEmpty(Config.Paths);
                Assert.True(
                    !Config.Paths.Any(path => path.Contains(ImageTypes.Wcf)),
                    "Image data filtering incorrectly filtered out WCF test data");

                // XUnit requires MemberData to return a non-empty set: https://github.com/xunit/xunit/issues/1113
                // Set a null placeholder to all the test to skip the test.
                imageData = new object[][] { new object[] { null } };
            }

            return imageData;
        }

        private static bool IsSkippable(ImageDescriptor imageDescriptor) =>
            imageDescriptor is null || imageDescriptor.Version == "3.5";

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            VerifyCommonEnvironmentVariables(AspnetImageTests.GetEnvironmentVariables(imageDescriptor), imageDescriptor);
        }

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithApps(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

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

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyNgenQueuesAreEmpty(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            VerifyCommmonNgenQueuesAreEmpty(imageDescriptor);
        }

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyShell(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string expectedShellValue;
            if (imageDescriptor.OsVariant == OsVersion.WSC_LTSC2016 ||
                imageDescriptor.OsVariant == OsVersion.WSC_LTSC2019)
            {
                expectedShellValue = ShellValue_PowerShell;
            }
            else
            {
                expectedShellValue = ShellValue_Default;
            }

            VerifyCommonShell(imageDescriptor, expectedShellValue);
        }
    }
}
