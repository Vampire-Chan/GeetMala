using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerWinForms
{
    public static class OAuthLoginManager
    {
        // Replace these values with your registered application's details.
        private static readonly string clientId = "YOUR_CLIENT_ID";
        private static readonly string redirectUri = "http://localhost:5000/";
        private static readonly string authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private static readonly string scope = "email profile"; // Adjust scopes as needed.

        public static async Task<string> PerformLoginAsync()
        {
            // Construct the authorization URL.
            string authorizationUrl = $"{authorizationEndpoint}?response_type=code" +
                                      $"&scope={Uri.EscapeDataString(scope)}" +
                                      $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                                      $"&client_id={Uri.EscapeDataString(clientId)}";

            // Open the system default browser.
            Process.Start(new ProcessStartInfo(authorizationUrl) { UseShellExecute = true });

            // Set up a local HTTP listener on the redirect URI.
            using (var http = new HttpListener())
            {
                http.Prefixes.Add(redirectUri);
                http.Start();

                // Wait for the incoming request from the OAuth provider.
                var context = await http.GetContextAsync();
                var request = context.Request;

                // Respond with a simple message.
                string responseString = "<html><body>Login successful. You can close this window.</body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();

                // Extract the authorization code.
                string code = request.QueryString["code"];
                http.Stop();

                if (string.IsNullOrEmpty(code))
                {
                    return null;
                }

                // In production, exchange the code for an access token. For now, return the code.
                return code;
            }
        }
    }
}