using YamlDotNet.Serialization;

namespace ClassicLib.Shared.Configuration;

public class ModInfo
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<string> Patterns { get; set; } = new();
    public List<string> ConflictsWith { get; set; } = new();
    public string? Solution { get; set; }
    public string? Link { get; set; }
}