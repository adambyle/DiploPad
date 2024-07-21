namespace DiploPad.Map;

using CoastalConnection = (
        string startCoast,
        TerritoryInfo destination,
        string destinationCoast);
using SeaConnection = (string startCoast, TerritoryInfo destination);

public class CoastalGeography : IGeography
{
    /// <summary>
    /// The names of the coasts on this territory.
    /// 
    /// If the territory has only one coast, it will be named "main".
    /// The user cannot specify the "main" coast in order-writing.
    /// </summary>
    public IReadOnlyList<string> Coasts { get; }

    /// <summary>
    /// A list of adjacent inland territories--not including land territories that are coastal.
    /// </summary>
    public IReadOnlyList<TerritoryInfo> InlandConnections { get; }

    /// <summary>
    /// A list of adjacent land territories along a coast.
    /// </summary>
    public IReadOnlyList<CoastalConnection> CoastalConnections { get; }

    /// <summary>
    /// A list of adjacent sea territories.
    /// </summary>
    public IReadOnlyList<SeaConnection> SeaConnections { get; }

    public Terrain Terrain => Terrain.Coastal;

    internal CoastalGeography(
        IEnumerable<string> coasts,
        IEnumerable<TerritoryInfo> inlandConnections,
        IEnumerable<CoastalConnection> coastalConnections,
        IEnumerable<SeaConnection> seaConnections)
    {
        Coasts = coasts.Select(coast => coast.ToLower()).Distinct().ToArray();
        switch (Coasts.Count)
        {
            case 0:
                throw new ArgumentException(
                    "At least one coast must be specified. (If only one, name it \"main\".)",
                    nameof(coasts));
            case 1 when Coasts[0] != "main":
                throw new ArgumentException(
                    "If only one coast is present, it must be named \"main\".",
                    nameof(coasts));
        }

        InlandConnections = inlandConnections.Distinct().ToArray();
        CoastalConnections =
            coastalConnections
            .Select(connection => (
                connection.startCoast.ToLower(),
                connection.destination,
                connection.destinationCoast.ToLower()))
            .Distinct()
            .ToArray();
        SeaConnections =
            seaConnections
            .Select(connection => (
                connection.startCoast.ToLower(),
                connection.destination))
            .Distinct()
            .ToArray();

        IEnumerable<string> startCoasts =
            CoastalConnections
            .Select(connection => connection.startCoast)
            .Concat(
                SeaConnections.Select(connection => connection.startCoast))
            .Distinct();
        string? invalidStartCoast = startCoasts.First(coast => !coast.Contains(coast));
        if (invalidStartCoast is not null)
            throw new ArgumentException(
                $"Invalid coast given as start coast: {invalidStartCoast}.");
    }

    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null)
    {
        if (unitKind is UnitKind.Army)
        {
            bool canTravel = InlandConnections.Contains(destination);
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

        if (destination.Geography is SeaGeography)
        {
            bool canTravelFromCoast =
                SeaConnections
                .Any(connection
                    => connection.startCoast == startCoast
                    && connection.destination == destination);
            return canTravelFromCoast ? TravelResult.CanTravel : TravelResult.CannotTravel;
        }

        // The destination territory is coastal.

        var possibleCoastalConnections =
            CoastalConnections
            .Where(connection
                => connection.startCoast == startCoast
                && connection.destination == destination);

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
