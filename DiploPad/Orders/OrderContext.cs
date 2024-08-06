using DiploPad.Games;
using DiploPad.Orders.Outcomes;

namespace DiploPad.Orders;

/// <summary>
/// A collection of orders applied to a certain state of the game.
/// </summary>
public class OrderContext
{
    /// <summary>
    /// The state of the game the orders are being applied to.
    /// </summary>
    public GameState GameState { get; }

    /// <summary>
    /// The orders being applied.
    /// </summary>
    public IReadOnlyList<IOrder> Orders { get; }

    /// <summary>
    /// The outcomes of the applied orders.
    /// </summary>
    public IReadOnlyList<OrderOutcome> Outcomes { get; }

    /// <summary>
    /// Create a new order context with its own set of outcomes.
    /// </summary>
    /// <param name="gameState">The state of the game to apply the orders to.</param>
    /// <param name="orders">The orders to apply.</param>
    public OrderContext(GameState gameState, IEnumerable<IOrder> orders)
    {
        GameState = gameState;
        Orders = orders.Distinct().ToArray();

        OrderOutcome OrderToOutcome(IOrder order)
        {
            var otherOrders = Orders.Where(otherOrder => !otherOrder.Equals(order));
            return order.GetOutcome(GameState, otherOrders);
        }

        Outcomes = Orders.Select(OrderToOutcome).ToArray();
    }

    /// <summary>
    /// Create a new order context with the same game state but a different set of orders.
    /// </summary>
    /// <param name="newOrders">The orders to add</param>
    /// <param name="removeOrders">The orders to remove.</param>
    /// <returns>A new order context with its own outcomes.</returns>
    public OrderContext WithOrders(IEnumerable<IOrder> newOrders, IEnumerable<IOrder> removeOrders)
    {
        var orders =
            Orders
            .Concat(newOrders)
            .Except(removeOrders);
        return new OrderContext(GameState, orders);
    }

    /// <summary>
    /// Create a new order context with the same game state but a different set of orders.
    /// </summary>
    /// <param name="newOrders">The orders to add</param>
    /// <returns>A new order context with its own outcomes.</returns>
    public OrderContext WithOrders(IEnumerable<IOrder> newOrders) =>
        WithOrders(newOrders, removeOrders: []);
}
