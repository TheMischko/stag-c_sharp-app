using System.Windows;

namespace STAGapp {
    public partial class MediasiteLoginWindow : Window {

        public string login;
        public string password;
        public MediasiteLoginWindow() {
            InitializeComponent();
        }

        void MediasiteLoginButton_OnClick(object sender, RoutedEventArgs e) {
            login = MediasiteLoginTextBox.Text;
            password = MediasitePasswordBox.Password;
            //TODO: Add login validation
            this.Close();
        }
    }
}