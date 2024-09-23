using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApp.Models
{
    public class Profesor : Korisnik
    {
        public string Email { get; set; }
        [XmlIgnore]
        public List<Ispit> Ispiti { get; set; }
        public List<string> Predmeti { get; set; }

        public Profesor()
        {
            Ispiti = new List<Ispit>();
            Predmeti = new List<string>();
        }

        public Profesor(string KorisnickoIme, string Lozinka, string Ime, string Prezime, string Email, string DatumRodjenja) : base(KorisnickoIme, Lozinka, Ime, Prezime, DatumRodjenja)
        {
            Ispiti = new List<Ispit>();
            Predmeti = new List<string>();
            this.Email = Email;
        }
    }

    public class XmlProfesori
    {
        public List<Profesor> Profesori { get; set; }

        public XmlProfesori()
        {
            Profesori = new List<Profesor>();
        }
    }
}