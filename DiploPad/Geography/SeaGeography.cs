namespace DiploPad.Geography;

using LandConnection = (Territory destination, string destinationCoast);

/// <summary>
/// Geographical information about a sea territory.
/// </summary>
public class SeaGeography : IGeography
{
    /// <summary>
    /// A list of adjacent sea territories.
    /// </summary>
    public IReadOnlyList<Territory> SeaConnections { get; }

    /// <summary>
    /// A list of adjacent land territories in the form of territory-coast pairs.
    /// </summary>
    public IReadOnlyList<LandConnection> LandConnections { get; }

    public Terrain Terrain => Terrain.Sea;

    internal SeaGeography(
        IEnumerable<LandConnection> landConnections,
        IEnumerable<Territory> seaConnections)
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
        Territory destination,
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

    public void VerifyConnections()
    {
        Territory? invalidLandConnection =
            LandConnections
            .Select(connection => connection.destination)
            .FirstOrDefault(destination =>
                destination.Geography is not CoastalGeography);
        if (invalidLandConnection is not null)
            throw new TerrainMismatchException(
                $"Territory {invalidLandConnection.Name} is not a coastal territory.")
            {
                BadTerritory = invalidLandConnection,
            };

        Territory? invalidSeaConnection =
            SeaConnections
            .FirstOrDefault(connection =>
                connection.Geography is not SeaGeography);
        if (invalidSeaConnection is not null)
            throw new TerrainMismatchException(
                $"Territory {invalidSeaConnection.Name} is not a sea territory.")
            {
                BadTerritory = invalidSeaConnection,
            };
    }

    public string? ParseCoast(string coastName) => null;
}
