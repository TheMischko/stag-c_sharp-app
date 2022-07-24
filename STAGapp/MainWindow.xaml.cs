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
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = "alexandr.broz";
            string password = "izobaba9898";
            try
            {
                string result = await LoginModel.LoginUserAsync(username, password);
                System.Console.WriteLine(result);
            } catch(LoginFailedException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
