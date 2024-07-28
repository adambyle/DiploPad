using DiploPad.Geography;

namespace DiploPad.Games;

/// <summary>
/// A frozen state of a game.
/// </summary>
public class GameState
{
    /// <summary>
    /// The map of the game board.
    /// </summary>
    public Map Map { get;  }

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
}
