// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.DotNet.Framework.UpdateDependencies
{
    public static class Program
    {
        public static string RepoRoot { get; } = Directory.GetCurrentDirectory();

        public static Task Main(string[] args)
        {
            RootCommand command = new RootCommand();
            foreach (Symbol symbol in Options.GetCliSymbols())
            {
                command.Add(symbol);
            };

            command.Handler = CommandHandler.Create<Options>(ExecuteAsync);

            return command.InvokeAsync(args);
        }

        private static async Task ExecuteAsync(Options options)
        {
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

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
