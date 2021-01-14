// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.CommandLine;

namespace Microsoft.DotNet.Framework.UpdateDependencies
{
    public class Options
    {
        public string? DateStampAll { get; private set; }

        public string? DateStampRuntime { get; private set; }

        public string? DateStampSdk { get; private set; }

        public string? DateStampAspnet { get; private set; }

        public string? DateStampWcf { get; private set; }

        public string? GitHubEmail { get; private set; }

        public string? GitHubPassword { get; private set; }

        public string GitHubProject => "dotnet-framework-docker";

        public string GitHubUpstreamBranch => "master";

        public string GitHubUpstreamOwner => "Microsoft";

        public string? GitHubUser { get; private set; }

        public bool UpdateOnly => GitHubEmail == null || GitHubPassword == null || GitHubUser == null;

        public void Parse(string[] args)
        {
            ArgumentSyntax argSyntax = ArgumentSyntax.Parse(args, syntax =>
            {
                const string dateStampAllName = "datestamp-all";
                string? dateStampAll = null;
                syntax.DefineOption(
                    "datestamp-all",
                    ref dateStampAll,
                    "Tag date stamp to assign to all image types");
                DateStampAll = dateStampAll;

                string? dateStampRuntime = null;
                syntax.DefineOption(
                    "datestamp-runtime",
                    ref dateStampRuntime,
                    $"Tag date stamp to assign to runtime image types (overrides {dateStampAllName})");
                DateStampRuntime = dateStampRuntime;

                string? dateStampSdk = null;
                syntax.DefineOption(
                    "datestamp-sdk",
                    ref dateStampSdk,
                    $"Tag date stamp to assign to SDK image types (overrides {dateStampAllName})");
                DateStampSdk = dateStampSdk;

                string? dateStampAspnet = null;
                syntax.DefineOption(
                    "datestamp-aspnet",
                    ref dateStampAspnet,
                    $"Tag date stamp to assign to ASP.NET image types (overrides {dateStampAllName})");
                DateStampAspnet = dateStampAspnet;

                string? dateStampWcf = null;
                syntax.DefineOption(
                    "datestamp-wcf",
                    ref dateStampWcf,
                    $"Tag date stamp to assign to WCF image types (overrides {dateStampAllName})");
                DateStampWcf = dateStampWcf;

                string? gitHubEmail = null;
                syntax.DefineOption(
                    "email",
                    ref gitHubEmail,
                    "GitHub email used to make PR (if not specified, a PR will not be created)");
                GitHubEmail = gitHubEmail;

                string? gitHubPassword = null;
                syntax.DefineOption(
                    "password",
                    ref gitHubPassword,
                    "GitHub password used to make PR (if not specified, a PR will not be created)");
                GitHubPassword = gitHubPassword;

                string? gitHubUser = null;
                syntax.DefineOption(
                    "user",
                    ref gitHubUser,
                    "GitHub user used to make PR (if not specified, a PR will not be created)");
                GitHubUser = gitHubUser;
            });
        }
    }
}
