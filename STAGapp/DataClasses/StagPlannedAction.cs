using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace STAGapp.DataClasses
{
    [Serializable, XmlRoot("rozvrhovaAkce")]
    public struct StagPlannedAction
    {
        [XmlElement(ElementName = "roakIdno")]
        public int RoakIdno { get; set; }
        /*
        [XmlElement(ElementName = "nazev")]
        public string nazev { get; set; }

        [XmlElement(ElementName = "katedra")]
        public string katedra { get; set; }

        [XmlElement(ElementName = "predmet")]
        public string predmet { get; set; }

        [XmlElement(ElementName = "statut")]
        public string statut { get; set; }

        [XmlElement(ElementName = "rok")]
        public string rok { get; set; }

        [XmlElement(ElementName = "planObsazeni")]
        public string planObsazeni { get; set; }

        [XmlElement(ElementName = "obsazeni")]
        public string obsazeni { get; set; }

        [XmlElement(ElementName = "typAkce")]
        public string typAkce { get; set; }

        [XmlElement(ElementName = "typAkceZkr")]
        public string typAkceZkr { get; set; }

        [XmlElement(ElementName = "semestr")]
        public string semestr { get; set; }

        [XmlElement(ElementName = "platnost")]
        public string platnost { get; set; }

        [XmlElement(ElementName = "pocetVyucHodin")]
        public string pocetVyucHodin { get; set; }

        [XmlElement(ElementName = "hodinaSkutOd")]
        public string hodinaSkutOd { get; set; }

        [XmlElement(ElementName = "hodinaSkutDo")]
        public string hodinaSkutDo { get; set; }

        [XmlElement(ElementName = "tydenOd")]
        public string tydenOd { get; set; }

        [XmlElement(ElementName = "tydenDo")]
        public string tydenDo { get; set; }

        [XmlElement(ElementName = "tyden")]
        public string tyden { get; set; }

        [XmlElement(ElementName = "tydenZkr")]
        public string tydenZkr { get; set; }

        [XmlElement(ElementName = "jeNadrazena")]
        public string jeNadrazena { get; set; }

        [XmlElement(ElementName = "maNadrazenou")]
        public string maNadrazenou { get; set; }

        [XmlElement(ElementName = "kontakt")]
        public string kontakt { get; set; }

        [XmlElement(ElementName = "druhAkce")]
        public string druhAkce { get; set; }

        [XmlElement(ElementName = "ucitIdno")]
        public string ucitIdno { get; set; }

        [XmlElement(ElementName = "ucitel")]
        public StagTeacher ucitel { get; set; }

        [XmlElement(ElementName = "vsichniUciteleUcitIdno")]
        public string vsichniUciteleUcitIdno { get; set; }

        [XmlElement(ElementName = "vsichniUciteleJmenaTituly")]
        public string vsichniUciteleJmenaTituly { get; set; }

        [XmlElement(ElementName = "vsichniUciteleJmenaTitulySPodily")]
        public string vsichniUciteleJmenaTitulySPodily { get; set; }

        [XmlElement(ElementName = "vsichniUcitelePrijmeni")]
        public string vsichniUcitelePrijmeni { get; set; }

        [XmlElement(ElementName = "referencedIdno")]
        public string referencedIdno { get; set; }

        [XmlElement(ElementName = "owner")]
        public string owner { get; set; }*/
    }
}
