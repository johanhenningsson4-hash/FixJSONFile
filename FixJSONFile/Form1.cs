using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace FixJSONFile
{
    public partial class Form1 : Form
    {
        private readonly JsonFixer _jsonFixer;

        public Form1()
        {
            InitializeComponent();
            _jsonFixer = new JsonFixer();
            InitializeFormSettings();
        }

        private void InitializeFormSettings()
        {
            this.Text = "JSON File Fixer";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            openFileDialog1.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            openFileDialog1.Title = "Select JSON File to Fix";

            saveFileDialog1.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Save Fixed JSON File";
            saveFileDialog1.DefaultExt = "json";

            UpdateStatus("Ready", Color.Gray);
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtInputFile.Text = openFileDialog1.FileName;

                if (string.IsNullOrWhiteSpace(txtOutputFile.Text) || txtOutputFile.Text == "c:\\temp\\corrected.json")
                {
                    string directory = Path.GetDirectoryName(openFileDialog1.FileName);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    txtOutputFile.Text = Path.Combine(directory, fileNameWithoutExt + "_fixed.json");
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInputFile.Text))
            {
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(txtInputFile.Text);
                saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(txtInputFile.Text) + "_fixed.json";
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputFile.Text = saveFileDialog1.FileName;
            }
        }

        private void btnFixFile_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            string inputPath = txtInputFile.Text;
            string outputPath = txtOutputFile.Text;

            try
            {
                btnFixFile.Enabled = false;
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                UpdateStatus("Processing...", Color.Orange);
                Application.DoEvents();

                _jsonFixer.FixJsonFile(inputPath, outputPath);

                progressBar.Visible = false;
                UpdateStatus("✓ File fixed successfully!", Color.Green);

                MessageBox.Show(
                    $"JSON file has been fixed and saved to:\n{outputPath}", 
                    "Success", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information
                );
            }
            catch (ArgumentException ex)
            {
                HandleError("Invalid Input", ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                HandleError("File Not Found", $"The input file could not be found:\n{ex.FileName}");
            }
            catch (DirectoryNotFoundException ex)
            {
                HandleError("Directory Not Found", ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                HandleError("Access Denied", "You do not have permission to access the specified file.");
            }
            catch (JsonException ex)
            {
                string errorMsg = $"Could not parse JSON after fixes:\n{ex.Message}\n\nA debug file has been saved.";
                try
                {
                    string raw = File.ReadAllText(inputPath);
                    string cleaned = _jsonFixer.FixJsonString(raw);
                    _jsonFixer.SaveDebugOutput(cleaned, outputPath);
                    errorMsg += $"\n\nDebug file location:\n{Path.Combine(Path.GetDirectoryName(outputPath), "debug_" + Path.GetFileName(outputPath))}";
                }
                catch
                {
                    // Ignore debug save errors
                }
                HandleError("JSON Parse Error", errorMsg);
            }
            catch (IOException ex)
            {
                HandleError("File I/O Error", $"An error occurred while reading or writing the file:\n{ex.Message}");
            }
            catch (Exception ex)
            {
                HandleError("Unexpected Error", $"An unexpected error occurred:\n{ex.Message}");
            }
            finally
            {
                btnFixFile.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtInputFile.Text))
            {
                MessageBox.Show(
                    "Please select an input file.", 
                    "Validation Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                txtInputFile.Focus();
                return false;
            }

            if (!File.Exists(txtInputFile.Text))
            {
                MessageBox.Show(
                    "The input file does not exist.", 
                    "Validation Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                txtInputFile.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtOutputFile.Text))
            {
                MessageBox.Show(
                    "Please specify an output file path.", 
                    "Validation Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                txtOutputFile.Focus();
                return false;
            }

            return true;
        }

        private void HandleError(string title, string message)
        {
            progressBar.Visible = false;
            UpdateStatus($"✗ {title}", Color.Red);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void UpdateStatus(string message, Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;
        }
    }
}
