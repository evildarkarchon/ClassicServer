# ClassicServer AI Coding Agent Instructions

## Project Overview

ClassicServer is a client-server implementation of the CLASSIC crash log analyzer for Fallout 4/Skyrim. The project ports Python crash analysis functionality to a .NET 8.0 architecture with ASP.NET Core server and console client components.

## Architecture Patterns

### Multi-Component Solution Structure
- **ClassicServer**: ASP.NET Core Web API (.NET 8.0) with Swagger/OpenAPI
- **ClassicClient**: Console application (.NET 8.0) for client interactions
- **CLASSIC-Fallout4**: Python reference implementation with full crash analysis logic
- **ClassicLib**: Shared Python library providing core analysis components

### Python Reference Implementation
The `CLASSIC-Fallout4/` directory contains the mature Python implementation that serves as the specification:
- **Dual Entry Points**: `CLASSIC_Interface.py` (GUI), `CLASSIC_ScanLogs.py` (CLI)
- **Orchestrator Pattern**: `ClassicLib.ScanLog.ScanOrchestrator` coordinates analysis components
- **Modular Architecture**: Separate analyzers for FormID, plugins, settings, and suspect detection

### Client-Server Design Goals
Following the implementation plan in `classiclib-implementation-plan.md`:
1. **Phase 1**: Core data models and YAML configuration parsing
2. **Phase 2**: Log parser implementation 
3. **Phase 3**: Analysis engine with pattern matching
4. **Phase 4**: Report generation and API endpoints

## Development Standards

### .NET 8.0 Conventions
- **Nullable Reference Types**: Enabled by default in both projects
- **Implicit Usings**: Leveraged for cleaner code
- **CheckForOverflowUnderflow**: Enabled in both Debug and Release builds
- **Docker Support**: Both projects have Dockerfile and docker-compose integration

### Configuration Management
- **YAML Configuration**: Port Python's YAML-based crash pattern definitions
- **Sample Data**: Use `sample_logs/` directory for testing and validation
- **Environment-Specific**: Leverage ASP.NET Core's configuration system

### Python Reference Integration
When porting Python functionality to C#:
- **Study Python Implementation**: Always reference the working Python code in `CLASSIC-Fallout4/`
- **Maintain Analysis Logic**: Preserve the exact pattern matching and analysis algorithms
- **Use Sample Logs**: Validate against existing crash logs in `sample_logs/`

## Critical Development Workflows

### Build Process
- **Solution Structure**: Use `ClassicServer.sln` for multi-project builds
- **Docker Deployment**: `compose.yaml` orchestrates both server and client containers
- **Global SDK**: `.NET 8.0` specified in `global.json` with latest minor rollforward

### Testing Strategy
- **Python Test Suite**: Reference `CLASSIC-Fallout4/tests/` for validation patterns
- **Sample Data Validation**: Ensure C# implementation produces same results as Python
- **Integration Tests**: Verify client-server communication and data flow

### Development Environment
- **VS Code/Rider**: Project optimized for both editors
- **Docker**: All components containerized for consistent deployment
- **Hot Reload**: ASP.NET Core supports development-time code changes

## Key Integration Points

### YAML Configuration System
Port Python's YAML-based configuration from `CLASSIC-Fallout4/`:
- **Crash Patterns**: `Crashlog_Error_Check` and `Crashlog_Stack_Check` definitions
- **Mod Definitions**: `Mods_Core` and plugin analysis rules
- **Game Info**: Version checking and compatibility matrices

### Log Analysis Pipeline
Mirror Python's processing workflow:
1. **Log Parsing**: Extract segments, headers, and module information
2. **Pattern Matching**: Apply YAML-defined crash patterns with conditional logic
3. **Component Analysis**: FormID lookup, plugin conflicts, settings validation
4. **Report Generation**: Markdown output with suspect identification

### Data Flow Architecture
- **Client → Server**: Log files and analysis requests
- **Server Processing**: Crash pattern matching and component analysis
- **Server → Client**: Analysis results and generated reports
- **Async Processing**: Support for background analysis of large log sets

## Reference Implementation Patterns

### Python Standards (from CLASSIC-Fallout4)
- **Type Annotations**: Python 3.12+ syntax with complete function signatures
- **Import Organization**: Standard library, third-party, local project imports
- **Error Handling**: MessageHandler system instead of print statements
- **File Operations**: Always use `pathlib.Path` with UTF-8 encoding

### C# Migration Guidelines
When porting Python code:
- **Async Patterns**: Use `async/await` for I/O operations
- **LINQ**: Leverage for collection operations and pattern matching
- **Record Types**: Use for immutable data transfer objects
- **Pattern Matching**: Utilize C# 8.0+ pattern matching for complex logic

## Common Pitfalls

1. **Don't Reinvent Analysis Logic**: Always port from working Python implementation
2. **Validate Against Sample Data**: Use `sample_logs/` to verify output matches Python
3. **Maintain YAML Compatibility**: Ensure configuration format remains consistent
4. **Test Cross-Platform**: Both Windows and Linux container deployment
5. **Follow Implementation Plan**: Respect phased development approach in planning document

## Key Reference Files

- `classiclib-implementation-plan.md`: Phased development roadmap
- `CLASSIC-Fallout4/.github/copilot-instructions.md`: Python implementation standards
- `CLASSIC-Fallout4/ClassicLib/`: Core analysis components to port
- `sample_logs/`: Test data for validation
- `compose.yaml`: Multi-container deployment configuration
