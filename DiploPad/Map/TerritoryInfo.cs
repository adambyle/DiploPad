namespace DiploPad.Map;

/// <summary>
/// Superficial information about a territory and its relationship
/// to other territories on the game board.
/// 
/// This class never includes any game instance data.
/// </summary>
public class TerritoryInfo
{
    /// <summary>
    /// The name of the territory, with proper capitalization,
    /// as it would appear on a game board.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// A list of abbreviations that identify this territory.
    /// 
    /// Territories may not share abbreviations.
    /// </summary>
    public IReadOnlyList<string> Abbreviations { get; }

    /// <summary>
    /// Geographical information about this territory.
    /// </summary>
    public IGeography Geography { get; internal set; }

    /// <summary>
    /// The abbreviation most conventionally used to identify this province.
    /// </summary>
    public string PrimaryAbbreviation => Abbreviations[0];

    /// <summary>
    /// Whether this territory is a supply center.
    /// </summary>
    public virtual bool IsSupplyCenter => false;

    internal TerritoryInfo(string name, IEnumerable<string> abbreviations)
    {
        if (!abbreviations.Any())
            throw new ArgumentException(
                "Territory must have at least one abbreviation.",
                nameof(abbreviations));

        Name = name;
        Abbreviations = abbreviations.Distinct().ToArray();
        Geography = new UnsetGeography();
    }
}
