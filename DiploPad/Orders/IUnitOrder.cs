using DiploPad.Geography;

namespace DiploPad.Orders;

/// <summary>
/// An instruction to do something with a unit.
/// </summary>
public interface IUnitOrder : IOrder
{
    /// <summary>
    /// The nation making the order. Nations can only legally order
    /// their own units.
    /// </summary>
    Nation OrderingNation { get; }

    /// <summary>
    /// The kind of unit being ordered.
    /// </summary>
    UnitKind SourceUnitKind { get; }

    /// <summary>
    /// The territory occupied by the unit being ordered.
    /// </summary>
    Territory SourceTerritory { get; }

    /// <summary>
    /// Whether this order shares a source unit with another order.
    /// </summary>
    /// <param name="other">The other unit order to check.</param>
    /// <returns></returns>
    sealed bool HasSameOrderedUnit(IUnitOrder other) =>
        OrderingNation == other.OrderingNation && SourceTerritory == other.SourceTerritory;
}
