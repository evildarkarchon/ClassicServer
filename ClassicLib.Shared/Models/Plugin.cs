namespace ClassicLib.Shared.Models;

public class Plugin
{
    public string Name { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public int LoadOrder { get; set; }
    public bool IsActive { get; set; }
    public bool IsLight { get; set; }
    public bool IsMaster { get; set; }
    public string? Version { get; set; }
    public List<string> FormIds { get; set; } = new();
}