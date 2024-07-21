namespace DiploPad.Map;

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
    }

    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null)
    {
        bool canTravel = unitKind is UnitKind.Army
            && LandConnections.Contains(destination);
        return canTravel ? TravelResult.CanTravel : TravelResult.CannotTravel;
    }
}
