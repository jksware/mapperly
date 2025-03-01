﻿//HintName: Mapper.g.cs
// <auto-generated />
#nullable enable
public partial class Mapper
{
    private partial global::System.Collections.Generic.List<int> Map(global::System.ReadOnlyMemory<int> source)
    {
        return MapToList(source.Span);
    }

    private global::System.Collections.Generic.List<int> MapToList(global::System.ReadOnlySpan<int> source)
    {
        var target = new global::System.Collections.Generic.List<int>();
        target.EnsureCapacity(source.Length + target.Count);
        foreach (var item in source)
        {
            target.Add(item);
        }
        return target;
    }
}