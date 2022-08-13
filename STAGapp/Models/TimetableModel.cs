using STAGapp.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace STAGapp.Models
{
    public static class TimetableModel
    {
        public static async Task<rozvrh> GetTimetable(string userToken, string personalNum)
        {
            HttpClient http = Globals.httpClient;
            http.DefaultRequestHeaders.Clear();
            byte[] tokenEncodedBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:", userToken));
            string tokenEncoded = System.Convert.ToBase64String(tokenEncodedBytes);
            http.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", tokenEncoded));

            HttpResponseMessage response = await http.GetAsync(String.Format("{0}/services/rest2/rozvrhy/getRozvrhByStudent?osCislo={1}", Globals.webServiceURL, personalNum));

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                System.Console.WriteLine("Error with getting timetable");
                throw new ArgumentException("Wrong data.");
            }
            System.Console.WriteLine("Timetable OK");

            string xmlContent = await response.Content.ReadAsStringAsync();

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(rozvrh));
            rozvrh actions;
            using (Stream reader = new FileStream("testData.xml", FileMode.Open))
            {
                actions = (rozvrh)serializer.Deserialize(reader);
            }
            //StagTimetable actions = (StagTimetable)reader.Deserialize(GenerateStreamFromString(xmlContent));


            return actions;
        }

        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();

            stream.Position = 0;
            return stream;
        }

        public static rozvrhovaAkce[,] getStableTimetable(rozvrh timeTable)
        {
            rozvrhovaAkce[,] eventsByDates = new rozvrhovaAkce[5, Globals.timetableStartingHours.Length];

            foreach (rozvrhovaAkce timetableEvent in timeTable.rozvrhovaAkce)
            {
                if (timetableEvent.den != null)
                {
                    int hourIndex = getStartingHourIndex(timetableEvent.hodinaSkutOd);
                    int dayIndex = Array.IndexOf(Globals.workdayStrings, timetableEvent.den);
                    eventsByDates[dayIndex, hourIndex] = timetableEvent;
                }
            }

            return eventsByDates;
        }

        private static int getStartingHourIndex(string inputHour)
        {
            string[] inputHourParts = inputHour.Split(':');
            TimeSpan inputHoursTimeSpan = new TimeSpan(int.Parse(inputHourParts[0]), int.Parse(inputHourParts[1]), 0);

            double lowestDifference = double.MaxValue;
            string differenceString = "";

            for(int i = 0; i < Globals.timetableStartingHours.Length; i++)
            {
                string[] startingHourParts = Globals.timetableStartingHours[i].Split(':');
                TimeSpan startingHoursTimeSpan = new TimeSpan(int.Parse(startingHourParts[0]), int.Parse(startingHourParts[1]), 0);

                double timeDifference = Math.Abs(inputHoursTimeSpan.Subtract(startingHoursTimeSpan).TotalMilliseconds);
                if(timeDifference < lowestDifference)
                {
                    lowestDifference = timeDifference;
                    differenceString = Globals.timetableStartingHours[i];
                }
            }
            return Array.IndexOf(Globals.timetableStartingHours, differenceString);

        }
    }
}
