using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace FixJSONFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFixFile_Click(object sender, EventArgs e)
        {

            var inputPath = txtInputFile.Text;
            var outputPath = txtOutputFile.Text;

            var raw = File.ReadAllText(inputPath);

            // 1. Ta bort radbrytningar som bryter JSON
            string clean = raw.Replace("\r", "").Replace("\n", "");

            // 2. Fix splittrade ord i keys (t.ex. lTechLayerA ccessID)
            clean = Regex.Replace(clean, @"([a-zA-Z])\s+([a-zA-Z])", "$1$2");

            // 3. Säkerställ att keys är korrekt citerade
            clean = Regex.Replace(clean, @"(?<!\""|\w)([a-zA-Z0-9_]+)(?=\s*:)", "\"$1\"");

            // 4. Ta bort ev. whitespace inne i strängar som splittrats
            //clean = Regex.Replace(clean, "\"\\s+\"", "\"\"");

            // 5. Fixa extra kommatecken (edge case)
            clean = Regex.Replace(clean, @",\s*}", "}");
            clean = Regex.Replace(clean, @",\s*]", "]");

            clean = clean.Replace("\u00F6", "ö");

            try
            {
                // 6. Validera + parse JSON
                var jsonDoc = JsonDocument.Parse(clean);

                // 7. Skriv ut snygg formatterad JSON
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                var jsonString = JsonSerializer.Serialize(jsonDoc, options);
                File.WriteAllText(outputPath, jsonString, Encoding.UTF8);

                Console.WriteLine("✅ JSON korrigerad och sparad till: " + outputPath);
                MessageBox.Show("✅ JSON korrigerad och sparad till: " + outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Kunde inte parsa JSON:");
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                // Debughjälp – dumpa halvfixad data
                File.WriteAllText("debug_output.txt", clean);
            }

        }
    }
}
