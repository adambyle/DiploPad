using DiploPad.Games;

namespace DiploPad.Orders;

public interface IOrder : IEquatable<IOrder>
{
    OrderOutcome GetOutcome(GameState gameState, IEnumerable<IOrder> otherOrders);
}
