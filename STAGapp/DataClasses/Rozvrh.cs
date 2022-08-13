using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAGapp
{

    // POZNÁMKA: Generovaný kód může vyžadovat alespoň rozhraní .NET Framework 4.5 nebo .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://stag-ws.zcu.cz/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://stag-ws.zcu.cz/", IsNullable = false)]
    public partial class rozvrh
    {

        private rozvrhovaAkce[] rozvrhovaAkceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("rozvrhovaAkce", Namespace = "")]
        public rozvrhovaAkce[] rozvrhovaAkce
        {
            get
            {
                return this.rozvrhovaAkceField;
            }
            set
            {
                this.rozvrhovaAkceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class rozvrhovaAkce
    {

        private uint roakIdnoField;

        private bool roakIdnoFieldSpecified;

        private string nazevField;

        private string katedraField;

        private string predmetField;

        private string statutField;

        private ushort ucitIdnoField;

        private bool ucitIdnoFieldSpecified;

        private rozvrhovaAkceUcitel ucitelField;

        private ushort rokField;

        private string budovaField;

        private string mistnostField;

        private byte kapacitaMistnostiField;

        private bool kapacitaMistnostiFieldSpecified;

        private sbyte planObsazeniField;

        private byte obsazeniField;

        private string typAkceField;

        private string typAkceZkrField;

        private string semestrField;

        private string platnostField;

        private string denField;

        private string denZkrField;

        private byte hodinaOdField;

        private bool hodinaOdFieldSpecified;

        private byte hodinaDoField;

        private bool hodinaDoFieldSpecified;

        private byte pocetVyucHodinField;

        private bool pocetVyucHodinFieldSpecified;

        private string hodinaSkutOdField;

        private string hodinaSkutDoField;

        private byte tydenOdField;

        private byte tydenDoField;

        private string tydenField;

        private string tydenZkrField;

        private string datumField;

        private string jeNadrazenaField;

        private string maNadrazenouField;

        private string krouzkyField;

        private string casovaRadaField;

        private string datumOdField;

        private string datumDoField;

        private string kontaktField;

        private string druhAkceField;

        private string vsichniUciteleUcitIdnoField;

        private string vsichniUciteleJmenaTitulyField;

        private string vsichniUciteleJmenaTitulySPodilyField;

        private string vsichniUcitelePrijmeniField;

        private uint referencedIdnoField;

        private string ownerField;

        private string zakazaneAkceField;

        /// <remarks/>
        public uint roakIdno
        {
            get
            {
                return this.roakIdnoField;
            }
            set
            {
                this.roakIdnoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool roakIdnoSpecified
        {
            get
            {
                return this.roakIdnoFieldSpecified;
            }
            set
            {
                this.roakIdnoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string nazev
        {
            get
            {
                return this.nazevField;
            }
            set
            {
                this.nazevField = value;
            }
        }

        /// <remarks/>
        public string katedra
        {
            get
            {
                return this.katedraField;
            }
            set
            {
                this.katedraField = value;
            }
        }

        /// <remarks/>
        public string predmet
        {
            get
            {
                return this.predmetField;
            }
            set
            {
                this.predmetField = value;
            }
        }

        /// <remarks/>
        public string statut
        {
            get
            {
                return this.statutField;
            }
            set
            {
                this.statutField = value;
            }
        }

        /// <remarks/>
        public ushort ucitIdno
        {
            get
            {
                return this.ucitIdnoField;
            }
            set
            {
                this.ucitIdnoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ucitIdnoSpecified
        {
            get
            {
                return this.ucitIdnoFieldSpecified;
            }
            set
            {
                this.ucitIdnoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public rozvrhovaAkceUcitel ucitel
        {
            get
            {
                return this.ucitelField;
            }
            set
            {
                this.ucitelField = value;
            }
        }

        /// <remarks/>
        public ushort rok
        {
            get
            {
                return this.rokField;
            }
            set
            {
                this.rokField = value;
            }
        }

        /// <remarks/>
        public string budova
        {
            get
            {
                return this.budovaField;
            }
            set
            {
                this.budovaField = value;
            }
        }

        /// <remarks/>
        public string mistnost
        {
            get
            {
                return this.mistnostField;
            }
            set
            {
                this.mistnostField = value;
            }
        }

        /// <remarks/>
        public byte kapacitaMistnosti
        {
            get
            {
                return this.kapacitaMistnostiField;
            }
            set
            {
                this.kapacitaMistnostiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool kapacitaMistnostiSpecified
        {
            get
            {
                return this.kapacitaMistnostiFieldSpecified;
            }
            set
            {
                this.kapacitaMistnostiFieldSpecified = value;
            }
        }

        /// <remarks/>
        public sbyte planObsazeni
        {
            get
            {
                return this.planObsazeniField;
            }
            set
            {
                this.planObsazeniField = value;
            }
        }

        /// <remarks/>
        public byte obsazeni
        {
            get
            {
                return this.obsazeniField;
            }
            set
            {
                this.obsazeniField = value;
            }
        }

        /// <remarks/>
        public string typAkce
        {
            get
            {
                return this.typAkceField;
            }
            set
            {
                this.typAkceField = value;
            }
        }

        /// <remarks/>
        public string typAkceZkr
        {
            get
            {
                return this.typAkceZkrField;
            }
            set
            {
                this.typAkceZkrField = value;
            }
        }

        /// <remarks/>
        public string semestr
        {
            get
            {
                return this.semestrField;
            }
            set
            {
                this.semestrField = value;
            }
        }

        /// <remarks/>
        public string platnost
        {
            get
            {
                return this.platnostField;
            }
            set
            {
                this.platnostField = value;
            }
        }

        /// <remarks/>
        public string den
        {
            get
            {
                return this.denField;
            }
            set
            {
                this.denField = value;
            }
        }

        /// <remarks/>
        public string denZkr
        {
            get
            {
                return this.denZkrField;
            }
            set
            {
                this.denZkrField = value;
            }
        }

        /// <remarks/>
        public byte hodinaOd
        {
            get
            {
                return this.hodinaOdField;
            }
            set
            {
                this.hodinaOdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hodinaOdSpecified
        {
            get
            {
                return this.hodinaOdFieldSpecified;
            }
            set
            {
                this.hodinaOdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public byte hodinaDo
        {
            get
            {
                return this.hodinaDoField;
            }
            set
            {
                this.hodinaDoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hodinaDoSpecified
        {
            get
            {
                return this.hodinaDoFieldSpecified;
            }
            set
            {
                this.hodinaDoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public byte pocetVyucHodin
        {
            get
            {
                return this.pocetVyucHodinField;
            }
            set
            {
                this.pocetVyucHodinField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool pocetVyucHodinSpecified
        {
            get
            {
                return this.pocetVyucHodinFieldSpecified;
            }
            set
            {
                this.pocetVyucHodinFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string hodinaSkutOd
        {
            get
            {
                return this.hodinaSkutOdField;
            }
            set
            {
                this.hodinaSkutOdField = value;
            }
        }

        /// <remarks/>
        public string hodinaSkutDo
        {
            get
            {
                return this.hodinaSkutDoField;
            }
            set
            {
                this.hodinaSkutDoField = value;
            }
        }

        /// <remarks/>
        public byte tydenOd
        {
            get
            {
                return this.tydenOdField;
            }
            set
            {
                this.tydenOdField = value;
            }
        }

        /// <remarks/>
        public byte tydenDo
        {
            get
            {
                return this.tydenDoField;
            }
            set
            {
                this.tydenDoField = value;
            }
        }

        /// <remarks/>
        public string tyden
        {
            get
            {
                return this.tydenField;
            }
            set
            {
                this.tydenField = value;
            }
        }

        /// <remarks/>
        public string tydenZkr
        {
            get
            {
                return this.tydenZkrField;
            }
            set
            {
                this.tydenZkrField = value;
            }
        }

        /// <remarks/>
        public string datum
        {
            get
            {
                return this.datumField;
            }
            set
            {
                this.datumField = value;
            }
        }

        /// <remarks/>
        public string jeNadrazena
        {
            get
            {
                return this.jeNadrazenaField;
            }
            set
            {
                this.jeNadrazenaField = value;
            }
        }

        /// <remarks/>
        public string maNadrazenou
        {
            get
            {
                return this.maNadrazenouField;
            }
            set
            {
                this.maNadrazenouField = value;
            }
        }

        /// <remarks/>
        public string krouzky
        {
            get
            {
                return this.krouzkyField;
            }
            set
            {
                this.krouzkyField = value;
            }
        }

        /// <remarks/>
        public string casovaRada
        {
            get
            {
                return this.casovaRadaField;
            }
            set
            {
                this.casovaRadaField = value;
            }
        }

        /// <remarks/>
        public string datumOd
        {
            get
            {
                return this.datumOdField;
            }
            set
            {
                this.datumOdField = value;
            }
        }

        /// <remarks/>
        public string datumDo
        {
            get
            {
                return this.datumDoField;
            }
            set
            {
                this.datumDoField = value;
            }
        }

        /// <remarks/>
        public string kontakt
        {
            get
            {
                return this.kontaktField;
            }
            set
            {
                this.kontaktField = value;
            }
        }

        /// <remarks/>
        public string druhAkce
        {
            get
            {
                return this.druhAkceField;
            }
            set
            {
                this.druhAkceField = value;
            }
        }

        /// <remarks/>
        public string vsichniUciteleUcitIdno
        {
            get
            {
                return this.vsichniUciteleUcitIdnoField;
            }
            set
            {
                this.vsichniUciteleUcitIdnoField = value;
            }
        }

        /// <remarks/>
        public string vsichniUciteleJmenaTituly
        {
            get
            {
                return this.vsichniUciteleJmenaTitulyField;
            }
            set
            {
                this.vsichniUciteleJmenaTitulyField = value;
            }
        }

        /// <remarks/>
        public string vsichniUciteleJmenaTitulySPodily
        {
            get
            {
                return this.vsichniUciteleJmenaTitulySPodilyField;
            }
            set
            {
                this.vsichniUciteleJmenaTitulySPodilyField = value;
            }
        }

        /// <remarks/>
        public string vsichniUcitelePrijmeni
        {
            get
            {
                return this.vsichniUcitelePrijmeniField;
            }
            set
            {
                this.vsichniUcitelePrijmeniField = value;
            }
        }

        /// <remarks/>
        public uint referencedIdno
        {
            get
            {
                return this.referencedIdnoField;
            }
            set
            {
                this.referencedIdnoField = value;
            }
        }

        /// <remarks/>
        public string owner
        {
            get
            {
                return this.ownerField;
            }
            set
            {
                this.ownerField = value;
            }
        }

        /// <remarks/>
        public string zakazaneAkce
        {
            get
            {
                return this.zakazaneAkceField;
            }
            set
            {
                this.zakazaneAkceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rozvrhovaAkceUcitel
    {

        private ushort ucitIdnoField;

        private string jmenoField;

        private string prijmeniField;

        private string titulPredField;

        private string titulZaField;

        private string platnostField;

        private string zamestnanecField;

        private byte podilNaVyuceField;

        private bool podilNaVyuceFieldSpecified;

        /// <remarks/>
        public ushort ucitIdno
        {
            get
            {
                return this.ucitIdnoField;
            }
            set
            {
                this.ucitIdnoField = value;
            }
        }

        /// <remarks/>
        public string jmeno
        {
            get
            {
                return this.jmenoField;
            }
            set
            {
                this.jmenoField = value;
            }
        }

        /// <remarks/>
        public string prijmeni
        {
            get
            {
                return this.prijmeniField;
            }
            set
            {
                this.prijmeniField = value;
            }
        }

        /// <remarks/>
        public string titulPred
        {
            get
            {
                return this.titulPredField;
            }
            set
            {
                this.titulPredField = value;
            }
        }

        /// <remarks/>
        public string titulZa
        {
            get
            {
                return this.titulZaField;
            }
            set
            {
                this.titulZaField = value;
            }
        }

        /// <remarks/>
        public string platnost
        {
            get
            {
                return this.platnostField;
            }
            set
            {
                this.platnostField = value;
            }
        }

        /// <remarks/>
        public string zamestnanec
        {
            get
            {
                return this.zamestnanecField;
            }
            set
            {
                this.zamestnanecField = value;
            }
        }

        /// <remarks/>
        public byte podilNaVyuce
        {
            get
            {
                return this.podilNaVyuceField;
            }
            set
            {
                this.podilNaVyuceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool podilNaVyuceSpecified
        {
            get
            {
                return this.podilNaVyuceFieldSpecified;
            }
            set
            {
                this.podilNaVyuceFieldSpecified = value;
            }
        }
    }
}

