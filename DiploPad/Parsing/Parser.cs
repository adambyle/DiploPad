using DiploPad.Map;

namespace DiploPad.Parsing;

/// <summary>
/// Collection of methods for parsing user input into Diplomacy objects.
/// </summary>
public static class Parser
{
    /// <summary>
    /// Parse a territory-like object from user input;
    /// </summary>
    /// <typeparam name="T">The territory-like type.</typeparam>
    /// <param name="input">The user input string.</param>
    /// <param name="sources">The territories to check against.</param>
    /// <returns>The result of the parse.</returns>
    public static ParseResult<T> ParseTerritory<T>(
        string input,
        IEnumerable<T> sources)
        where T : IParsable<TerritoryInfo>
    {
        bool TerritoryMatches(T source)
        {
            TerritoryInfo territoryInfo = source.Target;

            if (territoryInfo.Name.StartsWith(input, StringComparison.CurrentCultureIgnoreCase))
                return true;

            bool anyAbbreviationMatches =
                territoryInfo
                .Abbreviations
                .Any(abbreviation =>
                    abbreviation.Equals(input, StringComparison.CurrentCultureIgnoreCase));
            return anyAbbreviationMatches;
        }

        var matches = sources.Where(TerritoryMatches);
        return new ParseResult<T>(matches);
    }
    /// <summary>
    /// Parse a nation-like object from user input;
    /// </summary>
    /// <typeparam name="T">The nation-like type.</typeparam>
    /// <param name="input">The user input string.</param>
    /// <param name="sources">The nations to check against.</param>
    /// <returns>The result of the parse.</returns>
    public static ParseResult<T> ParseNation<T>(
        string input,
        IEnumerable<T> sources)
        where T : IParsable<NationInfo>
    {
        bool NationMatches(T source)
        {
            NationInfo nationInfo = source.Target;

            bool anyNameMatches =
                nationInfo
                .Names
                .Any(name => name.Equals(input, StringComparison.CurrentCultureIgnoreCase));
            return anyNameMatches;
        }

        var matches = sources.Where(NationMatches);
        return new ParseResult<T>(matches);
    }
}
