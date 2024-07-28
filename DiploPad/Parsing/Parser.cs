using DiploPad.Geography;

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
    public static ParseResult<Territory> ParseTerritory(
        string input,
        IEnumerable<Territory> sources)
    {
        bool TerritoryMatches(Territory source)
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

        Territory? primaryAbbreviationMatch = sources.FirstOrDefault(source =>
            source
            .PrimaryAbbreviation
            .Equals(input, StringComparison.CurrentCultureIgnoreCase));

        if (primaryAbbreviationMatch is not null)
            return new ParseResult<Territory>([primaryAbbreviationMatch]);

        var matches = sources.Where(TerritoryMatches);
        return new ParseResult<Territory>(matches);
    }
    /// <summary>
    /// Parse a nation from user input;
    /// </summary>
    /// <param name="input">The user input string.</param>
    /// <param name="sources">The nations to check against.</param>
    /// <returns>The result of the parse.</returns>
    public static ParseResult<Nation> ParseNation(
        string input,
        IEnumerable<Nation> sources)
    {
        bool NationMatches(Nation source)
        {
            bool anyNameMatches =
                source
                .Names
                .Any(name => name.Equals(input, StringComparison.CurrentCultureIgnoreCase));
            return anyNameMatches;
        }

        var matches = sources.Where(NationMatches);
        return new ParseResult<Nation>(matches);
    }
}
