namespace DiploPad.Map;

using LandConnection = (TerritoryInfo destination, string destinationCoast);

/// <summary>
/// Geographical information about a sea territory.
/// </summary>
public class SeaGeography : IGeography
{
    /// <summary>
    /// A list of adjacent sea territories.
    /// </summary>
    public IReadOnlyList<TerritoryInfo> SeaConnections { get; }

    /// <summary>
    /// A list of adjacent land territories in the form of territory-coast pairs.
    /// </summary>
    public IReadOnlyList<LandConnection> LandConnections { get; }

    public Terrain Terrain => Terrain.Sea;

    internal SeaGeography(
        IEnumerable<LandConnection> landConnections,
        IEnumerable<TerritoryInfo> seaConnections)
    {
        LandConnections =
            landConnections
            .Select(connection => (
                connection.destination,
                connection.destinationCoast.ToLower()))
            .Distinct()
            .ToArray();
        SeaConnections = seaConnections.Distinct().ToArray();
    }

    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null)
    {
        if (unitKind is not UnitKind.Fleet)
            return TravelResult.CannotTravel;

        if (destination.Geography is SeaGeography
            && SeaConnections.Contains(destination))
            return TravelResult.CanTravel;

        if (destination.Geography is InlandGeography)
            return TravelResult.CannotTravel;

        // The destination territory is coastal.

        var possibleCoastalConnections =
            LandConnections
            .Where(connection => connection.destination == destination);

        if (!possibleCoastalConnections.Any())
            return TravelResult.CannotTravel;

        if (destinationCoast is null)
        {
            // Because the destination coast was not specified, travel is only permitted
            // if there is one unambiguous option.
            bool onlyOneOption = possibleCoastalConnections.Count() is 1;
            return onlyOneOption ? TravelResult.CanTravel : TravelResult.CoastNeeded;
        }
        else
        {
            // A destination coast was specified, so search for an exact match.
            bool matchExists =
                possibleCoastalConnections
                .Any(connection => connection.destinationCoast == destinationCoast);
            return matchExists ? TravelResult.CanTravel : TravelResult.CannotTravel;
        }
    }
}
