using DiploPad.Geography;

namespace DiploPad.Games;

/// <summary>
/// A unit on the game board.
/// </summary>
/// <param name="Territory">The territory the unit is in.</param>
/// <param name="Coast">The coast the unit is on (only valid for fleets).</param>
/// <param name="UnitKind">The kind of unit on the board.</param>
/// <param name="Nation">The nation the unit belongs to.</param>
public record Unit(
    Territory Territory,
    string? Coast,
    UnitKind UnitKind,
    Nation Nation);
