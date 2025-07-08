// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.DotNet.Framework.UpdateDependencies;

internal interface IVariableUpdater
{
    /// <summary>
    /// Determines whether a variable should be updated based on its name.
    /// </summary>
    /// <param name="variableKey">
    /// The name of the variable being checked for update eligibility.
    /// </param>
    /// <param name="variables">
    /// All variables in the current context, in case the updater needs to
    /// reference other variables when determining if an update is needed.
    /// </param>
    /// <returns>
    /// True if the variable should be updated.
    /// </returns>
    bool ShouldUpdate(string variableKey, IVariableContext variables);

    /// <summary>
    /// Computes a new value for a variable based on its current name and the
    /// surrounding context. This method is only called for variables whose
    /// names match the <see cref="KeyPattern"/>.
    /// </summary>
    /// <param name="variableKey">
    /// The name of the variable being updated.
    /// </param>
    /// <param name="variables">
    /// All variables in the current context. Used to reference other variables
    /// when generating a new value.
    /// </param>
    /// <returns>
    /// The new value that should be assigned to the variable.
    /// </returns>
    Task<string> GetNewValueAsync(string variableKey, IVariableContext variables);
}
