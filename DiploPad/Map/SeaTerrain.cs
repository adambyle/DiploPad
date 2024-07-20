namespace DiploPad.Map;

public class SeaTerrain : ITerrain
{
    /// <summary>
    /// A list of adjacent sea territories.
    /// </summary>
    public IReadOnlyList<TerritoryInfo> SeaConnections { get; }

    /// <summary>
    /// A list of adjacent land territories in the form of territory-destinationCoast pairs.
    /// </summary>
    public IReadOnlyList<(TerritoryInfo territory, string coast)> LandConnections { get; }

    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null)
    {
        if (unitKind is not UnitKind.Fleet)
            return TravelResult.CannotTravel;

        if (destination.Terrain is SeaTerrain
            && SeaConnections.Contains(destination))
            return TravelResult.CanTravel;

        if (destination.Terrain is InlandTerrain)
            return TravelResult.CannotTravel;

        // The destination territory is coastal.

        var possibleCoastalConnections =
            LandConnections
            .Where(connection => connection.territory == destination);

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
                .Any(connection => connection.coast == destinationCoast);
            return matchExists ? TravelResult.CanTravel : TravelResult.CannotTravel;
        }
    }
}
