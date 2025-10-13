// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    [Trait("Category", ImageTypes.Runtime)]
    public class RuntimeOnlyImageTests : ImageTests
    {
        public RuntimeOnlyImageTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected override string ImageType => ImageTypes.Runtime;

        public static IEnumerable<object[]> GetImageData() =>
            ImageTestHelper.ApplyImageDataFilters(TestData.ImageData, ImageTypes.Runtime);

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
                imageDescriptor.OsVariant != OsVersion.WSC_LTSC2019)
            {
                yield return new EnvironmentVariableInfo("DOTNET_RUNNING_IN_CONTAINER", "true");
            }
        }
    }
}
