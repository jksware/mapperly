using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Riok.Mapperly.Descriptors.Enumerables.EnsureCapacity;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Riok.Mapperly.Emit.Syntax.SyntaxFactoryHelper;

namespace Riok.Mapperly.Descriptors.Mappings.ExistingTarget;

/// <summary>
/// Represents a foreach dictionary mapping which works by looping through the source,
/// mapping each element and set it to the target collection.
/// </summary>
public class ForEachSetDictionaryExistingTargetMapping : ExistingTargetMapping
{
    private const string LoopItemVariableName = "item";
    private const string ExplicitCastVariableName = "targetDict";
    private const string KeyPropertyName = nameof(KeyValuePair<object, object>.Key);
    private const string ValuePropertyName = nameof(KeyValuePair<object, object>.Value);

    private readonly INewInstanceMapping _keyMapping;
    private readonly INewInstanceMapping _valueMapping;
    private readonly INamedTypeSymbol? _explicitCast;
    private readonly EnsureCapacityInfo? _ensureCapacity;

    public ForEachSetDictionaryExistingTargetMapping(
        ITypeSymbol sourceType,
        ITypeSymbol targetType,
        INewInstanceMapping keyMapping,
        INewInstanceMapping valueMapping,
        INamedTypeSymbol? explicitCast,
        EnsureCapacityInfo? ensureCapacity
    )
        : base(sourceType, targetType)
    {
        _keyMapping = keyMapping;
        _valueMapping = valueMapping;
        _explicitCast = explicitCast;
        _ensureCapacity = ensureCapacity;
    }

    public override IEnumerable<StatementSyntax> Build(TypeMappingBuildContext ctx, ExpressionSyntax target)
    {
        if (_explicitCast != null)
        {
            var type = FullyQualifiedIdentifier(_explicitCast);
            var cast = CastExpression(type, target);

            var castedVariable = ctx.NameBuilder.New(ExplicitCastVariableName);
            target = IdentifierName(castedVariable);

            yield return ctx.SyntaxFactory.DeclareLocalVariable(castedVariable, cast);
        }

        if (_ensureCapacity != null)
        {
            yield return _ensureCapacity.Build(ctx, target);
        }

        var loopItemVariableName = ctx.NameBuilder.New(LoopItemVariableName);

        var convertedKeyExpression = _keyMapping.Build(ctx.WithSource(MemberAccess(loopItemVariableName, KeyPropertyName)));
        var convertedValueExpression = _valueMapping.Build(ctx.WithSource(MemberAccess(loopItemVariableName, ValuePropertyName)));

        var assignment = Assignment(ElementAccess(target, convertedKeyExpression), convertedValueExpression);

        yield return ctx.SyntaxFactory.ForEach(loopItemVariableName, ctx.Source, assignment);
    }
}
