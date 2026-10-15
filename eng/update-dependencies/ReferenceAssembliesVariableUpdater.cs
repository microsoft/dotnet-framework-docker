// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.DotNet.Framework.UpdateDependencies.NuGet;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Updates the reference assemblies version to the latest stable
/// Microsoft.NETFramework.ReferenceAssemblies package. The images install the
/// per-framework packages at this version from the dotnet-public feed, so that
/// feed is the source of the version rather than nuget.org.
/// </summary>
internal sealed class ReferenceAssembliesVariableUpdater(NuGetFeedClient feed) : IVariableUpdater
{
    private const string PackageId = "Microsoft.NETFramework.ReferenceAssemblies";

    /// <inheritdoc/>
    public bool ShouldUpdate(string variableKey, IVariableContext variables)
    {
        return variableKey == "referenceassemblies|version";
    }

    /// <inheritdoc/>
    public async Task<string> GetNewValueAsync(string variableKey, IVariableContext variables)
    {
        string source = variables["dotnet-public|source"];
        if (string.IsNullOrEmpty(source))
        {
            throw new InvalidOperationException("Manifest has no dotnet-public|source variable.");
        }

        string version = await feed.GetLatestStableVersionAsync(source, PackageId);
        Console.WriteLine($"Reference assemblies version: {version}");
        return version;
    }
}
