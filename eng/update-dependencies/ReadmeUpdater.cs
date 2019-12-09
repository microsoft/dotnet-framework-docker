// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.DotNet.VersionTools.Dependencies;

namespace Microsoft.DotNet.Framework.UpdateDependencies
{
    public class ReadmeUpdater : IDependencyUpdater
    {
        public IEnumerable<DependencyUpdateTask> GetUpdateTasks(IEnumerable<IDependencyInfo> dependencyInfos)
        {
            return new DependencyUpdateTask[] {
                new DependencyUpdateTask(
                    () => InvokeGetTagsDocumentationScript(),
                    Enumerable.Empty<IDependencyInfo>(),
                    Enumerable.Empty<string>()
                )
            };
        }

        private void InvokeGetTagsDocumentationScript()
        {
            Trace.TraceInformation($"InvokeGetTagsDocumentationScript");

            // Support both execution within Windows 10, Nano Server and Linux environments.
            string scriptPath = Path.Combine(Program.RepoRoot, "eng", "Get-TagsDocumentation.ps1");
            try
            {
                Process process = Process.Start("pwsh", scriptPath);
                process.WaitForExit();
            }
            catch (Win32Exception)
            {
                Process process = Process.Start("powershell", scriptPath);
                process.WaitForExit();
            }
        }
    }
}
