namespace DiploPad.Geography;

/// <summary>
/// ParseTerritory info for a supply center.
/// </summary>
public class SupplyCenter : Territory
{
    public override bool IsSupplyCenter => true;

    internal SupplyCenter(string name, IEnumerable<string> abbreviations)
        : base(name, abbreviations) { }
}
