using Riok.Mapperly.Abstractions.ReferenceHandling;

namespace Riok.Mapperly.Abstractions;

/// <summary>
/// Marks a partial class as a mapper.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class MapperAttribute : Attribute
{
    /// <summary>
    /// Strategy on how to match mapping property names.
    /// </summary>
    public PropertyNameMappingStrategy PropertyNameMappingStrategy { get; set; } = PropertyNameMappingStrategy.CaseSensitive;

    /// <summary>
    /// The default enum mapping strategy.
    /// Can be overwritten on specific enums via mapping method configurations.
    /// </summary>
    public EnumMappingStrategy EnumMappingStrategy { get; set; } = EnumMappingStrategy.ByValue;

    /// <summary>
    /// Whether the case should be ignored for enum mappings.
    /// </summary>
    public bool EnumMappingIgnoreCase { get; set; }

    /// <summary>
    /// Specifies the behaviour in the case when the mapper tries to return <c>null</c> in a mapping method with a non-nullable return type.
    /// If set to <c>true</c> an <see cref="ArgumentNullException"/> is thrown.
    /// If set to <c>false</c> the mapper tries to return a default value.
    /// For a <see cref="string"/> this is <see cref="string.Empty"/>,
    /// for value types <c>default</c>
    /// and for reference types <c>new()</c> if a parameterless constructor exists or else an <see cref="ArgumentNullException"/> is thrown.
    /// </summary>
    public bool ThrowOnMappingNullMismatch { get; set; } = true;

    /// <summary>
    /// Specifies the behaviour in the case when the mapper tries to set a non-nullable property to a <c>null</c> value.
    /// If set to <c>true</c> an <see cref="ArgumentNullException"/> is thrown.
    /// If set to <c>false</c> the property assignment is ignored.
    /// This is ignored for required init properties and <see cref="IQueryable{T}"/> projection mappings.
    /// </summary>
    public bool ThrowOnPropertyMappingNullMismatch { get; set; }

    /// <summary>
    /// Specifies whether <c>null</c> values are assigned to the target.
    /// If <c>true</c> (default), the source is <c>null</c>, and the target does allow <c>null</c> values,
    /// <c>null</c> is assigned.
    /// If <c>false</c>, <c>null</c> values are never assigned to the target property.
    /// This is ignored for required init properties and <see cref="IQueryable{T}"/> projection mappings.
    /// </summary>
    public bool AllowNullPropertyAssignment { get; set; } = true;

    /// <summary>
    /// Whether to always deep copy objects.
    /// Eg. when the type <c>Person[]</c> should be mapped to the same type <c>Person[]</c>,
    /// when <c>false</c>, the same array is reused.
    /// when <c>true</c>, the array and each person is cloned.
    /// </summary>
    public bool UseDeepCloning { get; set; }

    /// <summary>
    /// Enabled conversions which Mapperly automatically implements.
    /// By default all supported type conversions are enabled.
    /// <example>
    /// Eg. to disable all automatically implemented conversions:<br />
    /// <c>EnabledConversions = MappingConversionType.None</c>
    /// </example>
    /// <example>
    /// Eg. to disable <c>ToString()</c> method calls:<br />
    /// <c>EnabledConversions = MappingConversionType.All &amp; ~MappingConversionType.ToStringMethod</c>
    /// </example>
    /// </summary>
    public MappingConversionType EnabledConversions { get; set; } = MappingConversionType.All;

    /// <summary>
    /// Enables the reference handling feature.
    /// Disabled by default for performance reasons.
    /// When enabled, an <see cref="IReferenceHandler"/> instance is passed through the mapping methods
    /// to keep track of and reuse existing target object instances.
    /// </summary>
    public bool UseReferenceHandling { get; set; }

    /// <summary>
    /// The ignore obsolete attribute strategy. Determines how <see cref="ObsoleteAttribute"/> marked members are mapped.
    /// Defaults to <see cref="IgnoreObsoleteMembersStrategy.None"/>.
    /// </summary>
    public IgnoreObsoleteMembersStrategy IgnoreObsoleteMembersStrategy { get; set; } = IgnoreObsoleteMembersStrategy.None;

    /// <summary>
    /// Defines the strategy used when emitting warnings for unmapped members.
    /// By default this is <see cref="RequiredMappingStrategy.Both"/>, emitting warnings for unmapped source and target members.
    /// </summary>
    public RequiredMappingStrategy RequiredMappingStrategy { get; set; } = RequiredMappingStrategy.Both;
}
