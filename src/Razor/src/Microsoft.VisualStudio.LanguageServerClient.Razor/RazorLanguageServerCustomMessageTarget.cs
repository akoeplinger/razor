﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.LanguageServer;
using Microsoft.AspNetCore.Razor.LanguageServer.ColorPresentation;
using Microsoft.AspNetCore.Razor.LanguageServer.CodeActions;
using Microsoft.AspNetCore.Razor.LanguageServer.CodeActions.Models;
using Microsoft.AspNetCore.Razor.LanguageServer.Common;
using Microsoft.AspNetCore.Razor.LanguageServer.Diagnostics;
using Microsoft.AspNetCore.Razor.LanguageServer.DocumentColor;
using Microsoft.AspNetCore.Razor.LanguageServer.DocumentPresentation;
using Microsoft.AspNetCore.Razor.LanguageServer.Folding;
using Microsoft.AspNetCore.Razor.LanguageServer.Formatting;
using Microsoft.AspNetCore.Razor.LanguageServer.Protocol;
using Microsoft.AspNetCore.Razor.LanguageServer.Semantic;
using Microsoft.AspNetCore.Razor.LanguageServer.Semantic.Models;
using Microsoft.VisualStudio.LanguageServer.Protocol;
using Microsoft.VisualStudio.LanguageServerClient.Razor.WrapWithTag;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using ImplementationResult = Microsoft.VisualStudio.LanguageServer.Protocol.SumType<
    Microsoft.VisualStudio.LanguageServer.Protocol.Location[],
    Microsoft.VisualStudio.LanguageServer.Protocol.VSInternalReferenceItem[]>;

namespace Microsoft.VisualStudio.LanguageServerClient.Razor;

internal abstract class RazorLanguageServerCustomMessageTarget
{

    // Called by Visual Studio to wrap the current selection with a tag
    [JsonRpcMethod(LanguageServerConstants.RazorWrapWithTagEndpoint, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<VSInternalWrapWithTagResponse> RazorWrapWithTagAsync(VSInternalWrapWithTagParams wrapWithParams, CancellationToken cancellationToken);

    // Called by the Razor Language Server to provide inline completions from the platform.
    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorInlineCompletionEndpoint, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<VSInternalInlineCompletionList?> ProvideInlineCompletionAsync(RazorInlineCompletionRequest inlineCompletionParams, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorFoldingRangeEndpoint, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<RazorFoldingRangeResponse?> ProvideFoldingRangesAsync(RazorFoldingRangeRequestParam foldingRangeParams, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorTextPresentationEndpoint, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<WorkspaceEdit?> ProvideTextPresentationAsync(RazorTextPresentationParams presentationParams, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorUriPresentationEndpoint, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<WorkspaceEdit?> ProvideUriPresentationAsync(RazorUriPresentationParams presentationParams, CancellationToken cancellationToken);

    [JsonRpcMethod(LanguageServerConstants.RazorCompletionEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<JToken?> ProvideCompletionsAsync(DelegatedCompletionParams completionParams, CancellationToken cancellationToken);

    [JsonRpcMethod(LanguageServerConstants.RazorCompletionResolveEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<JToken?> ProvideResolvedCompletionItemAsync(DelegatedCompletionItemResolveParams completionResolveParams, CancellationToken cancellationToken);

    [JsonRpcMethod(LanguageServerConstants.RazorGetFormattingOptionsEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<FormattingOptions?> GetFormattingOptionsAsync(TextDocumentIdentifierAndVersion document, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorRenameEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<WorkspaceEdit?> RenameAsync(DelegatedRenameParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorHoverEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<VSInternalHover?> HoverAsync(DelegatedPositionParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorDefinitionEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<Location[]?> DefinitionAsync(DelegatedPositionParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorDocumentHighlightEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<DocumentHighlight[]?> DocumentHighlightAsync(DelegatedPositionParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorSignatureHelpEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<SignatureHelp?> SignatureHelpAsync(DelegatedPositionParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorImplementationEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<ImplementationResult> ImplementationAsync(DelegatedPositionParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorOnAutoInsertEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<VSInternalDocumentOnAutoInsertResponseItem?> OnAutoInsertAsync(DelegatedOnAutoInsertParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorValidateBreakpointRangeName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<Range?> ValidateBreakpointRangeAsync(DelegatedValidateBreakpointRangeParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorPullDiagnosticEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<RazorPullDiagnosticResponse?> DiagnosticsAsync(DelegatedDiagnosticParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorReferencesEndpointName, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<VSInternalReferenceItem[]?> ReferencesAsync(DelegatedPositionParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorSpellCheckEndpoint, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<VSInternalSpellCheckableRangeReport[]> SpellCheckAsync(DelegatedSpellCheckParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorProjectContextsEndpoint, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<VSProjectContextList?> ProjectContextsAsync(DelegatedProjectContextsParams request, CancellationToken cancellationToken);

    [JsonRpcMethod(RazorLanguageServerCustomMessageTargets.RazorDocumentSymbolEndpoint, UseSingleObjectParameterDeserialization = true)]
    public abstract Task<SumType<DocumentSymbol[], SymbolInformation[]>?> DocumentSymbolsAsync(DelegatedDocumentSymbolParams request, CancellationToken cancellationToken);
}
