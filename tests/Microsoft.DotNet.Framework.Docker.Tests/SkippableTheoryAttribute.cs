// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.RegularExpressions;
using Xunit;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class SkippableTheoryAttribute : TheoryAttribute
    {
        public string[] SkipOnOsVersions { get; set; } = Array.Empty<string>();

        public SkippableTheoryAttribute(params string[] skipOnRuntimeVersions)
        {
            bool skipped = CheckForSkip(Config.Version, skipOnRuntimeVersions);
            if (!skipped)
            {
                CheckForSkip(Config.OS, SkipOnOsVersions);
            }
        }

        private bool CheckForSkip(string configuredVersion, string[] skipOnVersions)
        {
            if (!string.IsNullOrEmpty(configuredVersion) && configuredVersion != "*")
            {
                string versionPattern =
                    configuredVersion != null ? Config.GetFilterRegexPattern(configuredVersion) : null;
                foreach (string skipOnVersion in skipOnVersions)
                {
                    if (Regex.IsMatch(skipOnVersion, versionPattern, RegexOptions.IgnoreCase))
                    {
                        Skip = $"{skipOnVersion} is unsupported";
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
