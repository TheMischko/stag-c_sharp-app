using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using FTPClient;
using Microsoft.Win32;
using STAGapp.DataClasses;
using STAGapp.Models;

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
            TitleTextBlock.Text = String.Format("Nahrát novou přednášku k předmětu {0}", "Test");
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

        void UploadButton_OnClick(object sender, RoutedEventArgs e) {
            string file = (string)FileSelectDialogButton.Content;
            string title = PresentationTitleTextBox.Text;
            string description = PresentationDescriptionTextBox.Text;
            DateTime date = DateTime.Now;
            if (PresentationDatePicker.SelectedDate != null) {
                date = (DateTime) PresentationDatePicker.SelectedDate;
            }

            string folderName = CreateFolderName(this.timetableEvent);
            string fileName = CreateFileName(this.timetableEvent, date, title);

            string extension = file.Split('.').Last();

            string newFullFileName = String.Format("{0}.{1}", fileName, extension);
            
            UploadProgressBar.Visibility = Visibility.Visible;
            UploadProgressBarText.Visibility = Visibility.Visible;
            
            if ((bool) FtpRadioButton.IsChecked) {
                //Use FTP
                FTPModel.ChunkUploaded += OnChunkUploadedHandler;
                FTPModel.UploadCompleted += OnFileUploadedHandler;
                FTPModel.CreateFolder(folderName);
                FTPModel.UploadFile(file, Path.Combine(folderName, newFullFileName));
            }
            else {
                //Use HTTP
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
    }
}