// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.DotNet.Framework.UpdateDependencies;
using System.CommandLine;

// Command-line tool for updating dependency variables in .NET Framework Docker
// manifest files.
//
// Before running, you may need to build the app and then run:
// pwsh bin/Debug/net*/playwright.ps1 install
//
// Usage: dotnet run -- --help

var manifestFileOption = new Argument<string>("manifest file path")
{
    DefaultValueFactory = _ => "manifest.versions.json",
};

var updateLcusCommand = new Command(
    name: "update-lcus",
    description: "Update all LCU variables in the specified manifest file.")
{
    manifestFileOption
};

var rootCommand = new RootCommand() { updateLcusCommand };

updateLcusCommand.SetAction(
    async parseResult =>
    {
        string manifestFilePath = parseResult.GetValue(manifestFileOption) ??
            throw new ArgumentException("Manifest file path is required.");

        var manifestVersionsContent = await File.ReadAllTextAsync(manifestFilePath);
        var manifestVersionsContext = new ManifestVariableContext(manifestVersionsContent);

        await using var lcuUpdater = new LcuVariableUpdater();
        try
        {
            await manifestVersionsContext.ApplyAsync(lcuUpdater);
            await File.WriteAllTextAsync(manifestFilePath, manifestVersionsContext.Content);
        }
        finally
        {
            await lcuUpdater.DisposeAsync();
        }
    }
);

return rootCommand.Parse(args).Invoke();
