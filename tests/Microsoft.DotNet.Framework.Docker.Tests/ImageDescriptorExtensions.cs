// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.DotNet.Framework.Docker.Tests;

internal static class ImageDescriptorExtensions
{
    /// <summary>
    /// The path to the Dockerfile that was used to build the image.
    /// </summary>
    /// <param name="imageType">"sdk", "runtime", "aspnet", or "wcf".</param>
    public static string GetDockerfilePath(this ImageDescriptor imageDescriptor, string imageType) =>
        $"src/{imageType}/{imageDescriptor.Version}/{imageDescriptor.OsVariant}";

    /// <summary>
    /// The expected Visual Studio installation path inside the image.
    /// </summary>
    public static string GetExpectedVsInstallationPath(this ImageDescriptor imageDescriptor) =>
        imageDescriptor.OsVariant switch
        {
            // Visual Studio 2026/dev18 does not support Windows Server 2016.
            // See https://learn.microsoft.com/visualstudio/releases/2026/compatibility
            OsVersion.WSC_LTSC2016 => @"C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools",
            _ =>                      @"C:\Program Files (x86)\Microsoft Visual Studio\18\BuildTools",
        };
}
