using YamlDotNet.Serialization;

namespace ClassicLib.Shared.Configuration;

public class CrashPattern
{
    public int Severity { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Pattern { get; set; } = string.Empty;
    public List<string> StackPatterns { get; set; } = new();
    public List<string> RequiredInMainError { get; set; } = new();
    public List<string> OptionalInMainError { get; set; } = new();
    public List<string> NotPatterns { get; set; } = new();
    public int MinOccurrences { get; set; } = 1;
}