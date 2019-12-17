// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Newtonsoft.Json;

namespace Microsoft.DotNet.Framework.Models
{
    public class VsInfo
    {
        [JsonProperty(Required = Required.Always)]
        public string VsVersion { get; set; } = String.Empty;

        [JsonProperty(Required = Required.Always)]
        public string TestAgentUrl { get; set; } = String.Empty;

        [JsonProperty(Required = Required.Always)]
        public string BuildToolsUrl { get; set; } = String.Empty;

        [JsonProperty(Required = Required.Always)]
        public string WebTargetsUrl { get; set; } = String.Empty;
    }
}
