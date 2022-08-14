using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace STAGapp
{
    public struct BorderSettings
    {
        public BorderSettings(Thickness thickness, Brush brush)
        {
            this.thickness = thickness;
            this.brush = brush;
        }
        public Thickness thickness {get; set;}
        public Brush brush { get; set; }
    }
    public static class Globals
    {
        public static readonly HttpClientHandler httpHandler = new HttpClientHandler();
        public static readonly HttpClient httpClient = new HttpClient(httpHandler);

        public static readonly string cookiePath = "https://stag-ws.tul.cz";
        public static readonly string webServiceURL = "https://stag-ws.tul.cz/ws";

        public static readonly string[] timetableStartingHours = { "7:00", "7:50", "8:50", "9:40", "10:40", "11:30", "12:30", "13:20", "14:20", "15:10", "16:10", "17:00", "18:00", "18:50" };
        public static readonly string[] timetableEndingHours = { "7:45", "8:35", "9:35", "10:25", "11:25", "12:15", "13:15", "14:05", "15:05", "15:55", "16:55", "17:45", "18:45", "19:35" };
        public static readonly string[] workdayStrings = { "Pondělí", "Úterý", "Středa", "Čtvrtek", "Pátek" };
        public static readonly BorderSettings timetableBorderSettings = new BorderSettings(new Thickness(1), Brushes.DarkGray);
    }
}
