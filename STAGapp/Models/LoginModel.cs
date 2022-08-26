using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using STAGapp;
using STAGapp.DataClasses;

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
        public static async Task<StagLoginTicket> LoginUserAsync(string username, string password)
        {
            byte[] credentialsEncodedBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", username, password));
            string credentialsEncoded = System.Convert.ToBase64String(credentialsEncodedBytes);

            HttpClient http = Globals.httpClient;
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", credentialsEncoded));

            HttpResponseMessage response = await http.GetAsync("https://stag-ws.tul.cz/ws/login?originalURL=https%3A%2F%2Fwww.google.com&basic=1");

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new LoginFailedException(response.ReasonPhrase);
            }

            string[] uriParsedParts = response.RequestMessage.RequestUri.Query.Split(new char[] { '?', '&' });
            uriParsedParts = uriParsedParts.Where(val => val.Length > 0).ToArray();
            Dictionary<string, string> userData = new Dictionary<string, string>();

            for(int i = 0; i < uriParsedParts.Length; i++)
            {
                string[] parts = uriParsedParts[i].Split('=');
                userData.Add(parts[0], parts[1]);
            }

            string decodedUserInfoHash = HttpUtility.UrlDecode(userData["stagUserInfo"]);
            byte[] base64UserInfo = System.Convert.FromBase64String(decodedUserInfoHash);
            string userInfoJSON = System.Text.Encoding.UTF8.GetString(base64UserInfo);
            StagLoginTicket ticket = JsonConvert.DeserializeObject<StagLoginTicket>(userInfoJSON);
            ticket.Token = userData["stagUserTicket"];

            return ticket;
        }

        public static void LoginMediasiteAsync(string username, string password)
        {

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
