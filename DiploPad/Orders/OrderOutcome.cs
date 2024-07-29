namespace DiploPad.Orders;

/// <summary>
/// The outcome of an order on the turn.
/// </summary>
public class OrderOutcome
{
    /// <summary>
    /// The order that this is the outcome of.
    /// </summary>
    public IOrder Order { get; }

    /// <summary>
    /// The success status of the order.
    /// </summary>
    public OrderOutcomeStatus Status { get; }

    /// <summary>
    /// The action to apply to the 
    /// </summary>
    internal OrderTransform Transform { get; }

    internal OrderOutcome(
        IOrder order,
        OrderOutcomeStatus status,
        OrderTransform transform)
    {
        Order = order;
        Status = status;
        Transform = transform;
    }
}
