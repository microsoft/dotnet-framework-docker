// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.DotNet.Framework.UpdateDependencies
{
    public static class Program
    {
        public static string RepoRoot { get; } = Directory.GetCurrentDirectory();

        public static async Task Main(string[] args)
        {
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

                Options options = new Options();
                options.Parse(args);

                await new DependencyUpdater(options).ExecuteAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to update dependencies:{Environment.NewLine}{e.ToString()}");
                Environment.Exit(1);
            }

            Environment.Exit(0);
        }
    }
}
