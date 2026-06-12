using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FixJSONFile
{
    public class JsonFixer
    {
        public class FixOptions
        {
            public bool RemoveLineBreaks { get; set; } = true;
            public bool FixSplitWords { get; set; } = true;
            public bool QuoteKeys { get; set; } = true;
            public bool RemoveTrailingCommas { get; set; } = true;
            public bool FixCharacterEncoding { get; set; } = true;
        }

        private readonly FixOptions _options;

        public JsonFixer() : this(new FixOptions())
        {
        }

        public JsonFixer(FixOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public string FixJsonString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input JSON string cannot be null or empty.", nameof(input));
            }

            string clean = input;

            // 1. Remove line breaks that break JSON structure
            if (_options.RemoveLineBreaks)
            {
                clean = clean.Replace("\r", "").Replace("\n", "");
            }

            // 2. Fix split words in keys (e.g., "TechLayerA ccessID" -> "TechLayerAccessID")
            if (_options.FixSplitWords)
            {
                clean = Regex.Replace(clean, @"([a-zA-Z])\s+([a-zA-Z])", "$1$2");
            }

            // 3. Ensure keys are properly quoted
            if (_options.QuoteKeys)
            {
                clean = Regex.Replace(clean, @"(?<!""|\\w)([a-zA-Z0-9_]+)(?=\s*:)", "\"$1\"");
            }

            // 4. Fix trailing commas (edge case)
            if (_options.RemoveTrailingCommas)
            {
                clean = Regex.Replace(clean, @",\s*}", "}");
                clean = Regex.Replace(clean, @",\s*]", "]");
            }

            // 5. Fix character encoding issues
            if (_options.FixCharacterEncoding)
            {
                clean = clean.Replace("\\ö", "ö");
            }

            return clean;
        }

        public void FixJsonFile(string inputPath, string outputPath)
        {
            ValidateInputPath(inputPath);
            ValidateOutputPath(outputPath);

            string raw = File.ReadAllText(inputPath);
            string cleaned = FixJsonString(raw);

            // Validate and parse JSON
            JsonDocument jsonDoc = JsonDocument.Parse(cleaned);

            // Write formatted JSON
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(jsonDoc, options);
            File.WriteAllText(outputPath, jsonString, Encoding.UTF8);

            jsonDoc.Dispose();
        }

        public void SaveDebugOutput(string content, string outputPath)
        {
            string debugPath = Path.Combine(
                Path.GetDirectoryName(outputPath) ?? Environment.CurrentDirectory,
                "debug_" + Path.GetFileName(outputPath)
            );
            File.WriteAllText(debugPath, content, Encoding.UTF8);
        }

        private void ValidateInputPath(string inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                throw new ArgumentException("Input file path cannot be empty.", nameof(inputPath));
            }

            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException("Input file does not exist.", inputPath);
            }
        }

        private void ValidateOutputPath(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
            {
                throw new ArgumentException("Output file path cannot be empty.", nameof(outputPath));
            }

            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"Output directory does not exist: {directory}");
            }
        }
    }
}
