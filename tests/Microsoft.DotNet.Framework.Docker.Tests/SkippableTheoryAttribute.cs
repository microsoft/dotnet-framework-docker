// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;
using Xunit;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class SkippableTheoryAttribute : TheoryAttribute
    {
        public string[] SkipOnRuntimeVersions { get; }

        public SkippableTheoryAttribute(params string[] skipOnRuntimeVersions)
        {
            if (!string.IsNullOrEmpty(Config.Version) && Config.Version != "*")
            {
                string versionPattern =
                    Config.Version != null ? Config.GetFilterRegexPattern(Config.Version) : null;
                foreach (string skipOnRuntimeVersion in skipOnRuntimeVersions)
                {
                    if (Regex.IsMatch(skipOnRuntimeVersion, versionPattern, RegexOptions.IgnoreCase))
                    {
                        Skip = $"{skipOnRuntimeVersion} is unsupported";
                        break;
                    }
                }
            }
        }
    }
}
