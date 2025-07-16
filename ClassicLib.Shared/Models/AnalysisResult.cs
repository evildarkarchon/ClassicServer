namespace ClassicLib.Shared.Models;

public class AnalysisResult
{
    public string LogFileName { get; set; } = string.Empty;
    public List<CrashSuspect> Suspects { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public Dictionary<string, int> NamedRecords { get; set; } = new();
    public List<string> FormIdSuspects { get; set; } = new();
    public List<string> PluginSuspects { get; set; } = new();
    public DateTime AnalysisTimestamp { get; set; } = DateTime.UtcNow;
    public string ReportId { get; set; } = string.Empty;
    public string MarkdownReport { get; set; } = string.Empty;
}