// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Updates Latest Cumulative Update (LCU) download URLs variables by fetching
/// the most recent update information from the Microsoft Update Catalog.
/// </summary>
internal sealed partial class LcuVariableUpdater : IVariableUpdater
{
    /// <summary>
    /// Matches LCU variable names in the format "lcu|{version}|{framework}"
    /// where version contains digits and periods. Examples:
    /// "lcu|ltsc2019|4.8", "lcu|ltsc2022|3.5".
    /// </summary>
    [GeneratedRegex(@"lcu\|.*\|\d+(\.\d+)+?")]
    private partial Regex LcuVariablePattern { get; }

    /// <inheritdoc/>
    public bool ShouldUpdate(string variableKey, IVariableContext variables)
    {
        return LcuVariablePattern.IsMatch(variableKey);
    }

    /// <inheritdoc/>
    public async Task<string> GetNewValueAsync(string variableKey, IVariableContext variables)
    {
        throw new NotImplementedException();
    }
}
