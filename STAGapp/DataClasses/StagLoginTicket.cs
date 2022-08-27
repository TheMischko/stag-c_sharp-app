using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAGapp.DataClasses
{
    public struct StagLoginTicket
    {
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string TitulPred { get; set; }
        public string TitulZa { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public StagUserInfo[] StagUserInfo { get; set; }
}
}
