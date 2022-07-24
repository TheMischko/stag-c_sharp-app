using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAGapp.DataClasses
{
    public struct StagUserInfo
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string RoleNazev { get; set; }
        public string Fakulta { get; set; }
        public string Katedra { get; set; }
        public string UcitIdno { get; set; }
        public string Aktivni { get; set; }
        public string OsCislo { get; set; }
        public string Email { get; set; }

    }
}
