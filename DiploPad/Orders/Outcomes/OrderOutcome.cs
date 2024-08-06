namespace DiploPad.Orders.Outcomes;

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
    /// Information about the outcome.
    /// </summary>
    public IOutcomeInfo Info { get; }

    /// <summary>
    /// The action to apply to the game state.
    /// </summary>
    internal OrderTransform Transform { get; }

    private OrderOutcome(
        IOrder order,
        OrderOutcomeStatus status,
        IOutcomeInfo info,
        OrderTransform transform)
    {
        Order = order;
        Status = status;
        Info = info;
        Transform = transform;
    }

    internal static OrderOutcome Illegal(IOrder order, IOutcomeInfo info) =>
        new(order, OrderOutcomeStatus.Illegal, info, OrderTransformTemplates.NoOp);

    internal static OrderOutcome Failed(IOrder order, IOutcomeInfo info) =>
        new(order, OrderOutcomeStatus.Failed, info, OrderTransformTemplates.NoOp);

    internal static OrderOutcome Succeeded(
        IOrder order,
        IOutcomeInfo info,
        OrderTransform orderTransform)
    {
        return new(order, OrderOutcomeStatus.Succeeded, info, orderTransform);
    }
}
