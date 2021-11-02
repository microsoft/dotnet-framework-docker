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
        private readonly Lazy<string> _skip;
        public string[] SkipOnOsVersions { get; set; } = Array.Empty<string>();
        public string[] SkipOnRuntimeVersions { get; }

        public SkippableTheoryAttribute(params string[] skipOnRuntimeVersions)
        {
            SkipOnRuntimeVersions = skipOnRuntimeVersions;
            _skip = new Lazy<string>(() => LoadSkip());
        }

        public override string Skip { get => _skip.Value; set => throw new NotSupportedException(); }

        private string LoadSkip()
        {
            string skip = CheckForSkip(Config.Version, SkipOnRuntimeVersions);
            if (skip is null)
            {
                skip = CheckForSkip(Config.OS, SkipOnOsVersions);
            }

            return skip;
        }

        private string CheckForSkip(string configuredVersion, string[] skipOnVersions)
        {
            if (!string.IsNullOrEmpty(configuredVersion) && configuredVersion != "*")
            {
                string versionPattern =
                    configuredVersion != null ? Config.GetFilterRegexPattern(configuredVersion) : null;
                foreach (string skipOnVersion in skipOnVersions)
                {
                    if (Regex.IsMatch(skipOnVersion, versionPattern, RegexOptions.IgnoreCase))
                    {
                        return $"{skipOnVersion} is unsupported";
                    }
                }
            }

            return null;
        }
    }
}
