// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Framework.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class SdkOnlyImageTests : ImageTests
    {
        private static ImageDescriptor[] ImageData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_1903 },
            new ImageDescriptor { Version = "3.5", OsVariant = OsVersion.WSC_1909 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_1903 },
            new ImageDescriptor { Version = "4.8", OsVariant = OsVersion.WSC_1909 },
        };

        public SdkOnlyImageTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected override string ImageType => "sdk";

        public static IEnumerable<object[]> GetImageData()
        {
            return ImageTestHelper.ApplyImageDataFilters(ImageData);
        }

        /// <summary>
        /// Verifies the SDK images contain the expected targeting packs.
        /// </summary>
        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetImageData))]
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

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"targetingpacks-{DateTime.Now.ToFileTime()}";
            string command = @"cmd /c dir /B ""C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework""";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

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

        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            List<EnvironmentVariableInfo> variables = new List<EnvironmentVariableInfo>();

            variables.AddRange(RuntimeOnlyImageTests.GetRuntimeEnvironmentVariableInfos(imageDescriptor));

            variables.Add(new EnvironmentVariableInfo("ROSLYN_COMPILER_LOCATION",
                @"C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn"));

            if (imageDescriptor.OsVariant != OsVersion.WSC_LTSC2016 &&
                imageDescriptor.OsVariant != OsVersion.WSC_LTSC2019 &&
                imageDescriptor.OsVariant != OsVersion.WSC_1903)
            {
                variables.Add(new EnvironmentVariableInfo("DOTNET_USE_POLLING_FILE_WATCHER", "true"));
            }

            VerifyCommonEnvironmentVariables(variables, imageDescriptor);
        }

        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetImageData))]
        public void VerifyVsWhereOperability(ImageDescriptor imageDescriptor)
        {
            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"vswhere-{DateTime.Now.ToFileTime()}";
            const string securityProtocolCmd = "[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12";
            const string vsWhereUrl = "https://github.com/Microsoft/vswhere/releases/download/2.8.4/vswhere.exe";
            string downloadVsWhereCmd = $"Invoke-WebRequest -UseBasicParsing {vsWhereUrl} -OutFile vswhere.exe";
            const string executeVsWhereCmd = @".\vswhere.exe -products * -latest -format json";
            string command = $@"powershell {securityProtocolCmd}; {downloadVsWhereCmd}; {executeVsWhereCmd}";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            JArray json = (JArray)JsonConvert.DeserializeObject(output);

            Assert.Single(json);
            Assert.Equal(@"C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools", json[0]["installationPath"]);

            Version actualVsVersion = Version.Parse(json[0]["catalog"]["productDisplayVersion"].ToString());

            VsInfo vsInfo = Config.GetVsInfo();
            Version expectedVsVersion = Version.Parse(vsInfo.VsVersion);

            Assert.Equal(expectedVsVersion.Major, actualVsVersion.Major);
            Assert.Equal(expectedVsVersion.Minor, actualVsVersion.Minor);
        }
    }
}
