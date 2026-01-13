// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Updates Latest Cumulative Update (LCU) download URLs variables by fetching
/// the most recent update information from the Microsoft Update Catalog.
/// </summary>
internal sealed partial class LcuVariableUpdater : IVariableUpdater, IAsyncDisposable
{
    private static readonly BrowserNewContextOptions s_newBrowserOptions = new() { Locale = "en-US" };

    private readonly Lazy<Task<IPlaywright>> _playwright;
    private readonly Lazy<Task<IBrowser>> _browser;

    public LcuVariableUpdater()
    {
        _playwright = new Lazy<Task<IPlaywright>>(Playwright.Playwright.CreateAsync);
        _browser = new(
            async () =>
            {
                var playwright = await _playwright.Value;
                return await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions { Headless = true }
                );
            }
        );
    }

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
        var browser = await _browser.Value;
        var context = await browser.NewContextAsync(s_newBrowserOptions);
        var page = await context.NewPageAsync();

        await page.GotoAsync($"https://catalog.update.microsoft.com/Search.aspx?q={kb}");

        // Some windows versions require a more precise regex to match the
        // correct LCU in on the update catalog page. By convention, the
        // Windows version is the second part of the version name.
        var tableRowRegex = windowsVersion switch
        {
            "ltsc2022" => Server2022TableRowRegex,
            _ => WindowsServerTableRowRegex
        };

        var downloadPopUpPage = await page.RunAndWaitForPopupAsync(
            async () =>
            {
                await page
                    .GetByRole(AriaRole.Row, new PageGetByRoleOptions() { NameRegex = tableRowRegex, Exact = true })
                    .GetByRole(AriaRole.Button)
                    .ClickAsync();
            }
        );

        var url = await downloadPopUpPage
            .GetByRole(AriaRole.Link, s_getDownloadLinkOptions)
            .GetAttributeAsync("href");

        await context.CloseAsync();

        Console.WriteLine($"{kb} download URL: {url}");

        return url ?? "";
    }

    public async ValueTask DisposeAsync()
    {
        if (_browser.IsValueCreated)
        {
            var browser = await _browser.Value;
            await browser.DisposeAsync();
        }

        if (_playwright.IsValueCreated)
        {
            var playwright = await _playwright.Value;
            playwright.Dispose();
        }
    }

    private static readonly PageGetByRoleOptions s_getDownloadLinkOptions = new()
    {
        NameRegex = DownloadLinkRegex,
    };

    [GeneratedRegex(@"^windows.*\.msu$")]
    private static partial Regex DownloadLinkRegex { get; }

    [GeneratedRegex(@"server.*x64", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex WindowsServerTableRowRegex { get; }

    [GeneratedRegex(@"server.*21H2.*x64", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex Server2022TableRowRegex { get; }
}
