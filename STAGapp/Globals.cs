using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace STAGapp
{
    public static class Globals
    {
        public static readonly HttpClientHandler httpHandler = new HttpClientHandler();
        public static readonly HttpClient httpClient = new HttpClient(httpHandler);

        public static readonly string cookiePath = "https://stag-ws.tul.cz";
        public static readonly string webServiceURL = "https://stag-ws.tul.cz/ws";
    }
}
