// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class ImageDescriptor
    {
        public string Version { get; set; }
        public string OsVariant { get; set; }
        public string SdkVersion { get; set; }
    }
}
