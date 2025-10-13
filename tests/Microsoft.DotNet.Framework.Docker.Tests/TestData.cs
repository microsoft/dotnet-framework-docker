// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace Microsoft.DotNet.Framework.Docker.Tests;

internal static class TestData
{
    public static IEnumerable<ImageDescriptor> ImageData =>
    [
        new() { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
        new() { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
        new() { Version = "4.8.1", SdkVersion = "4.8.1", OsVariant = OsVersion.WSC_LTSC2022 },
        new() { Version = "4.8.1", SdkVersion = "4.8.1", OsVariant = OsVersion.WSC_LTSC2025 },
    ];
}
