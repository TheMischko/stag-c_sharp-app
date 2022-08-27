using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FTPClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace STAGapp.Models
{
    public static class MediaSiteModel
    {
        public static event EventHandler<FileTransferingArgs> ChunkUploaded;
        public static event EventHandler UploadCompleted;
        
        private static string API_key;
        private static string API_host;
        private static string AuthTicket;

        static MediaSiteModel()
        {
            API_key = ConfigurationManager.AppSettings["MediaSite_API_key"];
            API_host = ConfigurationManager.AppSettings["MediaSite_host"];
            HttpClient http = Globals.httpClient;
        }
        
        /// <summary>
        /// Tries if given authentication method will result to success when operating with mediasite API.
        /// </summary>
        /// <param name="authentication">Mediasite authentication.</param>
        /// <returns>True if authentication is valid.</returns>
        public static async Task<bool> Authenticate(IMediasiteAuth authentication)
        {
            HttpClient http = CreateRequest();
            authentication.AddAuthentication(http);
            HttpResponseMessage response = await http.GetAsync(String.Format("{0}/Presentations", API_host));
            return response.StatusCode != HttpStatusCode.Unauthorized && response.StatusCode != HttpStatusCode.Forbidden;
        }
        
        /// <summary>
        /// Creates new instance of presentation on mediasite storage.
        /// </summary>
        /// <param name="auth">Mediasite authenticator.</param>
        /// <param name="title">Title of new presentation.</param>
        /// <param name="description">Description of new presentation.</param>
        /// <returns>Representation of newly created presentation.</returns>
        public static async Task<JObject> CreatePresentation(IMediasiteAuth auth,string title, string description) {
            HttpClient request = CreateRequest();
            auth.AddAuthentication(request);

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("title", title),
                new KeyValuePair<string, string>("description", description)
            });
            HttpResponseMessage response = await request.PostAsync(
                new Uri(String.Format("{0}/{1}", API_host, "Presentations")), content);
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject presentation = JsonConvert.DeserializeObject<JObject>(responseBody);
            return presentation;
        }
        
        /// <summary>
        /// Event-based method that asynchronously uploads file to Mediasite storage.
        /// <br/>
        /// After each chunk of data is uploaded <b>ChunkUploaded</b> event is raised.<br/>
        /// After whole file is uploaded <b>UploadCompleted</b> event is raised.
        /// </summary>
        /// <param name="auth">Mediasite authenticator.</param>
        /// <param name="presentation">Presentation detail object. (must contains #upload attribute that tells the absolute path to file)</param>
        /// <param name="presentationId">ID of the presentation.</param>
        /// <param name="pathToFile">Path of file that should be uploaded.</param>
        /// <param name="newFile">Relative path to place where the new file should be created.</param>
        public static void UploadFile(IMediasiteAuth auth, JObject presentation, string presentationId, string pathToFile, string newFileRelative)
        {
            Thread uploadThread = new Thread(() => UploadPresentation(auth, presentation, presentationId, pathToFile, newFileRelative));
            uploadThread.Start();
        }

        /// <summary>
        /// Connects uploaded file with created presentation.
        /// </summary>
        /// <param name="auth">Mediasite authenticator.</param>
        /// <param name="presentationId">ID of existing presentation.</param>
        /// <param name="uploadedFile">Relative path to uploaded file at Mediasite storage.</param>
        public static async void AttachFileToPresentation(IMediasiteAuth auth, string presentationId, string uploadedFile) {
            HttpClient request = CreateRequest();
            auth.AddAuthentication(request);
            
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("Filename", uploadedFile)
            });
            HttpResponseMessage response = await request.PostAsync(
                new Uri(String.Format("{0}/Presentations('{1}')/CreateMediaUpload", API_host, presentationId)), content);
        }
        
        /// <summary>
        /// Creates new request with Mediasite additional headers.
        /// </summary>
        /// <returns></returns>
        private static HttpClient CreateRequest()
        {
            HttpClient http = Globals.httpClient;
            http.DefaultRequestHeaders.Clear();

            if (String.IsNullOrEmpty(API_key)) http.DefaultRequestHeaders.Add("sfapikey", API_key);
            if (String.IsNullOrEmpty(AuthTicket)) http.DefaultRequestHeaders.Add("SfAuthTicket", AuthTicket);

            return http;
        }
        
        /// <summary>
        /// Uploads file to mediasite storage.
        /// <br/>
        /// After each buffer is uploaded <b>ChunkUploaded</b> event is raised.<br/>
        /// After whole file is uploaded <b>UploadCompleted</b> event is raised.
        /// </summary>
        /// <param name="auth">Mediasite authenticator.</param>
        /// <param name="presentation">Presentation detail object. (must contains #upload attribute that tells the absolute path to file)</param>
        /// <param name="presentationId">ID of the presentation.</param>
        /// <param name="pathToFile">Path of file that should be uploaded.</param>
        /// <param name="newFile">Relative path to place where the new file should be created.</param>
        private static async void UploadPresentation(IMediasiteAuth auth, JObject presentation, string presentationId, string pathToFile, string newFile) {
            JObject uploadDetails = presentation["#Upload"] as JObject;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(
                Path.Combine((uploadDetails["target"] as JValue).Value.ToString(), newFile)
            );
            request.Method = WebRequestMethods.Http.Post;
            auth.AddAuthentication(request);
            
            Stream inputStream = await request.GetRequestStreamAsync();
            FileStream fs = new FileStream(pathToFile, FileMode.Open);
            
            byte[] buffer = new byte[1024];
            double total = (double)fs.Length;
            int byteRead = 0;
            double read = 0;
            do
            {
                byteRead = fs.Read(buffer, 0, 1024);
                inputStream.Write(buffer, 0, byteRead);
                read += (double)byteRead;
                double percentage = read / total * 100;

                FileTransferingArgs args =  new FileTransferingArgs();
                args.Percentage = percentage;
                ChunkUploaded?.Invoke(null, args);
            } while (byteRead != 0);

            fs.Close();
            inputStream.Close();

            AttachFileToPresentation(auth, presentationId, newFile);
            
            UploadCompleted?.Invoke(null, EventArgs.Empty);
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
        /// <summary>
        /// Adds authentication header into the HTTP request.
        /// </summary>
        /// <param name="client">HTTP request</param>
        void AddAuthentication(HttpWebRequest client);
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

        public void AddAuthentication(HttpWebRequest client) {
            byte[] credentialsEncodedBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", username, password));
            string credentialsEncoded = System.Convert.ToBase64String(credentialsEncodedBytes);
            client.Headers.Add(HttpRequestHeader.Authorization, String.Format("Basic {0}", credentialsEncoded));
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
        
        public void AddAuthentication(HttpWebRequest client) {
            byte[] credentialsEncodedBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:", token));
            string credentialsEncoded = System.Convert.ToBase64String(credentialsEncodedBytes);
            client.Headers.Add(HttpRequestHeader.Authorization, String.Format("Basic {0}", credentialsEncoded));
        }
    }
}
