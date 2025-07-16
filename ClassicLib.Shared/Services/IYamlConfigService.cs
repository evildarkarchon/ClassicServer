using ClassicLib.Shared.Configuration;

namespace ClassicLib.Shared.Services;

public interface IYamlConfigService
{
    Task<ClassicYamlConfig> LoadConfigAsync(string filePath);
    Task<ClassicYamlConfig> LoadConfigFromStringAsync(string yamlContent);
    ClassicYamlConfig LoadConfig(string filePath);
    ClassicYamlConfig LoadConfigFromString(string yamlContent);
}