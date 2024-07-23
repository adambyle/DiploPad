namespace DiploPad.Map;

/// <summary>
/// Geographical information about an inland territory.
/// </summary>
public class InlandGeography : IGeography
{
    /// <summary>
    /// A list of adjacent land territories.
    /// </summary>
    public IReadOnlyList<TerritoryInfo> LandConnections { get; }

    public Terrain Terrain => Terrain.Inland;

    internal InlandGeography(IEnumerable<TerritoryInfo> landConnections)
    {
        LandConnections = landConnections.Distinct().ToArray();
        TerritoryInfo? invalidLandConnection =
            LandConnections
            .FirstOrDefault(connection =>
                connection.Geography is not InlandGeography or CoastalGeography);
        if (invalidLandConnection is not null)
            throw new ArgumentException(
                $"Territory {invalidLandConnection.Name} is not an inland or coastal territory.",
                nameof(landConnections));
    }

    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null)
    {
        bool canTravel = unitKind is UnitKind.Army &&
            LandConnections.Contains(destination);
        return canTravel ? TravelResult.CanTravel : TravelResult.CannotTravel;
    }
}
