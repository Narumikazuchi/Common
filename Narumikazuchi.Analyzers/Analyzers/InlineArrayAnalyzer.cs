using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Narumikazuchi.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed partial class InlineArrayAnalyzer : DiagnosticAnalyzer
{
    public sealed override void Initialize(AnalysisContext context)
    {
        if (Debugger.IsAttached is false)
        {
            context.EnableConcurrentExecution();
        }

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterSyntaxNodeAction(action: this.AnalyzeTypeDeclaration,
                                         SyntaxKind.StructDeclaration,
                                         SyntaxKind.RecordStructDeclaration);
    }

    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = new DiagnosticDescriptor[]
    {
        s_AddAttributeDescriptor,
        s_ImplementInterfaceDescriptor,
    }.ToImmutableArray();

    static private Boolean IsInlineArrayInterface(INamedTypeSymbol symbol,
                                                  INamedTypeSymbol @interface)
    {
        if (symbol.IsGenericType is false)
        {
            return false;
        }
        else
        {
            return SymbolEqualityComparer.Default.Equals(x: symbol.ConstructedFrom,
                                                         y: @interface);
        }
    }

    private void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is StructDeclarationSyntax structDeclaration)
        {
            this.ReportDiagnosticIfApplicable(context: context,
                                              type: structDeclaration);
        }
        else if (context.Node is RecordDeclarationSyntax recordDeclaration)
        {
            this.ReportDiagnosticIfApplicable(context: context,
                                              type: recordDeclaration);
        }
    }

    private void ReportDiagnosticIfApplicable(SyntaxNodeAnalysisContext context,
                                              TypeDeclarationSyntax type)
    {
        foreach (SyntaxToken modifier in type.Modifiers)
        {
            // Skip ref structs, since they can't implement interfaces
            if (modifier.IsKind(kind: SyntaxKind.RefKeyword) is true)
            {
                return;
            }
        }

        INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName(fullyQualifiedMetadataName: INLINE_ARRAY_ATTRIBUTE);
        INamedTypeSymbol interfaceSymbol = context.Compilation.GetTypeByMetadataName(fullyQualifiedMetadataName: INLINE_ARRAY_INTERFACE);
        INamedTypeSymbol typeSymbol = context.SemanticModel.GetDeclaredSymbol(declarationSyntax: type);
        if (typeSymbol is null)
        {
            return;
        }

        ImmutableArray<AttributeData> attributes = typeSymbol.GetAttributes();
        ImmutableArray<INamedTypeSymbol> interfaces = typeSymbol.AllInterfaces;
        AttributeData attribute = attributes.FirstOrDefault(attribute => attribute.AttributeClass is not null &&
                                                                         SymbolEqualityComparer.Default.Equals(x: attribute.AttributeClass,
                                                                                                               y: attributeSymbol));
        if (attribute is not null)
        {
            if (
                interfaces.Any(@interface => IsInlineArrayInterface(symbol: @interface,
                                                                    @interface: interfaceSymbol)) is true)
            {
                return;
            }

            Diagnostic implementInterface = CreateImplementInterfaceDiagnostic(type: type);
            context.ReportDiagnostic(diagnostic: implementInterface);
        }
        else
        {
            if (interfaces.Any(@interface => IsInlineArrayInterface(symbol: @interface,
                                                                    @interface: interfaceSymbol)) is false)
            {
                return;
            }

            Diagnostic addAttribute = CreateAddAttributeDiagnostic(type: type);
            context.ReportDiagnostic(diagnostic: addAttribute);
        }
    }

    private const String INLINE_ARRAY_ATTRIBUTE = "System.Runtime.CompilerServices.InlineArrayAttribute";
    private const String INLINE_ARRAY_INTERFACE = "Narumikazuchi.IInlineArray`2";
}