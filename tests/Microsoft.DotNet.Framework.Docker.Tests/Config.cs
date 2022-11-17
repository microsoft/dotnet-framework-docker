﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public static class Config
    {
        public static bool PullImages { get; } = Environment.GetEnvironmentVariable("PULL_IMAGES") != null;
        public static string OS => Environment.GetEnvironmentVariable("IMAGE_OS");
        public static string RepoPrefix { get; } = Environment.GetEnvironmentVariable("REPO_PREFIX") ?? string.Empty;
        public static string Registry { get; } = Environment.GetEnvironmentVariable("REGISTRY") ?? GetManifestRegistry();
        public static string SourceRepoRoot { get; } = Environment.GetEnvironmentVariable("SOURCE_REPO_ROOT") ?? string.Empty;
        public static string[] Paths { get; } =
            Environment.GetEnvironmentVariable("DOCKERFILE_PATHS")?
                .Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

        public static string GetManifestRegistry()
        {
            string manifestJson = File.ReadAllText("manifest.json");
            JObject manifest = JObject.Parse(manifestJson);
            return (string)manifest["registry"];
        }

        public static Version GetManifestVsVersion()
        {
            string manifestJson = File.ReadAllText("manifest.versions.json");
            JObject manifest = JObject.Parse(manifestJson);
            return System.Version.Parse((string)(manifest["variables"]["vs|version"]));
        }

        public static string GetFilterRegexPattern(string filter)
        {
            return $"^{Regex.Escape(filter).Replace(@"\*", ".*").Replace(@"\?", ".")}$";
        }
    }
}
