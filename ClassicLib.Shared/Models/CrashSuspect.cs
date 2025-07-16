namespace ClassicLib.Shared.Models;

public class CrashSuspect
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Severity { get; set; }
    public List<string> Evidence { get; set; } = new();
    public List<string> Recommendations { get; set; } = new();
    public string Category { get; set; } = string.Empty;
    public bool RequiresManualCheck { get; set; }
}