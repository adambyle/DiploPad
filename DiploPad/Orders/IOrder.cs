using DiploPad.Games;
using DiploPad.Orders.Outcomes;

namespace DiploPad.Orders;

/// <summary>
/// An instruction for how to affect the game this turn.
/// </summary>
public interface IOrder : IEquatable<IOrder>
{
    /// <summary>
    /// Get a string representation of the order.
    /// </summary>
    string ToString();

    /// <summary>
    /// Get the outcome of this order in the context of the game
    /// and other orders.
    /// </summary>
    /// <param name="gameState">The game to apply the order to.</param>
    /// <param name="otherOrders">The other orders that may affect this one.</param>
    OrderOutcome GetOutcome(GameState gameState, IEnumerable<IOrder> otherOrders);
}
