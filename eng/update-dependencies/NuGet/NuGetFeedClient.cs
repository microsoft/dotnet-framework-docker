// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;
using NuGet.Versioning;

namespace Microsoft.DotNet.Framework.UpdateDependencies.NuGet;

/// <summary>
/// Client for a NuGet v3 feed. Reads the service index, then the package's
/// version list.
/// </summary>
internal sealed class NuGetFeedClient(HttpClient httpClient)
{
    private const string PackageBaseAddress = "PackageBaseAddress/3.0.0";

    private static readonly JsonSerializerOptions s_jsonOptions = new(JsonSerializerDefaults.Web)
    {
        RespectNullableAnnotations = true,
        RespectRequiredConstructorParameters = true,
    };

    /// <summary>
    /// Gets the highest stable version of a package. Prerelease versions are
    /// ignored. The version list is not assumed to be sorted.
    /// </summary>
    public async Task<string> GetLatestStableVersionAsync(string serviceIndexUrl, string packageId)
    {
        string packageBaseAddress = await GetPackageBaseAddressAsync(serviceIndexUrl);
        string versionsUrl =
            $"{packageBaseAddress.TrimEnd('/')}/{packageId.ToLowerInvariant()}/index.json";

        PackageVersions packageVersions = await GetJsonAsync<PackageVersions>(versionsUrl);

        var stable = new List<NuGetVersion>();
        foreach (string versionText in packageVersions.Versions)
        {
            if (!NuGetVersion.TryParse(versionText, out NuGetVersion? version))
            {
                throw new InvalidOperationException(
                    $"Package '{packageId}' has an unparseable version '{versionText}'.");
            }

            if (!version.IsPrerelease)
            {
                stable.Add(version);
            }
        }

        if (stable.Count == 0)
        {
            throw new InvalidOperationException(
                $"Package '{packageId}' has no stable version at {versionsUrl}.");
        }

        // OriginalVersion is the string the feed listed, not a normalized rewrite.
        NuGetVersion latest = stable.Max()!;
        return latest.OriginalVersion ?? latest.ToNormalizedString();
    }

    private async Task<string> GetPackageBaseAddressAsync(string serviceIndexUrl)
    {
        ServiceIndex index = await GetJsonAsync<ServiceIndex>(serviceIndexUrl);

        string? packageBaseAddress = index.Resources
            .FirstOrDefault(resource => IsPackageBaseAddress(resource.Type))
            ?.Id;

        if (packageBaseAddress is null)
        {
            throw new InvalidOperationException(
                $"NuGet service index {serviceIndexUrl} has no {PackageBaseAddress} resource.");
        }

        return packageBaseAddress;
    }

    private async Task<T> GetJsonAsync<T>(string url)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        using HttpResponseMessage response = await httpClient.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new InvalidOperationException($"NuGet feed returned 404 for {url}.");
        }

        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, s_jsonOptions) ??
            throw new InvalidOperationException($"NuGet feed response from {url} was empty.");
    }

    private static bool IsPackageBaseAddress(JsonElement type)
    {
        return type.ValueKind switch
        {
            JsonValueKind.String => type.GetString() == PackageBaseAddress,
            JsonValueKind.Array => type.EnumerateArray().Any(item => item.GetString() == PackageBaseAddress),
            _ => false,
        };
    }

    private sealed record ServiceIndex(IReadOnlyList<ServiceResource> Resources);

    private sealed record ServiceResource(
        [property: JsonPropertyName("@id")] string Id,
        [property: JsonPropertyName("@type")] JsonElement Type);

    private sealed record PackageVersions(IReadOnlyList<string> Versions);
}
