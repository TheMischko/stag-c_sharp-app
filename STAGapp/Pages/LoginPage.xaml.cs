using STAGapp.DataClasses;
using STAGapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using STAGApp.Forms;
using ToastNotifications.Messages;

namespace STAGapp
{
    /// <summary>
    /// Interakční logika pro LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page {
        public LoginPage()
        {
            InitializeComponent();
#if DEBUG
            CheatTextBox.Visibility = Visibility.Visible;
            #endif
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            
            string username = usernameTextBox.Text;
            MainWindow window = (MainWindow)Window.GetWindow(this);
            
            ErrorTextBlock.Visibility = Visibility.Hidden;
            if (username.Length == 0)
            {
                window.Notifier.ShowError("Neplatné uživatelské jméno.");
                Mouse.OverrideCursor = null;
                return;
            }
            string password = passwordTextBox.Password;
            if (password.Length == 0)
            {
                window.Notifier.ShowError("Neplatné heslo.");
                Mouse.OverrideCursor = null;
                return;
            }
            try
            {
                // Successful log-in
                StagLoginTicket result = await LoginModel.LoginUserAsync(username, password);
                UserModel.SetUser(result);
                window.NavigateToTimetablePage(result);
                window.Notifier.ShowSuccess("Přihlášení bylo úspěšné.");
                Mouse.OverrideCursor = null;
            }
            catch (LoginFailedException ex)
            {
                window.Notifier.ShowError("Neplatné přihlašovací údaje.");
                Mouse.OverrideCursor = null;
                return;
            }
            Mouse.OverrideCursor = null;
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Visibility = Visibility.Visible;
            ErrorTextBlock.Text = message;
        }

        void CalendarButton_OnClick(object sender, RoutedEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            FormKalendar formKalendar = new FormKalendar();
            window.ShowForm(formKalendar);
        }

        void MeetButton_OnClick(object sender, RoutedEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            FormPrezentovat formPrezentovat = new FormPrezentovat();
            window.ShowForm(formPrezentovat);
        }

        void PlayerButton_OnClick(object sender, RoutedEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            FormPlayer formPlayer = new FormPlayer();
            window.ShowForm(formPlayer);
        }
    }
}
