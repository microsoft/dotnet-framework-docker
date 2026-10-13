// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.DotNet.Framework.UpdateDependencies;
using Microsoft.DotNet.Framework.UpdateDependencies.NuGet;
using Microsoft.DotNet.Framework.UpdateDependencies.ServiceMonitor;
using Microsoft.DotNet.Framework.UpdateDependencies.UpdateCatalog;
using System.CommandLine;

// Command-line tool for updating dependency variables in .NET Framework Docker
// manifest files.
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

var updateNuGetCommand = new Command(
    name: "update-nuget",
    description: "Update the NuGet version variable in the specified manifest file.")
{
    manifestFileOption
};

var updateReferenceAssembliesCommand = new Command(
    name: "update-referenceassemblies",
    description: "Update the reference assemblies version variable in the specified manifest file.")
{
    manifestFileOption
};

var updateServiceMonitorCommand = new Command(
    name: "update-servicemonitor",
    description: "Update the ServiceMonitor version variable in the specified manifest file.")
{
    manifestFileOption
};

var rootCommand = new RootCommand()
{
    updateLcusCommand,
    updateNuGetCommand,
    updateReferenceAssembliesCommand,
    updateServiceMonitorCommand
};

updateLcusCommand.SetAction(
    async parseResult =>
    {
        string manifestFilePath = parseResult.GetValue(manifestFileOption) ??
            throw new ArgumentException("Manifest file path is required.");

        var manifestVersionsContent = await File.ReadAllTextAsync(manifestFilePath);
        var manifestVersionsContext = new ManifestVariableContext(manifestVersionsContent);

        using var httpClient = new HttpClient();
        var catalog = new UpdateCatalogClient(httpClient);
        var lcuUpdater = new LcuVariableUpdater(catalog);
        await manifestVersionsContext.ApplyAsync(lcuUpdater);

        await File.WriteAllTextAsync(manifestFilePath, manifestVersionsContext.Content);
    }
);

updateNuGetCommand.SetAction(
    async parseResult =>
    {
        string manifestFilePath = parseResult.GetRequiredValue(manifestFileOption);

        var manifestVersionsContent = await File.ReadAllTextAsync(manifestFilePath);
        var manifestVersionsContext = new ManifestVariableContext(manifestVersionsContent);

        using var httpClient = new HttpClient();
        var nuget = new NuGetClient(httpClient);
        var nugetUpdater = new NuGetVariableUpdater(nuget);
        await manifestVersionsContext.ApplyAsync(nugetUpdater);

        await File.WriteAllTextAsync(manifestFilePath, manifestVersionsContext.Content);
    }
);

updateReferenceAssembliesCommand.SetAction(
    async parseResult =>
    {
        string manifestFilePath = parseResult.GetRequiredValue(manifestFileOption);

        var manifestVersionsContent = await File.ReadAllTextAsync(manifestFilePath);
        var manifestVersionsContext = new ManifestVariableContext(manifestVersionsContent);

        using var httpClient = new HttpClient();
        var feed = new NuGetFeedClient(httpClient);
        var updater = new ReferenceAssembliesVariableUpdater(feed);
        await manifestVersionsContext.ApplyAsync(updater);

        await File.WriteAllTextAsync(manifestFilePath, manifestVersionsContext.Content);
    }
);

updateServiceMonitorCommand.SetAction(
    async parseResult =>
    {
        string manifestFilePath = parseResult.GetRequiredValue(manifestFileOption);

        var manifestVersionsContent = await File.ReadAllTextAsync(manifestFilePath);
        var manifestVersionsContext = new ManifestVariableContext(manifestVersionsContent);

        var serviceMonitor = new ServiceMonitorClient();
        var updater = new ServiceMonitorVariableUpdater(serviceMonitor);
        await manifestVersionsContext.ApplyAsync(updater);

        await File.WriteAllTextAsync(manifestFilePath, manifestVersionsContext.Content);
    }
);

return rootCommand.Parse(args).Invoke();
