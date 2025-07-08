// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Manages variables stored in a manifest JSON file. Variables can reference
/// other variables using the $(name) syntax. Changes are automatically
/// synchronized back to the original JSON text data via <see cref="Content"/>.
/// </summary>
internal partial class ManifestVariableContext : IVariableContext
{
    private static readonly JsonDocumentOptions s_jsonDocumentOptions = new()
    {
        CommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
    };

    private readonly JsonObject _variables;

    /// <summary>
    /// Initializes a new variable context from manifest JSON content. The JSON
    /// must contain a root object with a "variables" property whose value is
    /// an object mapping variable names to their string values.
    /// </summary>
    /// <param name="manifestJsonContent">
    /// Complete JSON content of the manifest file, which will be parsed and
    /// modified as variables change.
    /// </param>
    public ManifestVariableContext(string manifestJsonContent)
    {
        Content = manifestJsonContent;

        JsonNode manifest = JsonNode.Parse(Content, documentOptions: s_jsonDocumentOptions) ??
            throw new InvalidOperationException($"""
                Failed to serialize variables manifest from content:

                {Content}
                """
            );

        _variables = (JsonObject?)manifest["variables"] ??
            throw new InvalidOperationException($"""
                Manifest JSON does not contain 'variables' section:

                {Content}
                """
            );
    }

    /// <summary>
    /// The current JSON content of the manifest file, automatically updated
    /// whenever variables are modified through the indexer. This content
    /// preserves the original formatting and structure while reflecting any
    /// variable changes.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Enumerates all variable names defined in the manifest's variables
    /// section.
    /// </summary>
    public IEnumerable<string> AllVariables => _variables.Select(kvp => kvp.Key);

    /// <summary>
    /// Retrieves or updates a variable by name. Embedded variable references
    /// using $(variableName) syntax are resolved recursively. When modifying,
    /// <see cref="Content"/> is updated to reflect the changes.
    /// </summary>
    public string this[string key]
    {
        get => GetVariable(key);
        set => SetVariable(key, value);
    }

    /// <summary>
    /// Updates all variables that match <paramref name="variableUpdater"/>'s
    /// rules.
    /// </summary>
    /// <param name="variableUpdater">
    /// The updater that defines which variables to modify and calculates their
    /// new values.
    /// </param>
    public async Task ApplyAsync(IVariableUpdater variableUpdater)
    {
        foreach (string variableName in AllVariables)
        {
            if (variableUpdater.ShouldUpdate(variableName, this))
            {
                string newValue = await variableUpdater.GetNewValueAsync(variableName, this);
                this[variableName] = newValue;
            }
        }
    }

    /// <summary>
    /// Resolves a variable's value, including any nested variable references.
    /// Variable references use the $(variableName) syntax and are resolved
    /// recursively.
    /// </summary>
    /// <param name="key">
    /// The name of the variable to resolve.
    /// </param>
    /// <returns>
    /// The fully resolved value, or an empty string if the variable is not
    /// found.
    /// </returns>
    private string GetVariable(string key)
    {
        string value = _variables[key]?.ToString() ?? "";

        // Look through any variables in this variable's value. If there are
        // any, resolve them recursively.
        var matchedSubVariables = VariableRegex.Matches(value);
        foreach (Match match in matchedSubVariables)
        {
            string subVariableName = match.Groups["name"].Value;
            string subVariableValue = GetVariable(subVariableName);
            value = value.Replace(match.Value, subVariableValue);
        }

        return value;
    }

    private void SetVariable(string key, string value)
    {
        // Update the variable in the json object representation
        _variables[key] = value;

        // Update the variable in the file contents. Use regex to preserve formatting.
        string jsonPropertyPattern = $@"
            ""{Regex.Escape(key)}"" # property name in quotes
            \s*:\s*                 # colon with optional whitespace
            ""[^""]*""              # value in quotes (no inner quotes)
        ";

        var regex = new Regex(jsonPropertyPattern, RegexOptions.IgnorePatternWhitespace);
        Content = regex.Replace(Content, $"\"{key}\": \"{value}\"");
    }

    /// <summary>
    /// Matches variable reference patterns in the format $(variableName) for
    /// recursive resolution. Has one named group, "name" which captures the
    /// variable name inside the parentheses.
    /// </summary>
    [GeneratedRegex(@"\$\((?<name>.*)\)")]
    private static partial Regex VariableRegex { get; }
}
