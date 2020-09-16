// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class EnvironmentVariableInfo
    {
        public bool AllowAnyValue { get; private set; }
        public string ExpectedValue { get; private set; }
        public string Name { get; private set; }

        public EnvironmentVariableInfo(string name, string expectedValue)
        {
            Name = name;
            ExpectedValue = expectedValue;
        }

        public EnvironmentVariableInfo(string name, bool allowAnyValue)
        {
            Name = name;
            AllowAnyValue = allowAnyValue;
        }
    }
}
