using DiploPad.Geography;

namespace DiploPad.Games;

/// <summary>
/// A unit on the game board.
/// </summary>
public class Unit
{
    /// <summary>
    /// The territory the unit is in.
    /// </summary>
    public Territory Territory { get; }

    /// <summary>
    /// The coast the unit is on (only valid for fleets).
    /// </summary>
    public string? Coast { get; }

    /// <summary>
    /// The kind of unit on the board.
    /// </summary>
    public UnitKind UnitKind { get; }

    /// <summary>
    /// The nation the unit belongs to.
    /// </summary>
    public Nation Nation { get; }

    internal Unit(
        Territory territory,
        string? coast,
        UnitKind unitKind,
        Nation nation)
    {
        Territory = territory;
        Coast = coast;
        UnitKind = unitKind;
        Nation = nation;
    }

    /// <summary>
    /// Create an army.
    /// </summary>
    /// <param name="territory">The inland or coastal territory the army occupies.</param>
    /// <param name="nation">The nation the army belongs to.</param>
    /// <exception cref="UnitException">
    /// The specified territory is a sea territory.
    /// </exception>
    public static Unit Army(Territory territory, Nation nation)
    {
        if (territory.Geography is SeaGeography)
            throw new UnitException(
                $"Army cannot occupy sea territory {territory.Name}.");

        return new Unit(
            territory,
            coast: null,
            UnitKind.Army,
            nation);
    }

    /// <summary>
    /// Create a fleet.
    /// </summary>
    /// <param name="territory">The sea or coastal territory the fleet occupies.</param>
    /// <param name="coast">The coast the fleet is on, if in a coastal territory.</param>
    /// <param name="nation">The nation the fleet belongs to.</param>
    /// <exception cref="UnitException">
    /// The fleet does not occupy a valid territory, or there was an error with the coast.
    /// </exception>
    public static Unit Fleet(Territory territory, string? coast, Nation nation)
    {
        string ValidateCoast(CoastalGeography coastalGeography)
        {
            if (coast is null)
                throw new UnitException(
                    $"Coast must be specified for coastal territory {territory.Name}.");

            string? parsedCoast = coastalGeography.ParseCoast(coast);
            return parsedCoast ??
                throw new UnitException(
                    $"Invalid coast {coast} on coastal territory {territory.Name}.");
        }

        string? validatedCoast = territory.Geography switch
        {
            InlandGeography => throw new UnitException(
                $"Fleet cannot occupy inland territory {territory.Name}."),

            CoastalGeography coastalGeography => ValidateCoast(coastalGeography),

            _ => null,
        };

        return new Unit(
            territory,
            validatedCoast,
            UnitKind.Fleet,
            nation);
    }
}

/// <summary>
/// An error the occured creating a unit object.
/// </summary>
public class UnitException : Exception
{
    public UnitException() { }

    public UnitException(string message) : base(message) { }

    public UnitException(string message, Exception inner) : base(message, inner) { }
}
