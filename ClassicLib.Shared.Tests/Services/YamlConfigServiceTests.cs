using ClassicLib.Shared.Services;
using ClassicLib.Shared.Configuration;

namespace ClassicLib.Shared.Tests.Services;

public class YamlConfigServiceTests
{
    private readonly YamlConfigService _yamlConfigService;

    public YamlConfigServiceTests()
    {
        _yamlConfigService = new YamlConfigService();
    }

    [Fact]
    public void LoadConfigFromString_WithValidGameInfo_ShouldParseCorrectly()
    {
        // Arrange
        var yamlContent = @"
Game_Info:
  GameVersion: 1.10.163
  GameVersionNEW: 1.10.984
  CRASHGEN_LatestVer: Buffout 4 v1.28.6
  XSE_Acronym: F4SE
  XSE_FullName: Fallout 4 Script Extender (F4SE)
  XSE_Ver_Latest: 0.6.23
  XSE_Ver_Latest_NG: 0.7.2";

        // Act
        var result = _yamlConfigService.LoadConfigFromString(yamlContent);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.GameInfo);
        Assert.Equal("1.10.163", result.GameInfo.GameVersion);
        Assert.Equal("1.10.984", result.GameInfo.GameVersionNew);
        Assert.Equal("Buffout 4 v1.28.6", result.GameInfo.CrashGenLatestVer);
        Assert.Equal("F4SE", result.GameInfo.XseAcronym);
        Assert.Equal("Fallout 4 Script Extender (F4SE)", result.GameInfo.XseFullName);
        Assert.Equal("0.6.23", result.GameInfo.XseVerLatest);
        Assert.Equal("0.7.2", result.GameInfo.XseVerLatestNg);
    }

    [Fact]
    public void LoadConfigFromString_WithCrashPatterns_ShouldParseCorrectly()
    {
        // Arrange
        var yamlContent = @"
Crashlog_Error_Check:
  5 | Stack Overflow Crash: EXCEPTION_STACK_OVERFLOW
  5 | Active Effects Crash: ""0x000100000000""
  3 | C++ Redist Crash: MSVC";

        // Act
        var result = _yamlConfigService.LoadConfigFromString(yamlContent);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.CrashlogErrorCheck);
        Assert.True(result.CrashlogErrorCheck.Count > 0);
        Assert.Contains(result.CrashlogErrorCheck, kvp => kvp.Value.Contains("EXCEPTION_STACK_OVERFLOW"));
        Assert.Contains(result.CrashlogErrorCheck, kvp => kvp.Value.Contains("0x000100000000"));
        Assert.Contains(result.CrashlogErrorCheck, kvp => kvp.Value.Contains("MSVC"));
    }

    [Fact]
    public void LoadConfigFromString_WithModDefinitions_ShouldParseCorrectly()
    {
        // Arrange
        var yamlContent = @"
Mods_CORE:
  CanarySaveFileMonitor | Canary Save File Monitor: |
    This is a highly recommended mod that can detect save file corruption.
    Link: https://www.nexusmods.com/fallout4/mods/44949?tab=files

Mods_FREQ:
  DamageThresholdFramework: |
    Damage Threshold Framework
        - Can cause crashes in combat on some occasions due to how damage calculations are done.
        -----";

        // Act
        var result = _yamlConfigService.LoadConfigFromString(yamlContent);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ModsCore);
        Assert.NotNull(result.ModsFreq);
        Assert.True(result.ModsCore.Count > 0);
        Assert.True(result.ModsFreq.Count > 0);
        Assert.Contains(result.ModsCore, kvp => kvp.Key.Contains("Canary Save File Monitor"));
        Assert.Contains(result.ModsFreq, kvp => kvp.Key.Contains("DamageThresholdFramework"));
    }

    [Fact]
    public void LoadConfigFromString_WithEmptyString_ShouldReturnEmptyConfig()
    {
        // Arrange
        var yamlContent = "";

        // Act
        var result = _yamlConfigService.LoadConfigFromString(yamlContent);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.GameInfo);
        Assert.NotNull(result.CrashlogErrorCheck);
        Assert.NotNull(result.ModsCore);
    }

    [Fact]
    public void LoadConfigFromString_WithInvalidYaml_ShouldThrowException()
    {
        // Arrange
        var invalidYamlContent = @"
Invalid YAML:
  - missing closing bracket
    invalid: syntax [";

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _yamlConfigService.LoadConfigFromString(invalidYamlContent));
    }

    [Fact]
    public async Task LoadConfigAsync_WithNonExistentFile_ShouldThrowFileNotFoundException()
    {
        // Arrange
        var nonExistentPath = "non-existent-file.yaml";

        // Act & Assert
        await Assert.ThrowsAsync<FileNotFoundException>(() => _yamlConfigService.LoadConfigAsync(nonExistentPath));
    }
}