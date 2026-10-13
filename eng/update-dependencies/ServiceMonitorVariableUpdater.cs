// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.DotNet.Framework.UpdateDependencies.ServiceMonitor;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Updates the ServiceMonitor version variable to the latest GitHub release
/// that publishes ServiceMonitor.exe.
/// </summary>
internal sealed class ServiceMonitorVariableUpdater(ServiceMonitorClient serviceMonitor) : IVariableUpdater
{
    /// <inheritdoc/>
    public bool ShouldUpdate(string variableKey, IVariableContext variables)
    {
        // The manifest also has servicemonitor|url, which references this version.
        return variableKey == "servicemonitor|version";
    }

    /// <inheritdoc/>
    public async Task<string> GetNewValueAsync(string variableKey, IVariableContext variables)
    {
        string version = await serviceMonitor.GetLatestVersionAsync();
        Console.WriteLine($"ServiceMonitor version: {version}");
        return version;
    }
}
