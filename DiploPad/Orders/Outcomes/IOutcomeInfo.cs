namespace DiploPad.Orders.Outcomes;

/// <summary>
/// Details about the outcome of order resolution.
/// </summary>
public interface IOutcomeInfo
{
    /// <summary>
    /// A text description of the order outcome.
    /// </summary>
    string Message { get; }
}
