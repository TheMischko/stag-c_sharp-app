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
    /// Interakční logika pro TimetablePage.xaml
    /// </summary>
    public partial class TimetablePage : Page
    {
        private StagLoginTicket ticket;
        public TimetablePage(StagLoginTicket ticket)
        {
            InitializeComponent();
            InitializeTimeTableDataGrid();
            this.ticket = ticket;
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            rozvrh timetable = await TimetableModel.GetTimetable(ticket.Token, ticket.StagUserInfo[0].OsCislo);
            rozvrhovaAkce[,] eventsForCurrentWeek = TimetableModel.getStableTimetable(timetable);

            TimeTableGrid.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                rozvrhovaAkce[] row = new rozvrhovaAkce[Globals.timetableStartingHours.Length];
                for (int j = 0; j < Globals.timetableStartingHours.Length; j++)
                {
                    row[j] = eventsForCurrentWeek[i, j];
                }
                
                TimeTableGrid.Items.Add(row);
            }
        }

        private void InitializeTimeTableDataGrid()
        {
            for (int i = 0; i < Globals.timetableStartingHours.Length; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn();

                column.Header = String.Format("{0} - {1}", Globals.timetableStartingHours[i], Globals.timetableEndingHours[i]);
                column.Binding = new Binding(String.Format("[{0}].predmet", i));

                TimeTableGrid.Columns.Add(column);
            }
                
        }
    }
}
