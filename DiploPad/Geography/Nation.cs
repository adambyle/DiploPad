namespace DiploPad.Geography;

/// <summary>
/// Superficial information about a nation on the game board.
/// 
/// This class never includes any game instance data.
/// </summary>
public class Nation
{
    /// <summary>
    /// A list of names that identify this nation.
    /// </summary>
    public IReadOnlyList<string> Names { get; }

    /// <summary>
    /// The name most conventionally used to identify this nation.
    /// </summary>
    public string PrimaryName => Names[0];

    internal Nation(IEnumerable<string> names)
    {
        if (!names.Any())
            throw new ArgumentException(
                "Nation must have at least one name.",
                nameof(names));

        Names = names.Distinct().ToArray();
    }
}
