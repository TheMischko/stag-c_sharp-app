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
    /// Interakční logika pro LectureRecordingsPage.xaml
    /// </summary>
    public partial class LectureRecordingsPage : Page
    {
        private rozvrhovaAkce timetableEvent;
        private Page previousPage;
        public LectureRecordingsPage(rozvrhovaAkce timetableEvent, TimetablePage previousPage)
        {
            this.timetableEvent = timetableEvent;
            this.previousPage = previousPage;
            InitializeComponent();
            lectureTitle.Text = timetableEvent.nazev;
            
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.Main.Content = this.previousPage;
        }
    }
}
