namespace DiploPad.Games;

/// <summary>
/// A continuous game of Diplomacy.
/// </summary>
public class Game
{
    private List<GameState> _gameStates;

    public GameState CurrentGameState => _gameStates[^1];
}
