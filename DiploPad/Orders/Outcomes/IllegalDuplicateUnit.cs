namespace DiploPad.Orders.Outcomes;

/// <summary>
/// The order was illegal because multiple orders were given
/// for this unit.
/// </summary>
/// <param name="unitOrder"><see cref="SourceUnitOrder"/></param>
/// <param name="otherOrders"><see cref="OtherOrders"/></param>
public class IllegalDuplicateUnit(
    IUnitOrder unitOrder,
    IEnumerable<IUnitOrder> otherOrders) : IOutcomeInfo
{
    /// <summary>
    /// The illegal order that was made.
    /// </summary>
    public IUnitOrder SourceUnitOrder { get; } = unitOrder;

    /// <summary>
    /// The other orders made for the same unit.
    /// </summary>
    public IReadOnlyList<IUnitOrder> OtherOrders { get; } = otherOrders.ToArray();

    public string Message =>
        $"{OtherOrders.Count + 1} orders were made for " +
        $"the same unit in {SourceUnitOrder.SourceTerritory}.";
}
