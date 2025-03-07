using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MusicPlayerWinForms
{
    public partial class LoginForm : Form
    {
        // P/Invoke declaration to retrieve cookies from a URL.
        [DllImport("wininet.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool InternetGetCookieEx(
            string url,
            string cookieName,
            StringBuilder cookieData,
            ref int size,
            int flags,
            IntPtr reserved);

        private const int INTERNET_COOKIE_HTTPONLY = 0x00002000;

        public LoginForm()
        {
            InitializeComponent();
            // Set the login URL – for example, "https://accounts.google.com" for Google login.
            webBrowser.Url = new Uri("https://accounts.google.com");
        }

        // Retrieve cookies from the provided URL using InternetGetCookieEx.
        private string GetUriCookieContainer(Uri uri)
        {
            int size = 512;
            StringBuilder cookieData = new StringBuilder(size);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
            {
                if (size < 0)
                    return null;
                // Allocate larger buffer and try again.
                cookieData = new StringBuilder(size);
                if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
                    return null;
            }
            return cookieData.ToString();
        }

        // When the user clicks the "Finish Login" button, capture cookies and update the global CookieContainer.
        private void finishLoginButton_Click(object sender, EventArgs e)
        {
            // Optionally, you could check if the current URL indicates a successful login.
            Uri currentUri = webBrowser.Url;
            string cookies = GetUriCookieContainer(currentUri);
            if (string.IsNullOrEmpty(cookies))
            {
                MessageBox.Show("No cookies were retrieved - please ensure you are logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Parse the cookie string and add cookies to the global CookieContainer.
            // The cookie string is typically in the format "key1=value1; key2=value2"
            foreach (string cookiePair in cookies.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries))
            {
                int separatorIndex = cookiePair.IndexOf('=');
                if (separatorIndex > 0 && separatorIndex < cookiePair.Length - 1)
                {
                    string name = cookiePair.Substring(0, separatorIndex);
                    string value = cookiePair.Substring(separatorIndex + 1);

                    try
                    {
                        // Add the cookie to the GlobalCookieContainer for the current domain.
                       // Program.GlobalCookieContainer.Add(currentUri, new Cookie(name, value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding cookie " + name + ": " + ex.Message);
                    }
                }
            }

            // Indicate that login was successful.
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}