using DiploPad.Geography;
using DiploPad.Orders;
using DiploPad.Parsing;

namespace DiploPad.Games;

/// <summary>
/// A frozen state of a game.
/// </summary>
public class GameState
{
    private List<Unit> _units;
    private List<GameSupplyCenter> _supplyCenters;

    /// <summary>
    /// The map of the game board.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// The year of the game.
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// The phase of the game.
    /// </summary>
    public Phase Phase { get; }

    /// <summary>
    /// The units on the board.
    /// </summary>
    public IReadOnlyList<Unit> Units { get; }

    /// <summary>
    /// The supply centers and ownership information.
    /// </summary>
    public IReadOnlyList<GameSupplyCenter> SupplyCenters { get; }

    /// <summary>
    /// Information about retreats that need resolving.
    /// </summary>
    public RetreatContext RetreatContext { get; }

    internal GameState(
        Map map,
        int year,
        Phase phase,
        IEnumerable<Unit> units,
        IEnumerable<GameSupplyCenter> supplyCenters,
        RetreatContext retreatContext)
    {
        Map = map;
        Year = year;
        Phase = phase;
        Units = units.ToArray();
        SupplyCenters = supplyCenters.ToArray();
        RetreatContext = retreatContext;
    }

    internal GameState(GameState other)
    {
        Map = other.Map;
        Year = other.Year;
        Phase = other.Phase;
        Units = [.. other.Units];
        SupplyCenters = [.. other.SupplyCenters];
        RetreatContext = other.RetreatContext;
    }

    internal GameState(
        GameState other,
        OrderContext orderContext,
        bool advancePhase = true,
        bool skipRedundantPhases = true) : this(other)
    {
        foreach (var outcome in orderContext.Outcomes)
            outcome.Transform(this);

        if (!advancePhase)
            return;

        Phase += 1;
        if (Phase > Phase.WinterBuilds)
        {
            Year += 1;
            Phase = Phase.SpringOrders;
        }

        if (!skipRedundantPhases)
            return;

        // Sometimes, in retreat phases, no retreats are needed.
        if (
            Phase.IsRetreatPhase() &&
            !RetreatContext.AttentionNeeded)
        {
            Phase += 1;
        }

        // Sometines, in build phases, no builds are needed.
        if (
            Phase is Phase.WinterBuilds &&
            !AnyBuildOrdersNeeded())
        {
            Phase = Phase.SpringOrders;
        }
    }

    #region Geography Getters

    /// <summary>
    /// Get territory information for the specified name.
    /// </summary>
    /// <param name="territoryName">The name of the territory.</param>
    /// <returns></returns>
    public Territory GetTerritory(string territoryName) =>
        Parser
        .ParseTerritory(territoryName, sources: Map.Territories)
        .Expect($"Could not get territory from name: {territoryName}.");

    /// <summary>
    /// Get nation information for the specified name.
    /// </summary>
    /// <param name="nationName">The name of the nation.</param>
    /// <returns></returns>
    public Nation GetNation(string nationName) =>
        Parser
        .ParseNation(nationName, sources: Map.Nations)
        .Expect($"Could not get nation from name: {nationName}.");

    #endregion

    #region Occupancy

    /// <summary>
    /// Get the unit, if any, in the specified territory.
    /// </summary>
    /// <param name="territory">The territory to check.</param>
    public Unit? UnitInTerritory(Territory territory) =>
        Units.FirstOrDefault(unit => unit.Territory == territory);


    /// <summary>
    /// Get the unit, if any, in the specified territory on the specified coast.
    /// </summary>
    /// <param name="territory">The territory to check.</param>
    /// <param name="coastName">The coast to check.</param>
    public Unit? UnitInTerritory(Territory territory, string coastName) =>
        Units.FirstOrDefault(unit =>
            unit.Territory == territory && unit.Coast == coastName);

    /// <summary>
    /// Whether the specified territory is empty (has no occupying unit).
    /// </summary>
    /// <param name="territory">The territory to check.</param>
    /// <returns></returns>
    public bool IsTerritoryEmpty(Territory territory) =>
        !Units.Any(unit => unit.Territory == territory);

    #endregion

    #region Unit Counts; Supply Centers; Builds

    /// <summary>
    /// A list of units belonging to the specified nation.
    /// </summary>
    /// <param name="nation">The nation owning the units.</param>
    public IEnumerable<Unit> UnitsForNation(Nation nation) =>
        Units.Where(unit => unit.Nation == nation);

    /// <summary>
    /// A list of units belonging to the specified nation.
    /// </summary>
    /// <param name="nationName">The name of the nation owning the units.</param>
    public IEnumerable<Unit> UnitsForNation(string nationName) =>
        UnitsForNation(GetNation(nationName));

    /// <summary>
    /// A list of supply centers owned by the specified nation.
    /// </summary>
    /// <param name="nation">The nation owning the supply centers.</param>
    public IEnumerable<GameSupplyCenter> SupplyCentersForNation(Nation nation) =>
        SupplyCenters.Where(center => center.OwningNation == nation);

    /// <summary>
    /// A list of supply centers owned by the specified nation.
    /// </summary>
    /// <param name="nationName">The name of the nation owning the supply centers.</param>
    public IEnumerable<GameSupplyCenter> SupplyCentersForNation(string nationName) =>
        SupplyCentersForNation(GetNation(nationName));

    /// <summary>
    /// A list of home supply centers belonging to the specified nation.
    /// Includes nations that are owned by rival nations.
    /// </summary>
    /// <param name="nation">The nation owning the supply centers.</param>
    public IEnumerable<GameSupplyCenter> HomeSupplyCentersForNation(Nation nation) =>
        SupplyCenters.Where(center =>
            center.SupplyCenter is HomeSupplyCenter { HomeNation: Nation homeNation } &&
            homeNation == nation);

    /// <summary>
    /// A list of home supply centers belonging to the specified nation.
    /// Includes nations that are owned by rival nations.
    /// </summary>
    /// <param name="nationName">The name of the nation owning the supply centers.</param>
    public IEnumerable<GameSupplyCenter> HomeSupplyCentersForNation(string nationName) =>
        HomeSupplyCentersForNation(GetNation(nationName));

    /// <summary>
    /// A list of home supply centers owned by the specified nation that
    /// have no occupying unit.
    /// </summary>
    /// <param name="nation">The nation owning the supply centers.</param>
    public IEnumerable<GameSupplyCenter> OpenOwnedHomeSupplyCentersForNation(Nation nation) =>
        HomeSupplyCentersForNation(nation)
        .Where(center =>
            center.OwningNation == nation &&
            IsTerritoryEmpty(center.SupplyCenter));

    /// <summary>
    /// A list of home supply centers owned by the specified nation that
    /// have no occupying unit.
    /// </summary>
    /// <param name="nationName">The name of the nation owning the supply centers.</param>
    public IEnumerable<GameSupplyCenter> OpenOwnedHomeSupplyCentersForNation(string nationName) =>
        OpenOwnedHomeSupplyCentersForNation(GetNation(nationName));

    /// <summary>
    /// The difference between the owned supply centers and the number of units
    /// for the specified nation.
    /// </summary>
    /// <param name="nation">The nation to check.</param>
    public int UnitDeltaForNation(Nation nation)
    {
        int supplyCenterCount = SupplyCentersForNation(nation).Count();
        int unitCount = UnitsForNation(nation).Count();
        return supplyCenterCount - unitCount;
    }

    /// <summary>
    /// The difference between the owned supply centers and the number of units
    /// for the specified nation.
    /// </summary>
    /// <param name="nationName">The name of the nation to check.</param>
    public int UnitDeltaForNation(string nationName) =>
        UnitDeltaForNation(GetNation(nationName));

    /// <summary>
    /// The number of builds the specified nation gets.
    /// Unlike <see cref="UnitDeltaForNation"/>, this accounts for
    /// occupied home supply centers, capping the number of possible builds.
    /// </summary>
    /// <param name="nation">The nation to check.</param>
    public int BuildsForNation(Nation nation)
    {
        int unitDelta = UnitDeltaForNation(nation);
        int openHomeCenterCount = OpenOwnedHomeSupplyCentersForNation(nation).Count();
        return Math.Min(unitDelta, openHomeCenterCount);
    }

    /// <summary>
    /// The number of builds the specified nation gets.
    /// Unlike <see cref="UnitDeltaForNation"/>, this accounts for
    /// occupied home supply centers, capping the number of possible builds.
    /// </summary>
    /// <param name="nationName">The name of the nation to check.</param>
    public int BuildsForNation(string nationName) =>
        BuildsForNation(GetNation(nationName));

    /// <summary>
    /// Whether any player needs to write build or disband orders.
    /// 
    /// Either a player has fewer owned supply centers than units,
    /// or they have more and there are open home centers to build on.
    /// </summary>
    public bool AnyBuildOrdersNeeded()
    {
        return Map.Nations.Any(
            nation => BuildsForNation(nation) is not 0);
    }

    #endregion

    #region Order Transform Handles

    // NOTICE: These methods only to be called from OrderOutcome.Transform.
    // OurderOutcome.Transform, in turn, is only to be called from the constructor.

    public void ForceClearTerritory(Territory territory) =>
        _units.RemoveAll(unit => unit.Territory == territory);

    public void ForceAddUnit(Unit unit)
    {
        if (!Map.Territories.Contains(unit.Territory))
            return;

        ForceClearTerritory(unit.Territory);

        _units.Add(unit);
    }

    #endregion
}
