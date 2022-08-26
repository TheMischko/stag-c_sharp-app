using STAGapp.Controls;
using STAGapp.DataClasses;
using STAGapp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interakční logika pro TimetablePage.xaml
    /// </summary>
    public partial class TimetablePage : Page
    {
        private StagLoginTicket ticket;
        private string selectedYear = "";
        private string selectedSemestr = "";
        private List<TimeTableCell> cells;
        public TimetablePage(StagLoginTicket ticket)
        {
            InitializeComponent();
            InitializeTimeTableDataGrid();
            this.ticket = ticket;
            this.cells = new List<TimeTableCell>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            selectedYear = now.Month >= 9 ? now.Year.ToString() : (now.Year - 1).ToString();
            selectedSemestr = now.Month >= 9 ? "zs" : "ls";

            //TODO: If student then hide
            if (false) {
                MediasiteStatusGrid.Visibility = Visibility.Hidden;
            }
            
            LoadNewCalendar();
        }

        private async void LoadNewCalendar()
        {
            string authToken = UserModel.GetAuthToken();
            bool isTeacher = UserModel.IsUserInRole(Roles.Teacher);
            string userID = UserModel.GetIDByRole(isTeacher ? Roles.Teacher : Roles.Student);

            rozvrh timetable = await TimetableModel.GetTimetable(authToken, userID, selectedYear, selectedSemestr, isTeacher ? Roles.Teacher : Roles.Student);
            rozvrhovaAkce[,] eventsForCurrentWeek = TimetableModel.getStableTimetable(timetable);

            foreach(TimeTableCell cell in cells)
            {
                TimetableGrid.Children.Remove(cell);
            }

            for (int i = 0; i < Globals.workdayStrings.Length; i++)
            {
                for (int j = 0; j < Globals.timetableStartingHours.Length; j++)
                {
                    if (eventsForCurrentWeek[i, j] == null) continue;

                    TimeTableCell timeTableCell = new TimeTableCell();
                    int colSpan = timeTableCell.InitByEvent(eventsForCurrentWeek[i, j], j + 2, i + 2);
                    timeTableCell.TimetableEvent = eventsForCurrentWeek[i, j];
                    timeTableCell.MouseClickHandlerFunc = EventCellClickHandler;
                    j += colSpan - 1;
                    cells.Add(timeTableCell);
                    TimetableGrid.Children.Add(timeTableCell);
                }
            }
        }

        private void EventCellClickHandler(object sender, MouseButtonEventArgs e)
        {
            TimeTableCell timeTableCell = (TimeTableCell)sender;
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.NavigateToLectureRecordingsPage(timeTableCell.TimetableEvent, this);
        }

        private void InitializeTimeTableDataGrid()
        {
            TimetableGrid.Children.Clear();
            // Add margin column
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(10);
            TimetableGrid.ColumnDefinitions.Add(columnDefinition);

            // Add day name column
            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(50);
            TimetableGrid.ColumnDefinitions.Add(columnDefinition);

            // Add column for each timetable hour
            for (int i = 0; i < Globals.timetableStartingHours.Length; i++)
            {
                columnDefinition = new ColumnDefinition();
                columnDefinition.Width = GridLength.Auto;
                columnDefinition.MinWidth = 80;
                TimetableGrid.ColumnDefinitions.Add(columnDefinition);

                // For each hour add Textblock to header row
                Border timeBorder = new Border();
                double topBorder = Globals.timetableBorderSettings.thickness.Top;
                double leftBorder = i == 0 ?  Globals.timetableBorderSettings.thickness.Left : 0;
                double rightBorder = Globals.timetableBorderSettings.thickness.Right;
                double bottomBorder = 0;
                timeBorder.BorderThickness = new Thickness(leftBorder, topBorder, rightBorder, bottomBorder);
                timeBorder.BorderBrush = Globals.timetableBorderSettings.brush;

                TextBlock timeblock = new TextBlock();
                timeblock.TextAlignment = TextAlignment.Center;
                timeblock.VerticalAlignment = VerticalAlignment.Center;
                timeblock.HorizontalAlignment = HorizontalAlignment.Center;
                timeblock.Text = String.Format("{0} - {1}", Globals.timetableStartingHours[i], Globals.timetableEndingHours[i]);
                timeBorder.Child = timeblock;

                TimetableGrid.Children.Add(timeBorder);
                Grid.SetColumn(timeBorder, i + 2);
                Grid.SetRow(timeBorder, 1);
            }

            // Add margin column
            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(10);
            TimetableGrid.ColumnDefinitions.Add(columnDefinition);

            TimetableGrid.Width = ((Globals.timetableStartingHours.Length+1) * 100) + 20;


            // Margin row
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(10);
            TimetableGrid.RowDefinitions.Add(rowDefinition);

            // Table header row
            rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(50);
            TimetableGrid.RowDefinitions.Add(rowDefinition);

            for (int i = 0; i < Globals.workdayStrings.Length; i++)
            {
                // Add row for each workday
                rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(50);
                TimetableGrid.RowDefinitions.Add(rowDefinition);

                // For each day add Textblock to header col
                Border workDayBorder = new Border();
                double topBorder = Globals.timetableBorderSettings.thickness.Top;
                double leftBorder = Globals.timetableBorderSettings.thickness.Left;
                double rightBorder = Globals.timetableBorderSettings.thickness.Right;
                double bottomBorder = i == Globals.workdayStrings.Length - 1 ? Globals.timetableBorderSettings.thickness.Bottom : 0;
                workDayBorder.BorderThickness = new Thickness(leftBorder, topBorder, rightBorder, bottomBorder);
                workDayBorder.BorderBrush = Globals.timetableBorderSettings.brush;

                TextBlock dayblock = new TextBlock();
                dayblock.Text = Globals.workdayStrings[i];
                dayblock.TextAlignment = TextAlignment.Center;
                dayblock.VerticalAlignment = VerticalAlignment.Center;
                dayblock.HorizontalAlignment = HorizontalAlignment.Center;
                workDayBorder.Child = dayblock;

                TimetableGrid.Children.Add(workDayBorder);
                Grid.SetColumn(workDayBorder, 1);
                Grid.SetRow(workDayBorder, i + 2);
            }

            // Add margin row
            rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(10);
            TimetableGrid.RowDefinitions.Add(rowDefinition);


            for (int i = 0; i < Globals.timetableStartingHours.Length; i++)
            {
                for (int j = 0; j < Globals.workdayStrings.Length; j++)
                {
                    Border emptyCell = new Border();

                    double topBorder = Globals.timetableBorderSettings.thickness.Top;
                    double leftBorder = 0;
                    double rightBorder = Globals.timetableBorderSettings.thickness.Right;
                    double bottomBorder = j == Globals.workdayStrings.Length-1 ? Globals.timetableBorderSettings.thickness.Bottom : 0;
                    emptyCell.BorderThickness = new Thickness(leftBorder, topBorder, rightBorder, bottomBorder);

                    emptyCell.BorderBrush = Globals.timetableBorderSettings.brush;
                    Grid.SetRow(emptyCell, j + 2);
                    Grid.SetColumn(emptyCell, i + 2);
                    TimetableGrid.Children.Add(emptyCell);
                }
            }
        }

        private void YearComboBox_ValueChanged(object sender, EventArgs e)
        {
            ComboBoxItem yearComboBox = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            selectedYear = (string)yearComboBox.Tag;
            LoadNewCalendar();
        }

        private void SemestrComboBox_ValueChanged(object sender, EventArgs e)
        {
            ComboBoxItem semestrComboBox = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            selectedSemestr = (string)semestrComboBox.Tag;
            LoadNewCalendar();
        }

        void MediasiteLoginTextBlock_OnMouseUp(object sender, MouseButtonEventArgs e) {
            MediasiteLoginWindow msLoginWindow = new MediasiteLoginWindow();
            msLoginWindow.Closing += MediasiteLoginWindow_Closing;
            msLoginWindow.Show();
        }

        private void MediasiteLoginWindow_Closing(object sender, CancelEventArgs e) {
            MediasiteLoginWindow msLoginWindow = (MediasiteLoginWindow) sender;
            string msLogin = msLoginWindow.login;
            string msPassword = msLoginWindow.password;
        }
    }
}
