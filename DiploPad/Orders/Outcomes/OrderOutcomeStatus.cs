namespace DiploPad.Orders.Outcomes;

/// <summary>
/// The success status of the order.
/// </summary>
public enum OrderOutcomeStatus
{
    /// <summary>
    /// The order was improperly written or fundamentally against the game rules.
    /// </summary>
    Illegal,

    /// <summary>
    /// The order did not meet the right conditions to succeed.
    /// </summary>
    Failed,

    /// <summary>
    /// The order succeeded and had an effect on the turn outcome.
    /// </summary>
    Succeeded,
}
