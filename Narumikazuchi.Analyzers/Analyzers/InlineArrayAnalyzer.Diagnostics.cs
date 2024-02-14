using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Narumikazuchi.Analyzers;

public partial class InlineArrayAnalyzer
{
    static public Diagnostic CreateImplementInterfaceDiagnostic(TypeDeclarationSyntax type)
    {
        return Diagnostic.Create(descriptor: s_ImplementInterfaceDescriptor,
                                 location: type.Identifier.GetLocation());
    }

    static public Diagnostic CreateAddAttributeDiagnostic(TypeDeclarationSyntax type)
    {
        return Diagnostic.Create(descriptor: s_AddAttributeDescriptor,
                                 location: type.Identifier.GetLocation());
    }

    static private readonly DiagnosticDescriptor s_ImplementInterfaceDescriptor = new(id: "NCA001",
                                                                                      category: "Compiler",
                                                                                      title: "Implement IInlineArray`2 interface",
                                                                                      messageFormat: "Implement IInlineArray`2 interface to gain generics support with helper classes",
                                                                                      description: null,
                                                                                      defaultSeverity: DiagnosticSeverity.Info,
                                                                                      isEnabledByDefault: true);

    static private readonly DiagnosticDescriptor s_AddAttributeDescriptor = new(id: "NCA002",
                                                                                category: "Compiler",
                                                                                title: "Add InlineArray attribute",
                                                                                messageFormat: "Add the InlineArrayAttribute to this struct since the IInlineArray`2 interface indicates an inline array",
                                                                                description: null,
                                                                                defaultSeverity: DiagnosticSeverity.Error,
                                                                                isEnabledByDefault: true);
}