using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Narumikazuchi.Analyzers;

public partial class GenericConstraintAnalyzer
{
    static private Diagnostic CreateNotSelfDiagnostic(TypeDeclarationSyntax type,
                                                      INamedTypeSymbol typeSymbol,
                                                      ITypeParameterSymbol parameterSymbol)
    {
        return Diagnostic.Create(descriptor: s_OnlySelfDescriptor,
                                 location: type.Identifier.GetLocation(),
                                 messageArgs: [parameterSymbol.Name, typeSymbol.ToDisplayString()]);
    }

    static private Diagnostic CreateSelfConstrainNotAllowedDiagnostic(TypeParameterSyntax parameter)
    {
        return Diagnostic.Create(descriptor: s_SelfConstraintNotAllowedDescriptor,
                                 location: parameter.GetLocation());
    }

    static private Diagnostic CreateAddSelfConstraintDiagnostic(TypeDeclarationSyntax type,
                                                                INamedTypeSymbol typeSymbol,
                                                                ITypeParameterSymbol parameterSymbol)
    {
        return Diagnostic.Create(descriptor: s_AddSelfConstraintDescriptor,
                                 location: type.Identifier.GetLocation(),
                                 messageArgs: [parameterSymbol.Name, typeSymbol.ToDisplayString()]);
    }

    static private readonly DiagnosticDescriptor s_OnlySelfDescriptor = new(id: "NCA003",
                                                                            category: "Compiler",
                                                                            title: "Type parameter constraint violation",
                                                                            messageFormat: "Type parameter '{0}' is required to be of type '{1}'",
                                                                            description: null,
                                                                            defaultSeverity: DiagnosticSeverity.Error,
                                                                            isEnabledByDefault: true);

    static private readonly DiagnosticDescriptor s_AddSelfConstraintDescriptor = new(id: "NCA004",
                                                                                     category: "Compiler",
                                                                                     title: "Add built-in constraints",
                                                                                     messageFormat: "Constrain the type parameter '{0}' to the declaring interface type '{1}'",
                                                                                     description: null,
                                                                                     defaultSeverity: DiagnosticSeverity.Info,
                                                                                     isEnabledByDefault: true);

    static private readonly DiagnosticDescriptor s_SelfConstraintNotAllowedDescriptor = new(id: "NCA005",
                                                                                            category: "Compiler",
                                                                                            title: "Attribute not allowed in this context",
                                                                                            messageFormat: "The 'ConstrainToSelfAttribute' is only allowed on interface type parameters",
                                                                                            description: null,
                                                                                            defaultSeverity: DiagnosticSeverity.Error,
                                                                                            isEnabledByDefault: true);
}