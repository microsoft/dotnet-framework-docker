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
    [Trait("Category", "sdk")]
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
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ImageDescriptor imageDescriptor)
        {
            List<EnvironmentVariableInfo> variables = new List<EnvironmentVariableInfo>();

            variables.AddRange(RuntimeOnlyImageTests.GetEnvironmentVariables(imageDescriptor));

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
        [MemberData(nameof(GetImageData))]
        public void VerifyNuGetCli(ImageDescriptor imageDescriptor)
        {
            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"nuget-{DateTime.Now.ToFileTime()}";
            string command = "nuget help";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            Assert.StartsWith("NuGet Version:", output);
        }

        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [MemberData(nameof(GetImageData))]
        public void VerifyDotNetCli(ImageDescriptor imageDescriptor)
        {
            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"dotnetcli-{DateTime.Now.ToFileTime()}";
            string command = "dotnet --version";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            // Just verify the output is parseable to a Version object (it will throw if it's not)
            Version.Parse(output);
        }

        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [MemberData(nameof(GetImageData))]
        public void VerifyVsTest(ImageDescriptor imageDescriptor)
        {
            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"vstest-{DateTime.Now.ToFileTime()}";
            string command = "vstest.console.exe /?";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            Assert.StartsWith("Microsoft (R) Test Execution Command Line Tool", output);
        }

        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [MemberData(nameof(GetImageData))]
        public void VerifyGacUtil(ImageDescriptor imageDescriptor)
        {
            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"gacutil-{DateTime.Now.ToFileTime()}";
            string command = "gacutil";
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            Assert.StartsWith("Microsoft (R) .NET Global Assembly Cache Utility", output);
        }

        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [MemberData(nameof(GetImageData))]
        public void VerifyWebDeploy(ImageDescriptor imageDescriptor)
        {
            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            string appId = $"webdeploy-{DateTime.Now.ToFileTime()}";
            
            // Calling msdeploy's help command results in a -1 exit code. To prevent that from bubbling up and causing the
            // docker run call to fail, the call is batched with an echo command that will always run even if msdeploy
            // returns a non-zero exit code. This batch needs to be wrapped in space-separated quotes to work properly.
            string command = @"cmd /S /C "" ""C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe"" -help & echo > nul """;
            string output = ImageTestHelper.DockerHelper.Run(image: baseBuildImage, name: appId, command: command);

            Assert.StartsWith("Microsoft (R) Web Deployment Command Line Tool (MSDeploy.exe)", output);
        }

        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [MemberData(nameof(GetImageData))]
        public void VerifyClickOncePublishing(ImageDescriptor imageDescriptor)
        {
            string baseBuildImage = ImageTestHelper.GetImage("sdk", imageDescriptor.Version, imageDescriptor.OsVariant);
            const string PublishDir = "publish";
            List<string> buildArgs = new List<string>
            {
                $"BASE_BUILD_IMAGE={baseBuildImage}",
                $"PUBLISH_DIR={PublishDir}/"
            };

            string command = $"cmd /s /c dir /b {PublishDir}";
            string output = ImageTestHelper.BuildAndTestImage(imageDescriptor, buildArgs, "clickonce", command, null);

            string[] outputLines = output.Split(Environment.NewLine);

            Assert.Equal(4, outputLines.Length);
            Assert.Equal("Application Files", outputLines[0]);
            Assert.Equal($"clickonce-{imageDescriptor.Version}.application", outputLines[1]);
            Assert.Equal($"clickonce-{imageDescriptor.Version}.exe", outputLines[2]);
            Assert.Equal($"setup.exe", outputLines[3]);
        }

        [SkippableTheory("4.6.2", "4.7", "4.7.1", "4.7.2")]
        [MemberData(nameof(GetImageData))]
        public void VerifyShell(ImageDescriptor imageDescriptor)
        {
            VerifyCommonShell(imageDescriptor, ShellValue_PowerShell);
        }
    }
}
