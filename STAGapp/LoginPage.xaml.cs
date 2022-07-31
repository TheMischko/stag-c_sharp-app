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

namespace STAGapp
{
    /// <summary>
    /// Interakční logika pro LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent(); 
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Visibility = Visibility.Hidden;
            //string username = "alexandr.broz";
            //string password = "izobaba9898";
            string username = usernameTextBox.Text;
            if (username.Length == 0)
            {
                ShowError("Neplatné uživatelské jméno.");
                return;
            }
            string password = passwordTextBox.Password;
            if (password.Length == 0)
            {
                ShowError("Neplatné heslo.");
                return;
            }
            try
            {
                // Successful log-in
                StagLoginTicket result = await LoginModel.LoginUserAsync(username, password);
                MainWindow window = (MainWindow)Window.GetWindow(this);
                window.NavigateToTimetablePage(result);
                System.Console.WriteLine(result);
            }
            catch (LoginFailedException ex)
            {
                System.Console.WriteLine(ex.Message);
                ShowError("Neplatné přihlašovací údaje.");
                return;
            }
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Visibility = Visibility.Visible;
            ErrorTextBlock.Text = message;
        }
    }
}
