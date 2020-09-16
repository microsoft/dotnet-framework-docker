// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    [Trait("Category", "aspnet")]
    public class AspnetImageTests : ImageTests
    {
        private static readonly ImageDescriptor[] s_imageData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_1903 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_1909 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_2004 },
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
        };

        public AspnetImageTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected override string ImageType => "aspnet";

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            VerifyCommonEnvironmentVariables(GetEnvironmentVariables(imageDescriptor), imageDescriptor);
        }

        public static IEnumerable<EnvironmentVariableInfo> GetEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            List<EnvironmentVariableInfo> variables = new List<EnvironmentVariableInfo>();
            variables.AddRange(RuntimeOnlyImageTests.GetEnvironmentVariables(imageDescriptor));

            if (imageDescriptor.Version != "3.5")
            {
                variables.Add(new EnvironmentVariableInfo("ROSLYN_COMPILER_LOCATION", "c:\\RoslynCompilers\\tools"));
            }

            return variables;
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyImagesWithApps(ImageDescriptor imageDescriptor)
        {
            List<string> appBuildArgs = new List<string> { };

            string baseAspnetImage = ImageTestHelper.GetImage("aspnet", imageDescriptor.Version, imageDescriptor.OsVariant);
            appBuildArgs.Add($"BASE_ASPNET_IMAGE={baseAspnetImage}");

            ImageTestHelper.BuildAndTestImage(
                imageDescriptor: imageDescriptor,
                buildArgs: appBuildArgs,
                appDescriptor: "aspnet",
                runCommand: "",
                testUrl: "/hello-world.aspx"
                );
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyNgenQueuesAreEmpty(ImageDescriptor imageDescriptor)
        {
            VerifyCommmonNgenQueuesAreEmpty(imageDescriptor);
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyShell(ImageDescriptor imageDescriptor)
        {
            // 3.5 differs from the rest: https://github.com/microsoft/dotnet-framework-docker/issues/483
            string expectedShellValue = imageDescriptor.Version == "3.5" ? ShellValue_Default : ShellValue_PowerShell;
            VerifyCommonShell(imageDescriptor, expectedShellValue);
        }

        public static IEnumerable<object[]> GetImageData()
        {
            return ImageTestHelper.ApplyImageDataFilters(s_imageData);
        }
    }
}
