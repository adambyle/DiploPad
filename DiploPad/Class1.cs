namespace DiploPad.Map;

public class TerritoryInfo
{
    public string Name { get; }

    public IReadOnlyList<string> Abbreviations { get; }

    public ITerrain Terrain { get; }

    public string PrimaryAbbreviation => Abbreviations[0];

    public virtual bool IsSupplyCenter => false;
}

public class SupplyCenter : TerritoryInfo
{
    public override bool IsSupplyCenter => true;
}

public class HomeSupplyCenter : TerritoryInfo
{
    public PowerInfo Power { get; }

    public UnitKind UnitKind { get; }
}

public class PowerInfo(string[] names)
{
    public IReadOnlyList<string> Names { get; } = names;

    public string PrimaryName => Names[0];
}

public enum UnitKind
{
    Army,
    Fleet,
}

public interface ITerrain
{
    TravelResult CanTravelTo(TerritoryInfo destination, UnitKind unitKind, string? destinationCoast);
}

public class UnsetTerrain : ITerrain
{
    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? destinationCoast = null)
        => throw new NotImplementedException("Terrain info was not set during map construction.");
}

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
        string? destinationCoast = null)
    {
        bool canTravel = unitKind is UnitKind.Army
            && LandConnections.Contains(destination);
        return canTravel ? TravelResult.CanTravel : TravelResult.CannotTravel;
    }
}

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
        string? destinationCoast = null)
    {
        if (unitKind is not UnitKind.Fleet)
            return TravelResult.CannotTravel;

        if (destination.Terrain is SeaTerrain
            && SeaConnections.Contains(destination))
            return TravelResult.CanTravel;

        if (destination.Terrain is InlandTerrain)
            return TravelResult.CannotTravel;

        if (destination.Terrain is CoastalTerrain)
        {
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
}

public class CoastalTerrain : ITerrain
{
    public IReadOnlyList<string> NamedCoasts { get; }

    private IReadOnlyList<string> UnnamedCoasts { get; }

    public IReadOnlyList<TerritoryInfo> LandConnections { get; }

    public IReadOnlyList<(
        string myCoast,
        TerritoryInfo territory,
        string destinationCoast)> CoastalConnections { get; }

    public IReadOnlyList<(string myCoast, TerritoryInfo territory)> SeaConnections { get; }

    public TravelResult CanTravelTo(
        TerritoryInfo destination,
        UnitKind unitKind,
        string? destinationCoast = null)
    {

    }
}

public enum TravelResult
{
    CanTravel,
    CoastNeeded,
    CannotTravel,
}
