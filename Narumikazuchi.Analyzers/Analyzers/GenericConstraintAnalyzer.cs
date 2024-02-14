using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Narumikazuchi.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed partial class GenericConstraintAnalyzer : DiagnosticAnalyzer
{
    public sealed override void Initialize(AnalysisContext context)
    {
        if (Debugger.IsAttached is false)
        {
            context.EnableConcurrentExecution();
        }

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterSyntaxNodeAction(action: this.AnalyzeTypeDeclaration,
                                         SyntaxKind.ClassDeclaration,
                                         SyntaxKind.StructDeclaration,
                                         SyntaxKind.RecordDeclaration,
                                         SyntaxKind.RecordStructDeclaration,
                                         SyntaxKind.DelegateDeclaration,
                                         SyntaxKind.InterfaceDeclaration,
                                         SyntaxKind.MethodDeclaration);
    }

    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = new DiagnosticDescriptor[]
    {
        s_OnlySelfDescriptor,
        s_SelfConstraintNotAllowedDescriptor,
        s_AddSelfConstraintDescriptor,
    }.ToImmutableArray();

    private void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is DelegateDeclarationSyntax delegateDeclaration)
        {
            this.AnalyzeTypeDeclaration(context: context,
                                        declaration: delegateDeclaration);
        }
        else if (context.Node is StructDeclarationSyntax structDeclaration)
        {
            this.AnalyzeTypeDeclaration(context: context,
                                        declaration: structDeclaration);
        }
        else if (context.Node is ClassDeclarationSyntax classDeclaration)
        {
            this.AnalyzeTypeDeclaration(context: context,
                                        declaration: classDeclaration);
        }
        else if (context.Node is RecordDeclarationSyntax recordDeclaration)
        {
            this.AnalyzeTypeDeclaration(context: context,
                                        declaration: recordDeclaration);
        }
        else if (context.Node is InterfaceDeclarationSyntax interfaceDeclaration)
        {
            this.AnalyzeTypeDeclaration(context: context,
                                        declaration: interfaceDeclaration);
        }
        else if (context.Node is MethodDeclarationSyntax methodDeclaration)
        {
            this.AnalyzeTypeDeclaration(context: context,
                                        declaration: methodDeclaration);
        }
    }

    private void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext context,
                                        DelegateDeclarationSyntax declaration)
    {
        INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName(fullyQualifiedMetadataName: CONSTRAIN_TO_SELF_ATTRIBUTE);
        INamedTypeSymbol typeSymbol = context.SemanticModel.GetDeclaredSymbol(declarationSyntax: declaration);
        if (typeSymbol is null ||
            typeSymbol.IsGenericType is false)
        {
            return;
        }

        foreach (TypeParameterSyntax parameter in declaration.TypeParameterList.Parameters)
        {
            ITypeParameterSymbol parameterSymbol = context.SemanticModel.GetDeclaredSymbol(typeParameter: parameter);
            if (parameterSymbol is null)
            {
                continue;
            }

            ImmutableArray<AttributeData> attributes = parameterSymbol.GetAttributes();
            AttributeData attribute = attributes.FirstOrDefault(attribute => attribute.AttributeClass is not null &&
                                                                             SymbolEqualityComparer.Default.Equals(x: attribute.AttributeClass,
                                                                                                                   y: attributeSymbol));
            if (attribute is null)
            {
                continue;
            }

            Diagnostic diagnostic = CreateSelfConstrainNotAllowedDiagnostic(parameter: parameter);
            context.ReportDiagnostic(diagnostic: diagnostic);
        }
    }

    private void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext context,
                                        MethodDeclarationSyntax declaration)
    {
        INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName(fullyQualifiedMetadataName: CONSTRAIN_TO_SELF_ATTRIBUTE);
        IMethodSymbol methodSymbol = context.SemanticModel.GetDeclaredSymbol(declarationSyntax: declaration);
        if (methodSymbol is null ||
            methodSymbol.IsGenericMethod is false)
        {
            return;
        }

        foreach (TypeParameterSyntax parameter in declaration.TypeParameterList.Parameters)
        {
            ITypeParameterSymbol parameterSymbol = context.SemanticModel.GetDeclaredSymbol(typeParameter: parameter);
            if (parameterSymbol is null)
            {
                continue;
            }

            ImmutableArray<AttributeData> attributes = parameterSymbol.GetAttributes();
            AttributeData attribute = attributes.FirstOrDefault(attribute => attribute.AttributeClass is not null &&
                                                                             SymbolEqualityComparer.Default.Equals(x: attribute.AttributeClass,
                                                                                                                   y: attributeSymbol));
            if (attribute is null)
            {
                continue;
            }

            Diagnostic diagnostic = CreateSelfConstrainNotAllowedDiagnostic(parameter: parameter);
            context.ReportDiagnostic(diagnostic: diagnostic);
        }
    }

    private void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext context,
                                        TypeDeclarationSyntax declaration)
    {
        INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName(fullyQualifiedMetadataName: CONSTRAIN_TO_SELF_ATTRIBUTE);
        INamedTypeSymbol typeSymbol = context.SemanticModel.GetDeclaredSymbol(declarationSyntax: declaration);
        if (typeSymbol is null)
        {
            return;
        }

        if (typeSymbol.IsGenericType is true)
        {
            foreach (TypeParameterSyntax parameter in declaration.TypeParameterList.Parameters)
            {
                ITypeParameterSymbol parameterSymbol = context.SemanticModel.GetDeclaredSymbol(typeParameter: parameter);
                if (parameterSymbol is null)
                {
                    continue;
                }

                ImmutableArray<AttributeData> attributes = parameterSymbol.GetAttributes();
                AttributeData attribute = attributes.FirstOrDefault(attribute => attribute.AttributeClass is not null &&
                                                                                 SymbolEqualityComparer.Default.Equals(x: attribute.AttributeClass,
                                                                                                                       y: attributeSymbol));
                if (attribute is null)
                {
                    continue;
                }

                Diagnostic diagnostic = CreateSelfConstrainNotAllowedDiagnostic(parameter: parameter);
                context.ReportDiagnostic(diagnostic: diagnostic);
            }
        }

        if (declaration.BaseList is not null)
        {
            this.AnalyzeInheritance(context: context,
                                    declaration: declaration);
        }
    }

    private void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext context,
                                        InterfaceDeclarationSyntax declaration)
    {
        INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName(fullyQualifiedMetadataName: CONSTRAIN_TO_SELF_ATTRIBUTE);
        INamedTypeSymbol typeSymbol = context.SemanticModel.GetDeclaredSymbol(declarationSyntax: declaration);
        if (typeSymbol is null ||
            typeSymbol.IsGenericType is false)
        {
            return;
        }

        foreach (TypeParameterSyntax parameter in declaration.TypeParameterList.Parameters)
        {
            ITypeParameterSymbol parameterSymbol = context.SemanticModel.GetDeclaredSymbol(typeParameter: parameter);
            if (parameterSymbol is null)
            {
                continue;
            }

            ImmutableArray<AttributeData> attributes = parameterSymbol.GetAttributes();
            AttributeData attribute = attributes.FirstOrDefault(attribute => attribute.AttributeClass is not null &&
                                                                             SymbolEqualityComparer.Default.Equals(x: attribute.AttributeClass,
                                                                                                                   y: attributeSymbol));
            if (attribute is null)
            {
                continue;
            }

            Boolean hasAdditionalConstraint = false;
            foreach (TypeParameterConstraintClauseSyntax clause in declaration.ConstraintClauses)
            {
                if (hasAdditionalConstraint is true)
                {
                    break;
                }

                if (parameter.Identifier.Value is not null &&
                    parameter.Identifier.Value.Equals(obj: clause.Name.Identifier.Value) is true)
                {
                    foreach (TypeParameterConstraintSyntax constraint in clause.Constraints)
                    {
                        if (constraint is not TypeConstraintSyntax typeConstraint)
                        {
                            continue;
                        }

                        TypeSyntax type = typeConstraint.Type;
                        SymbolInfo symbolInfo = context.SemanticModel.GetSymbolInfo(expression: type);
                        INamedTypeSymbol constraintType = symbolInfo.Symbol as INamedTypeSymbol;
                        if (SymbolEqualityComparer.Default.Equals(x: constraintType,
                                                                  y: typeSymbol) is true)
                        {
                            hasAdditionalConstraint = true;
                            break;
                        }
                    }
                }
            }

            if (hasAdditionalConstraint is false)
            {
                Diagnostic diagnostic = CreateAddSelfConstraintDiagnostic(type: declaration,
                                                                          typeSymbol: typeSymbol,
                                                                          parameterSymbol: parameterSymbol);
                context.ReportDiagnostic(diagnostic: diagnostic);
            }
        }
    }

    private void AnalyzeInheritance(SyntaxNodeAnalysisContext context,
                                    TypeDeclarationSyntax declaration)
    {
        INamedTypeSymbol attributeSymbol = context.Compilation.GetTypeByMetadataName(fullyQualifiedMetadataName: CONSTRAIN_TO_SELF_ATTRIBUTE);
        INamedTypeSymbol typeSymbol = context.SemanticModel.GetDeclaredSymbol(declarationSyntax: declaration);
        foreach (BaseTypeSyntax baseType in declaration.BaseList.Types)
        {
            SymbolInfo symbolInfo = context.SemanticModel.GetSymbolInfo(expression: baseType.Type);
            if (symbolInfo.Symbol is not INamedTypeSymbol baseTypeSymbol ||
                baseTypeSymbol.IsGenericType is false)
            {
                continue;
            }

            for (Int32 index = 0;
                 index < baseTypeSymbol.TypeParameters.Length;
                 index++)
            {
                ITypeParameterSymbol parameter = baseTypeSymbol.TypeParameters[index];
                ImmutableArray<AttributeData> attributes = parameter.GetAttributes();
                AttributeData attribute = attributes.FirstOrDefault(attribute => attribute.AttributeClass is not null &&
                                                                                 SymbolEqualityComparer.Default.Equals(x: attribute.AttributeClass,
                                                                                                                       y: attributeSymbol));
                if (attribute is null)
                {
                    continue;
                }

                ITypeSymbol argument = baseTypeSymbol.TypeArguments[index];
                if (SymbolEqualityComparer.Default.Equals(x: argument,
                                                          y: typeSymbol) is false)
                {
                    Diagnostic diagnostic = CreateNotSelfDiagnostic(type: declaration,
                                                                    typeSymbol: typeSymbol,
                                                                    parameterSymbol: parameter);
                    context.ReportDiagnostic(diagnostic: diagnostic);
                }
            }
        }
    }

    private const String CONSTRAIN_TO_SELF_ATTRIBUTE = "Narumikazuchi.ConstrainToImplementationAttribute";
}