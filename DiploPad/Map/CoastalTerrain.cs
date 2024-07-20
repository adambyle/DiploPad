namespace DiploPad.Map;

public class CoastalTerrain : ITerrain
{
    public IReadOnlyList<string> NamedCoasts { get; }

    private IReadOnlyList<string> UnnamedCoasts { get; }

    public IReadOnlyList<TerritoryInfo> LandConnections { get; }

    public IReadOnlyList<(
        string startCoast,
        TerritoryInfo territory,
        string destinationCoast)> CoastalConnections
    { get; }

    public IReadOnlyList<(string startCoast, TerritoryInfo territory)> SeaConnections { get; }

    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null)
    {
        if (unitKind is UnitKind.Army)
        {
            bool canTravel = LandConnections.Contains(destination);
            return canTravel ? TravelResult.CanTravel : TravelResult.CannotTravel;
        }

        // The unit is a fleet.

        if (startCoast is null)
        {
            // Because the unit is a fleet on a coastal province, the specific coast must be known.
            // The start coast will never be a user error; the program must
            // specify it whenever resolving fleet orders.
            throw new ArgumentNullException(
                nameof(startCoast),
                "Start coast must be specified for fleet orders.");
        }

        if (destination.Terrain is SeaTerrain)
        {
            bool canTravelFromCoast =
                SeaConnections
                .Any(connection
                    => connection.startCoast == startCoast
                    && connection.territory == destination);
            return canTravelFromCoast ? TravelResult.CanTravel : TravelResult.CannotTravel;
        }

        // The destination territory is coastal.

        var possibleCoastalConnections =
            CoastalConnections
            .Where(connection
                => connection.startCoast == startCoast
                && connection.territory == destination);

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
