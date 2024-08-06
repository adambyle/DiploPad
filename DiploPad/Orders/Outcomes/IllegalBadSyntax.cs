namespace DiploPad.Orders.Outcomes;

/// <summary>
/// The order was illegal because no meaning could be parsed from it.
/// </summary>
/// <param name="order"><see cref="SourceOrder"/></param>
public class IllegalBadSyntax(BadSyntaxOrder order) : IOutcomeInfo
{
    /// <summary>
    /// The illegal order that was made.
    /// </summary>
    public BadSyntaxOrder SourceOrder { get; } = order;

    public string Message => $"Order could not be parsed: {SourceOrder.OriginalText}.";
}
