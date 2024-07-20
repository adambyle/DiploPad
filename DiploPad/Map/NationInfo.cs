namespace DiploPad.Map;

public class NationInfo(string[] names)
{
    public IReadOnlyList<string> Names { get; } = names;

    public string PrimaryName => Names[0];
}
