using DiploPad.Parsing;

namespace DiploPad.Map;

/// <summary>
/// Type for building custom game maps.
/// </summary>
public class MapBuilder
{
    private readonly List<NationInfo> _nations = [];
    private readonly List<(TerritoryInfo, IUnresolvedGeography)> _territories = [];

    /// <summary>
    /// Add a nation to the map.
    /// </summary>
    /// <param name="names">The names used to identify the nation.</param>
    /// <returns>The MapBuilder object for method chaining.</returns>
    public MapBuilder AddNation(params string[] names)
    {
        _nations.Add(new NationInfo(names.ToArray()));
        return this;
    }

    /// <summary>
    /// Add a nation to the map.
    /// </summary>
    /// <param name="name">The nation's name.</param>
    /// <returns>The MapBuilder object for method chaining.</returns>
    public MapBuilder AddNation(string name) => AddNation([name]);

    /// <summary>
    /// Add an inland territory to the map (non home supply center).
    /// </summary>
    /// <param name="name">The name of the territory.</param>
    /// <param name="abbreviations">The abbreviations used to identify the territory.</param>
    /// <param name="isSupplyCenter">Whether this is a supply center (non-home).</param>
    /// <param name="landConnections">The land territories connected to this one.</param>
    /// <returns>The MapBuilder object for method chaining.</returns>
    public MapBuilder AddInlandTerritory(
        string name,
        IEnumerable<string> abbreviations,
        bool isSupplyCenter,
        IEnumerable<string> landConnections)
    {
        TerritoryInfo territoryInfo =
            isSupplyCenter ?
                new TerritoryInfo(name, abbreviations) :
                new SupplyCenterInfo(name, abbreviations);

        var geography = new UnresolvedInlandGeography(landConnections.ToArray());

        _territories.Add((territoryInfo, geography));

        return this;
    }

    /// <summary>
    /// Add a coastal territory to the map (non home supply center).
    /// </summary>
    /// <param name="name">The name of the territory.</param>
    /// <param name="abbreviations">The abbreviations used to identify the territory.</param>
    /// <param name="coasts">The names of the coasts ("main" if just one).</param>
    /// <param name="isSupplyCenter">Whether this is a supply center (non-home).</param>
    /// <param name="landConnections">The inland and coastal territories connected to this one.</param>
    /// <param name="coastalConnections">The coastal territories connected to this one.</param>
    /// <param name="seaConnections">The sea territories connected to this one.</param>
    /// <returns>The MapBuilder object for method chaining.</returns>
    public MapBuilder AddCoastalTerritory(
        string name,
        IEnumerable<string> abbreviations,
        IEnumerable<string> coasts,
        bool isSupplyCenter,
        IEnumerable<string> landConnections,
        IEnumerable<(
            string startCoast,
            string destination,
            string destinationCoast)> coastalConnections,
        IEnumerable<(string startCoast, string destination)> seaConnections)
    {
        TerritoryInfo territoryInfo =
            isSupplyCenter ?
                new TerritoryInfo(name, abbreviations) :
                new SupplyCenterInfo(name, abbreviations);

        var geography = new UnresolvedCoastalGeography(
            coasts.ToArray(),
            landConnections.ToArray(),
            coastalConnections.ToArray(),
            seaConnections.ToArray());

        _territories.Add((territoryInfo, geography));

        return this;
    }

    /// <summary>
    /// Add a sea territory to the map.
    /// </summary>
    /// <param name="name">The name of the territory.</param>
    /// <param name="abbreviations">The abbreviations used to identify the territory.</param>
    /// <param name="landConnections">The coastal territories connected to this one.</param>
    /// <param name="seaConnections">The sea territories connected to this one.</param>
    /// <returns>The MapBuilder object for method chaining.</returns>
    public MapBuilder AddSeaTerritory(
        string name,
        IEnumerable<string> abbreviations,
        IEnumerable<(string destination, string destinationCoast)> landConnections,
        IEnumerable<string> seaConnections)
    {
        TerritoryInfo territoryInfo = new(name, abbreviations);

        var geography = new UnresolvedSeaGeography(
            landConnections.ToArray(),
            seaConnections.ToArray());

        _territories.Add((territoryInfo, geography));

        return this;
    }

    /// <summary>
    /// Add an inland territory to the map as a home supply center.
    /// </summary>
    /// <param name="name">The name of the territory.</param>
    /// <param name="abbreviations">The abbreviations used to identify the territory.</param>
    /// <param name="landConnections">The land territories connected to this one.</param>
    /// <param name="nation">The nation this center belongs to.</param>
    /// <returns>The MapBuilder object for method chaining.</returns>
    public MapBuilder AddInlandHomeSupplyCenter(
        string name,
        IEnumerable<string> abbreviations,
        IEnumerable<string> landConnections,
        string nation)
    {
        NationInfo nationInfo =
            Parser
            .ParseNation(nation, _nations)
            .Expect($"Unknown nation {nation}.");

        TerritoryInfo territoryInfo = new HomeSupplyCenterInfo(
            name,
            abbreviations,
            nationInfo,
            UnitKind.Army);

        var geography = new UnresolvedInlandGeography(landConnections.ToArray());

        _territories.Add((territoryInfo, geography));

        return this;
    }

    /// <summary>
    /// Add a coastal territory to the map as a home supply center.
    /// </summary>
    /// <param name="name">The name of the territory.</param>
    /// <param name="abbreviations">The abbreviations used to identify the territory.</param>
    /// <param name="coasts">The names of the coasts ("main" if just one).</param>
    /// <param name="landConnections">The inland and coastal territories connected to this one.</param>
    /// <param name="coastalConnections">The coastal territories connected to this one.</param>
    /// <param name="seaConnections">The sea territories connected to this one.</param>
    /// <param name="nation">The nation this center belongs to.</param>
    /// <param name="startUnitCoast">The coast the unit starts on.</param>
    /// <returns></returns>
    public MapBuilder AddCoastalHomeSupplyCenter(
        string name,
        IEnumerable<string> abbreviations,
        IEnumerable<string> coasts,
        IEnumerable<string> landConnections,
        IEnumerable<(
            string startCoast,
            string destination,
            string destinationCoast)> coastalConnections,
        IEnumerable<(string startCoast, string destination)> seaConnections,
        string nation,
        string? startUnitCoast = null)
    {
        NationInfo nationInfo =
            Parser
            .ParseNation(nation, _nations)
            .Expect($"Unknown nation {nation}.");

        UnitKind startUnitKind = startUnitCoast is null ? UnitKind.Army : UnitKind.Fleet;
        TerritoryInfo territoryInfo = new HomeSupplyCenterInfo(
            name,
            abbreviations,
            nationInfo,
            startUnitKind,
            startUnitCoast);

        var geography = new UnresolvedCoastalGeography(
            coasts.ToArray(),
            landConnections.ToArray(),
            coastalConnections.ToArray(),
            seaConnections.ToArray());

        _territories.Add((territoryInfo, geography));

        return this;
    }

    /// <summary>
    /// Create the map.
    /// </summary>
    /// <returns>The complete map.</returns>
    /// <exception cref="MapBuilderException">There was invalid input building the map.</exception>
    public Map Build()
    {
        string? duplicateNationName =
            _nations
            .SelectMany(nation => nation.Names)
            .GroupBy(name => name.ToLower())
            .FirstOrDefault(group => group.Count() > 1)
            ?.FirstOrDefault();
        if (duplicateNationName is not null)
            throw new MapBuilderException(
                $"Nation name \"${duplicateNationName}\" is used more than once.")
            {
                DuplicateNameOrAbbr = duplicateNationName,
            };

        string? duplicateTerritoryName =
            _territories
            .Select(territory => territory.Item1.Name)
            .GroupBy(name => name.ToLower())
            .FirstOrDefault(group => group.Count() > 1)
            ?.FirstOrDefault();
        if (duplicateTerritoryName is not null)
            throw new MapBuilderException(
                $"Territory name \"${duplicateTerritoryName}\" is used more than once.")
            {
                DuplicateNameOrAbbr = duplicateTerritoryName,
            };

        string? duplicateTerritoryAbbr =
            _territories
            .Select(territory => territory.Item1.PrimaryAbbreviation)
            .GroupBy(abbr => abbr.ToLower())
            .FirstOrDefault(group => group.Count() > 1)
            ?.FirstOrDefault();
        if (duplicateTerritoryAbbr is not null)
            throw new MapBuilderException(
                $"Territory abbreviation \"${duplicateTerritoryAbbr}\" is used more than once.")
            {
                DuplicateNameOrAbbr = duplicateTerritoryAbbr,
            };

        var territoryInfos = _territories.Select(territory => territory.Item1);

        // At this point, connection information is just strings.
        // "Resolving" the geographies means finding the territory objects
        // associated with each string. They are not yet tested for validity.
        foreach ((var territory, var unresolvedGeography) in _territories)
            territory.Geography = unresolvedGeography.ResolveGeography(territoryInfos);

        foreach (var territory in territoryInfos)
        {
            var geography = territory.Geography;

            if (territory is HomeSupplyCenterInfo homeSupplyCenter)
            {
                // It is guaranteed during construction that if a fleet starts here,
                // the territory is coastal and the startUnitCoast value is present.

                if (homeSupplyCenter.StartUnitKind is not UnitKind.Fleet)
                    continue;

                string startUnitCoast = homeSupplyCenter.StartUnitCoast!;
                var coasts = (homeSupplyCenter.Geography as CoastalGeography)!.Coasts;
                if (!coasts.Contains(startUnitCoast))
                    throw new MapBuilderException(
                        $"Coast {startUnitCoast} does not exist on" +
                            $" home center {homeSupplyCenter.Name}.")
                    {
                        BadCoast = (homeSupplyCenter, startUnitCoast),
                    };
            }

            // Coastal and sea territories can border territories which are coastal.
            // So far, we have not verified that the destination startUnitCoast for each connection
            // exists on the destination territory.
            if (geography is CoastalGeography coastalGeography)
            {
                foreach (var (startCoast, destination, destinationCoast)
                    in coastalGeography.CoastalConnections)
                {
                    // Coastal landConnections will always have coastal geography.
                    var destinationGeography =
                        destination.Geography as CoastalGeography;
                    bool coastExists = destinationGeography!.Coasts.Contains(destinationCoast);
                    if (!coastExists)
                        throw new MapBuilderException(
                            $"Coast {destinationCoast} does not exist on" +
                                $" destination {destination.Name}.")
                        {
                            BadCoast = (destination, destinationCoast),
                        };
                }
            }
            else if (geography is SeaGeography seaGeography)
            {
                foreach (var (destination, destinationCoast)
                    in seaGeography.LandConnections)
                {
                    // Land landConnections will always have coastal geography.
                    var destinationGeography =
                        destination.Geography as CoastalGeography;
                    bool coastExists = destinationGeography!.Coasts.Contains(destinationCoast);
                    if (!coastExists)
                        throw new MapBuilderException(
                            $"Coast {destinationCoast} does not exist on" +
                                $" destination {destination.Name}")
                        {
                            BadCoast = (destination, destinationCoast)
                        };
                }
            }
        }

        return new Map(_nations, territoryInfos);
    }

    private interface IUnresolvedGeography
    {
        IGeography ResolveGeography(IEnumerable<TerritoryInfo> territories);

        protected static sealed Func<string, TerritoryInfo> GetResolver(
            IEnumerable<TerritoryInfo> territories)
        {
            return (string territory) =>
                Parser
                .ParseTerritory(territory, territories)
                .Expect($"Unrecognized connection: {territory}.");
        }
    }

    private record UnresolvedInlandGeography(string[] LandConnections) : IUnresolvedGeography
    {
        public IGeography ResolveGeography(IEnumerable<TerritoryInfo> territories)
        {
            var resolver = IUnresolvedGeography.GetResolver(territories);

            var landConnections =
                LandConnections
                .Select(resolver);

            return new InlandGeography(landConnections);
        }
    }

    private record UnresolvedCoastalGeography(
        string[] Coasts,
        string[] LandConnections,
        (string startCoast, string territory, string destinationCoast)[] CoastalConnections,
        (string startCoast, string territory)[] SeaConnections) : IUnresolvedGeography
    {
        public IGeography ResolveGeography(IEnumerable<TerritoryInfo> territories)
        {
            var resolver = IUnresolvedGeography.GetResolver(territories);

            var landConnections =
                LandConnections
                .Select(resolver);
            var coastalConnections =
                CoastalConnections
                .Select(connection => (
                    connection.startCoast,
                    resolver(connection.territory),
                    connection.destinationCoast));
            var seaConnections =
                SeaConnections
                .Select(connection => (
                    connection.startCoast,
                    resolver(connection.territory)));

            return new CoastalGeography(
                Coasts,
                landConnections,
                coastalConnections,
                seaConnections);
        }
    }

    private record UnresolvedSeaGeography(
        (string territory, string destinationCoast)[] LandConnections,
        string[] SeaConnections) : IUnresolvedGeography
    {
        public IGeography ResolveGeography(IEnumerable<TerritoryInfo> territories)
        {
            var resolver = IUnresolvedGeography.GetResolver(territories);

            var landConnections =
                LandConnections
                .Select(connection => (
                    resolver(connection.territory),
                    connection.destinationCoast));
            var seaConnections =
                SeaConnections
                .Select(resolver);

            return new SeaGeography(
                landConnections,
                seaConnections);
        }
    }
}
