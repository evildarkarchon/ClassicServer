# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

ClassicServer is a client-server implementation of the CLASSIC crash log analyzer for Fallout 4/Skyrim. This project ports Python crash analysis functionality to a .NET 8.0 architecture with ASP.NET Core server and console client components.

## Essential Commands

### Development
```bash
# Build the entire solution
dotnet build ClassicServer.sln

# Run the server (development with Swagger)
dotnet run --project ClassicServer

# Run the client
dotnet run --project ClassicClient

# Test (when implemented)
dotnet test

# Production build
dotnet build --configuration Release
```

### Docker Deployment
```bash
# Build and run both services
docker-compose up --build

# Build individual services
docker build -f ClassicServer/Dockerfile -t classicserver .
docker build -f ClassicClient/Dockerfile -t classicclient .
```

## Architecture Overview

### Multi-Component Solution Structure
- **ClassicServer**: ASP.NET Core Web API (.NET 8.0) with Swagger/OpenAPI
- **ClassicClient**: Console application (.NET 8.0) for client interactions  
- **CLASSIC-Fallout4**: Python reference implementation (for porting guidance only)
- **sample_logs**: Test data for validation against Python implementation

### Implementation Status
This is an early-stage .NET port following the phased approach in `classiclib-implementation-plan.md`:
1. **Phase 1**: Core data models and YAML configuration parsing
2. **Phase 2**: Log parser implementation
3. **Phase 3**: Analysis engine with pattern matching
4. **Phase 4**: Report generation and API endpoints

### Current State
- **ClassicServer**: Basic ASP.NET Core skeleton with Swagger
- **ClassicClient**: Basic console application template
- **Configuration**: Global .NET 8.0 SDK specified, Docker support configured

## Development Guidelines

### .NET 8.0 Standards
- **Nullable Reference Types**: Enabled by default in both projects
- **Implicit Usings**: Leveraged for cleaner code
- **CheckForOverflowUnderflow**: Enabled in both Debug and Release builds
- **Target Framework**: .NET 8.0 as specified in global.json

### Python Reference Integration
When porting functionality from the Python implementation:
- **Study Python Code**: Always reference working Python code in `CLASSIC-Fallout4/`
- **Maintain Analysis Logic**: Preserve exact pattern matching and analysis algorithms
- **Validate with Sample Data**: Use `sample_logs/` to ensure C# output matches Python results
- **Follow YAML Structure**: Maintain compatibility with Python's YAML configuration format

### C# Migration Patterns
- **Async Patterns**: Use `async/await` for I/O operations
- **LINQ**: Leverage for collection operations and pattern matching
- **Record Types**: Use for immutable data transfer objects
- **Pattern Matching**: Utilize C# pattern matching for complex logic

## Key Architecture Components

### Data Models (Planned)
Following the implementation plan:
```csharp
public class CrashLog {
    public string FileName { get; set; }
    public string Content { get; set; }
    public DateTime CrashTime { get; set; }
    public string MainError { get; set; }
    public List<string> CallStack { get; set; }
    public List<Plugin> Plugins { get; set; }
}

public class AnalysisResult {
    public string LogFileName { get; set; }
    public List<CrashSuspect> Suspects { get; set; }
    public List<string> Warnings { get; set; }
    public Dictionary<string, int> NamedRecords { get; set; }
}
```

### YAML Configuration System
Port Python's YAML-based configuration:
- **Crash Patterns**: `Crashlog_Error_Check` and `Crashlog_Stack_Check` definitions
- **Mod Definitions**: `Mods_Core` and plugin analysis rules
- **Game Info**: Version checking and compatibility matrices

### Log Analysis Pipeline
Mirror Python's processing workflow:
1. **Log Parsing**: Extract segments, headers, and module information
2. **Pattern Matching**: Apply YAML-defined crash patterns with conditional logic
3. **Component Analysis**: FormID lookup, plugin conflicts, settings validation
4. **Report Generation**: Markdown output with suspect identification

## Important Development Notes

### Critical Pitfalls to Avoid
1. **Don't Reinvent Analysis Logic**: Always port from working Python implementation
2. **Validate Against Sample Data**: Use `sample_logs/` to verify output matches Python
3. **Maintain YAML Compatibility**: Ensure configuration format remains consistent
4. **Follow Implementation Plan**: Respect phased development approach

### Key Reference Files
- `classiclib-implementation-plan.md`: Phased development roadmap
- `.github/copilot-instructions.md`: Project-specific AI coding guidelines
- `CLASSIC-Fallout4/ClassicLib/`: Core analysis components to port
- `sample_logs/`: Test data for validation
- `compose.yaml`: Multi-container deployment configuration

### Docker Support
Both projects include Docker support:
- Individual Dockerfiles in each project directory
- `compose.yaml` orchestrates both server and client containers
- Optimized for both development and production deployment