// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.DotNet.VersionTools;
using Microsoft.DotNet.VersionTools.Automation;
using Microsoft.DotNet.VersionTools.Dependencies;
using Microsoft.DotNet.VersionTools.Dependencies.BuildOutput;

namespace Microsoft.DotNet.Framework.UpdateDependencies
{
    public class DependencyUpdater
    {
        private readonly Options options;
        private static readonly Lazy<IEnumerable<DockerfileInfo>> dockerfiles;

        public const string RuntimeImageVariant = "runtime";
        public const string SdkImageVariant = "sdk";
        public const string AspnetImageVariant = "aspnet";
        public const string WcfImageVariant = "wcf";

        public DependencyUpdater(Options options)
        {
            this.options = options;
        }

        static DependencyUpdater()
        {
            dockerfiles = new Lazy<IEnumerable<DockerfileInfo>>(() =>
                Directory.GetFiles(
                    Path.Combine(Program.RepoRoot, "src"),
                    "Dockerfile",
                    SearchOption.AllDirectories)
                .Select(file => new DockerfileInfo(file))
                .ToArray());
        }

        public async Task ExecuteAsync()
        {
            IEnumerable<IDependencyInfo> dependencyInfos = new IDependencyInfo[]
            {
                CreateBuildInfo(RuntimeImageVariant,
                    this.options.DateStampRuntime?? this.options.DateStampAll ?? String.Empty),
                CreateBuildInfo(SdkImageVariant,
                    this.options.DateStampSdk ?? this.options.DateStampAll ?? String.Empty),
                CreateBuildInfo(AspnetImageVariant,
                    this.options.DateStampAspnet ?? this.options.DateStampAll ?? String.Empty),
                CreateBuildInfo(WcfImageVariant,
                    this.options.DateStampWcf ?? this.options.DateStampAll ?? String.Empty),
            };

            DependencyUpdateResults updateResults = UpdateFiles(dependencyInfos);
            if (updateResults.ChangesDetected())
            {
                if (this.options.UpdateOnly)
                {
                    Trace.TraceInformation($"Changes made but no GitHub credentials specified, skipping PR creation");
                }
                else
                {
                    await CreatePullRequestAsync(dependencyInfos);
                }
            }
        }

        private async Task CreatePullRequestAsync(IEnumerable<IDependencyInfo> buildInfos)
        {
            GitHubAuth gitHubAuth = new GitHubAuth(this.options.GitHubPassword, this.options.GitHubUser, this.options.GitHubEmail);
            PullRequestCreator prCreator = new PullRequestCreator(gitHubAuth, this.options.GitHubUser);
            PullRequestOptions prOptions = new PullRequestOptions()
            {
                BranchNamingStrategy = new SingleBranchNamingStrategy($"UpdateDependencies-{this.options.GitHubUpstreamBranch}")
            };

            string commitMessage = $"[{this.options.GitHubUpstreamBranch}] Update image dependencies";

            await prCreator.CreateOrUpdateAsync(
                commitMessage,
                commitMessage,
                string.Empty,
                new GitHubBranch(this.options.GitHubUpstreamBranch, new GitHubProject(this.options.GitHubProject, this.options.GitHubUpstreamOwner)),
                new GitHubProject(this.options.GitHubProject, gitHubAuth.User),
                prOptions);
        }

        private static BuildDependencyInfo CreateBuildInfo(string name, string version)
        {
            return new BuildDependencyInfo(
                new BuildInfo
                {
                    Name = name,
                    LatestPackages = new Dictionary<string, string> { },
                    LatestReleaseVersion = version
                },
                false,
                Enumerable.Empty<string>());
        }

        private DependencyUpdateResults UpdateFiles(IEnumerable<IDependencyInfo> buildInfos)
        {
            List<IDependencyUpdater> updaters = new List<IDependencyUpdater>();

            updaters.AddRange(CreateManifestUpdaters());
            updaters.Add(ScriptRunnerUpdater.GetDockerfileUpdater(Program.RepoRoot));
            updaters.Add(ScriptRunnerUpdater.GetReadMeUpdater(Program.RepoRoot));

            return DependencyUpdateUtils.Update(updaters, buildInfos);
        }

        private IEnumerable<IDependencyUpdater> CreateManifestUpdaters()
        {
            const string RuntimePrefix = "Runtime";
            const string SdkPrefix = "Sdk";
            const string AspnetPrefix = "Aspnet";
            const string WcfPrefix = "Wcf";

            if (this.options.DateStampAll != null)
            {
                yield return CreateManifestUpdater(RuntimePrefix, RuntimeImageVariant);
                yield return CreateManifestUpdater(SdkPrefix, SdkImageVariant);
                yield return CreateManifestUpdater(AspnetPrefix, AspnetImageVariant);
                yield return CreateManifestUpdater(WcfPrefix, WcfImageVariant);
            }
            else
            {
                if (this.options.DateStampRuntime != null)
                {
                    yield return CreateManifestUpdater(RuntimePrefix, RuntimeImageVariant);
                }

                if (this.options.DateStampSdk != null)
                {
                    yield return CreateManifestUpdater(SdkPrefix, SdkImageVariant);
                }

                if (this.options.DateStampAspnet != null)
                {
                    yield return CreateManifestUpdater(AspnetPrefix, AspnetImageVariant);
                }

                if (this.options.DateStampWcf != null)
                {
                    yield return CreateManifestUpdater(WcfPrefix, WcfImageVariant);
                }
            }
        }

        private static IDependencyUpdater CreateManifestUpdater(string dateStampVariablePrefix, string buildInfoName)
        {
            const string TagDateStampGroupName = "tagDateStampValue";

            return new FileRegexReleaseUpdater
            {
                Path = Path.Combine(Program.RepoRoot, "manifest.json"),
                BuildInfoName = buildInfoName,
                Regex = new Regex($"\"{dateStampVariablePrefix}ReleaseDateStamp\": \"(?<{TagDateStampGroupName}>\\d{{8}})\""),
                VersionGroupName = TagDateStampGroupName
            };
        }

        private class DockerfileInfo
        {
            public DockerfileInfo(string path)
            {
                this.Path = path;

                string[] pathParts = path.Substring(Program.RepoRoot.Length + 1)
                    .Replace(@"\", "/")
                    .Split("/");

                this.FrameworkVersion = pathParts[2];
                this.ImageVariant = pathParts[1];
                this.OsVersion = pathParts[3];
            }

            public string Path { get; }
            public string FrameworkVersion { get; }
            public string OsVersion { get; }
            public string ImageVariant { get; }
        }
    }
}
