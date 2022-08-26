using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace FTPClient
{
    public struct FtpSettings
    {
        public string HostServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public static class FTPModel
    {
        public static event EventHandler<FileTransferingArgs> ChunkUploaded;
        public static event EventHandler<FileTransferingArgs> ChunkDownloaded;
        public static event EventHandler UploadCompleted;
        public static event EventHandler DownloadCompleted;

        private static FtpSettings settings = new FtpSettings();
        static FTPModel()
        {
            
            settings.HostServer = ConfigurationManager.AppSettings["FTP_host"];
            settings.Username = ConfigurationManager.AppSettings["FTP_username"];
            settings.Password = ConfigurationManager.AppSettings["FTP_password"];
        }

        public static async Task<string> ListFolderAsync(string folder = "")
        {
            FtpWebRequest request = CreateFTPRequest(folder, WebRequestMethods.Ftp.ListDirectory);
            FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();

            Stream ftpStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(ftpStream);

            string result = await reader.ReadToEndAsync();

            reader.Close();
            response.Close();

            return result;
        }

        public static bool CreateFolder(string folderPath)
        {
            try
            {
                FtpWebRequest request = CreateFTPRequest(folderPath, WebRequestMethods.Ftp.MakeDirectory);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();

                return true;
            }
            catch(WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if(response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return true;
                } else
                {
                    response.Close();
                    return false;
                }
            }
        }

        public static async Task RenameFileAsync(string pathToFile, string newFilename)
        {
            FtpWebRequest request = CreateFTPRequest(pathToFile, WebRequestMethods.Ftp.Rename);
            request.RenameTo = newFilename;

            FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
            response.Close();
        }

        public static void UploadFile(string pathToFile, string targetFileName)
        {
            Thread uploadThread = new Thread(() => UploadFileHandler(pathToFile, targetFileName));
            uploadThread.Start();
        }

        public static void DownloadFile(string pathToFile, string pathToNewFile)
        {
            Thread downloadThread = new Thread(() => DownloadFileHandler(pathToFile, pathToNewFile));
            downloadThread.Start();
        }

        public static async Task<bool> DeleteFile(string pathToFile)
        {
            try
            {
                FtpWebRequest request = CreateFTPRequest(pathToFile, WebRequestMethods.Ftp.DeleteFile);
                FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
                response.Close();
                return true;
            } 
            catch(WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if(response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return true;
                }
                else
                {
                    response.Close();
                    return false;
                }
            }
        }

        private static FtpWebRequest CreateFTPRequest(string root, string method)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                String.Format("{0}/{1}",
                new Uri(settings.HostServer),
                root)
            );
            request.Method = method;
            request.Credentials = new NetworkCredential(settings.Username, settings.Password);

            return request;
        }

        private static async void UploadFileHandler(string pathToFile, string targetFileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                String.Format("{0}/{1}",
                new Uri(settings.HostServer),
                targetFileName)
            );

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(settings.Username, settings.Password);

            Stream inputStream = await request.GetRequestStreamAsync();
            FileStream fs = File.OpenRead(pathToFile);

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
            UploadCompleted?.Invoke(null, EventArgs.Empty);
        }

        private static async void DownloadFileHandler(string pathToFile, string pathToNewFile)
        {
            FtpWebRequest sizeRequest = CreateFTPRequest(pathToFile, WebRequestMethods.Ftp.GetFileSize);
            int size = (int)sizeRequest.GetResponse().ContentLength;

            FtpWebRequest request = CreateFTPRequest(pathToFile, WebRequestMethods.Ftp.DownloadFile);
            FtpWebResponse response = (FtpWebResponse )await request.GetResponseAsync();

            Stream downloadStream = response.GetResponseStream();
            FileStream fileStream = File.Create(pathToNewFile);

            byte[] buffer = new byte[1024];
            int read = 0;

            while((read = downloadStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, read);
                double percentage = (double)fileStream.Position/ (double)size*100;
                FileTransferingArgs args = new FileTransferingArgs
                {
                    Percentage = percentage
                };
                ChunkDownloaded?.Invoke(null, args);
            }

            downloadStream.Close();
            fileStream.Close();
            response.Close();

            DownloadCompleted?.Invoke(null, EventArgs.Empty);
        }

        public class FileTransferingArgs : EventArgs
        {
            public double Percentage { get; set; }
        }
    }
}
