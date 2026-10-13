// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.RegularExpressions;

namespace Microsoft.DotNet.Framework.UpdateDependencies.UpdateCatalog;

/// <summary>
/// Client for the Microsoft Update Catalog website, which has no API. It
/// requests the same pages a browser does and reads the results from them.
/// </summary>
internal sealed partial class UpdateCatalogClient(HttpClient httpClient)
{
    private const string CatalogUrl = "https://www.catalog.update.microsoft.com";

    // Shown instead of the results table when a search finds nothing.
    private const string NoResultsText = "We did not find any results for";

    // camelCase names, as the download dialog expects.
    private static readonly JsonSerializerOptions s_jsonOptions = new(JsonSerializerDefaults.Web);

    /// <summary>
    /// Searches the catalog, e.g. for a KB number. Returns an empty list if
    /// nothing matches.
    /// </summary>
    public async Task<IReadOnlyList<CatalogUpdate>> SearchAsync(string query)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{CatalogUrl}/Search.aspx?q={Uri.EscapeDataString(query)}");

        string html = await SendAsync(request);

        List<CatalogUpdate> updates = SearchResultPattern.Matches(html)
            .Select(match => new CatalogUpdate(Id: match.Groups["id"].Value, Title: match.Groups["title"].Value))
            .ToList();

        // The catalog reports errors as an ordinary page with a 200 status.
        if (updates.Count == 0 && !html.Contains(NoResultsText))
        {
            throw new InvalidOperationException(
                $"The Update Catalog returned an unexpected page for search '{query}'.");
        }

        return updates;
    }

    /// <summary>
    /// Gets the URLs of the files that can be downloaded for an update.
    /// </summary>
    public async Task<IReadOnlyList<string>> GetDownloadUrlsAsync(CatalogUpdate update)
    {
        // The dialog takes a list of updates to download.
        UpdateToDownload[] updatesToDownload = [new UpdateToDownload(update.Id)];
        string updateIDsJson = JsonSerializer.Serialize(updatesToDownload, s_jsonOptions);

        var form = new Dictionary<string, string> { ["updateIDs"] = updateIDsJson };

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{CatalogUrl}/DownloadDialog.aspx")
        {
            Content = new FormUrlEncodedContent(form),
        };

        string dialogHtml = await SendAsync(request);

        List<string> urls = DownloadUrlPattern.Matches(dialogHtml)
            .Select(match => match.Groups["url"].Value)
            .ToList();

        // An update that exists always has at least one file, so a page
        // without URLs is an error page.
        if (urls.Count == 0)
        {
            throw new InvalidOperationException(
                $"The Update Catalog returned no download URLs for '{update.Title}' ({update.Id}).");
        }

        return urls;
    }

    private async Task<string> SendAsync(HttpRequestMessage request)
    {
        // The titles we match on are in English.
        request.Headers.AcceptLanguage.ParseAdd("en-US");

        using HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    // Matches the link to an update's details in a search result row:
    // <a ... onclick='goToDetails("{update ID}");' ...>{title}</a>
    [GeneratedRegex(@"goToDetails\(""(?<id>[^""]+)""\);'[^>]*>\s*(?<title>[^<]+?)\s*</a>")]
    private static partial Regex SearchResultPattern { get; }

    // Matches a file URL in the script embedded in the download dialog's page:
    // downloadInformation[0].files[0].url = '{url}';
    [GeneratedRegex(@"downloadInformation\[\d+\]\.files\[\d+\]\.url\s*=\s*'(?<url>[^']+)'")]
    private static partial Regex DownloadUrlPattern { get; }

    // An update to download. The dialog expects its ID twice, and a size.
    private sealed record UpdateToDownload(string UpdateID)
    {
        public string Uid => UpdateID;
        public int Size => 0;
    }
}

/// <summary>
/// An update in the Update Catalog.
/// </summary>
internal sealed record CatalogUpdate(string Id, string Title);
