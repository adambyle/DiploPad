namespace DiploPad;

/// <summary>
/// The type of unit.
/// </summary>
public enum UnitKind
{
    /// <summary>
    /// Land-based unit that can occupy inland and coastal territories.
    /// </summary>
    Army,

    /// <summary>
    /// Water-based unit that can occupy sea and coastal territories.
    /// </summary>
    Fleet,
}

/// <summary>
/// Extension methods for the <see cref="UnitKind"/> type.
/// </summary>
public static class UnitKindExtensions
{
    /// <summary>
    /// Get the unit kind's abbreviation letter.
    /// </summary>
    public static string Abbreviation(this UnitKind unitKind) => unitKind switch
    {
        UnitKind.Army => "A",
        UnitKind.Fleet => "F",
        _ => throw new ArgumentException("Invalid unit kind.", nameof(unitKind)),
    };
}
