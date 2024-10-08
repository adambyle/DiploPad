﻿namespace DiploPad.Geography;

internal class UnsetGeography : IGeography
{
    private static NotImplementedException Error => new(
        "Geographical info was not set during map construction.");

    public Terrain Terrain => throw Error;

    public TravelResult CanTravelTo(
        Territory destination,
        UnitKind unitKind,
        string? startCoast = null,
        string? destinationCoast = null) =>
            throw Error;

    public void VerifyConnections() => throw Error;

    public string? ParseCoast(string coastName) => throw Error;
}
