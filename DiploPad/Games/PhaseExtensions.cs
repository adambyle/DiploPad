namespace DiploPad.Games;

/// <summary>
/// Extension methods for the <see cref="Phase"/> enum.
/// </summary>
public static class PhaseExtensions
{
    /// <summary>
    /// Whether this is a standard orders phase.
    /// </summary>
    public static bool IsOrderPhase(this Phase phase) =>
        phase is Phase.SpringOrders or Phase.FallOrders;

    /// <summary>
    /// Whether this is a retreats and disbands phase.
    /// </summary>
    public static bool IsRetreatPhase(this Phase phase) =>
        phase is Phase.SpringRetreats or Phase.FallRetreats;
}
