using DiploPad.Games;
using DiploPad.Orders.Outcomes;

namespace DiploPad.Orders;

/// <summary>
/// An order that wasn't able to be parsed into anything legal.
/// </summary>
public class BadSyntaxOrder(string originalText) : Order
{
    /// <summary>
    /// The text that wasn't able to be parsed into an order.
    /// </summary>
    public string OriginalText { get; } = originalText;

    public override OrderOutcome GetOutcome(GameState gameState, OrderContext context)
    {
        IOutcomeInfo info = new IllegalBadSyntax(this);
        return OrderOutcome.Illegal(this, info);
    }

    public override bool Equals(IOrder? other) => false;

    public override string ToString() => OriginalText;
}
