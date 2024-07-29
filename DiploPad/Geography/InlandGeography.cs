namespace DiploPad.Geography;

/// <summary>
/// Geographical information about an inland territory.
/// </summary>
public class InlandGeography : IGeography
{
    /// <summary>
    /// A list of adjacent land territories.
    /// </summary>
    public IReadOnlyList<Territory> LandConnections { get; }

    public Terrain Terrain => Terrain.Inland;

    internal InlandGeography(IEnumerable<Territory> landConnections)
    {
        LandConnections = landConnections.Distinct().ToArray();
    }

    public TravelResult CanTravelTo(
        Territory destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null)
    {
        bool canTravel = unitKind is UnitKind.Army &&
            LandConnections.Contains(destination);
        return canTravel ? TravelResult.CanTravel : TravelResult.CannotTravel;
    }

    public void VerifyConnections()
    {
        Territory? invalidLandConnection =
            LandConnections
            .FirstOrDefault(connection =>
                connection.Geography is not InlandGeography or CoastalGeography);
        if (invalidLandConnection is not null)
            throw new TerrainMismatchException(
                $"Territory {invalidLandConnection.Name} is not an inland or coastal territory.")
            {
                BadTerritory = invalidLandConnection,
            };
    }

    public string? ParseCoast(string coastName) => null;
}
