using YamlDotNet.Serialization;

namespace ClassicLib.Shared.Configuration;

public class ClassicYamlConfig
{
    [YamlMember(Alias = "Game_Info")]
    public GameInfo GameInfo { get; set; } = new();
    
    [YamlMember(Alias = "Crashlog_Error_Check")]
    public Dictionary<string, string> CrashlogErrorCheck { get; set; } = new();
    
    [YamlMember(Alias = "Crashlog_Stack_Check")]
    public Dictionary<string, object> CrashlogStackCheck { get; set; } = new();
    
    [YamlMember(Alias = "Mods_CORE")]
    public Dictionary<string, string> ModsCore { get; set; } = new();
    
    [YamlMember(Alias = "Mods_FREQ")]
    public Dictionary<string, string> ModsFreq { get; set; } = new();
    
    [YamlMember(Alias = "Mods_CONF")]
    public Dictionary<string, string> ModsConf { get; set; } = new();
    
    [YamlMember(Alias = "Mods_SOLU")]
    public Dictionary<string, string> ModsSolu { get; set; } = new();
    
    [YamlMember(Alias = "Warnings_CRASHGEN")]
    public Dictionary<string, string> WarningsCrashgen { get; set; } = new();
    
    [YamlMember(Alias = "Warnings_XSE")]
    public Dictionary<string, string> WarningsXse { get; set; } = new();
    
    [YamlMember(Alias = "Game_Hints")]
    public List<string> GameHints { get; set; } = new();
}