// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    [Trait("Category", "sdk")]
    public partial class SdkOnlyImageTests : ImageTests
    {
        public SdkOnlyImageTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected override string ImageType => "sdk";

        public static IEnumerable<object[]> GetImageData() =>
            ImageTestHelper.ApplyImageDataFilters(TestData.ImageData, ImageTypes.Sdk, allowEmptyResults: true);

        private static bool IsSkippable(ImageDescriptor imageDescriptor) =>
            imageDescriptor is null ||
            imageDescriptor.Version != imageDescriptor.SdkVersion;

        /// <summary>
        /// Verifies the SDK images contain the expected targeting packs.
        /// </summary>
        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyTargetingPacks(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            List<Version> allFrameworkVersions = new()
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
                new Version("4.8"),
                new Version("4.8.1")
            };

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"targetingpacks-{DateTime.Now.ToFileTime()}";
            string command = @"cmd /c dir /B ""C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework""";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            IEnumerable<Version> actualVersions = output.Split(Environment.NewLine)
                .Select(name => new Version(name.Substring(1))); // Trim the first character (v4.0 => 4.0)

            Assert.Equal(allFrameworkVersions, actualVersions);
        }

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string vsPath = imageDescriptor.GetExpectedVsInstallationPath();

            List<EnvironmentVariableInfo> variables =
            [
                ..RuntimeOnlyImageTests.GetEnvironmentVariables(imageDescriptor),
                new EnvironmentVariableInfo("ROSLYN_COMPILER_LOCATION", $@"{vsPath}\MSBuild\Current\Bin\Roslyn"),
                new EnvironmentVariableInfo("DOTNET_GENERATE_ASPNET_CERTIFICATE", "false"),
            ];

            if (imageDescriptor.OsVariant != OsVersion.WSC_LTSC2016 &&
                imageDescriptor.OsVariant != OsVersion.WSC_LTSC2019)
            {
                variables.Add(new EnvironmentVariableInfo("DOTNET_USE_POLLING_FILE_WATCHER", "true"));
            }

            VerifyCommonEnvironmentVariables(variables, imageDescriptor);
        }

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyVsWhereOperability(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"vswhere-{DateTime.Now.ToFileTime()}";

            // Include the prerelease option to allow prerelease versions to be returned. By default they won't be.
            // This allows this test to seamlessly work when testing against a preview version of VS.
            const string command = @"cmd /c ""C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe"" -products * -latest -format json -prerelease";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            JArray json = (JArray)JsonConvert.DeserializeObject(output);

            Assert.Single(json);
            Assert.Equal(imageDescriptor.GetExpectedVsInstallationPath(), json[0]["installationPath"]);

            // Get build version instead of a display version or semantic version because it's easier to parse and can
            // also seamlessly work with preview versions.
            Version actualVsVersion = Version.Parse(json[0]["catalog"]["buildVersion"].ToString());
            Version expectedVsVersion = imageDescriptor.GetExpectedVsVersion();

            // For VS 18 and later, only verify the major version since new minor versions release monthly.
            // See https://learn.microsoft.com/visualstudio/releases/2026/release-notes#december-update-1810
            Assert.Equal(expectedVsVersion.Major, actualVsVersion.Major);
            // For VS versions < 18, continue to verify the minor version.
            if (expectedVsVersion.Major < 18 || actualVsVersion.Major < 18)
            {
                Assert.Equal(expectedVsVersion.Minor, actualVsVersion.Minor);
            }
        }

        [SkippableTheory]
        [Trait("Category", "sdk")]
        [MemberData(nameof(GetImageData))]
        public void VerifyNuGetCli(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"nuget-{DateTime.Now.ToFileTime()}";
            string command = "nuget help";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            Assert.StartsWith("NuGet Version:", output);

            command = @"cmd /c ""C:\Program Files\NuGet\latest\nuget.exe"" help";
            string latestOutput = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);
            Assert.Equal(output, latestOutput);
        }

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyDotNetCli(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"dotnetcli-{DateTime.Now.ToFileTime()}";
            string command = "dotnet --version";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            // Just verify the output is parseable to a Version object (it will throw if it's not)
            // When testing preview versions, the .NET version may be a preview version as well so trim the preview
            // suffix off in that case.
            int suffixIndex = output.IndexOf("-");
            if (suffixIndex >= 0)
            {
                output = output.Substring(0, suffixIndex);
            }

            Version.Parse(output);
        }

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyVsTest(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"vstest-{DateTime.Now.ToFileTime()}";
            string command = "vstest.console.exe /?";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            Assert.Matches(VsTestOutputRegex(), output);
        }

        [GeneratedRegex(@"^VSTest version \d+\.\d+\.\d+")]
        private static partial Regex VsTestOutputRegex();

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyGacUtil(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"gacutil-{DateTime.Now.ToFileTime()}";
            string command = "gacutil";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            Assert.StartsWith("Microsoft (R) .NET Global Assembly Cache Utility", output);
        }

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyWebDeploy(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"webdeploy-{DateTime.Now.ToFileTime()}";

            // Calling msdeploy's help command results in a -1 exit code. To prevent that from bubbling up and causing the
            // docker run call to fail, the call is batched with an echo command that will always run even if msdeploy
            // returns a non-zero exit code. This batch needs to be wrapped in space-separated quotes to work properly.
            string command = @"cmd /S /C "" ""C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe"" -help & echo > nul """;
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            Assert.StartsWith("Microsoft (R) Web Deployment Command Line Tool (MSDeploy.exe)", output);
        }

        [SkippableTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyClickOncePublishing(ImageDescriptor imageDescriptor)
        {
            Skip.If(IsSkippable(imageDescriptor));

            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            List<string> buildArgs = new List<string>
            {
                $"BASE_BUILD_IMAGE={baseBuildImage}",
            };

            string command = $"cmd /s /c dir /b publish";
            string output = ImageTestHelper.BuildAndTestImage(imageDescriptor, buildArgs, "clickonce", command, null);

            string[] outputLines = output.Split(Environment.NewLine);

            Assert.Equal(4, outputLines.Length);
            Assert.Equal("Application Files", outputLines[0]);
            Assert.Equal($"clickonce-{imageDescriptor.Version}.application", outputLines[1]);
            Assert.Equal($"clickonce-{imageDescriptor.Version}.exe", outputLines[2]);
            Assert.Equal($"setup.exe", outputLines[3]);
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
