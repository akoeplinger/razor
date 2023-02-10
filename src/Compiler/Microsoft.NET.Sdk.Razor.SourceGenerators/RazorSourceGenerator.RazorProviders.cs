﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Microsoft.NET.Sdk.Razor.SourceGenerators
{
    public partial class RazorSourceGenerator
    {
        /// <summary>
        /// Gets a flag that determines if the source generator should no-op.
        /// <para>
        /// This flag exists to support scenarios in VS where design-time and EnC builds need
        /// to run without invoking the source generator to avoid duplicate types being produced.
        /// The property is set by the SDK via an editor config.
        /// </para>
        /// </summary>
        private static bool GetSuppressionStatus(AnalyzerConfigOptionsProvider optionsProvider, CancellationToken _)
        {
            return optionsProvider.GlobalOptions.TryGetValue("build_property.SuppressRazorSourceGenerator", out var suppressRazorSourceGenerator)
                && suppressRazorSourceGenerator == "true";
        }

        private static (RazorSourceGenerationOptions?, Diagnostic?) ComputeRazorSourceGeneratorOptions((AnalyzerConfigOptionsProvider, ParseOptions) pair, CancellationToken ct)
        {
            Log.ComputeRazorSourceGeneratorOptions();

            var (options, parseOptions) = pair;
            var globalOptions = options.GlobalOptions;

            globalOptions.TryGetValue("build_property.RazorConfiguration", out var configurationName);
            globalOptions.TryGetValue("build_property.RootNamespace", out var rootNamespace);
            globalOptions.TryGetValue("build_property.SupportLocalizedComponentNames", out var supportLocalizedComponentNames);
            globalOptions.TryGetValue("build_property.GenerateRazorMetadataSourceChecksumAttributes", out var generateMetadataSourceChecksumAttributes);

            var razorLanguageVersion = RazorLanguageVersion.Latest;
            Diagnostic? diagnostic = null;
            if (!globalOptions.TryGetValue("build_property.RazorLangVersion", out var razorLanguageVersionString) ||
                !RazorLanguageVersion.TryParse(razorLanguageVersionString, out razorLanguageVersion))
            {
                diagnostic = Diagnostic.Create(
                    RazorDiagnostics.InvalidRazorLangVersionDescriptor,
                    Location.None,
                    razorLanguageVersionString);
            }

            var razorConfiguration = RazorConfiguration.Create(razorLanguageVersion, configurationName ?? "default", System.Linq.Enumerable.Empty<RazorExtension>(), true);
            
            var razorSourceGenerationOptions = new RazorSourceGenerationOptions()
            {
                Configuration = razorConfiguration,
                GenerateMetadataSourceChecksumAttributes = generateMetadataSourceChecksumAttributes == "true",
                RootNamespace = rootNamespace ?? "ASP",
                SupportLocalizedComponentNames = supportLocalizedComponentNames == "true",
                CSharpLanguageVersion = ((CSharpParseOptions)parseOptions).LanguageVersion,
            };

            return (razorSourceGenerationOptions, diagnostic);
        }

        private static (SourceGeneratorProjectItem?, Diagnostic?) ComputeProjectItems((AdditionalText, AnalyzerConfigOptionsProvider) pair, CancellationToken ct)
        {
            var (additionalText, globalOptions) = pair;
            var options = globalOptions.GetOptions(additionalText);

            if (!options.TryGetValue("build_metadata.AdditionalFiles.TargetPath", out var encodedRelativePath) ||
                string.IsNullOrWhiteSpace(encodedRelativePath))
            {
                var diagnostic = Diagnostic.Create(
                    RazorDiagnostics.TargetPathNotProvided,
                    Location.None,
                    additionalText.Path);
                return (null, diagnostic);
            }

            options.TryGetValue("build_metadata.AdditionalFiles.CssScope", out var cssScope);
            var relativePath = Encoding.UTF8.GetString(Convert.FromBase64String(encodedRelativePath));

            var projectItem = new SourceGeneratorProjectItem(
                basePath: "/",
                filePath: '/' + relativePath
                    .Replace(Path.DirectorySeparatorChar, '/')
                    .Replace("//", "/"),
                relativePhysicalPath: relativePath,
                fileKind: additionalText.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase) ? FileKinds.Component : FileKinds.Legacy,
                additionalText: additionalText,
                cssScope: cssScope);
            return (projectItem, null);
        }
    }
}