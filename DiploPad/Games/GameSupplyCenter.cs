using DiploPad.Geography;

namespace DiploPad.Games;

/// <summary>
/// A supply center on a game board.
/// </summary>
/// <param name="SupplyCenter">The territory information about the supply center.</param>
/// <param name="OwningNation">The nation, if any, that has ownership of this center.</param>
public record GameSupplyCenter(
    SupplyCenter SupplyCenter,
    Nation? OwningNation);
