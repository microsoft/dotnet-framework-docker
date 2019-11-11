// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Newtonsoft.Json;

namespace Microsoft.DotNet.Framework.UpdateDependencies.Models
{
    public class LcuConfig
    {
        [JsonProperty(Required = Required.Always)]
        public string OsVersion { get; set; } = String.Empty;

        [JsonProperty(Required = Required.Always)]
        public string[] RuntimeVersions { get; set; } = Array.Empty<string>();

        [JsonProperty(Required = Required.Always)]
        public string DownloadUrl { get; set; } = String.Empty;
    }
}
