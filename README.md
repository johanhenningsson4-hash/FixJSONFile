# FixJSONFile

A Windows Forms application to fix and clean malformed JSON files.

## Description

This utility helps repair JSON files that have been corrupted by:
- Line breaks in the middle of JSON structures
- Split words in property keys
- Unquoted property keys
- Extra commas before closing brackets
- Character encoding issues

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
2. Select the input JSON file path
3. Select the output location for the fixed JSON file
4. Click "Fix File" to process and repair the JSON
5. The application will validate the output as proper JSON

## Building

1. Open `FixJSONFile.sln` in Visual Studio
2. Restore NuGet packages
3. Build the solution (F6)
4. Run the application (F5)

## Features

- Removes line breaks that break JSON structure
- Fixes split words in property keys
- Ensures property keys are properly quoted
- Removes trailing commas before closing brackets
- Handles character encoding issues

## License

[Specify your license here]

## Author

[Your name/organization]
