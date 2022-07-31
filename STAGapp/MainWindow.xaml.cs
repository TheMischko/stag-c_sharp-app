using STAGapp.DataClasses;
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

        public void NavigateToTimetablePage(StagLoginTicket loginTicket)
        {
            Main.Content = new TimetablePage(loginTicket);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new LoginPage();
        }
    }
}
