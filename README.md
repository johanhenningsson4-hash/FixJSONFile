# FixJSONFile

[![Build](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/build-and-test.yml)
[![Release](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/release.yml/badge.svg)](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/release.yml)
[![Code Quality](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/code-quality.yml/badge.svg)](https://github.com/johanhenningsson4-hash/FixJSONFile/actions/workflows/code-quality.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A Windows Forms application to fix and clean malformed JSON files with an intuitive user interface.

## Description

This utility helps repair JSON files that have been corrupted by:
- Line breaks in the middle of JSON structures
- Split words in property keys
- Unquoted property keys
- Extra commas before closing brackets
- Character encoding issues

The application features a clean, user-friendly interface with:
- ✓ Browse buttons for easy file selection
- ✓ Input validation and error handling
- ✓ Real-time status updates
- ✓ Progress indication
- ✓ Detailed error messages with debug output
- ✓ Automatic output file naming

## Requirements

- .NET Framework 4.7.2 or higher
- Visual Studio 2022 or later (for development)
- Windows OS

## Dependencies

- System.Text.Json (10.0.9)
- System.Buffers (4.6.1)
- System.Memory (4.6.3)
- System.IO.Pipelines (10.0.9)
- Microsoft.Bcl.AsyncInterfaces (10.0.9)

## How to Use

1. Launch the application
2. Click "Browse..." next to Input file to select a malformed JSON file
3. The output path will be automatically suggested (or click "Browse..." to choose your own)
4. Click "Fix JSON File" to process and repair the JSON
5. The application will:
   - Validate your inputs
   - Show a progress indicator
   - Parse and fix the JSON
   - Save the formatted output
   - Display success or detailed error messages

## Building

1. Open `FixJSONFile.sln` in Visual Studio
2. Restore NuGet packages (automatic on build)
3. Build the solution (F6 or Ctrl+Shift+B)
4. Run the application (F5)

## Features

### JSON Fixes Applied
- Removes line breaks that break JSON structure
- Fixes split words in property keys (e.g., "TechLayerA ccessID" → "TechLayerAccessID")
- Ensures property keys are properly quoted
- Removes trailing commas before closing brackets
- Handles character encoding issues

### User Interface
- Clean, professional interface
- File browser dialogs with JSON file filtering
- Real-time status updates with color coding
- Progress bar for visual feedback
- Comprehensive input validation
- Detailed error messages
- Debug file output on parsing errors

### Architecture
- Separation of concerns (UI and business logic)
- Robust error handling for all common scenarios
- Configurable fix options (extensible design)
- Professional code structure following best practices

## Error Handling

The application handles various error scenarios:
- Missing or invalid file paths
- Non-existent input files
- Access permission issues
- Invalid JSON syntax (with debug output)
- File I/O errors
- Unexpected errors with detailed messages

When JSON parsing fails after fixes, a debug file is automatically saved with the intermediate result for troubleshooting.

## License

[Specify your license here]

## Author

[Your name/organization]

## Version History

### v2.0 (Current)
- Added browse buttons for file selection
- Implemented comprehensive input validation
- Enhanced error handling with specific error types
- Added status label with color-coded feedback
- Added progress bar for visual feedback
- Separated business logic into JsonFixer class
- Improved UI layout and user experience
- Better debug output with proper file naming
- Automatic output file path suggestion

### v1.0
- Initial release with basic JSON fixing functionality

## CI/CD

This project uses GitHub Actions for continuous integration and deployment. See [CI-CD.md](CI-CD.md) for detailed documentation.

### Available Workflows
- **Build and Test** - Runs on every push/PR
- **Release** - Creates releases from version tags
- **Code Quality** - Analyzes code quality
- **Dependency Check** - Monitors package updates

### Creating a Release
```bash
git tag v2.0.0
git push origin v2.0.0
```

See [CI-CD.md](CI-CD.md) for complete instructions.

