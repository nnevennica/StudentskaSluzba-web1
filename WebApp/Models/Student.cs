using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApp.Models
{
    public class Student : Korisnik
    {
       
        public string BrIndex { get; set; }
        public string Email { get; set; }
        [XmlIgnore]
        public List<Ispit> PolozeniIspiti { get; set; }
        [XmlIgnore]
        public List<Ispit> PrijavljeniIspiti { get; set; }
        [XmlIgnore]
        public List<Ispit> NepolozeniIspiti { get; set; }

        public Student()
        {
            PolozeniIspiti = new List<Ispit>();
            PrijavljeniIspiti = new List<Ispit>();
            NepolozeniIspiti = new List<Ispit>();
        }

        public Student(string KorisnickoIme, string BrIndex, string Lozinka, string Ime, string Prezime, string Email,string DatumRodjenja) : base(KorisnickoIme, Lozinka, Ime, Prezime, DatumRodjenja)
        {
            PolozeniIspiti = new List<Ispit>();
            PrijavljeniIspiti = new List<Ispit>();
            NepolozeniIspiti = new List<Ispit>();
            this.BrIndex = BrIndex;
            this.Email = Email;
        }

    }

    public class XmlStudenti
    {
        public List<Student> Studenti { get; set; }

        public XmlStudenti()
        {
            Studenti = new List<Student>();
        }
    }
}