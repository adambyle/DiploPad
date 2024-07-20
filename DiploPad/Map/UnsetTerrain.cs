namespace DiploPad.Map;

public class UnsetTerrain : ITerrain
{
    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null)
        => throw new NotImplementedException("Terrain info was not set during map construction.");
}
