namespace DiploPad.Geography;

/// <summary>
/// Information about a game map, including its territories and the connections between them,
/// and the map's nations. It also carries supply center and starting unit positions.
/// </summary>
public class Map
{
    /// <summary>
    /// The map's nations.
    /// </summary>
    public IReadOnlyList<Nation> Nations { get; set; }

    /// <summary>
    /// The map's territories.
    /// </summary>
    public IReadOnlyList<Territory> Territories;

    /// <summary>
    /// The map's home supply centers.
    /// </summary>
    public IEnumerable<HomeSupplyCenter> HomeSupplyCenters =>
        Territories.OfType<HomeSupplyCenter>();

    /// <summary>
    /// The map's supply centers.
    /// </summary>
    public IEnumerable<SupplyCenter> SupplyCenters =>
        Territories.OfType<SupplyCenter>();

    internal Map(IEnumerable<Nation> nations, IEnumerable<Territory> territories)
    {
        Nations = nations.ToArray();
        Territories = territories.ToArray();
    }

    public static Map Default()
    {
        MapBuilder builder = new();

        builder
            .AddNation("Austria", "Austria-Hungary", "Hungary")
            .AddNation("England", "Britain")
            .AddNation("France")
            .AddNation("Germany")
            .AddNation("Italy")
            .AddNation("Russia")
            .AddNation("Turkey", "Ottomans", "Ottoman Empire");

        builder.AddSeaTerritory(
            "Adriatic Sea",
            ["ADR"],
            landConnections: [
                ("Alb", "main"),
                ("Apu", "main"),
                ("Tri", "main"),
                ("Ven", "main")],
            seaConnections: ["ION"]);

        builder.AddSeaTerritory(
            "Aegean Sea",
            ["AEG"],
            landConnections: [
                ("Bul", "south"),
                ("Con", "main"),
                ("Gre", "main"),
                ("Smy", "main")],
            seaConnections: ["EAS", "ION"]);

        builder.AddCoastalTerritory(
            "Albania",
            ["Alb"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Gre", "Ser", "Tri"],
            coastalConnections: [
                ("main", "Gre", "main"),
                ("main", "Tri", "main")],
            seaConnections: [
                ("main", "ADR"),
                ("main", "ION")]);

        builder.AddCoastalHomeSupplyCenter(
            "Ankara",
            ["Ank"],
            coasts: ["main"],
            landConnections: ["Arm", "Con", "Smy"],
            coastalConnections: [
                ("main", "Arm", "main"),
                ("main", "Con", "main")],
            seaConnections: [("main", "BLA")],
            nationName: "Turkey",
            startUnitCoast: "main");

        builder.AddCoastalTerritory(
            "Apulia",
            ["Apu"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Nap", "Rom", "Ven"],
            coastalConnections: [
                ("main", "Nap", "main"),
                ("main", "Ven", "main")],
            seaConnections: [
                ("main", "ADR"),
                ("main", "ION")]);

        builder.AddCoastalTerritory(
            "Armenia",
            ["Arm"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Ank", "Sev", "Smy", "Syr"],
            coastalConnections: [
                ("main", "Ank", "main"),
                ("main", "Sev", "main")],
            seaConnections: [("main", "BLA")]);

        builder.AddSeaTerritory(
            "Baltic Sea",
            ["BAL"],
            landConnections: [
                ("Ber", "main"),
                ("Den", "main"),
                ("Kie", "main"),
                ("Lvn", "main"),
                ("Pru", "main"),
                ("Swe", "main")],
            seaConnections: ["BOT"]);

        builder.AddSeaTerritory(
            "Barents Sea",
            ["BAR"],
            landConnections: [
                ("Nwy", "main"),
                ("StP", "north")],
            seaConnections: ["NWG"]);

        builder.AddCoastalTerritory(
            "Belgium",
            ["Bel"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["Bur", "Hol", "Pic", "Ruh"],
            coastalConnections: [
                ("main", "Hol", "main"),
                ("main", "Pic", "main")],
            seaConnections: [
                ("main", "ENG"),
                ("main", "NTH")]);

        builder.AddCoastalHomeSupplyCenter(
            "Berlin",
            ["Ber"],
            coasts: ["main"],
            landConnections: ["Kie", "Mun", "Pru", "Sil"],
            coastalConnections: [
                ("main", "Kie", "main"),
                ("main", "Pru", "main")],
            seaConnections: [("main", "BAL")],
            nationName: "Germany");

        builder.AddSeaTerritory(
            "Black Sea",
            ["BLA"],
            landConnections: [
                ("Ank", "main"),
                ("Arm", "main"),
                ("Bul", "east"),
                ("Con", "main"),
                ("Rum", "main"),
                ("Sev", "main")],
            seaConnections: []);

        builder.AddInlandTerritory(
            "Bohemia",
            ["Boh"],
            isSupplyCenter: false,
            landConnections: ["Mun", "Gal", "Sil", "Tyr", "Vie"]);

        builder.AddCoastalHomeSupplyCenter(
            "Brest",
            ["Bre"],
            coasts: ["main"],
            landConnections: ["Gas", "Par", "Pic"],
            coastalConnections: [
                ("main", "Gas", "main"),
                ("main", "Pic", "main")],
            seaConnections: [
                ("main", "ENG"),
                ("main", "MAO")],
            nationName: "France",
            startUnitCoast: "main");

        builder.AddInlandHomeSupplyCenter(
            "Budapest",
            ["Bud"],
            landConnections: ["Gal", "Rum", "Ser", "Tri", "Vie"],
            nationName: "Austria");

        builder.AddCoastalTerritory(
            "Bulgaria",
            ["Bul"],
            coasts: ["east", "south"],
            isSupplyCenter: true,
            landConnections: ["Con", "Gre", "Rum", "Ser"],
            coastalConnections: [
                ("east", "Con", "main"),
                ("south", "Con", "main"),
                ("south", "Gre", "main"),
                ("east", "Rum", "main")],
            seaConnections: [
                ("south", "AEG"),
                ("east", "BLA")]);

        builder.AddInlandTerritory(
            "Burgundy",
            ["Bur"],
            isSupplyCenter: false,
            landConnections: ["Bel", "Gas", "Mar", "Mun", "Par", "Pic", "Ruh"]);

        builder.AddCoastalTerritory(
            "Clyde",
            ["Cly"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Edi", "Lvp"],
            coastalConnections: [
                ("main", "Edi", "main"),
                ("main", "Lvp", "main")],
            seaConnections: [
                ("main", "NAO"),
                ("main", "NWG")]);

        builder.AddCoastalHomeSupplyCenter(
            "Constantinople",
            ["Con"],
            coasts: ["main"],
            landConnections: ["Ank", "Bul", "Smy"],
            coastalConnections: [
                ("main", "Ank", "main"),
                ("main", "Bul", "east"),
                ("main", "Bul", "south"),
                ("main", "Smy", "main")],
            seaConnections: [
                ("main", "AEG"),
                ("main", "BLA")],
            nationName: "Turkey");

        builder.AddCoastalTerritory(
            "Denmark",
            ["Den"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["Kie", "Swe"],
            coastalConnections: [
                ("main", "Kie", "main"),
                ("main", "Swe", "main")],
            seaConnections: [
                ("main", "BAL"),
                ("main", "HEL"),
                ("main", "NTH"),
                ("main", "SKA")]);

        builder.AddSeaTerritory(
            "Eastern Mediterranean",
            ["EAS"],
            landConnections: [
                ("Smy", "main"),
                ("Syr", "main")],
            seaConnections: ["AEG", "ION"]);

        builder.AddCoastalHomeSupplyCenter(
            "Edinburgh",
            ["Edi"],
            coasts: ["main"],
            landConnections: ["Cly", "Lvp", "Yor"],
            coastalConnections: [
                ("main", "Cly", "main"),
                ("main", "Yor", "main")],
            seaConnections: [
                ("main", "NTH"),
                ("main", "NWG")],
            nationName: "England",
            startUnitCoast: "main");

        builder.AddSeaTerritory(
            "English Channel",
            ["ENG"],
            landConnections: [
                ("Bel", "main"),
                ("Bre", "main"),
                ("Lon", "main"),
                ("Pic", "main"),
                ("Wal", "main")],
            seaConnections: ["IRI", "MAO", "NTH"]);

        builder.AddCoastalTerritory(
            "Finland",
            ["Fin"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Nwy", "Swe", "StP"],
            coastalConnections: [
                ("main", "Swe", "main"),
                ("main", "StP", "south")],
            seaConnections: [("main", "BOT")]);

        builder.AddInlandTerritory(
            "Galicia",
            ["Gal"],
            isSupplyCenter: false,
            landConnections: ["Boh", "Bud", "Rum", "Sil", "Ukr", "Vie", "War"]);

        builder.AddCoastalTerritory(
            "Gascony",
            ["Gas"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Bre", "Bur", "Mar", "Par", "Spa"],
            coastalConnections: [
                ("main", "Bre", "main"),
                ("main", "Spa", "north")],
            seaConnections: [("main", "MAO")]);

        builder.AddCoastalTerritory(
            "Greece",
            ["Gre"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["Alb", "Bul", "Ser"],
            coastalConnections: [
                ("main", "Alb", "main"),
                ("main", "Bul", "south")],
            seaConnections: [
                ("main", "AEG"),
                ("main", "ION")]);

        builder.AddSeaTerritory(
            "Gulf of Bothnia",
            ["BOT"],
            landConnections: [
                ("Fin", "main"),
                ("StP", "south"),
                ("Swe", "main")],
            seaConnections: ["BAL"]);

        builder.AddSeaTerritory(
            "Gulf of Lyon",
            ["LYO"],
            landConnections: [
                ("Mar", "main"),
                ("Pie", "main"),
                ("Spa", "south"),
                ("Tus", "main")],
            seaConnections: ["TYS", "WES"]);

        builder.AddSeaTerritory(
            "Helgoland Bight",
            ["HEL"],
            landConnections: [
                ("Den", "main"),
                ("Kie", "main"),
                ("Hol", "main")],
            seaConnections: ["NTH"]);

        builder.AddCoastalTerritory(
            "Holland",
            ["Hol"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["Bel", "Kie", "Ruh"],
            coastalConnections: [
                ("main", "Bel", "main"),
                ("main", "Kie", "main")],
            seaConnections: [
                ("main", "NTH"),
                ("main", "HEL")]);

        builder.AddSeaTerritory(
            "Ionian Sea",
            ["ION"],
            landConnections: [
                ("Alb", "main"),
                ("Apu", "main"),
                ("Gre", "main"),
                ("Nap", "main"),
                ("Tun", "main")],
            seaConnections: ["ADR", "AEG", "EAS", "TYS"]);

        builder.AddSeaTerritory(
            "Irish Sea",
            ["IRI"],
            landConnections: [
                ("Lvp", "main"),
                ("Wal", "main")],
            seaConnections: ["ENG", "MAO", "NAO"]);

        builder.AddCoastalHomeSupplyCenter(
            "Kiel",
            ["Kie"],
            coasts: ["main"],
            landConnections: ["Ber", "Den", "Hol", "Mun", "Ruh"],
            coastalConnections: [
                ("main", "Ber", "main"),
                ("main", "Den", "main"),
                ("main", "Hol", "main")],
            seaConnections: [
                ("main", "BAL"),
                ("main", "HEL")],
            nationName: "Germany",
            startUnitCoast: "main");

        builder.AddCoastalHomeSupplyCenter(
            "Liverpool",
            ["Lvp", "Liv"],
            coasts: ["main"],
            landConnections: ["Cly", "Edi", "Wal", "Yor"],
            coastalConnections: [
                ("main", "Cly", "main"),
                ("main", "Wal", "main")],
            seaConnections: [
                ("main", "IRI"),
                ("main", "NAO")],
            nationName: "England");

        builder.AddCoastalTerritory(
            "Livonia",
            ["Lvn", "Liv"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Mos", "Pru", "StP", "War"],
            coastalConnections: [
                ("main", "Pru", "main"),
                ("main", "StP", "south")],
            seaConnections: [
                ("main", "BAL"),
                ("main", "BOT")]);

        builder.AddCoastalHomeSupplyCenter(
            "London",
            ["Lon"],
            coasts: ["main"],
            landConnections: ["Wal", "Yor"],
            coastalConnections: [
                ("main", "Wal", "main"),
                ("main", "Yor", "main")],
            seaConnections: [
                ("main", "ENG"),
                ("main", "NTH")],
            nationName: "England",
            startUnitCoast: "main");

        builder.AddCoastalHomeSupplyCenter(
            "Marseilles",
            ["Mar"],
            coasts: ["main"],
            landConnections: ["Bur", "Gas", "Pie", "Spa"],
            coastalConnections: [
                ("main", "Pie", "main"),
                ("main", "Spa", "south")],
            seaConnections: [
                ("main", "LYO")],
            nationName: "France",
            startUnitCoast: "main");

        builder.AddSeaTerritory(
            "Mid-Atlantic Ocean",
            ["MAO", "MID"],
            landConnections: [
                ("Bre", "main"),
                ("Gas", "main"),
                ("Por", "main"),
                ("Spa", "north"),
                ("Spa", "south")],
            seaConnections: ["ENG", "IRI", "NAO", "WES"]);

        builder.AddInlandHomeSupplyCenter(
            "Moscow",
            ["Mos"],
            landConnections: ["Lvn", "Sev", "StP", "Ukr", "War"],
            nationName: "Russia");

        builder.AddInlandHomeSupplyCenter(
            "Munich",
            ["Mun"],
            landConnections: ["Boh", "Ber", "Bur", "Kie", "Ruh", "Sil", "Tyr"],
            nationName: "Germany");

        builder.AddCoastalHomeSupplyCenter(
            "Naples",
            ["Nap"],
            coasts: ["main"],
            landConnections: ["Apu", "Rom"],
            coastalConnections: [
                ("main", "Apu", "main"),
                ("main", "Rom", "main")],
            seaConnections: [
                ("main", "ION"),
                ("main", "TYS")],
            nationName: "Italy",
            startUnitCoast: "main");

        builder.AddCoastalTerritory(
            "Norway",
            ["Nwy", "Nor", "Nry"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["Fin", "StP", "Swe"],
            coastalConnections: [
                ("main", "StP", "north"),
                ("main", "Swe", "main")],
            seaConnections: [
                ("main", "BAR"),
                ("main", "NTH"),
                ("main", "NWG"),
                ("main", "SKA")]);

        builder.AddCoastalTerritory(
            "North Africa",
            ["NAf", "Nor"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Tun"],
            coastalConnections: [("main", "Tun", "main")],
            seaConnections: [
                ("main", "MAO"),
                ("main", "WES")]);

        builder.AddSeaTerritory(
            "North Atlantic Ocean",
            ["NAO", "NOR"],
            landConnections: [
                ("Cly", "main"),
                ("Lvp", "main")],
            seaConnections: ["IRI", "MAO", "NWG"]);

        builder.AddSeaTerritory(
            "North Sea",
            ["NTH", "NOR"],
            landConnections: [
                ("Bel", "main"),
                ("Den", "main"),
                ("Edi", "main"),
                ("Hol", "main"),
                ("Lon", "main"),
                ("Nwy", "main"),
                ("Yor", "main")],
            seaConnections: ["ENG", "HEL", "NWG", "SKA"]);

        builder.AddSeaTerritory(
            "Norwegian Sea",
            ["NWG", "NRG", "NOR"],
            landConnections: [
                ("Cly", "main"),
                ("Edi", "main"),
                ("Nwy", "main")],
            seaConnections: ["BAR", "NAO", "NTH"]);

        builder.AddInlandHomeSupplyCenter(
            "Paris",
            ["Par"],
            landConnections: ["Bre", "Bur", "Gas", "Pic"],
            nationName: "France");

        builder.AddCoastalTerritory(
            "Picardy",
            ["Pic"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Bel", "Bre", "Bur", "Par"],
            coastalConnections: [
                ("main", "Bel", "main"),
                ("main", "Bre", "main")],
            seaConnections: [("main", "ENG")]);

        builder.AddCoastalTerritory(
            "Piedmont",
            ["Pie"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Mar", "Tus", "Tyr", "Ven"],
            coastalConnections: [
                ("main", "Mar", "main"),
                ("main", "Tus", "main")],
            seaConnections: [("main", "LYO")]);

        builder.AddCoastalTerritory(
            "Portugal",
            ["Por"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["Spa"],
            coastalConnections: [
                ("main", "Spa", "north"),
                ("main", "Spa", "south")],
            seaConnections: [("main", "MAO")]);

        builder.AddCoastalTerritory(
            "Prussia",
            ["Pru"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Ber", "Lvn", "Sil", "War"],
            coastalConnections: [
                ("main", "Ber", "main"),
                ("main", "Lvn", "main")],
            seaConnections: [("main", "BAL")]);

        builder.AddCoastalHomeSupplyCenter(
            "Rome",
            ["Rom"],
            coasts: ["main"],
            landConnections: ["Apu", "Nap", "Tus", "Ven"],
            coastalConnections: [
                ("main", "Nap", "main"),
                ("main", "Tus", "main")],
            seaConnections: [("main", "TYS")],
            nationName: "Italy");

        builder.AddInlandTerritory(
            "Ruhr",
            ["Ruh"],
            isSupplyCenter: false,
            landConnections: ["Bel", "Bur", "Hol", "Kie", "Mun"]);

        builder.AddCoastalTerritory(
            "Rumania",
            ["Rum"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["Bud", "Bul", "Gal", "Ser", "Sev", "Ukr"],
            coastalConnections: [
                ("main", "Bul", "east"),
                ("main", "Sev", "main")],
            seaConnections: [("main", "BLA")]);

        builder.AddInlandTerritory(
            "Serbia",
            ["Ser"],
            isSupplyCenter: true,
            landConnections: ["Alb", "Bul", "Bud", "Gre", "Rum", "Tri"]);

        builder.AddCoastalHomeSupplyCenter(
            "Sevastopol",
            ["Sev"],
            coasts: ["main"],
            landConnections: ["Arm", "Mos", "Rum", "Ukr"],
            coastalConnections: [
                ("main", "Arm", "main"),
                ("main", "Rum", "main")],
            seaConnections: [("main", "BLA")],
            nationName: "Russia",
            startUnitCoast: "main");

        builder.AddInlandTerritory(
            "Silesia",
            ["Sil"],
            isSupplyCenter: false,
            landConnections: ["Ber", "Boh", "Gal", "Mun", "Pru", "War"]);

        builder.AddSeaTerritory(
            "Skagerrak",
            ["Ska"],
            landConnections: [
                ("Den", "main"),
                ("Nwy", "main"),
                ("Swe", "main")],
            seaConnections: ["NTH"]);

        builder.AddCoastalHomeSupplyCenter(
            "Smyrna",
            ["Smy"],
            coasts: ["main"],
            landConnections: ["Arm", "Con", "Smy"],
            coastalConnections: [
                ("main", "Arm", "main"),
                ("main", "Con", "main")],
            seaConnections: [("main", "BLA")],
            nationName: "Turkey",
            startUnitCoast: "main");

        builder.AddCoastalTerritory(
            "Spain",
            ["Spa"],
            coasts: ["north", "south"],
            isSupplyCenter: true,
            landConnections: ["Gas", "Mar", "Por"],
            coastalConnections: [
                ("north", "Gas", "main"),
                ("south", "Mar", "main"),
                ("north", "Por", "main"),
                ("south", "Por", "main")],
            seaConnections: [
                ("south", "LYO"),
                ("north", "MAO"),
                ("south", "WES")]);

        builder.AddCoastalHomeSupplyCenter(
            "St Petersburg",
            ["StP"],
            coasts: ["north", "south"],
            landConnections: ["Fin", "Lvn", "Mos", "Nwy"],
            coastalConnections: [
                ("south", "Fin", "main"),
                ("south", "Lvn", "main"),
                ("north", "Nwy", "main")],
            seaConnections: [
                ("north", "BAR"),
                ("south", "BOT")],
            nationName: "Russia",
            startUnitCoast: "south");

        builder.AddCoastalTerritory(
            "Sweden",
            ["Swe"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["Den", "Fin", "Nwy"],
            coastalConnections: [
                ("main", "Den", "main"),
                ("main", "Fin", "main"),
                ("main", "Nwy", "main")],
            seaConnections: [
                ("main", "BAL"),
                ("main", "BOT"),
                ("main", "SKA")]);

        builder.AddCoastalTerritory(
            "Syria",
            ["Syr"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Arm", "Smy"],
            coastalConnections: [("main", "Smy", "main")],
            seaConnections: [("main", "EAS")]);

        builder.AddCoastalHomeSupplyCenter(
            "Trieste",
            ["Tri"],
            coasts: ["main"],
            landConnections: ["Alb", "Bud", "Ser", "Tri", "Tyr", "Ven"],
            coastalConnections: [
                ("main", "Alb", "main"),
                ("main", "Ven", "main")],
            seaConnections: [("main", "ADR")],
            nationName: "Austria",
            startUnitCoast: "main");

        builder.AddCoastalTerritory(
            "Tunis",
            ["Tun"],
            coasts: ["main"],
            isSupplyCenter: true,
            landConnections: ["NAf"],
            coastalConnections: [("main", "NAf", "main")],
            seaConnections: [
                ("main", "ION"),
                ("main", "TYS"),
                ("main", "WES")]);

        builder.AddCoastalTerritory(
            "Tuscany",
            ["Tus"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Pie", "Rom", "Ven"],
            coastalConnections: [
                ("main", "Pie", "main"),
                ("main", "Rom", "main")],
            seaConnections: [
                ("main", "LYO"),
                ("main", "TYS")]);

        builder.AddSeaTerritory(
            "Tyrhennian Sea",
            ["TYS"],
            landConnections: [
                ("Nap", "main"),
                ("Rom", "main"),
                ("Tun", "main"),
                ("Tus", "main")],
            seaConnections: ["ION", "LYO", "TYS", "WES"]);

        builder.AddInlandTerritory(
            "Tyrolia",
            ["Tyr"],
            isSupplyCenter: false,
            landConnections: ["Boh", "Mun", "Tri", "Ven", "Vie"]);

        builder.AddInlandTerritory(
            "Ukraine",
            ["Ukr"],
            isSupplyCenter: false,
            landConnections: ["Gal", "Mos", "Rum", "Sev", "War"]);

        builder.AddCoastalHomeSupplyCenter(
            "Venice",
            ["Ven"],
            coasts: ["main"],
            landConnections: ["Apu", "Pie", "Rom", "Tri", "Tus", "Tyr"],
            coastalConnections: [
                ("main", "Apu", "main"),
                ("main", "Tri", "main")],
            seaConnections: [("main", "ADR")],
            nationName: "Italy");

        builder.AddInlandHomeSupplyCenter(
            "Vienna",
            ["Vie"],
            landConnections: ["Boh", "Bud", "Gal", "Tri", "Tyr"],
            nationName: "Austria");

        builder.AddCoastalTerritory(
            "Wales",
            ["Wal"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Lon", "Lvp", "Yor"],
            coastalConnections: [
                ("main", "Lon", "main"),
                ("main", "Lvp", "main")],
            seaConnections: [
                ("main", "ENG"),
                ("main", "IRI")]);

        builder.AddInlandHomeSupplyCenter(
            "Warsaw",
            ["War"],
            landConnections: ["Gal", "Lvn", "Mos", "Pru", "Sil", "Ukr"],
            nationName: "Russia");

        builder.AddSeaTerritory(
            "Western Mediterranean",
            ["WES"],
            landConnections: [
                ("NAf", "main"),
                ("Spa", "south"),
                ("Tun", "main")],
            seaConnections: ["LYO", "MAO", "TYS"]);

        builder.AddCoastalTerritory(
            "Yorkshire",
            ["Yor"],
            coasts: ["main"],
            isSupplyCenter: false,
            landConnections: ["Edi", "Lon", "Lvp", "Wal"],
            coastalConnections: [
                ("main", "Edi", "main"),
                ("main", "Lon", "main")],
            seaConnections: [("main", "Yor")]);

        return builder.Build();
    }
}
