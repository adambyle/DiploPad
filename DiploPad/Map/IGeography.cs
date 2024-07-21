namespace DiploPad.Map;

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
        TerritoryInfo destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null);
}

/// <summary>
/// The type of geography a territory has.
/// </summary>
public enum Terrain
{
    /// <summary>
    /// Landlocked territory that only armies may occupy;
    /// borders only inland and coastal territories.
    /// </summary>
    Inland,

    /// <summary>
    /// Land territory that may border sea territories.
    /// </summary>
    Coastal,

    /// <summary>
    /// Sea territory that only fleets may occupy;
    /// borders only coastal and sea territories.
    /// </summary>
    Sea,
}
