// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.DotNet.VersionTools.Dependencies;

namespace Microsoft.DotNet.Framework.UpdateDependencies
{
    public class CustomFileRegexUpdater : FileRegexUpdater
    {
        private readonly string replacementValue;
        private readonly string buildInfoName;

        public CustomFileRegexUpdater(string replacementValue, string buildInfoName)
        {
            this.replacementValue = replacementValue;
            this.buildInfoName = buildInfoName;
        }

        protected override string TryGetDesiredValue(IEnumerable<IDependencyInfo> dependencyInfos, out IEnumerable<IDependencyInfo> usedDependencyInfos)
        {
            usedDependencyInfos = dependencyInfos.Where(info => info.SimpleName == this.buildInfoName);

            return this.replacementValue;
        }
    }
}
