using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public enum NazivRoka { januarski, februarski, aprilski, junski, julski, avgustovski, septembarski, oktobarski}
    public class Ispit
    {
        public string Profesor { get; set; }
        public string IdIspita { get; set; }
        public string Predmet { get; set; }
        public DateTime VremeOdrzavanja { get; set; }
        public string NazivUcionice { get; set; }
        public NazivRoka NazivIspitnogRoka { get; set; }

        public Ispit() 
        {
            IdIspita = string.Format($"{Predmet}+{NazivIspitnogRoka}");
        }

        public Ispit(string Profesor, string Predmet, DateTime VremeOdrzavanja, string NazivUcionice, NazivRoka NazivIspitnogRoka)
        {
            IdIspita = string.Format($"{Predmet}+{NazivIspitnogRoka}");
            this.Profesor = Profesor;
            this.Predmet = Predmet;
            this.VremeOdrzavanja = VremeOdrzavanja;
            this.NazivIspitnogRoka = NazivIspitnogRoka;
            this.NazivUcionice = NazivUcionice;
        }
    }

    public class XmlIspiti
    {
        public List<Ispit> Ispiti { get; set; }

        public XmlIspiti()
        {
            Ispiti = new List<Ispit>();
        }
    }
}