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
    public abstract class ImageTests
    {
        protected const string ShellValue_PowerShell = "[powershell -Command $ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';]";
        protected const string ShellValue_Default = "[]";

        protected ImageTests(ITestOutputHelper outputHelper)
        {
            ImageTestHelper = new ImageTestHelper(outputHelper);
        }

        protected abstract string ImageType { get; }

        protected ImageTestHelper ImageTestHelper { get; }

        protected void VerifyCommmonNgenQueuesAreEmpty(ImageDescriptor imageDescriptor)
        {
            VerifyNgenQueueIsUpToDate(imageDescriptor, ImageType, @"\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen display");
            VerifyNgenQueueIsUpToDate(imageDescriptor, ImageType, @"\Windows\Microsoft.NET\Framework\v4.0.30319\ngen display");
        }

        protected void VerifyCommonEnvironmentVariables(IEnumerable<EnvironmentVariableInfo> variables, ImageDescriptor imageDescriptor)
        {
            const char delimiter = '|';
            string appId = $"envvar-{DateTime.Now.ToFileTime()}";
            IEnumerable<string> echoParts= variables.Select(envVar => $"%{envVar.Name}%");

            string combinedValues = ImageTestHelper.DockerHelper.Run(
                image: ImageTestHelper.GetImage(ImageType, imageDescriptor.Version, imageDescriptor.OsVariant),
                name: appId,
                entrypointOverride: "cmd",
                command: $"CMD /S /C \"echo {String.Join($"^{delimiter}", echoParts)}\"");

            string[] values = combinedValues.Split(delimiter);
            Assert.Equal(variables.Count(), values.Count());

            for (int i = 0; i < values.Count(); i++)
            {
                EnvironmentVariableInfo variable = variables.ElementAt(i);

                string actualValue;
                // Process unset variables in Windows
                if (!DockerHelper.IsLinuxContainerModeEnabled
                    && string.Equals(values[i], $"%{variable.Name}%", StringComparison.Ordinal))
                {
                    actualValue = string.Empty;
                }
                else
                {
                    actualValue = values[i];
                }

                if (variable.AllowAnyValue)
                {
                    Assert.NotEmpty(actualValue);
                }
                else
                {
                    Assert.Equal(variable.ExpectedValue, actualValue);
                }
            }
        }

        protected void VerifyCommonShell(ImageDescriptor imageDescriptor, string expectedShellValue)
        {
            string imageTag = ImageTestHelper.GetImage(ImageType, imageDescriptor.Version, imageDescriptor.OsVariant);
            string shell = ImageTestHelper.DockerHelper.GetImageShell(imageTag);

            Assert.Equal(expectedShellValue, shell);
        }

        private void VerifyNgenQueueIsUpToDate(ImageDescriptor imageDescriptor, string imageType, string ngenCommand)
        {
            string appId = $"ngen-{DateTime.Now.ToFileTime()}";

            string result = ImageTestHelper.DockerHelper.Run(
                image: ImageTestHelper.GetImage(imageType, imageDescriptor.Version, imageDescriptor.OsVariant),
                name: appId,
                entrypointOverride: "cmd",
                command: $"/c {ngenCommand}");

            Assert.DoesNotContain("(StatusPending)", result);
        }
    }
}
