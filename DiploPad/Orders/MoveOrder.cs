using DiploPad.Games;
using DiploPad.Geography;
using DiploPad.Orders.Outcomes;

namespace DiploPad.Orders;

/// <summary>
/// An order from one unit to an adjacent territory.
/// </summary>
/// <param name="orderingNation">The nation making the order.</param>
/// <param name="sourceUnitKind">The kind of unit being ordered.</param>
/// <param name="sourceTerritory">The territory the unit being ordered is in.</param>
/// <param name="destinationTerritory">The territory the unit is moving to.</param>
public class MoveOrder(
    Nation orderingNation,
    UnitKind sourceUnitKind,
    Territory sourceTerritory,
    Territory destinationTerritory,
    string? destinationCoast) : UnitOrder(orderingNation, sourceUnitKind, sourceTerritory)
{
    /// <summary>
    /// The territory the unit is moving to.
    /// </summary>
    public Territory DestinationTerritory { get; } = destinationTerritory;

    /// <summary>
    /// The coast on the territory the unit is moving to, if specified.
    /// </summary>
    public string? DestinationCoast { get; } = destinationCoast;

    /// <summary>
    /// The orders made to move to the same territory as this one.
    /// </summary>
    /// <param name="context">The context the order is being made in.</param>
    public IEnumerable<MoveOrder> ChallengingOrders(OrderContext context) =>
        OtherOrders(context)
        .OfType<MoveOrder>()
        .Where(order => DestinationTerritory == order.DestinationTerritory);

    /// <summary>
    /// The orders made to support this one, whether or not they succeed.
    /// </summary>
    /// <param name="context">The context the order is being made in.</param>
    public IEnumerable<SupportOrder> AttemptedSupportingOrders(OrderContext context) => throw new NotImplementedException();

    /// <summary>
    /// The orders successfully supporting this one.
    /// </summary>
    /// <param name="context">The context the order is being made in.</param>
    public IEnumerable<SupportOrder> SupportingOrders(OrderContext context) => throw new NotImplementedException();

    /// <summary>
    /// The power this unit is moving with. Counts 1 plus every successful support.
    /// </summary>
    /// <param name="context">The context the order is being made in.</param>
    public int Power(OrderContext context) => 1 + SupportingOrders(context).Count();

    /// <summary>
    /// Check whether the unit being ordered can reach the destination territory.
    /// </summary>
    /// <param name="context">The context the order is being made in.</param>
    /// <returns>An illegal order outcome if the order is illegal for travel reasons, otherwise null.</returns>
    public OrderOutcome? CheckCanTravel(OrderContext context)
    {
        if (context.GameState.UnitInTerritory(SourceTerritory) is not Unit unit
            || unit.Nation != OrderingNation
            || unit.Territory != SourceTerritory)
        {
            // We return null in these cases because, although the final outcome will be illegal,
            // it is not because there is a unit that is unable to travel.
            return null;
        }
        string? sourceCoast = unit.Coast;

        if (DestinationTerritory == SourceTerritory)
            throw new NotImplementedException();

        TravelResult travelResult = SourceTerritory.Geography.CanTravelTo(
            DestinationTerritory,
            SourceUnitKind,
            sourceCoast,
            DestinationCoast);
        if (travelResult is TravelResult.CanTravel)
            return null;
        else if (travelResult is TravelResult.CoastNeeded)
            throw new NotImplementedException();

        // The unit cannot travel on its own to the destination territory.
        // We need to check if a convoy is possible.
        throw new NotImplementedException();

        return null;
    }

    public override OrderOutcome GetOutcome(GameState gameState, OrderContext context) => throw new NotImplementedException();

    public override bool Equals(IOrder? other) =>
        other is MoveOrder moveOrder &&
        base.Equals(moveOrder) &&
        DestinationTerritory == moveOrder.DestinationTerritory;

    public override string ToString() => $"{base.ToString()} - {DestinationTerritory}";
}
