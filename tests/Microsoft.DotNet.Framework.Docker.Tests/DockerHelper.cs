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
        public ITestOutputHelper OutputHelper { get; set; }

        public DockerHelper(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        public void Build(string tag, string dockerfile, string buildContextPath, IEnumerable<string> buildArgs)
        {
            string buildArgsOption = null;
            if (buildArgs != null)
            {
                foreach (string arg in buildArgs)
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

        private string Execute(string args)
        {
            OutputHelper.WriteLine($"Executing : docker {args}");
            ProcessStartInfo startInfo = new ProcessStartInfo("docker", args);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            Process process = Process.Start(startInfo);
            StringBuilder stdError = new StringBuilder();
            Action<object, DataReceivedEventArgs> errorDataReceived = (sender, e) => stdError.AppendLine(e.Data);
            process.ErrorDataReceived += new DataReceivedEventHandler(errorDataReceived);

            string result = process.StandardOutput.ReadToEnd();
            OutputHelper.WriteLine(result);
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                string msg = $"Failed to execute {startInfo.FileName} {startInfo.Arguments}{Environment.NewLine}{stdError}";
                throw new InvalidOperationException(msg);
            }

            return result;
        }

        public static bool ImageExists(string tag)
        {
            return VerifyImageStatus(tag, "image ls -q");
        }

        public void Run(string image, string containerName, string command, bool detach = false)
        {
            string options = detach ? "run --rm -d --name" : "run --rm --name";
            Execute($"{options} {containerName} {image} {command}");
        }

        public void Pull(string image)
        {
            Execute($"pull {image}");
        }
        public void Stop(string containerName)
        {
            Execute($"stop {containerName}");
        }

        public string GetContainerAddress(string container)
        {
            string address = Execute("inspect -f \"{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}\" " + container);
            //remove the last character of the address
            return address.Remove(address.Length - 1);
        }
        public static bool ImageRunning(string tag)
        {
            return VerifyImageStatus(tag, "inspect -f '{{.State.Running}}'");
        }

        private static bool VerifyImageStatus(string tag, string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("docker", $"{command} {tag}");
            startInfo.RedirectStandardOutput = true;
            Process process = Process.Start(startInfo);
            string stdOutput = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();
            return process.ExitCode == 0 && stdOutput != "";

        }
    }
}
