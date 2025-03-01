﻿//HintName: Mapper.g.cs
// <auto-generated />
#nullable enable
public partial class Mapper
{
    private partial TTarget Map<TSource, TTarget>(TSource source)
    {
        var refHandler = new global::Riok.Mapperly.Internal.Tests.PreserveReferenceHandler();
        return source switch
        {
            global::A x when typeof(TTarget).IsAssignableFrom(typeof(global::B)) => (TTarget)(object)MapToB1(x, refHandler),
            global::C x when typeof(TTarget).IsAssignableFrom(typeof(global::D)) => (TTarget)(object)MapToD1(x, refHandler),
            null => throw new System.ArgumentNullException(nameof(source)),
            _ => throw new System.ArgumentException($"Cannot map {source.GetType()} to {typeof(TTarget)} as there is no known type mapping", nameof(source)),
        };
    }

    private partial global::B MapToB(global::A source)
    {
        return MapToB1(source, new global::Riok.Mapperly.Internal.Tests.PreserveReferenceHandler());
    }

    private partial global::D MapToD(global::C source)
    {
        return MapToD1(source, new global::Riok.Mapperly.Internal.Tests.PreserveReferenceHandler());
    }

    private global::B MapToB1(global::A source, global::Riok.Mapperly.Abstractions.ReferenceHandling.IReferenceHandler refHandler)
    {
        if (refHandler.TryGetReference<global::A, global::B>(source, out var existingTargetReference))
            return existingTargetReference;
        var target = new global::B();
        refHandler.SetReference<global::A, global::B>(source, target);
        target.Value = source.Value;
        return target;
    }

    private global::D MapToD1(global::C source, global::Riok.Mapperly.Abstractions.ReferenceHandling.IReferenceHandler refHandler)
    {
        if (refHandler.TryGetReference<global::C, global::D>(source, out var existingTargetReference))
            return existingTargetReference;
        var target = new global::D(source.Value1);
        refHandler.SetReference<global::C, global::D>(source, target);
        return target;
    }
}