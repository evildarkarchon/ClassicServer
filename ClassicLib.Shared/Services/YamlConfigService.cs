using ClassicLib.Shared.Configuration;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClassicLib.Shared.Services;

public class YamlConfigService : IYamlConfigService
{
    private readonly IDeserializer _deserializer;

    public YamlConfigService()
    {
        _deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();
    }

    public async Task<ClassicYamlConfig> LoadConfigAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"YAML configuration file not found: {filePath}");
        }

        var yamlContent = await File.ReadAllTextAsync(filePath);
        return LoadConfigFromString(yamlContent);
    }

    public async Task<ClassicYamlConfig> LoadConfigFromStringAsync(string yamlContent)
    {
        return await Task.Run(() => LoadConfigFromString(yamlContent));
    }

    public ClassicYamlConfig LoadConfig(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"YAML configuration file not found: {filePath}");
        }

        var yamlContent = File.ReadAllText(filePath);
        return LoadConfigFromString(yamlContent);
    }

    public ClassicYamlConfig LoadConfigFromString(string yamlContent)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(yamlContent))
            {
                return new ClassicYamlConfig();
            }
            
            var result = _deserializer.Deserialize<ClassicYamlConfig>(yamlContent);
            return result ?? new ClassicYamlConfig();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to parse YAML configuration: {ex.Message}", ex);
        }
    }
}