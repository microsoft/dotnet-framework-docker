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
using Microsoft.DotNet.Framework.UpdateDependencies.Models;
using Microsoft.DotNet.VersionTools;
using Microsoft.DotNet.VersionTools.Automation;
using Microsoft.DotNet.VersionTools.Dependencies;
using Microsoft.DotNet.VersionTools.Dependencies.BuildOutput;
using Newtonsoft.Json;

namespace Microsoft.DotNet.Framework.UpdateDependencies
{
    public class DependencyUpdater
    {
        private readonly Options options;

        public const string RuntimeBuildInfoName = "runtime";
        public const string SdkBuildInfoName = "sdk";
        public const string AspnetBuildInfoName = "aspnet";
        public const string WcfBuildInfoName = "wcf";

        public DependencyUpdater(Options options)
        {
            this.options = options;
        }

        public async Task ExecuteAsync()
        {
            IEnumerable<IDependencyInfo> dependencyInfos = new IDependencyInfo[]
            {
                CreateBuildInfo(RuntimeBuildInfoName,
                    this.options.DateStampRuntime?? this.options.DateStampAll ?? String.Empty),
                CreateBuildInfo(SdkBuildInfoName,
                    this.options.DateStampSdk ?? this.options.DateStampAll ?? String.Empty),
                CreateBuildInfo(AspnetBuildInfoName,
                    this.options.DateStampAspnet ?? this.options.DateStampAll ?? String.Empty),
                CreateBuildInfo(WcfBuildInfoName,
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

            string commitMessage = $"[{this.options.GitHubUpstreamBranch}] Update dependencies from dotnet/core-sdk";

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
            updaters.AddRange(CreateLcuUpdaters(buildInfos));
            updaters.AddRange(CreateVsUpdaters());
            updaters.AddRange(CreateNuGetClientVersionUpdaters());
            updaters.Add(new ReadmeUpdater());

            return DependencyUpdateUtils.Update(updaters, buildInfos);
        }

        private IEnumerable<IDependencyUpdater> CreateLcuUpdaters(IEnumerable<IDependencyInfo> buildInfos)
        {
            LcuConfig[] lcuConfigs = JsonConvert.DeserializeObject<LcuConfig[]>(
                File.ReadAllText(this.options.LcuConfigPath));

            const string UrlVersionGroupName = "Url";
            const string CabFileVersionGroupName = "CabFile";

            return GetDockerfiles()
                .Select(file => new
                {
                    Dockerfile = file,
                    LcuConfigInfo = GetLcuConfigInfo(file, lcuConfigs)
                })
                .Where(val => val.LcuConfigInfo.HasValue)
                .SelectMany(val =>
                    new IDependencyUpdater[]
                    {
                        new CustomFileRegexUpdater(val.LcuConfigInfo!.Value.Config.DownloadUrl, val.LcuConfigInfo!.Value.BuildInfoName)
                        {
                            Path = val.Dockerfile.Path,
                            VersionGroupName = UrlVersionGroupName,
                            Regex = new Regex(@$"# Apply latest patch(.|\n)+(?<{UrlVersionGroupName}>http:\/\/[^\s""]+)")
                        },
                        new CustomFileRegexUpdater(ParseCabFileName(val.LcuConfigInfo!.Value.Config.DownloadUrl), val.LcuConfigInfo!.Value.BuildInfoName)
                        {
                            Path = val.Dockerfile.Path,
                            VersionGroupName = CabFileVersionGroupName,
                            Regex = new Regex(@$"# Apply latest patch(.|\n)+dism.+C:\\patch\\(?<{CabFileVersionGroupName}>\S+)", RegexOptions.IgnoreCase)
                        }
                    }
                );
        }

        private IEnumerable<IDependencyUpdater> CreateVsUpdaters()
        {
            VsConfig vsConfig = JsonConvert.DeserializeObject<VsConfig>(
                File.ReadAllText(this.options.VsConfigPath));

            const string TestAgentGroupName = "TestAgent";
            const string BuildToolsGroupName = "BuildTools";
            const string WebTargetsGroupName = "WebTargets";

            return GetDockerfiles()
                .SelectMany(dockerfile =>
                    new IDependencyUpdater[]
                    {
                        new CustomFileRegexUpdater(vsConfig.TestAgentUrl, dockerfile.BuildInfoName)
                        {
                            VersionGroupName = TestAgentGroupName,
                            Path = dockerfile.Path,
                            Regex = new Regex(@$"(?<{TestAgentGroupName}>https:\/\/\S+vs_TestAgent\.exe)"),
                        },
                        new CustomFileRegexUpdater(vsConfig.BuildToolsUrl, dockerfile.BuildInfoName)
                        {
                            VersionGroupName = BuildToolsGroupName,
                            Path = dockerfile.Path,
                            Regex = new Regex(@$"(?<{BuildToolsGroupName}>https:\/\/\S+vs_BuildTools\.exe)"),
                        },
                        new CustomFileRegexUpdater(vsConfig.WebTargetsUrl, dockerfile.BuildInfoName)
                        {
                            VersionGroupName = WebTargetsGroupName,
                            Path = dockerfile.Path,
                            Regex = new Regex(@$"# Install web targets(.|\n)+?(?<{WebTargetsGroupName}>https:\/\/\S+)"),
                        }
                    });
        }

        private static string ParseCabFileName(string lcuDownloadUrl)
        {
            string msuFilename = lcuDownloadUrl.Substring(lcuDownloadUrl.LastIndexOf("/") + 1);
            return msuFilename.Substring(0, msuFilename.IndexOf("_")) + ".cab";
        }

        private static (LcuConfig Config, string BuildInfoName)? GetLcuConfigInfo(DockerfileInfo dockerfile, LcuConfig[] lcuConfigs)
        {
            if (dockerfile.BuildInfoName != RuntimeBuildInfoName)
            {
                return null;
            }

            LcuConfig? lcuConfig = lcuConfigs
                .FirstOrDefault(config => config.OsVersion == dockerfile.OsVersion &&
                    config.RuntimeVersions.Any(runtime => runtime == dockerfile.FrameworkVersion));

            if (lcuConfig != null)
            {
                return (lcuConfig, dockerfile.BuildInfoName);
            }

            return null;
        }

        private IEnumerable<IDependencyUpdater> CreateNuGetClientVersionUpdaters()
        {
            NuGetVersion[] nuGetVersions = JsonConvert.DeserializeObject<NuGetVersion[]>(
                File.ReadAllText(this.options.NuGetClientVersionsPath));
            
            const string NuGetVersionGroupName = "version";

            return GetDockerfiles()
                .SelectMany(file => nuGetVersions.Select(nuGetVersion => 
                    new CustomFileRegexUpdater(nuGetVersion.Version, file.BuildInfoName)
                    {
                        Path = file.Path,
                        VersionGroupName = NuGetVersionGroupName,
                        Regex = new Regex($"ENV NUGET_VERSION (?<{NuGetVersionGroupName}>{nuGetVersion.Filter.Replace(".", @"\.").Replace("*", $@"\d*")})")
                    }
                ));
        }

        private static IEnumerable<DockerfileInfo> GetDockerfiles()
        {
            IEnumerable<DirectoryInfo> frameworkVersionDirs =
                new DirectoryInfo(Program.RepoRoot).GetDirectories("4.*")
                .Concat(new DirectoryInfo[] { new DirectoryInfo(Path.Combine(Program.RepoRoot, "3.5")) });

            return frameworkVersionDirs
                .SelectMany(dir => dir.GetFiles("Dockerfile", SearchOption.AllDirectories))
                .Select(file => new DockerfileInfo(file.FullName));
        }

        private IEnumerable<IDependencyUpdater> CreateManifestUpdaters()
        {
            const string RuntimePrefix = "Runtime";
            const string SdkPrefix = "Sdk";
            const string AspnetPrefix = "Aspnet";
            const string WcfPrefix = "Wcf";

            if (this.options.DateStampAll != null)
            {
                yield return CreateManifestUpdater(RuntimePrefix, RuntimeBuildInfoName);
                yield return CreateManifestUpdater(SdkPrefix, SdkBuildInfoName);
                yield return CreateManifestUpdater(AspnetPrefix, AspnetBuildInfoName);
                yield return CreateManifestUpdater(WcfPrefix, WcfBuildInfoName);
            }
            else
            {
                if (this.options.DateStampRuntime != null)
                {
                    yield return CreateManifestUpdater(RuntimePrefix, RuntimeBuildInfoName);
                }

                if (this.options.DateStampSdk != null)
                {
                    yield return CreateManifestUpdater(SdkPrefix, SdkBuildInfoName);
                }

                if (this.options.DateStampAspnet != null)
                {
                    yield return CreateManifestUpdater(AspnetPrefix, AspnetBuildInfoName);
                }

                if (this.options.DateStampWcf != null)
                {
                    yield return CreateManifestUpdater(WcfPrefix, WcfBuildInfoName);
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

                this.FrameworkVersion = pathParts[0];
                this.BuildInfoName = pathParts[1];
                this.OsVersion = pathParts[2];
            }

            public string Path { get; }
            public string FrameworkVersion { get; }
            public string OsVersion { get; }
            public string BuildInfoName { get; }
        }
    }
}
