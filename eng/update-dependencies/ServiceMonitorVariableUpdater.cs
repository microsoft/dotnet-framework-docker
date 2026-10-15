// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NuGet.Versioning;
using Octokit;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Updates the ServiceMonitor version variable to the latest GitHub release
/// that publishes ServiceMonitor.exe.
/// </summary>
internal sealed class ServiceMonitorVariableUpdater : IVariableUpdater
{
    private const string Owner = "microsoft";
    private const string Repository = "IIS.ServiceMonitor";

    // The images download this exact file name from the release.
    private const string AssetName = "ServiceMonitor.exe";

    private readonly GitHubClient _github = new(new ProductHeaderValue("dotnet-framework-docker-update-dependencies"));

    /// <inheritdoc/>
    public bool ShouldUpdate(string variableKey, IVariableContext variables)
    {
        // The manifest also has servicemonitor|url, which references this version.
        return variableKey == "servicemonitor|version";
    }

    /// <inheritdoc/>
    public async Task<string> GetNewValueAsync(string variableKey, IVariableContext variables)
    {
        string version = await GetLatestVersionAsync();
        Console.WriteLine($"ServiceMonitor version: {version}");
        return version;
    }

    /// <summary>
    /// Gets the latest stable release version that publishes ServiceMonitor.exe.
    /// The returned version has no leading "v"; the manifest URL adds that.
    /// </summary>
    private async Task<string> GetLatestVersionAsync()
    {
        IReadOnlyList<Release> releases = await _github.Repository.Release.GetAll(Owner, Repository);

        NuGetVersion? latest = null;
        foreach (Release release in releases)
        {
            if (release.Draft || release.Prerelease ||
                !release.Assets.Any(asset => asset.Name == AssetName))
            {
                continue;
            }

            // Tags look like "v2.0.1.10". The manifest stores the part after the "v".
            string versionText = release.TagName.StartsWith('v')
                ? release.TagName[1..]
                : release.TagName;
            if (!NuGetVersion.TryParse(versionText, out NuGetVersion? version) || version.IsPrerelease)
            {
                throw new InvalidOperationException(
                    $"Release {release.TagName} has {AssetName} but its tag is not a stable version.");
            }

            if (latest is null || version > latest)
            {
                latest = version;
            }
        }

        if (latest is null)
        {
            throw new InvalidOperationException(
                $"No IIS.ServiceMonitor release has a {AssetName} asset.");
        }

        return latest.OriginalVersion ?? latest.ToNormalizedString();
    }
}
