# ClassicLib Client-Server Architecture Implementation Plan

## Project Overview
ClassicLib is a client-server system for analyzing Fallout 4 crash logs (primarily Buffout 4 logs) and generating reports based on criteria defined in YAML configuration files.

## Architecture Overview

```
┌─────────────────┐     ┌──────────────────┐     ┌─────────────────┐
│  Discord Bot    │     │   CLI Client     │     │  Other Apps     │
└────────┬────────┘     └────────┬─────────┘     └────────┬────────┘
         │                       │                          │
         └───────────────────────┴──────────────────────────┘
                                 │
                          REST/gRPC API
                                 │
                    ┌────────────┴────────────┐
                    │   ClassicLib Server     │
                    ├─────────────────────────┤
                    │  • API Gateway          │
                    │  • Analysis Engine      │
                    │  • Report Generator     │
                    │  • YAML Parser          │
                    │  • Cache Layer         │
                    └────────────┬────────────┘
                                 │
                    ┌────────────┴────────────┐
                    │    Data Storage         │
                    │  • Crash Log Cache      │
                    │  • Reports Database    │
                    │  • YAML Configs        │
                    └─────────────────────────┘
```

---

## Phase 1: Core Infrastructure Setup

### 1.1 Project Structure
- [ ] Create main repository structure
  ```
  ClassicLib/
  ├── server/
  │   ├── src/
  │   │   ├── api/
  │   │   ├── analyzers/
  │   │   ├── parsers/
  │   │   ├── generators/
  │   │   └── utils/
  │   ├── tests/
  │   └── config/
  ├── client/
  │   ├── cli/
  │   └── sdk/
  ├── shared/
  │   ├── models/
  │   └── contracts/
  ├── reference-implementation/
  │   ├── original-source/      # Original CLASSIC Python/other code
  │   ├── algorithms/           # Key algorithms with explanations
  │   ├── patterns/             # Pattern matching logic examples
  │   ├── sample-logs/          # Example crash logs for testing
  │   ├── sample-reports/       # Example generated reports
  │   └── porting-notes.md      # Notes on differences and conversions
  └── yaml-configs/
      └── CLASSIC-Fallout4.yaml
  ```

### 1.2 Development Environment
- [ ] Set up .NET 8.0 project structure
- [ ] Configure development containers/Docker
- [ ] Set up CI/CD pipeline (GitHub Actions/Azure DevOps)
- [ ] Configure linting and code formatting rules
- [ ] Set up logging framework (Serilog)
- [ ] Configure unit test framework (xUnit/NUnit)

### 1.3 Reference Implementation Setup
- [ ] Populate reference-implementation directory
  - [ ] Copy original CLASSIC source code (if available)
  - [ ] Document key algorithms with pseudocode
  - [ ] Create pattern matching examples with explanations
  - [ ] Add annotated sample crash logs showing:
    - [ ] Main error sections
    - [ ] Call stack structures
    - [ ] Plugin lists
    - [ ] System information blocks
  - [ ] Include sample generated reports for comparison
  - [ ] Create porting-notes.md with:
    - [ ] Python to C# idiom conversions
    - [ ] Regex pattern adaptations
    - [ ] Data structure mappings
    - [ ] Performance considerations
- [ ] Add AI-friendly documentation:
  - [ ] Function signatures with detailed comments
  - [ ] Input/output examples for each component
  - [ ] Edge cases and error handling patterns

### 1.4 Shared Libraries
- [ ] Create shared data models
  - [ ] CrashLog model
  - [ ] AnalysisResult model
  - [ ] Report model
  - [ ] YamlConfiguration model
- [ ] Define API contracts/interfaces
- [ ] Create common utility functions
- [ ] Implement validation attributes

---

## Phase 2: YAML Configuration System

### 2.1 YAML Parser
- [ ] Implement YAML deserializer using YamlDotNet
- [ ] Create strongly-typed configuration classes:
  ```csharp
  public class ClassicConfig
  {
      public GameInfo GameInfo { get; set; }
      public Dictionary<string, CrashPattern> CrashlogErrorCheck { get; set; }
      public Dictionary<string, CrashPattern> CrashlogStackCheck { get; set; }
      public Dictionary<string, ModInfo> ModsCore { get; set; }
      public Dictionary<string, string> Warnings { get; set; }
  }
  ```
- [ ] Implement configuration validation
- [ ] Create configuration hot-reload capability
- [ ] Add configuration versioning support

### 2.2 Pattern Matching Engine
- [ ] Implement pattern matching for crash signatures
- [ ] Support conditional patterns (ME-REQ, ME-OPT, NOT)
- [ ] Implement stack occurrence counting (e.g., [3|UIMessage])
- [ ] Create pattern priority/severity system
- [ ] Add regex support for flexible matching
- [ ] Reference original implementation in `/reference-implementation/original-source/`
- [ ] Use documented patterns from `/reference-implementation/patterns/` for testing
- [ ] Ensure compatibility with existing CLASSIC pattern syntax

---

## Phase 3: Crash Log Analysis Engine

### 3.1 Log Parser
- [ ] Implement Buffout 4 log parser
- [ ] Extract key sections:
  - [ ] Main error information
  - [ ] Call stack traces
  - [ ] Plugin list
  - [ ] System information
- [ ] Handle different log formats/versions
- [ ] Implement error recovery for malformed logs

### 3.2 Analysis Core
- [ ] Create analyzer interface:
  ```csharp
  public interface ICrashAnalyzer
  {
      Task<AnalysisResult> AnalyzeAsync(CrashLog log, ClassicConfig config);
  }
  ```
- [ ] Implement analyzers:
  - [ ] ErrorAnalyzer (main error patterns)
  - [ ] StackAnalyzer (call stack patterns)
  - [ ] PluginAnalyzer (mod conflicts)
  - [ ] SystemAnalyzer (system requirements)
- [ ] Create analysis pipeline coordinator
- [ ] Implement confidence scoring system

### 3.3 Caching Layer
- [ ] Implement Redis/MemoryCache integration
- [ ] Cache analysis results
- [ ] Cache parsed YAML configurations
- [ ] Implement cache invalidation strategies

---

## Phase 4: Report Generation System

### 4.1 Report Generator
- [ ] Create report template engine
- [ ] Implement Markdown report generator
- [ ] Generate sections:
  - [ ] Executive summary
  - [ ] Identified issues with severity
  - [ ] Recommended fixes
  - [ ] Mod conflict warnings
  - [ ] System recommendations
- [ ] Support multiple report formats (MD, HTML, JSON)
- [ ] Implement report customization options

### 4.2 File Naming and Storage
- [ ] Implement file naming convention: `crash-{DateTime:yyyy-MM-dd-HHmmss}-REPORT.md`
- [ ] Create report storage service
- [ ] Implement report retrieval API
- [ ] Add report versioning/history

---

## Phase 5: Server API Development

### 5.1 API Gateway
- [ ] Implement ASP.NET Core Web API
- [ ] Create endpoints:
  ```
  POST   /api/analyze          - Submit crash log for analysis
  GET    /api/reports/{id}     - Retrieve report
  GET    /api/reports          - List reports (with filters)
  GET    /api/configs          - List available YAML configs
  POST   /api/configs          - Upload new YAML config
  GET    /api/health           - Health check endpoint
  ```
- [ ] Implement authentication/authorization
- [ ] Add rate limiting
- [ ] Implement request validation

### 5.2 Background Services
- [ ] Create hosted service for async analysis
- [ ] Implement job queue (using Hangfire/custom)
- [ ] Add progress tracking for long analyses
- [ ] Implement cleanup service for old reports

### 5.3 Real-time Updates
- [ ] Implement SignalR hub for real-time updates
- [ ] Create progress notifications
- [ ] Add analysis completion notifications

---

## Phase 6: Client Development

### 6.1 Client SDK (C#)
- [ ] Create NuGet package
- [ ] Implement client interface:
  ```csharp
  public interface IClassicLibClient
  {
      Task<string> SubmitCrashLogAsync(Stream logFile);
      Task<Report> GetReportAsync(string reportId);
      Task<IEnumerable<Report>> ListReportsAsync(ReportFilter filter);
  }
  ```
- [ ] Add retry policies
- [ ] Implement response caching
- [ ] Create typed exceptions

### 6.2 CLI Client
- [ ] Create command-line application using System.CommandLine
- [ ] Implement commands:
  ```
  classiclib analyze <logfile> [--output <path>] [--format md|json|html]
  classiclib list [--from <date>] [--to <date>]
  classiclib get <report-id> [--output <path>]
  classiclib config list
  classiclib config update <yaml-file>
  ```
- [ ] Add progress indicators
- [ ] Implement error handling and user feedback
- [ ] Add configuration file support

### 6.3 Python SDK (Optional)
- [ ] Create Python package with type hints
- [ ] Mirror C# SDK functionality
- [ ] Add async support
- [ ] Create comprehensive examples

---

## Phase 7: Discord Bot Integration

### 7.1 Bot Framework
- [ ] Create Discord bot using Discord.NET or DSharpPlus
- [ ] Implement commands:
  - [ ] `/analyze` - Upload and analyze crash log
  - [ ] `/report <id>` - Retrieve specific report
  - [ ] `/recent` - List recent analyses
- [ ] Add file upload handling
- [ ] Implement response formatting for Discord

### 7.2 User Experience
- [ ] Create embedded messages for reports
- [ ] Add reaction-based navigation
- [ ] Implement DM support for private logs
- [ ] Add server-specific configurations

---

## Phase 8: Testing and Quality Assurance

### 8.1 Unit Testing
- [ ] Achieve 80%+ code coverage
- [ ] Test all analyzers with sample logs
- [ ] Test YAML parsing edge cases
- [ ] Test report generation variations

### 8.2 Integration Testing
- [ ] Test end-to-end analysis pipeline
- [ ] Test API endpoints with various payloads
- [ ] Test client SDK scenarios
- [ ] Test concurrent analysis handling

### 8.3 Performance Testing
- [ ] Load test with multiple concurrent analyses
- [ ] Benchmark analysis performance
- [ ] Test cache effectiveness
- [ ] Optimize memory usage

### 8.4 Test Data
- [ ] Create comprehensive test crash log set
- [ ] Include edge cases and malformed logs
- [ ] Create test YAML configurations
- [ ] Document test scenarios

---

## Phase 9: Documentation and Deployment

### 9.1 Documentation
- [ ] API documentation (OpenAPI/Swagger)
- [ ] SDK usage documentation
- [ ] YAML configuration guide
- [ ] Deployment guide
- [ ] Contributing guidelines

### 9.2 Deployment
- [ ] Create Docker containers
- [ ] Set up Kubernetes manifests (optional)
- [ ] Configure environment variables
- [ ] Set up monitoring (Prometheus/Grafana)
- [ ] Implement health checks

### 9.3 DevOps
- [ ] Automated deployment pipeline
- [ ] Database migration strategy
- [ ] Backup and recovery procedures
- [ ] Performance monitoring setup

---

## Phase 10: Post-Launch Enhancements

### 10.1 Advanced Features
- [ ] Machine learning for pattern discovery
- [ ] Community-contributed YAML configs
- [ ] Web dashboard for analytics
- [ ] Batch analysis support
- [ ] Export to various bug tracking systems

### 10.2 Optimizations
- [ ] Implement distributed analysis (if needed)
- [ ] Optimize pattern matching algorithms
- [ ] Add GPU acceleration for ML features
- [ ] Implement smart caching strategies

---

## Technical Stack Summary

### Server
- **Language**: C# (.NET 8.0)
- **Framework**: ASP.NET Core
- **Database**: PostgreSQL/SQLite for reports, Redis for caching
- **Message Queue**: RabbitMQ or Azure Service Bus (optional)
- **Real-time**: SignalR
- **Jobs**: Hangfire or custom implementation

### Client
- **CLI**: C# with System.CommandLine
- **SDK**: C# (primary), Python (secondary)
- **Discord Bot**: Discord.NET or DSharpPlus

### Infrastructure
- **Containers**: Docker
- **Orchestration**: Kubernetes (optional)
- **CI/CD**: GitHub Actions/Azure DevOps
- **Monitoring**: Prometheus + Grafana
- **Logging**: Serilog + Seq/ELK

### Libraries
- **YAML Parsing**: YamlDotNet
- **HTTP**: HttpClient with Polly
- **Testing**: xUnit, Moq, FluentAssertions
- **Validation**: FluentValidation

### AI-Assisted Development
- **Reference Code**: Original implementation preserved in `/reference-implementation`
- **Documentation**: Comprehensive examples and patterns for AI code generation
- **Porting Guides**: Detailed notes on language-specific conversions
- **Test Cases**: Extensive sample data for validation

---

## AI-Assisted Development Guide

### Reference Implementation Directory Structure

The `/reference-implementation` directory should contain:

1. **original-source/**
   - Complete source code from the original CLASSIC tool (the actual implementation)
   - Any existing crash log analysis implementations
   - Original regex patterns and matching logic as implemented

2. **algorithms/**
   - Key algorithms extracted from original source with detailed comments
   - Score calculation methods with examples
   - Priority/severity determination logic explained
   - Example: "How stack occurrence counting works with [3|pattern] syntax"

3. **patterns/**
   - Crash patterns extracted and documented from the original implementation
   - Examples of successful matches and non-matches for each pattern
   - Edge cases and special handling requirements
   - Pattern precedence and conflict resolution rules

4. **sample-logs/**
   - Variety of real crash logs (anonymized)
   - Logs demonstrating each type of crash pattern
   - Malformed logs for error handling testing
   - Different Buffout 4 versions

5. **sample-reports/**
   - Expected output for each sample log
   - Different report format examples
   - Reports showing various severity levels

6. **porting-notes.md**
   ```markdown
   # Porting Notes
   
   ## Key Conversions
   - Python dict → C# Dictionary<TKey, TValue>
   - Python regex → .NET Regex (note: slight syntax differences)
   - File I/O patterns
   - String manipulation differences
   
   ## Critical Algorithms
   - Pattern matching state machine
   - Stack trace parsing logic
   - Severity calculation formula
   
   ## Performance Considerations
   - Original implementation bottlenecks
   - Suggested C# optimizations
   - Memory usage patterns
   ```

7. **README.md**
   ```markdown
   # Reference Implementation Guide
   
   This directory contains reference materials for AI-assisted porting to C#.
   
   ## Quick Start for AI Assistants
   1. Review `/original-source` for implementation logic
   2. Check `/algorithms` for detailed explanations
   3. Use `/patterns` for test case generation
   4. Validate against `/sample-logs` and `/sample-reports`
   
   ## Key Files to Review First
   - `algorithms/pattern_matching.py` - Core matching logic
   - `patterns/crash_signatures.json` - All crash patterns
   - `porting-notes.md` - Language-specific conversions
   
   ## Testing Your Implementation
   - Each sample log should produce matching report
   - Pattern matching should handle all edge cases
   - Performance should meet targets in main README
   ```

This structure ensures AI coding assistants have all necessary context for accurate code generation during the porting process.

---

## Success Criteria

1. **Performance**: Analyze a crash log in < 5 seconds
2. **Accuracy**: 95%+ accuracy in identifying known crash patterns
3. **Reliability**: 99.9% uptime for API
4. **Scalability**: Handle 1000+ concurrent analyses
5. **Usability**: Clear, actionable reports that users can understand

---

## Risk Mitigation

1. **Log Format Changes**: Design flexible parser that can adapt
2. **Large Log Files**: Implement streaming and chunked processing
3. **Pattern Complexity**: Use efficient pattern matching algorithms
4. **API Abuse**: Implement rate limiting and authentication
5. **Data Privacy**: Never store personally identifiable information
6. **Reference Code Security**: Ensure all crash logs in `/reference-implementation` are properly anonymized

This implementation plan provides a comprehensive roadmap for building ClassicLib as a robust, scalable solution for crash log analysis.