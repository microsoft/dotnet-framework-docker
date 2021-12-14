﻿// Licensed to the .NET Foundation under one or more agreements.
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
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_20H2 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2022 },
            new ImageDescriptor { Version = "4.6.2", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.1", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_20H2 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2022 },
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
                variables.Add(new EnvironmentVariableInfo("ROSLYN_COMPILER_LOCATION", "C:\\RoslynCompilers-3.6.0\\tools"));
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
            // 3.5 uses cmd: https://github.com/microsoft/dotnet-framework-docker/issues/483
            // Server Core 20H2 and higher also use cmd
            string expectedShellValue;
            if (imageDescriptor.Version != "3.5" &&
                (
                    imageDescriptor.OsVariant == OsVersion.WSC_LTSC2016 ||
                    imageDescriptor.OsVariant == OsVersion.WSC_LTSC2019
                ))
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
