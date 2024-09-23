using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Administrator : Korisnik
    {
        public Administrator() : base() { }
        public Administrator(string KorisnickoIme, string Lozinka, string Ime, string Prezime, string DatumRodjenja) : base(KorisnickoIme, Lozinka, Ime, Prezime, DatumRodjenja) { }
    }

    public class XmlAdministratori
    {
        public List<Administrator> Administratori { get; set; }

        public XmlAdministratori()
        {
            Administratori = new List<Administrator>();
        }
    }
}