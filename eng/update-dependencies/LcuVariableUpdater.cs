// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;
using Microsoft.DotNet.Framework.UpdateDependencies.UpdateCatalog;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Updates Latest Cumulative Update (LCU) download URLs variables by fetching
/// the most recent update information from the Microsoft Update Catalog.
/// </summary>
internal sealed partial class LcuVariableUpdater(UpdateCatalogClient catalog) : IVariableUpdater
{
    /// <summary>
    /// Matches LCU variable names in the format "lcu|{version}|{framework}"
    /// where version contains digits and periods. Examples:
    /// "lcu|ltsc2019|4.8", "lcu|ltsc2022|4.8.1".
    /// </summary>
    [GeneratedRegex(@"lcu\|[^|]+\|\d+(\.\d+)+?")]
    private partial Regex LcuVariablePattern { get; }

    /// <inheritdoc/>
    public bool ShouldUpdate(string variableKey, IVariableContext variables)
    {
        return LcuVariablePattern.IsMatch(variableKey);
    }

    /// <inheritdoc/>
    public async Task<string> GetNewValueAsync(string variableKey, IVariableContext variables)
    {
        // Assuming that variableKey is in a format like "lcu|ltsc2019|4.8", we
        // want to look at the variable "kb|ltsc2019|4.8".
        string[] variableNameParts = variableKey.Split('|');
        variableNameParts[0] = "kb";
        string kbVariableName = string.Join('|', variableNameParts);
        string kbNumber = variables[kbVariableName];

        // By convention, the second/middle part of the variable name contains
        // the Windows version.
        var windowsVersion = variableNameParts[1];

        string kbDownloadUrl = await GetKbDownloadUrlAsync(kbNumber, windowsVersion);
        return kbDownloadUrl;
    }

    /// <summary>
    /// Fetches the download URL for a given KB number from the Microsoft
    /// Update Catalog.
    /// </summary>
    /// <param name="kb">The KB article to get the URL for.</param>
    /// <param name="windowsVersion">
    /// The Windows version to get the download URL for, since KB articles can
    /// have versions for different Windows versions. It should be a string
    /// like "ltsc2016", "ltsc2019", "ltsc2022" etc.
    /// </param>
    /// <returns>KB article download URL</returns>
    private async Task<string> GetKbDownloadUrlAsync(string kb, string windowsVersion)
    {
        // Some windows versions require a more precise regex to match the
        // correct LCU in on the update catalog page. By convention, the
        // Windows version is the second part of the version name.
        Regex titleRegex = windowsVersion switch
        {
            "ltsc2022" => Server2022TitleRegex,
            "ltsc2025" => Server2025TitleRegex,
            _ => WindowsServerTitleRegex
        };

        // A KB has an update for each Windows version and architecture.
        IReadOnlyList<CatalogUpdate> results = await catalog.SearchAsync(kb);
        CatalogUpdate[] matchingUpdates = results
            .Where(result => titleRegex.IsMatch(result.Title))
            .ToArray();

        CatalogUpdate update = matchingUpdates switch
        {
            [var match] => match,

            [] => throw new InvalidOperationException(
                $"No Update Catalog result for {kb} matches {windowsVersion}. "
                    + $"Results: [{string.Join("; ", results.Select(result => result.Title))}]"),

            _ => throw new InvalidOperationException(
                $"Multiple Update Catalog results for {kb} match {windowsVersion}: "
                    + $"[{string.Join("; ", matchingUpdates.Select(match => match.Title))}]"),
        };

        IReadOnlyList<string> downloadUrls = await catalog.GetDownloadUrlsAsync(update);
        string[] msuUrls = downloadUrls
            .Where(url => MsuUrlRegex.IsMatch(url))
            .ToArray();

        string msuUrl = msuUrls switch
        {
            [var url] => url,

            [] => throw new InvalidOperationException(
                $"No MSU download URL for '{update.Title}'. URLs: [{string.Join("; ", downloadUrls)}]"),

            _ => throw new InvalidOperationException(
                $"Multiple MSU download URLs for '{update.Title}': [{string.Join("; ", msuUrls)}]"),
        };

        Console.WriteLine($"{kb} download URL: {msuUrl}");

        return msuUrl;
    }

    // The file name at the end of the URL, e.g. ".../windows10.0-kb5126422-x64-ndp481_1e92b2.msu".
    [GeneratedRegex(@"/windows[^/]*\.msu$")]
    private static partial Regex MsuUrlRegex { get; }

    [GeneratedRegex(@"server.*x64", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex WindowsServerTitleRegex { get; }

    [GeneratedRegex(@"server.*21H2.*x64", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex Server2022TitleRegex { get; }

    [GeneratedRegex(@"server.*24H2.*x64", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex Server2025TitleRegex { get; }
}
