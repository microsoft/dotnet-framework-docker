// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.DotNet.Framework.UpdateDependencies.NuGet;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Updates the NuGet CLI version variable to the latest recommended nuget.exe.
/// </summary>
internal sealed class NuGetVariableUpdater(NuGetClient nuget) : IVariableUpdater
{
    /// <inheritdoc/>
    public bool ShouldUpdate(string variableKey, IVariableContext variables)
    {
        // The manifest has a single NuGet variable, "nuget|version".
        return variableKey == "nuget|version";
    }

    /// <inheritdoc/>
    public async Task<string> GetNewValueAsync(string variableKey, IVariableContext variables)
    {
        string version = await nuget.GetLatestVersionAsync();
        Console.WriteLine($"NuGet version: {version}");
        return version;
    }
}
