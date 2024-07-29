using DiploPad.Games;

namespace DiploPad.Orders;

public class OrderContext
{
    public GameState GameState { get; }

    public IReadOnlyList<IOrder> Orders { get; }

    public IReadOnlyList<OrderOutcome> Outcomes { get; }

    public OrderContext(GameState gameState, IEnumerable<IOrder> orders)
    {
        GameState = gameState;
        Orders = orders.ToArray();

        OrderOutcome OrderToOutcome(IOrder order)
        {
            var otherOrders = Orders.Where(otherOrder => otherOrder != order);
            return order.GetOutcome(GameState, otherOrders);
        }

        Outcomes = Orders.Select(OrderToOutcome).ToArray();
    }

    public OrderContext WithOrders(IEnumerable<IOrder> newOrders, IEnumerable<IOrder> removeOrders)
    {
        throw new NotImplementedException();
    }

    public OrderContext WithOrders(IEnumerable<IOrder> newOrders) =>
        WithOrders(newOrders, removeOrders: []);
}
