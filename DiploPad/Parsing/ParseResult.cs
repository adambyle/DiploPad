namespace DiploPad.Parsing;

/// <summary>
/// The result of a parsing operation.
/// </summary>
/// <typeparam name="T">The type of the parsing result.</typeparam>
public class ParseResult<T>
{
    /// <summary>
    /// If there was only one match, returns it. If there were zero
    /// or more than one, this is null.
    /// </summary>
    public T? OnlyMatch
    {
        get
        {
            bool onlyOneMatch = Matches.Count is 1;
            return onlyOneMatch ? Matches[0] : default;
        }
    }

    /// <summary>
    /// All the matches from the parsing expression.
    /// </summary>
    public IReadOnlyList<T> Matches { get; }

    internal ParseResult(IEnumerable<T> matches)
    {
        Matches = matches.ToArray();
    }

    /// <summary>
    /// Extract the result, expecting there to be only one.
    /// </summary>
    /// <param name="message">The error message if there is not exactly one.</param>
    /// <returns>The one result.</returns>
    /// <exception cref="NonOnlyParseException{T}">There is not exactly one result.</exception>
    public T Expect(string message)
    {
        if (OnlyMatch is null)
            throw new NonOnlyParseException<T>(message)
            {
                Matches = Matches,
            };

        return OnlyMatch;
    }
    
    /// <summary>
    /// Extract the result, expecting there to be only one.
    /// </summary>
    /// <returns>There is not exactly one result.</returns>
    public T Expect()
        => Expect($"Expected exactly one result from parse operation; got {Matches.Count}.");
}
