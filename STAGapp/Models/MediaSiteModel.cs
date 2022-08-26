using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace STAGapp.Models
{
    public static class MediaSiteModel
    {
        private static string API_key = "";
        private static string login = "";
        private static string password = "";
        private static string AuthTicket = "";

        static MediaSiteModel()
        {
            API_key = ConfigurationManager.AppSettings["MediaSite_API_key"];
            HttpClient http = Globals.httpClient;
        }

        private static HttpClient CreateRequest()
        {
            HttpClient http = Globals.httpClient;
            http.DefaultRequestHeaders.Clear();

            if (API_key.Length > 0) http.DefaultRequestHeaders.Add("sfapikey", API_key);
            if (AuthTicket.Length > 0) http.DefaultRequestHeaders.Add("SfAuthTicket", AuthTicket);

            return http;
        }

        public static async Task<bool> Authenticate(IMediasiteAuth authentication)
        {
            HttpClient http = CreateRequest();
            authentication.AddAuthentication(http);
            HttpResponseMessage response = await http.GetAsync(String.Format("{0}/Presentations", Globals.mediasiteAPIUrl));
            return response.StatusCode != HttpStatusCode.Unauthorized && response.StatusCode != HttpStatusCode.Forbidden;
        } 
    }
    /// <summary>
    /// Interface over various types of possible ways how to get access to Mediasite operations.
    /// </summary>
    public interface IMediasiteAuth {
        /// <summary>
        /// Adds authentication header into the HTTP request.
        /// </summary>
        /// <param name="client">HTTP request</param>
        void AddAuthentication(HttpClient client);
    }
    
    /// <summary>
    /// Basic authentication that operates with Mediasite user login and password.
    /// </summary>
    public class CredentialAuth : IMediasiteAuth {
        string username;
        string password;
        /// <summary>
        /// Creates authentication instance that operates with Mediasite user login and password.
        /// </summary>
        /// <param name="username">Mediasite username</param>
        /// <param name="password">Mediasite password</param>
        public CredentialAuth(string username, string password) {
            this.username = username;
            this.password = password;
        }
        
        public void AddAuthentication(HttpClient client) {
            byte[] credentialsEncodedBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", username, password));
            string credentialsEncoded = System.Convert.ToBase64String(credentialsEncodedBytes);
            client.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", credentialsEncoded));
        }
    }
    
    /// <summary>
    /// Basic authentication that operates with access token.
    /// </summary>
    public class TokenAuth : IMediasiteAuth {
        string token;
        /// <summary>
        /// Creates authorization instance that works with token of already logged in user.
        /// </summary>
        /// <param name="token">MediasiteAuth token</param>
        public TokenAuth(string token) {
            this.token = token;
        }

        public void AddAuthentication(HttpClient client) {
            byte[] credentialsEncodedBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:", token));
            string credentialsEncoded = System.Convert.ToBase64String(credentialsEncodedBytes);
            client.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", credentialsEncoded));
        }
    }
}
