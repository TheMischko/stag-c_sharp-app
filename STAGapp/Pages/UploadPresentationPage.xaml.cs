using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using FTPClient;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using STAGapp.DataClasses;
using STAGapp.Models;
using ToastNotifications.Messages;

namespace STAGapp.Pages {
    public partial class UploadPresentationPage : Page {
        Page previousPage;
        rozvrhovaAkce timetableEvent;
        public UploadPresentationPage(Page previousPage, rozvrhovaAkce timetableEvent) {
            InitializeComponent();
            this.previousPage = previousPage;
            this.timetableEvent = timetableEvent;
        }

        void UploadPresentationPage_OnLoaded(object sender, RoutedEventArgs e) {
            TitleTextBlock.Text = String.Format("Nahrát novou přednášku k předmětu {0}", timetableEvent.nazev);
            FtpRadioButton.IsEnabled = Globals.useMediasite;
            HttpRadioButton.Visibility = Globals.useMediasite ? Visibility.Hidden : Visibility.Visible;
        }

        void FileSelectDialogButton_OnClick(object sender, RoutedEventArgs e) {
            Button fileSelectButton = (Button) sender;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
                "Video|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV|Vše|*.*";
            if (openFileDialog.ShowDialog() == true) {
                fileSelectButton.Content = openFileDialog.FileName;
            }
        }

        async void UploadButton_OnClick(object sender, RoutedEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            // Check access to Mediasite.
            if (Globals.MediasiteAuth == null) {
                window.Notifier.ShowWarning("Nejste přihlášen k Mediasite.");
                return;
            }
            // Get values from inputs.
            string file = (string)FileSelectDialogButton.Content;
            string title = PresentationTitleTextBox.Text;
            string description = PresentationDescriptionTextBox.Text;
            DateTime date = DateTime.Now;
            if (PresentationDatePicker.SelectedDate != null) {
                date = (DateTime) PresentationDatePicker.SelectedDate;
            }
            //TODO: Input validation

            //Get target folder name.
            string folderName = CreateFolderName(this.timetableEvent);
            // Get new filename.
            string fileName = CreateFileName(this.timetableEvent, date, title);
            // Get extenstion of MIME type of uploaded file.
            string extension = file.Split('.').Last();
            // Combine paths to get full relative path on target storage.
            string newFullFileName = String.Format("{0}.{1}", fileName, extension);
            string newFullFilePath = Path.Combine(folderName, newFullFileName);
            // Show progress bar and text.
            UploadProgressBar.Visibility = Visibility.Visible;
            UploadProgressBarText.Visibility = Visibility.Visible;

            try {
                // Create new record for new presentation in Mediashare database.
                JObject presentation =
                    await MediaSiteModel.CreatePresentation(Globals.MediasiteAuth, title, description);
                // Get newly created record's ID.
                string presentationId = (presentation["id"] as JValue).Value.ToString();
                // Upload file via chosen method.
                if ((bool) FtpRadioButton.IsChecked) {
                    //Use FTP
                    FTPModel.ChunkUploaded += OnChunkUploadedHandler;
                    FTPModel.UploadCompleted += OnFileUploadedHandler;
                    FTPModel.CreateFolder(folderName);
                    FTPModel.UploadFile(file, newFullFilePath);
                }
                else {
                    //Use HTTP
                    MediaSiteModel.ChunkUploaded += OnChunkUploadedHandler;
                    MediaSiteModel.UploadCompleted += OnFileUploadedHandler;
                    MediaSiteModel.UploadFile(Globals.MediasiteAuth, presentation, presentationId, file,
                        newFullFilePath);
                }
                // Link file to presentation record at Mediashare database.
                MediaSiteModel.AttachFileToPresentation(Globals.MediasiteAuth, presentationId, newFullFilePath);
            }
            catch (Exception ex) {
                window.Notifier.ShowError("Chyba během nahrávání souboru.");
                window.Notifier.ShowError(ex.Message);
            }
        }

        void OnChunkUploadedHandler(object sender, FileTransferingArgs e) {
            Application.Current.Dispatcher.Invoke(() => {
                UploadProgressBar.Value = e.Percentage;
                UploadProgressBarText.Text = String.Format("{0:0.0}%", e.Percentage);
            });
        }

        void OnFileUploadedHandler(object sender, EventArgs e) {
            Application.Current.Dispatcher.Invoke(() => {
                UploadProgressBar.Value = 100;
                UploadProgressBarText.Text = "Úspěšně nahráno";
            });
        }

        /// <summary>
        /// Creates folder name by special pattern.<br/>
        /// Example of folder name: <br/>
        /// KEK_MA1 - Makroekonomie I - ales_kocourek 2021_2022
        /// </summary>
        /// <param name="timetableEvent">Event or subject that corresponds to created folder.</param>
        /// <returns>Folder name</returns>
        static string CreateFolderName(rozvrhovaAkce timetableEvent) {
            StringBuilder sb = new StringBuilder();
            sb.Append(timetableEvent.katedra + "_" + timetableEvent.predmet);
            if(timetableEvent.nazev.Length > 7) sb.Append(" - " + RemoveDiacritics(timetableEvent.nazev));
            sb.Append(" - ");
            sb.Append(UserModel.GetUser().Username.Replace('.', '_'));
            sb.Append(" ");
            sb.Append(timetableEvent.rok);
            sb.Append("_");
            sb.Append(timetableEvent.rok+1);

            return sb.ToString();
        }
        /// <summary>
        /// Creates folder name by special pattern.<br/>
        /// Example of folder name: <br/>
        /// KEK_MA1 02_21 – Makroekonomie a ČR - ales_kocourek 2021_2022
        /// </summary>
        /// <param name="timetableEvent">Event or subject that corresponds to created presentation.</param>
        /// <param name="date">Record date of the presentation.</param>
        /// <param name="title">Title of the presentation. If none is given subject title is used instead.</param>
        /// <returns></returns>
        static string CreateFileName(rozvrhovaAkce timetableEvent, DateTime date, string title) {
            StringBuilder sb = new StringBuilder();
            sb.Append(timetableEvent.katedra + "_" + timetableEvent.predmet);
            sb.Append(" ");
            sb.AppendFormat("{0:D2}_{1:D2}", date.Month, date.Day);
            if (!String.IsNullOrEmpty(title)) {
                sb.Append(" - " + RemoveDiacritics(title));
            }
            else {
                if(timetableEvent.nazev.Length > 7) sb.Append(" - " + RemoveDiacritics(timetableEvent.nazev));
            }
            sb.Append(" - ");
            sb.Append(UserModel.GetUser().Username.Replace('.', '_'));
            sb.Append(" ");

            sb.Append(timetableEvent.rok);
            sb.Append("_");
            sb.Append(timetableEvent.rok+1);
            
            return sb.ToString();
        }
        static string RemoveDiacritics(string text) 
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        void ReturnBackButton_OnClick(object sender, RoutedEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.Main.Content = this.previousPage;
        }
    }
}