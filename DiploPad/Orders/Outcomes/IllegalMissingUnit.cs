namespace DiploPad.Orders.Outcomes;

/// <summary>
/// The order was illegal because the ordering nation did not
/// have a unit in the specified territory.
/// </summary>
/// <param name="unitOrder"><see cref="SourceUnitOrder"/></param>
public class IllegalMissingUnit(IUnitOrder unitOrder) : IOutcomeInfo
{
    /// <summary>
    /// The illegal order that was made.
    /// </summary>
    public IUnitOrder SourceUnitOrder { get; } = unitOrder;

    public string Message =>
        $"{SourceUnitOrder.OrderingNation} does not have a unit" +
        $"in {SourceUnitOrder.SourceTerritory}.";
}
