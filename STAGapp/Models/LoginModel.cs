using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using STAGapp;

namespace STAGapp.Models
{
    public static class LoginModel
    {
        /// <summary>
        /// With given credentials tries to log user into STAG service and gets its access token.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <exception cref="LoginFailedException">Target webserver can't login with given credentials. (e.g. wrong username or password)</exception>
        /// <returns>Access token for given user.</returns>
        public static async Task<string> LoginUserAsync(string username, string password)
        {
            byte[] credentialsEncodedBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", username, password));
            string credentialsEncoded = System.Convert.ToBase64String(credentialsEncodedBytes);

            HttpClient http = Globals.httpClient;
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", credentialsEncoded));

            HttpResponseMessage response = await http.GetAsync(Globals.webServiceURL+"/login?basic=1");

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new LoginFailedException(response.ReasonPhrase);
            }

            HttpClientHandler client = Globals.httpHandler;
            var cookies = client.CookieContainer.GetCookies(new System.Uri(Globals.webServiceURL));

            Cookie token = cookies.Cast<Cookie>().First(x => x.Name == "WSCOOKIE");


            return token.Value;
        }
    }

    public class LoginFailedException : Exception
    {
        public LoginFailedException(string message)
            : base(message)
        {
        }
    }
}
