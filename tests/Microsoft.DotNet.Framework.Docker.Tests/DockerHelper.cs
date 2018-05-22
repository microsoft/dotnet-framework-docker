// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Framework.Docker.Tests
{
    public class DockerHelper
    {
        private ITestOutputHelper OutputHelper { get; set; }

        public DockerHelper(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        public void Build(string tag, string dockerfile, string buildContextPath, List<string> buildArgs)
        {
            string buildArgsOption = null;
            if (buildArgs != null)
            {
                foreach (Object arg in buildArgs)
                {
                    buildArgsOption += $" --build-arg {arg}";
                }
            }

            Execute($"build -t {tag} {buildArgsOption} -f {dockerfile} {buildContextPath}");
        }

        public void DeleteImage(string tag)
        {
            if (ImageExists(tag))
            {
                Execute($"image rm -f {tag}");
            }
        }

        private void Execute(string args)
        {
            OutputHelper.WriteLine($"Executing : docker {args}");
            ProcessStartInfo startInfo = new ProcessStartInfo("docker", args);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            Process process = Process.Start(startInfo);
            StringBuilder stdError = new StringBuilder();
            Action<object, DataReceivedEventArgs> errorDataReceived = (sender, e) => stdError.AppendLine(e.Data);
            process.ErrorDataReceived += new DataReceivedEventHandler(errorDataReceived);

            OutputHelper.WriteLine(process.StandardOutput.ReadToEnd());
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                string msg = $"Failed to execute {startInfo.FileName} {startInfo.Arguments}{Environment.NewLine}{stdError}";
                throw new InvalidOperationException(msg);
            }
        }

        public static bool ImageExists(string tag)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("docker", $"image ls -q {tag}");
            startInfo.RedirectStandardOutput = true;
            Process process = Process.Start(startInfo);
            string stdOutput = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();
            return process.ExitCode == 0 && stdOutput != "";
        }

        public void Run(string image, string containerName, string command)
        {
            Execute($"run --rm --name {containerName} {image} {command}");
        }
    }
}
