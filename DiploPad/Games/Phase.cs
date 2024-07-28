namespace DiploPad.Games;

/// <summary>
/// A phase of the game.
/// </summary>
public enum Phase
{
    /// <summary>
    /// The _year's first round of regular orders.
    /// </summary>
    SpringOrders,

    /// <summary>
    /// Retreat orders following spring regular orders.
    /// </summary>
    SpringRetreats,

    /// <summary>
    /// The _year's second round of regular orders.
    /// </summary>
    FallOrders,

    /// <summary>
    /// Retreat orders following fall regular orders.
    /// </summary>
    FallRetreats,

    /// <summary>
    /// Builds at the end of the _year (also known as winter).
    /// </summary>
    WinterBuilds,
}
