namespace DiploPad.Map;

/// <summary>
/// A list of connections included a territory with wrong terrain type.
/// </summary>
public class TerrainMismatchException : Exception
{
    /// <summary>
    /// The territory in the connections list that caused the problem.
    /// </summary>
    public required TerritoryInfo BadTerritory { get; init;  }

    public TerrainMismatchException() { }

    public TerrainMismatchException(string message) : base(message) { }

    public TerrainMismatchException(string message, Exception inner) : base(message, inner) { }
}
