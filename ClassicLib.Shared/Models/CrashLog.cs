namespace ClassicLib.Shared.Models;

public class CrashLog
{
    public string FileName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CrashTime { get; set; }
    public string MainError { get; set; } = string.Empty;
    public List<string> CallStack { get; set; } = new();
    public List<Plugin> Plugins { get; set; } = new();
    public string? GameVersion { get; set; }
    public string? BuffoutVersion { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new();
    public List<string> ModuleNames { get; set; } = new();
}