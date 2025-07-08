// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.DotNet.Framework.UpdateDependencies;

/// <summary>
/// Provides access to a collection of named variables that can be retrieved
/// and modified. Variables support recursive resolution, allowing one variable
/// to reference another using the $(variableName) syntax.
/// </summary>
internal interface IVariableContext
{
    /// <summary>
    /// Retrieves or updates a variable by name. When retrieving, any embedded
    /// variable references are resolved recursively. When setting, the
    /// underlying content is updated to reflect the change.
    /// </summary>
    /// <param name="key">
    /// The name of the variable to access.
    /// </param>
    /// <returns>
    /// The resolved value of the variable, or an empty string if not found.
    /// </returns>
    string this[string key] { get; set; }

    /// <summary>
    /// Enumerates all available variable names in this context.
    /// </summary>
    IEnumerable<string> AllVariables { get; }
}
