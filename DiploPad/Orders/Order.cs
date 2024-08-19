using DiploPad.Games;
using DiploPad.Orders.Outcomes;

namespace DiploPad.Orders;

public abstract class Order : IOrder
{
    public override abstract string ToString();

    public abstract OrderOutcome GetOutcome(GameState gameState, OrderContext context);

    public abstract bool Equals(IOrder? other);

    protected IEnumerable<IOrder> OtherOrders(OrderContext context) =>
        context.Orders.Where(order => !Equals(order, this));
}
