using DiploPad.Games;

namespace DiploPad.Orders;

/// <summary>
/// An order that wasn't able to be parsed into anything legal.
/// </summary>
public class BadSyntaxOrder : IOrder
{
    bool IEquatable<IOrder>.Equals(IOrder? other) => false;

    OrderOutcome IOrder.GetOutcome(GameState gameState, IEnumerable<IOrder> otherOrders) =>
        new(
            order: this,
            OrderOutcomeStatus.Illegal,
            OrderTransformTemplates.NoOp);
}
