using System.Windows;
using STAGapp.Models;
using ToastNotifications.Messages;

namespace STAGapp {
    public partial class MediasiteLoginWindow : Window {

        public string login;
        public string password;
        MainWindow mainWindow;
        public MediasiteLoginWindow(MainWindow mainWindow) {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        async void MediasiteLoginButton_OnClick(object sender, RoutedEventArgs e) {
            login = MediasiteLoginTextBox.Text;
            password = MediasitePasswordBox.Password;

            CredentialAuth auth = new CredentialAuth(login, password);
            bool isLoginValid = await MediaSiteModel.Authenticate(auth);

            if (isLoginValid) {
                Globals.MediasiteAuth = auth;
                this.Close();
            }
            else {
                mainWindow.Notifier.ShowError("Neplatné přihlašovací údaje pro Mediasite.");
            }

        }
    }
}