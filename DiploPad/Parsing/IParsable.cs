namespace DiploPad.Parsing;

/// <summary>
/// Can be parsed from the specified type.
/// </summary>
public interface IParsable<T>
{
    /// <summary>
    /// The target of the parse operation.
    /// </summary>
    T Target { get; }
}
