# ClassicLib Client-Server Implementation Plan

## Overview
Port the ClassicLib crash log analysis functionality from Python to a C# client-server architecture, starting with core features and building incrementally.

## Current Assets
- **ClassicServer**: .NET 8.0 ASP.NET Core skeleton with Swagger
- **ClassicClient**: .NET 8.0 client project skeleton
- **Python Implementation**: CLASSIC-Fallout4 repository with working log scanner
- **YAML Configuration**: CLASSIC Fallout4.yaml with crash patterns and mod definitions
- **Sample Logs & Reports**: Example crash logs and their generated reports (located in `sample_logs/`)

---

## Phase 1: Core Data Models & YAML Parser

### 1.1 Shared Data Models
- [x] Create shared library project `ClassicLib.Shared`
- [x] Define core models:
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

### 1.2 YAML Configuration System
- [x] Add YamlDotNet NuGet package
- [x] Create YAML models matching Python structure:
  ```csharp
  public class ClassicYamlConfig {
      public GameInfo Game_Info { get; set; }
      public Dictionary<string, CrashPattern> Crashlog_Error_Check { get; set; }
      public Dictionary<string, ModInfo> Mods_Core { get; set; }
  }
  ```
- [x] Implement YAML loader service
- [x] Add unit tests for YAML parsing

---

## Phase 2: Log Parser Implementation

### 2.1 Basic Parser
- [ ] Port `Parser.py` functionality to C#:
  - [ ] `extract_segments()` - Extract log sections
  - [ ] `parse_crash_header()` - Parse header info
  - [ ] `extract_module_names()` - Extract DLL/module names
- [ ] Handle Buffout 4 log format
- [ ] Add error handling for malformed logs

### 2.2 Plugin Analyzer
- [ ] Port `PluginAnalyzer.py`:
  - [ ] Plugin list extraction
  - [ ] Load order parsing
  - [ ] Plugin filtering logic
- [ ] Implement regex patterns for plugin detection

---

## Phase 3: Analysis Engine

### 3.1 Pattern Matching
- [ ] Port crash pattern matching from Python:
  - [ ] Error pattern checking (`Crashlog_Error_Check`)
  - [ ] Stack pattern checking (`Crashlog_Stack_Check`)
  - [ ] Conditional patterns (ME-REQ, ME-OPT, NOT)
- [ ] Implement occurrence counting (e.g., `[3|pattern]`)

### 3.2 Core Analyzers
- [ ] Port `SuspectScanner.py` - Identify crash suspects
- [ ] Port `RecordScanner.py` - Extract named records
- [ ] Port `PluginAnalyzer.py` - Analyze mod conflicts
- [ ] Create `IAnalyzer` interface for consistency

### 3.3 Analysis Orchestrator
- [ ] Port `ScanOrchestrator.py`:
  - [ ] Coordinate all analyzers
  - [ ] Aggregate results
  - [ ] Handle analysis workflow

---

## Phase 4: Report Generation

### 4.1 Report Generator
- [ ] Port `ReportGenerator.py`:
  - [ ] Generate markdown headers
  - [ ] Format suspect sections
  - [ ] Create plugin warnings
  - [ ] Add footer with tips
- [ ] Match existing report format exactly

### 4.2 Report Storage
- [ ] Implement file naming: `crash-{timestamp}-AUTOSCAN.md`
- [ ] Add in-memory storage for now
- [ ] Plan for future database integration

---

## Phase 5: Server API

### 5.1 Basic Endpoints
- [ ] Update ClassicServer with endpoints:
  ```csharp
  [HttpPost("api/analyze")]
  public async Task<IActionResult> AnalyzeCrashLog(IFormFile logFile)
  
  [HttpGet("api/reports/{id}")]
  public async Task<IActionResult> GetReport(string id)
  
  [HttpGet("api/health")]
  public IActionResult HealthCheck()
  ```

### 5.2 Analysis Service
- [ ] Create `CrashLogAnalysisService`:
  - [ ] Accept crash log upload
  - [ ] Run analysis pipeline
  - [ ] Generate report
  - [ ] Return analysis ID
- [ ] Add async processing support

### 5.3 Configuration Management
- [ ] Load YAML config on startup
- [ ] Add config reload endpoint (dev only)
- [ ] Cache parsed configuration

---

## Phase 6: Simple CLI Client

### 6.1 Basic Commands
- [ ] Implement using System.CommandLine:
  ```bash
  classiclib analyze <crash-log.txt>
  classiclib get-report <report-id>
  ```
- [ ] Add file upload functionality
- [ ] Display analysis results

### 6.2 Output Formatting
- [ ] Console output for analysis summary
- [ ] Save full report to file
- [ ] Add progress indicators

---

## Phase 7: Testing & Validation

### 7.1 Unit Tests
- [ ] Parser tests with sample logs
- [ ] Pattern matching tests
- [ ] YAML parsing tests
- [ ] Report generation tests

### 7.2 Integration Tests
- [ ] End-to-end analysis flow
- [ ] API endpoint tests
- [ ] Client-server communication

### 7.3 Validation
- [ ] Compare outputs with Python implementation
- [ ] Test with all sample crash logs
- [ ] Verify report format matches

---

## Phase 8: Performance & Polish

### 8.1 Optimization
- [ ] Add caching for repeated analyses
- [ ] Implement parallel processing where applicable
- [ ] Optimize regex patterns

### 8.2 Error Handling
- [ ] Graceful handling of malformed logs
- [ ] Clear error messages
- [ ] Logging throughout pipeline

### 8.3 Documentation
- [ ] API documentation (Swagger)
- [ ] Client usage guide
- [ ] Configuration guide

---

## Future Enhancements (Post-MVP)

- [ ] Database storage (PostgreSQL/SQLite)
- [ ] Web UI dashboard
- [ ] Batch analysis support
- [ ] Discord bot integration
- [ ] FormID database lookups
- [ ] Advanced caching with Redis
- [ ] Real-time analysis updates via SignalR

---

## Key Implementation Notes

### Pattern Syntax Examples
From the YAML config, patterns use special syntax:
- `ME-REQ:` - Must exist (required)
- `ME-OPT:` - Optional match
- `NOT:` - Must not exist
- `[3|pattern]` - Pattern must occur 3+ times

### Report Structure
Standard report includes:
1. Header with version info
2. Mod compatibility warnings
3. Crash suspect analysis
4. Plugin suspects list
5. FormID suspects (if applicable)
6. Named records found
7. Footer with tips and links

### Critical Files to Reference
- `ScanOrchestrator.py` - Main workflow
- `ReportGenerator.py` - Report formatting
- `CLASSIC Fallout4.yaml` - All patterns/configs
- Sample crash logs for testing

---

## Success Metrics
- [ ] Correctly analyze sample crash logs
- [ ] Generate reports matching Python output
- [ ] API response time < 5 seconds
- [ ] Support concurrent analyses
- [ ] 95%+ pattern matching accuracy