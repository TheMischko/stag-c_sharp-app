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
using STAGapp.Pages;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

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

        public Notifier Notifier => new Notifier(config => {
            config.PositionProvider = new WindowPositionProvider(
                parentWindow: this,
                corner: Corner.BottomCenter,
                offsetX: 0,
                offsetY: 10
            );

            config.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5)
            );

            config.Dispatcher = Application.Current.Dispatcher;
        });

        public void NavigateToTimetablePage(StagLoginTicket loginTicket)
        {
            Main.Content = new TimetablePage(loginTicket);
        }

        public void NavigateToLectureRecordingsPage(rozvrhovaAkce timetableEvent, TimetablePage timetablePage)
        {
            Main.Content = new LectureRecordingsPage(timetableEvent, timetablePage);
        }

        public void NavigateToUploadPresentationPage(Page previousPage, rozvrhovaAkce timetableEvent) {
            Main.Content = new UploadPresentationPage(previousPage, timetableEvent);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new LoginPage();
        }
    }
}
