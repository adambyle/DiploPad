namespace DiploPad.Map;

public class InlandTerrain : ITerrain
{
    public IReadOnlyList<TerritoryInfo> LandConnections { get; }

    public InlandTerrain(TerritoryInfo[] landConnections)
    {
        TerritoryInfo? seaConnection =
            landConnections
            .FirstOrDefault(territory => territory.Terrain is SeaTerrain);
        if (seaConnection is not null)
            throw new ArgumentException(
                $"Land territory cannot connect by land to sea territory {seaConnection.Name}.",
                nameof(landConnections));

        LandConnections = landConnections;
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
