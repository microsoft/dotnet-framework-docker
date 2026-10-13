// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Updates the NuGet CLI version variable to the latest recommended nuget.exe.
/// </summary>
internal sealed class NuGetVariableUpdater(HttpClient httpClient) : IVariableUpdater
{
    private const string ToolsUrl = "https://dist.nuget.org/tools.json";

    // Recommended for consumption. See https://learn.microsoft.com/nuget/api/tools-json
    private const string ReleasedAndBlessed = "ReleasedAndBlessed";

    private static readonly JsonSerializerOptions s_jsonOptions = new(JsonSerializerDefaults.Web)
    {
        RespectNullableAnnotations = true,
        RespectRequiredConstructorParameters = true,
    };

    /// <inheritdoc/>
    public bool ShouldUpdate(string variableKey, IVariableContext variables)
    {
        // The manifest has a single NuGet variable, "nuget|version".
        return variableKey == "nuget|version";
    }

    /// <inheritdoc/>
    public async Task<string> GetNewValueAsync(string variableKey, IVariableContext variables)
    {
        string version = await GetLatestVersionAsync();
        Console.WriteLine($"NuGet version: {version}");
        return version;
    }

    /// <summary>
    /// Gets the latest recommended nuget.exe version from tools.json.
    /// </summary>
    /// <remarks>
    /// tools.json is sorted by SemVer descending, so the latest recommended
    /// version is the first entry with stage ReleasedAndBlessed.
    /// </remarks>
    private async Task<string> GetLatestVersionAsync()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, ToolsUrl);
        using HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        NuGetTools tools = JsonSerializer.Deserialize<NuGetTools>(json, s_jsonOptions) ??
            throw new InvalidOperationException($"tools.json response from {ToolsUrl} was empty.");

        NuGetTool? latest = tools.NuGetExe.FirstOrDefault(tool => tool.Stage == ReleasedAndBlessed);
        if (latest is null)
        {
            throw new InvalidOperationException(
                $"tools.json from {ToolsUrl} has no ReleasedAndBlessed nuget.exe version.");
        }

        return latest.Version;
    }

    private sealed record NuGetTools(
        [property: JsonPropertyName("nuget.exe")] IReadOnlyList<NuGetTool> NuGetExe);

    private sealed record NuGetTool(string Version, string Stage);
}
