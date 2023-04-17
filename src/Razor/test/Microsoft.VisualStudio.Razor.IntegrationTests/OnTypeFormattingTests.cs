﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using Xunit;

namespace Microsoft.VisualStudio.Razor.IntegrationTests;

public class OnTypeFormattingTests : AbstractRazorEditorTest
{
    [IdeFact]
    public async Task TypeScript_Semicolon()
    {
        // Open the file
        await TestServices.SolutionExplorer.OpenFileAsync(RazorProjectConstants.BlazorProjectName, RazorProjectConstants.ErrorCshtmlFile, ControlledHangMitigatingCancellationToken);

        await TestServices.Editor.WaitForSemanticClassificationAsync("RazorTagHelperElement", ControlledHangMitigatingCancellationToken, count: 2);

        // Change text to refer back to Program class
        await TestServices.Editor.SetTextAsync(@"
<script>
    function F()
    {
        var    x     =   3
    }
</script>
", ControlledHangMitigatingCancellationToken);
        await TestServices.Editor.PlaceCaretAsync("3", charsOffset: 1, ControlledHangMitigatingCancellationToken);

        await Task.Delay(500, ControlledHangMitigatingCancellationToken);

        // Act
        TestServices.Input.Send(";");

        // Assert
        await TestServices.Editor.WaitForCurrentLineTextAsync("var x = 3;", ControlledHangMitigatingCancellationToken);
    }
}
