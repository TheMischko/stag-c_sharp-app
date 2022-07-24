using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace STAGapp.Models
{
    public static class TimetableModel
    {
        public static async Task<string> GetTimetable(string userToken, string personalNum)
        {
            HttpClient http = Globals.httpClient;
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", userToken));

            HttpResponseMessage response = await http.GetAsync(String.Format("{0}/services/rest2/rozvrhy/getRozvrhByStudent?osCislo={1}", Globals.webServiceURL, personalNum));

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                System.Console.WriteLine("Error with getting timetable");
                throw new ArgumentException("Wrong data.");
            }
            System.Console.WriteLine("Timetable OK");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
