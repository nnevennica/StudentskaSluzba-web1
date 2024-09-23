using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public enum UlogaKorisnika { Administrator, Student, Profesor }
    public class Korisnik
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public UlogaKorisnika Uloga { get; set; }
        public Korisnik() { }
        public Korisnik(string KorisnickoIme, string Lozinka, string Ime, string Prezime, string DatumRodjenja)
        {
            this.KorisnickoIme = KorisnickoIme;
            this.Lozinka = Lozinka;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.DatumRodjenja = DatumRodjenja;

        }
    }
    
}