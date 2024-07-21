namespace DiploPad.Map;

/// <summary>
/// An error occured while building the map.
/// </summary>
public class MapBuilderException : Exception
{
    /// <summary>
    /// A name or abbreviation that was used twice.
    /// </summary>
    public string? DuplicateNameOrAbbr { get; init; }

    /// <summary>
    /// The specified coast does not exist on the destination territory.
    /// </summary>
    public (TerritoryInfo territory, string destinationCoast)? BadCoast { get; init; }

    public MapBuilderException() { }

    public MapBuilderException(string message) : base(message) { }

    public MapBuilderException(string message, Exception inner) : base(message, inner) { }
}
