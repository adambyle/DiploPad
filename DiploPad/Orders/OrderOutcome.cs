using DiploPad.Games;

namespace DiploPad.Orders;

public class OrderOutcome
{
    internal OrderTransform Transform { get; }

    public IOrder Order { get; }

    public OrderOutcomeStatus Status { get; }
}

public enum OrderOutcomeStatus
{
    Illegal,
    Failed,
    Succeeded,
}

internal delegate void OrderTransform(GameState gameState);

internal static class OrderTransformTemplates
{
    public static OrderTransform NoOp => (_) => { };
}
