// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class RuntimeOnlyImageTests
    {
        private static ImageDescriptor[] ImageData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_1903 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_1909 },
            new ImageDescriptor { Version = "4.6.2", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.1", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_1903 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_1909 },
        };

        private readonly ImageTestHelper imageTestHelper;

        public RuntimeOnlyImageTests(ITestOutputHelper outputHelper)
        {
            imageTestHelper = new ImageTestHelper(outputHelper);
        }

        public static IEnumerable<object[]> GetImageData()
        {
            return ImageTestHelper.ApplyImageDataFilters(ImageData);
        }

        [Theory]
        [Trait("Category", "runtime")]
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            EnvironmentVariableInfo.Validate(
                GetRuntimeEnvironmentVariableInfos(imageDescriptor), "runtime", imageDescriptor, imageTestHelper);
        }

        public static IEnumerable<EnvironmentVariableInfo> GetRuntimeEnvironmentVariableInfos(ImageDescriptor imageDescriptor)
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
