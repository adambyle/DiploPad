namespace DiploPad.Parsing;

/// <summary>
/// The parse operation expected exactly one result.
/// </summary>
/// <typeparam name="T">The expected result type.</typeparam>
public class NonOnlyParseException<T> : Exception
{
    /// <summary>
    /// The non-one number of results from the parse operation.
    /// </summary>
    public required IReadOnlyList<T> Matches { get; init; }

    public NonOnlyParseException() { }

    public NonOnlyParseException(string message) : base(message) { }

    public NonOnlyParseException(string message, Exception inner) : base(message, inner) { }
}
