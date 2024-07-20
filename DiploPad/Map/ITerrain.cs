namespace DiploPad.Map;

public interface ITerrain
{
    TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null);
}
