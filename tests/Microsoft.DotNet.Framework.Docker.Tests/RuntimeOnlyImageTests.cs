// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    [Trait("Category", "runtime")]
    public class RuntimeOnlyImageTests : ImageTests
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

        public RuntimeOnlyImageTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected override string ImageType => "runtime";

        public static IEnumerable<object[]> GetImageData()
        {
            return ImageTestHelper.ApplyImageDataFilters(s_imageData);
        }

        [Theory]
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            VerifyCommonEnvironmentVariables(GetEnvironmentVariables(imageDescriptor), imageDescriptor);
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
            VerifyCommonShell(imageDescriptor, ShellValue_Default);
        }

        public static IEnumerable<EnvironmentVariableInfo> GetEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            yield return new EnvironmentVariableInfo("COMPLUS_NGenProtectedProcess_FeatureEnabled", "0");

            if ((imageDescriptor.Version == "3.5" || imageDescriptor.Version == "4.8") &&
                imageDescriptor.OsVariant != OsVersion.WSC_LTSC2016 &&
                imageDescriptor.OsVariant != OsVersion.WSC_LTSC2019 &&
                imageDescriptor.OsVariant != OsVersion.WSC_1903)
            {
                yield return new EnvironmentVariableInfo("DOTNET_RUNNING_IN_CONTAINER", "true");
            }
        }
    }
}
