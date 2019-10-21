// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class SdkImageTests
    {
        private static ImageDescriptor[] SdkTestData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_1803 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_1903 },
            new ImageDescriptor { Version = "4.7.1", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_1803 },
            new ImageDescriptor { Version = "4.7.2", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_1803 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_1903 },
        };

        private readonly ImageTestHelper imageTestHelper;

        public SdkImageTests(ITestOutputHelper outputHelper)
        {
            imageTestHelper = new ImageTestHelper(outputHelper);
        }

        public static IEnumerable<object[]> GetVerifySdkImagesData()
        {
            return ImageTestHelper.GetVerifyImagesData(SdkTestData);
        }

        /// <summary>
        /// Verifies the SDK images contain the expected targeting packs.
        /// </summary>
        [SkippableTheory("4.6.2", "4.7")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetVerifySdkImagesData))]
        public void VerifyTargetingPacks(ImageDescriptor imageDescriptor)
        {
            Version[] allFrameworkVersions = new Version[]
            {
                new Version("4.0"),
                new Version("4.5"),
                new Version("4.5.1"),
                new Version("4.5.2"),
                new Version("4.6"),
                new Version("4.6.1"),
                new Version("4.6.2"),
                new Version("4.7"),
                new Version("4.7.1"),
                new Version("4.7.2"),
                new Version("4.8")
            };

            string baseBuildImage = imageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"targetingpacks-{DateTime.Now.ToFileTime()}";
            string command = @"cmd /c dir /B ""C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework""";
            string output = imageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            IEnumerable<Version> actualVersions = output.Split(Environment.NewLine)
                .Select(name => new Version(name.Substring(1))); // Trim the first character (v4.0 => 4.0)

            Version buildVersion = new Version(imageDescriptor.Version);

            IEnumerable<Version> expectedVersions = allFrameworkVersions;
            if (imageDescriptor.Version != "3.5")
            {
                expectedVersions = allFrameworkVersions.Where(version => version <= buildVersion);
            }

            Assert.Equal(expectedVersions, actualVersions);
        }
    }
}
