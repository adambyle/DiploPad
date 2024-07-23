namespace DiploPad.Map;

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
