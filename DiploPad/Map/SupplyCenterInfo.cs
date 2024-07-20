namespace DiploPad.Map;

/// <summary>
/// Territory info for a supply center.
/// </summary>
public class SupplyCenterInfo : TerritoryInfo
{
    public override bool IsSupplyCenter => true;

    internal SupplyCenterInfo(string name, IEnumerable<string> abbreviations)
        : base(name, abbreviations) { }
}
