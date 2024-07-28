namespace DiploPad.Games;

/// <summary>
/// Game creation options.
/// </summary>
public class GameOptions
{
    /// <summary>
    /// Start the game with an empty board, instead of units in their usual starting places.
    /// 
    /// Units may be added to the board afterward using Force methods.
    /// </summary>
    public bool EmptyBoard { get; set; } = false;

    /// <summary>
    /// The last _year played in the game. If unset, the game has no set end.
    /// </summary>
    public int? FinalYear { get; set; } = null;
}
