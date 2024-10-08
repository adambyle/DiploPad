﻿using DiploPad.Games;
using DiploPad.Geography;

namespace DiploPad.Orders;

/// <summary>
/// Information needed for retreats.
/// </summary>
public class RetreatContext
{
    /// <summary>
    /// A retreat context where no retreats are needed.
    /// </summary>
    public static RetreatContext NoRetreatsContext =>
        new(standoffs: [], invasions: [], dislodgedUnits: []);

    /// <summary>
    /// Territories where a standoff occured.
    /// 
    /// Units cannot retreat here.
    /// </summary>
    public IReadOnlyList<Territory> Standoffs;

    /// <summary>
    /// Successful invasions that caused dislodgement.
    /// 
    /// Units from a destination territory here cannot retreat
    /// to the corresponding start territory.
    /// </summary>
    public IReadOnlyList<(Territory start, Territory destination)> Invasions;

    /// <summary>
    /// Units that need retreat orders.
    /// 
    /// The "territories" of these units are where they were dislodged from.
    /// They are not said to occupy the territories specified here.
    /// </summary>
    public IReadOnlyList<Unit> DislodgedUnits;

    /// <summary>
    /// Whether there are any units that need retreat orders from players.
    /// </summary>
    public bool AttentionNeeded => DislodgedUnits.Count > 0;

    internal RetreatContext(
        IEnumerable<Territory> standoffs,
        IEnumerable<(Territory start, Territory destination)> invasions,
        IEnumerable<Unit> dislodgedUnits)
    {
        Standoffs = standoffs.ToArray();
        Invasions = invasions.ToArray();
        DislodgedUnits = dislodgedUnits.ToArray();
    }
}
