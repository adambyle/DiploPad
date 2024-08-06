using DiploPad.Games;
using DiploPad.Geography;
using DiploPad.Orders.Outcomes;

namespace DiploPad.Orders;

/// <summary>
/// Base class for orders given to a specific unit.
/// </summary>
public abstract class UnitOrder(
    Nation orderingNation,
    UnitKind sourceUnitKind,
    Territory sourceTerritory) : IUnitOrder
{
    public Nation OrderingNation { get; } = orderingNation;

    public UnitKind SourceUnitKind { get; } = sourceUnitKind;

    public Territory SourceTerritory { get; } = sourceTerritory;

    OrderOutcome IOrder.GetOutcome(GameState gameState, IEnumerable<IOrder> otherOrders)
    {
        var ordersWithSameUnit =
            otherOrders
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

        return GetOutcome(gameState, otherOrders);
    }

    /// <summary>
    /// Get the outcome of the order after it has been verified that
    /// only one order exists for this unit.
    /// </summary>
    public abstract OrderOutcome GetOutcome(GameState gameState, IEnumerable<IOrder> otherOrders);

    /// <summary>
    /// Whether this order shares a source unit with another order.
    /// </summary>
    /// <param name="other">The other unit order to check.</param>
    /// <returns></returns>
    public bool HasSameOrderedUnit(IUnitOrder other) =>
        OrderingNation == other.OrderingNation && SourceTerritory == other.SourceTerritory;

    public abstract bool Equals(IOrder? other);

    public override abstract string ToString();
}
