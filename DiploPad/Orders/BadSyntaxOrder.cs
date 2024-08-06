using DiploPad.Games;
using DiploPad.Orders.Outcomes;

namespace DiploPad.Orders;

/// <summary>
/// An order that wasn't able to be parsed into anything legal.
/// </summary>
public class BadSyntaxOrder(string originalText) : IOrder
{
    /// <summary>
    /// The text that wasn't able to be parsed into an order.
    /// </summary>
    public string OriginalText { get; } = originalText;

    public OrderOutcome GetOutcome(GameState gameState, IEnumerable<IOrder> otherOrders)
    {
        IOutcomeInfo info = new IllegalBadSyntax(this);
        return OrderOutcome.Illegal(this, info);
    }

    public bool Equals(IOrder? other) => false;

    public override string ToString() => OriginalText;
}
