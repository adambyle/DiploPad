using DiploPad.Games;
using DiploPad.Geography;
using DiploPad.Orders.Outcomes;

namespace DiploPad.Orders;

/// <summary>
/// Base class for orders given to a specific unit.
/// </summary>
/// <param name="orderingNation">The nation making the order.</param>
/// <param name="sourceUnitKind">The kind of unit being ordered.</param>
/// <param name="sourceTerritory">The territory the unit being ordered is in.</param>
public abstract class UnitOrder(
    Nation orderingNation,
    UnitKind sourceUnitKind,
    Territory sourceTerritory) : Order, IUnitOrder
{
    /// <summary>
    /// The nation making the order.
    /// </summary>
    public Nation OrderingNation { get; } = orderingNation;

    /// <summary>
    /// The kind of unit being ordered.
    /// </summary>
    public UnitKind SourceUnitKind { get; } = sourceUnitKind;

    /// <summary>
    /// The territory the unit being ordered is in.
    /// </summary>
    public Territory SourceTerritory { get; } = sourceTerritory;

    OrderOutcome IOrder.GetOutcome(GameState gameState, OrderContext context)
    {
        var ordersWithSameUnit =
            OtherOrders(context)
            .OfType<IUnitOrder>()
            .Where(HasSameOrderedUnit);
        if (ordersWithSameUnit.Any())
        {
            IOutcomeInfo info = new IllegalDuplicateUnit(this, ordersWithSameUnit);
            return OrderOutcome.Illegal(this, info);
        }

        bool orderingNationOwnsUnit =
            gameState.UnitInTerritory(SourceTerritory) is { Nation: Nation unitNation } &&
            unitNation == OrderingNation;
        if (!orderingNationOwnsUnit)
        {
            IOutcomeInfo info = new IllegalMissingUnit(this);
            return OrderOutcome.Illegal(this, info);
        }

        return GetOutcome(gameState, context);
    }

    /// <summary>
    /// Get the outcome of the order after it has been verified that
    /// only one order exists for this unit.
    /// </summary>
    public abstract override OrderOutcome GetOutcome(GameState gameState, OrderContext context);

    /// <summary>
    /// Whether this order shares a source unit with another order.
    /// </summary>
    /// <param name="other">The other unit order to check.</param>
    /// <returns></returns>
    public bool HasSameOrderedUnit(IUnitOrder other) =>
        OrderingNation == other.OrderingNation && SourceTerritory == other.SourceTerritory;

    public override bool Equals(IOrder? other) =>
        other is UnitOrder unitOrder &&
        OrderingNation == unitOrder.OrderingNation &&
        SourceTerritory == unitOrder.SourceTerritory;

    public override string ToString() => $"{SourceUnitKind.Abbreviation()} {SourceTerritory}";
}
