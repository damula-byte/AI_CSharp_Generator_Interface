using System.Data;
using System.Text;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis;
using System.Text.RegularExpressions;   
using System.IO;

namespace GeminiC__App
{
    public partial class Form1 : Form
    {
        /*
        private const string ApiKey = "AIzaSyChVB52MO6q_GWG9Po4CCOq7YZPN_UlaTE";
        private const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=" + ApiKey;
        */

        private const string ApiKey = "AIzaSyCGuJvxNXXMrdOUXCp4l6LZwtT5p_nkkUs";
        private const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + ApiKey;
        

        private string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeneratedScript.csx"); // Application directory
        // Autosize Prep
        private Size originalFormSize;
        private Rectangle originalbtnGenerate;
        private Rectangle originalbtnExecute;
        private Rectangle originalbtnShow;
        private Rectangle originalrtbOutput;
        private Rectangle originaltxtPrompt;
        private Rectangle originalrtbRun;
        private Rectangle originallblPerformance;

        private float originalGenerateFont;
        private float originalExecuteFont;
        private float originalShowFont;
        private float originalLabelFont;

        public Form1()
        {
            InitializeComponent();  // Things are displayed on the form y default
            lblPerformance.Text = "";
            rtbOutput.Text = " Welcome user, please input a Prompt!\n" +
                             "------------------------------------------------------------------------------------------------\n " +
                             "Default: The generated C# program will not use Class or Method. Please manually request if needed.\n" +
                             "------------------------------------------------------------------------------------------------\n " +
                             "Generate: Call Gemini\n " +
                             "Execute: Run the script\n " +
                             "Show: Display existing file content";
            // Saving the element's orginal size
            originalFormSize = this.Size;
            originalbtnGenerate = new Rectangle(btnGenerate.Location, btnGenerate.Size);
            originalGenerateFont = btnGenerate.Font.Size;
            originalbtnExecute = new Rectangle(btnExecute.Location, btnExecute.Size);
            originalExecuteFont = btnExecute.Font.Size;
            originalbtnShow = new Rectangle(btnOpen.Location, btnOpen.Size);
            originalShowFont = btnOpen.Font.Size;
            originaltxtPrompt = new Rectangle(txtPrompt.Location, txtPrompt.Size);
            originalrtbOutput = new Rectangle(rtbOutput.Location, rtbOutput.Size);
            originalrtbRun = new Rectangle(rtbRun.Location, rtbRun.Size);
            originallblPerformance = new Rectangle(lblPerformance.Location, lblPerformance.Size);
            originalLabelFont = lblPerformance.Font.Size;

            this.Resize += Form1_Resize;

        }
        // Resize the form
        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeControl(btnGenerate, originalbtnGenerate, originalGenerateFont);
            ResizeControl(btnExecute, originalbtnExecute, originalExecuteFont);
            ResizeControl(btnOpen, originalbtnShow, originalShowFont);
            ResizeControl(txtPrompt, originaltxtPrompt);
            ResizeControl(rtbOutput, originalrtbOutput);
            ResizeControl(rtbRun, originalrtbRun);
            ResizeControl(lblPerformance, originallblPerformance, originalLabelFont);
        }
        // Resize the control
        private void ResizeControl(Control control, Rectangle originalRect, float? originalFontSize = null)
        {
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;

            int newX = (int)(originalRect.X * xRatio);
            int newY = (int)(originalRect.Y * yRatio);
            int newWidth = (int)(originalRect.Width * xRatio);
            int newHeight = (int)(originalRect.Height * yRatio);

            control.Location = new Point(newX, newY);
            control.Size = new Size(newWidth, newHeight);

            if (originalFontSize.HasValue)
            {
                float newFontSize = Math.Min(xRatio, yRatio) * originalFontSize.Value;
                control.Font = new Font(control.Font.FontFamily, newFontSize, control.Font.Style);
            }
        }

        // Generate button
        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            lblPerformance.ForeColor = Color.Black;
            if (string.IsNullOrWhiteSpace(txtPrompt.Text))
            {
                MessageBox.Show("Please enter a prompt.");
                return;
            }

            btnGenerate.Enabled = false;
            btnExecute.Enabled = false;
            btnOpen.Enabled = false;
            lblPerformance.Text = "Generating.....";

            string promptHolder = txtPrompt.Text;
            string promptReady;
            if (promptHolder.Contains("class") || promptHolder.Contains("method"))
            {
                promptReady = promptHolder;
            }
            else
            {
                promptReady = promptHolder + " Do not use any Class or Method in the program.";
            }

            var startTime = DateTime.Now;
            var scriptContent = await GenerateScriptFromGemini(promptReady); // Input prompt
            var endTime = DateTime.Now;

            if (!string.IsNullOrEmpty(scriptContent))
            {
                SaveScriptToFile(scriptContent);
                // API response time
                lblPerformance.Text = $"API Response Time: {(endTime - startTime).TotalSeconds:F2} seconds";
                lblPerformance.ForeColor = Color.Green;
            }
            btnGenerate.Enabled = true;
            btnExecute.Enabled = true;
            btnOpen.Enabled = true;
        }

        // Execute button
        private async void btnExecute_Click_1(object sender, EventArgs e)
        {
            lblPerformance.ForeColor = Color.Black;
            btnGenerate.Enabled = false;
            btnExecute.Enabled = false;
            btnOpen.Enabled = false;
            lblPerformance.Text = "Executing.....";
            string result = await ExecuteScript();
            rtbRun.Text = result ?? "No result returned from script execution.";
            lblPerformance.Text = "Execution completed.";
            lblPerformance.ForeColor = Color.Green;
            btnGenerate.Enabled = true;
            btnExecute.Enabled = true;
            btnOpen.Enabled = true;
        }

        // Show button
        private void btnOpen_Click(object sender, EventArgs e)
        {
            lblPerformance.ForeColor = Color.Black;
            btnGenerate.Enabled = false;
            btnExecute.Enabled = false;
            btnOpen.Enabled = false;
            lblPerformance.Text = "Opening...";

            if (File.Exists(scriptPath))
            {
                rtbOutput.Text = "The last program was created:\n------------------------------------\n" + File.ReadAllText(scriptPath);
            }
            else
            {
                rtbOutput.Text = "Script file not found.";
                rtbOutput.ForeColor = Color.Red;
            }
            lblPerformance.Text = "Open complete";
            lblPerformance.ForeColor = Color.Green;
            btnGenerate.Enabled = true;
            btnExecute.Enabled = true;
            btnOpen.Enabled = true;
        }

        // Call Gemini API to generate C# script
        private async Task<string> GenerateScriptFromGemini(string prompt)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var jsonBody = $@"{{
                        ""contents"": [
                            {{
                                ""role"": ""user"",
                                ""parts"": [
                                    {{
                                        ""text"": ""Generate a C# script (.csx) that {prompt}.Include all necessary 'using' statements (e.g., 'using System;') at the top of the code.""
                                    }}
                                ]
                            }}
                        ],
                        ""generationConfig"": {{
                            ""temperature"": 0.7,
                            ""maxOutputTokens"": 4096
                        }}
                    }}";

                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(GeminiApiUrl, content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    string rawScript = jsonResponse.candidates[0].content.parts[0].text.ToString();
                    rawScript = rawScript.Replace("\\n", Environment.NewLine).Replace("\\\"", "\"");
                    return ExtractCSharpCode(rawScript); // Single extraction step
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calling Gemini API: {ex.Message}");
                return null;
            }
        }

        // Extract C# code from the raw Gemini responsec
        private string ExtractCSharpCode(string rawResponse)
        {
            try
            {
                // Match code block between ```csharp or ``` markers
                var match = Regex.Match(rawResponse, @"```(?:csharp)?\s*([\s\S]*?)\s*```");
                if (match.Success)
                {
                    string code = match.Groups[1].Value.Trim();
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        return code; // Return the raw C# code as-is
                    }
                }
                return rawResponse.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error extracting code: {ex.Message}");
                return rawResponse; // Fallback to raw response
            }

        }

        // Save generated script to file
        private void SaveScriptToFile(string scriptContent)
        {
            try
            {
                File.WriteAllText(scriptPath, scriptContent);
                // Display the saved script content in the output box
                rtbOutput.Text = "Script saved to: " + scriptPath + "\n\n" + scriptContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving script: {ex.Message}");
            }
        }

        // Execute script using Roslyn
        private async Task<string> ExecuteScript()
        {
            try
            {
                if (!File.Exists(scriptPath))
                {
                    return "Script file not found.";
                }

                var scriptOptions = ScriptOptions.Default
                                    .WithReferences(AppDomain.CurrentDomain.GetAssemblies()
                                    .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                                    .Select(a => MetadataReference.CreateFromFile(a.Location)))
                                    .WithImports("System.Math", 
                                                 "System.Linq",
                                                 "System.Numerics",
                                                 "System.Collections.Generic");
                string scriptContent = await File.ReadAllTextAsync(scriptPath);
                var originalConsoleOut = Console.Out;
                using var consoleOutput = new StringWriter();
                Console.SetOut(consoleOutput); // Redirect console output to capture it

                try
                {
                    var resultState = await CSharpScript.RunAsync(scriptContent, scriptOptions);

                    // Capture console output and return value

                    consoleOutput.Flush(); // Force flush to capture all output
                    string consoleOutputText = consoleOutput.ToString(); // Get captured console output

                    string result = resultState?.ReturnValue?.ToString() ?? "null";

                    // Combine output and return value
                    string output = "Execution successful.";
                    if (!string.IsNullOrEmpty(consoleOutputText))
                    {
                        return output += $"\nOutput:\n{consoleOutputText}";
                    }
                    if (result != "null")
                    {
                        return output += $"\nReturn Value: {result}";
                    }
                    return output + "\nNo output or value return\n" +
                        "void method had been used !";
                }

                catch (CompilationErrorException ex)
                {
                    return $"Compilation Error: {string.Join("\n", ex.Diagnostics)}";
                }

                catch (Exception ex)
                {
                    return $"Execution executing script: {ex.Message}";
                }
                finally
                {
                    Console.SetOut(originalConsoleOut); // Restore original console output
                }
            }

            catch (Exception ex)
            {
                return $"Execution Error: {ex.Message}";
            }

        }

        private void txtPrompt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}