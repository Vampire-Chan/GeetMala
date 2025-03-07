using System;
using System.Windows.Forms;

namespace MusicPlayerWinForms
{
    static class Program
    {
        // Your API key from Google Developer Console.
        public static string YouTubeApiKey { get; private set; } = "AIzaSyADEBZhml1FHjIk35e1Gee0-sV6PIw0hYo";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(YouTubeApiKey));
        }
    }
}