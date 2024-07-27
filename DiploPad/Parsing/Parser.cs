using DiploPad.Map;

namespace DiploPad.Parsing;

/// <summary>
/// Collection of methods for parsing user input into Diplomacy objects.
/// </summary>
public static class Parser
{
    /// <summary>
    /// Parse a territory from user input;
    /// </summary>
    /// <param name="input">The user input string.</param>
    /// <param name="sources">The territories to check against.</param>
    /// <returns>The result of the parse.</returns>
    public static ParseResult<TerritoryInfo> ParseTerritory(
        string input,
        IEnumerable<TerritoryInfo> sources)
    {
        bool TerritoryMatches(TerritoryInfo source)
        {
            if (source.Name.StartsWith(input, StringComparison.CurrentCultureIgnoreCase))
                return true;

            bool anyAbbreviationMatches =
                source
                .Abbreviations
                .Any(abbreviation =>
                    abbreviation.Equals(input, StringComparison.CurrentCultureIgnoreCase));
            return anyAbbreviationMatches;
        }

        TerritoryInfo? primaryAbbreviationMatch = sources.FirstOrDefault(source =>
            source
            .PrimaryAbbreviation
            .Equals(input, StringComparison.CurrentCultureIgnoreCase));

        if (primaryAbbreviationMatch is not null)
            return new ParseResult<TerritoryInfo>([primaryAbbreviationMatch]);

        var matches = sources.Where(TerritoryMatches);
        return new ParseResult<TerritoryInfo>(matches);
    }
    /// <summary>
    /// Parse a nation from user input;
    /// </summary>
    /// <param name="input">The user input string.</param>
    /// <param name="sources">The nations to check against.</param>
    /// <returns>The result of the parse.</returns>
    public static ParseResult<NationInfo> ParseNation(
        string input,
        IEnumerable<NationInfo> sources)
    {
        bool NationMatches(NationInfo source)
        {
            bool anyNameMatches =
                source
                .Names
                .Any(name => name.Equals(input, StringComparison.CurrentCultureIgnoreCase));
            return anyNameMatches;
        }

        var matches = sources.Where(NationMatches);
        return new ParseResult<NationInfo>(matches);
    }
}
