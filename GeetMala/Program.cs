using System;
using System.Configuration; // Required for ConfigurationManager
using System.Windows.Forms;

namespace MusicPlayerWinForms
{
    static class Program
    {
        // Your API key from Google Developer Console.
        public static string YouTubeApiKey { get; private set; } = "";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load API key from app.config
            LoadApiKeyFromConfig();

            Application.Run(new MainForm(YouTubeApiKey));
        }

        static void LoadApiKeyFromConfig()
        {
            try
            {
                // Read the API key from the AppSettings section of the config file.
                string apiKey = ConfigurationManager.AppSettings["YouTubeApiKey"];

                if (!string.IsNullOrEmpty(apiKey))
                {
                    YouTubeApiKey = apiKey;
                }
                else
                {
                    MessageBox.Show("YouTubeApiKey is missing from app.config!", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Consider exiting the application or using a default/prompting the user.
                    // For now, it will run with an empty API key, which will likely cause errors.
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show($"Error reading app.config: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Handle configuration errors, maybe exit or use default.
            }
        }
    }
}