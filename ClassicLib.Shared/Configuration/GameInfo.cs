using YamlDotNet.Serialization;

namespace ClassicLib.Shared.Configuration;

public class GameInfo
{
    [YamlMember(Alias = "GameVersion")]
    public string GameVersion { get; set; } = string.Empty;
    
    [YamlMember(Alias = "GameVersionNEW")]
    public string GameVersionNew { get; set; } = string.Empty;
    
    [YamlMember(Alias = "CRASHGEN_LatestVer")]
    public string CrashGenLatestVer { get; set; } = string.Empty;
    
    [YamlMember(Alias = "XSE_Acronym")]
    public string XseAcronym { get; set; } = string.Empty;
    
    [YamlMember(Alias = "XSE_FullName")]
    public string XseFullName { get; set; } = string.Empty;
    
    [YamlMember(Alias = "XSE_Ver_Latest")]
    public string XseVerLatest { get; set; } = string.Empty;
    
    [YamlMember(Alias = "XSE_Ver_Latest_NG")]
    public string XseVerLatestNg { get; set; } = string.Empty;
}