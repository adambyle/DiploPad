﻿namespace DiploPad.Geography;

/// <summary>
/// ParseTerritory info for a home supply center.
/// </summary>
public class HomeSupplyCenter : SupplyCenter
{
    /// <summary>
    /// The nation this home supply center belongs to at the start of the game.
    /// 
    /// This value does not change throughout the game; it controls who is
    /// allowed to build on this supply center.
    /// </summary>
    public Nation HomeNation { get; }

    /// <summary>
    /// The type of unit the nation has in this territory at the
    /// start of the game.
    /// </summary>
    public UnitKind StartUnitKind { get; }

    /// <summary>
    /// The coast the starting unit appears on.
    /// 
    /// This value should only be set if the starting unit is a fleet,
    /// and only needs to be set if the territory has multiple coasts.
    /// </summary>
    public string? StartUnitCoast { get; }

    internal HomeSupplyCenter(
        string name,
        IEnumerable<string> abbreviations,
        Nation homeNation,
        UnitKind startUnitKind,
        string? startUnitCoast = null) : base(name, abbreviations)
    {
        HomeNation = homeNation;
        StartUnitKind = startUnitKind;
        StartUnitCoast = startUnitCoast;
    }
}
