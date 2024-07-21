namespace DiploPad.Map;

/// <summary>
/// Travel information from one territory to another.
/// 
/// This is based solely on map geography, and has nothing to do with
/// occupying units, standoffs, or other game factors. Potential
/// convoy routes are also ignored.
/// </summary>
public enum TravelResult
{
    /// <summary>
    /// The destination territory can be reached.
    /// </summary>
    CanTravel,

    /// <summary>
    /// The destination territory has multiple valid coasts.
    /// 
    /// The coast must be specified.
    /// </summary>
    CoastNeeded,

    /// <summary>
    /// The destination territory cannot be reached.
    /// </summary>
    CannotTravel,
}
