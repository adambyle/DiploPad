namespace DiploPad.Geography;

/// <summary>
/// Geographical information about a territory on the map.
/// </summary>
public interface IGeography
{
    /// <summary>
    /// The type of geography this territory has.
    /// </summary>
    Terrain Terrain { get; }

    /// <summary>
    /// Whether the specified unit kind can reach some place.
    /// 
    /// This is based solely on map geography, and has nothing to do with
    /// occupying units, standoffs, or other game factors. Potential
    /// convoy routes are also ignored.
    /// </summary>
    /// <param name="destination">The destination territory.</param>
    /// <param name="unitKind">The kind of unit traveling.</param>
    /// <param name="startCoast">The start coast (required for fleets).</param>
    /// <param name="destinationCoast">
    /// The destination coast (required for fleets for more than one possible choice).
    /// </param>
    /// <returns></returns>
    TravelResult CanTravelTo(
        Territory destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null);

    /// <summary>
    /// Verify whether the connections are to valid territory terrains.
    /// </summary>
    void VerifyConnections();

    /// <summary>
    /// Attempt to parse a coast name.
    /// </summary>
    /// <param name="coastName">The string to parse from.</param>
    /// <returns>The full name of the coast, or null if none was found.</returns>
    string? ParseCoast(string coastName);
}
