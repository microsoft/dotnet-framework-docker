// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace Microsoft.DotNet.Framework.Docker.Tests;

internal static class TestData
{
    private static readonly ImageDescriptor[] s_imageData = new ImageDescriptor[]
        {
            new ImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "3.5", SdkVersion = "3.5", OsVariant = OsVersion.WSC_LTSC2022 },
            new ImageDescriptor { Version = "4.6.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.1", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.7.2", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2016 },
            new ImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2019 },
            new ImageDescriptor { Version = "4.8", SdkVersion = "4.8", OsVariant = OsVersion.WSC_LTSC2022 },
            new ImageDescriptor { Version = "4.8.1", SdkVersion = "4.8.1", OsVariant = OsVersion.WSC_LTSC2022 },
            new ImageDescriptor { Version = "4.8.1", SdkVersion = "4.8.1", OsVariant = OsVersion.WSC_LTSC2025 },
        };

    public static IEnumerable<ImageDescriptor> GetImageData() => s_imageData;
}
