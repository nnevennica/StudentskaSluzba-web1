using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.Serijalizacija;

namespace WebApp.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KreirajStudenta()
        {
            ViewBag.Message = "";
            return View();
        }

        public ActionResult PregledStudenata()
        {
            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;
            return View(studenti);
        }

        [HttpPost]
        public ActionResult KreirajStudenta(FormCollection form)
        {

            string korisnickoIme = form.Get("korisnickoIme");
            string oznakaSmera = form.Get("oznakaSmera");
            string brojIndexa = form.Get("brojIndexa");
            string godinaUpisa = form.Get("godinaUpisa");

            string index = string.Format($"{oznakaSmera}{brojIndexa}/{godinaUpisa}");

            string lozinka = form.Get("lozinka");
            string ime = form.Get("ime");
            string prezime = form.Get("prezime");
            string datumRodjenja = form.Get("datumRodjenja");
            string email = form.Get("email");

            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;
            List<Profesor> profesori = HttpContext.Application["Profesori"] as List<Profesor>;
            if (studenti.Any(x => x.Email == email) || profesori.Any(x => x.Email == email))
            {
                ViewBag.Message = "Email adresa vec u upotrebi.";
                return View();
            }
            if (studenti.Any(x => x.BrIndex == index))
            {
                ViewBag.Message = "Index je vec u upotrebi.";
                return View();
            }
           
            List<Administrator> administratori = HttpContext.Application["Administratori"] as List<Administrator>;

            if (studenti.Any(x => x.KorisnickoIme == korisnickoIme) || profesori.Any(x=>x.KorisnickoIme == korisnickoIme) || administratori.Any(x=>x.KorisnickoIme == korisnickoIme))
            {
                ViewBag.Message = "Korisnicko ime je vec u upotrebi.";
                return View();
            }

            Student student = new Student(korisnickoIme, index, lozinka, ime, prezime, email, datumRodjenja);

            student.Uloga = UlogaKorisnika.Student;
            Serijalizacija.DodajStudenta(student);
            studenti.Add(student);

            HttpContext.Application["Studenti"] = studenti; //da bi na nivou cele aplikacije studenti bili azurirani

            ViewBag.Message = "";
            return RedirectToAction("PregledStudenata");
        }

        [HttpPost]
        public ActionResult AzurirajStudentaPriprema(FormCollection form)
        {
            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;
            string brIndexa = form.Get("brIndexa");
            Student student = studenti.Find(x => x.BrIndex == brIndexa);
            HttpContext.Application["AzuriranjeStudenta"] = student;
            return View("AzurirajStudenta");
        }

        [HttpPost]
        public ActionResult AzurirajStudenta(FormCollection form)
        {

            string lozinka = form.Get("lozinka");
            string ime = form.Get("ime");
            string prezime = form.Get("prezime");
            string datumRodjenja = form.Get("datumRodjenja");
            string email = form.Get("email");
            string index = form.Get("brIndexa");

            

            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;
            Student student = studenti.Find(x => x.BrIndex == index);

            if(lozinka != "")
            {
                student.Lozinka = lozinka;
            }
            if (ime != student.Ime)
                student.Ime = ime;
            if (prezime != student.Prezime)
                student.Ime = ime;


            List<Profesor> profesori = HttpContext.Application["Profesori"] as List<Profesor>;

            if(email != student.Email)
            {
                if (studenti.Any(x => x.Email == email) || profesori.Any(x => x.Email == email))
                {
                    ViewBag.Message = "Email adresa vec u upotrebi.";
                    return View();
                }
            }
            if(datumRodjenja != student.DatumRodjenja && datumRodjenja!="")
            {
                student.DatumRodjenja = datumRodjenja;
            }

            Serijalizacija.IzmeniStudenta(student);
            studenti.RemoveAll(x => x.BrIndex == student.BrIndex);
            studenti.Add(student);


            HttpContext.Application["Studenti"] = studenti; //da bi na nivou cele aplikacije studenti bili azurirani

            List<RezultatIspita> rezultatiIspita = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            foreach(var rez in rezultatiIspita)
            {
                if(rez.Student.BrIndex == student.BrIndex)
                {
                    rez.Student = student;
                    Serijalizacija.IzmeniRezultat(rez);
                }
            }
            HttpContext.Application["Rezultati"] = rezultatiIspita;

            ViewBag.Message = "";
            return RedirectToAction("PregledStudenata");
        }



        [HttpPost]
        public ActionResult ObrisiStudenta(FormCollection form)
        {
            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;
            string brIndexa = form.Get("brIndexa");

            studenti.RemoveAll(x => x.BrIndex == brIndexa);
            HttpContext.Application["Studenti"] = studenti;

            Serijalizacija.ObrisiStudenta(brIndexa);

            return RedirectToAction("PregledStudenata");
        }


        [HttpPost]
        public ActionResult Pretraga(FormCollection form)
        {
            string ime = form.Get("imeFil");
            string prezime = form.Get("prezimeFil");
            string index = form.Get("indexFil");

            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;


            if (ime != "")
            {
                studenti = studenti.FindAll(x => x.Ime.Contains(ime));
            }
            if (prezime != "")
            {
                studenti = studenti.FindAll(x => x.Prezime.Contains(prezime));
            }
            if (index != "")
            {
                studenti = studenti.FindAll(x => x.BrIndex.Contains(index));
            }

            return View("PregledStudenata", studenti);
        }


        [HttpPost]
        public ActionResult Sortiranje(FormCollection form)
        {
            string vrstaFiltera = form.Get("vrstaFiltera");
            string vrednostFiltera = form.Get("vrednostFiltera");
            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;

            switch (vrednostFiltera)
            {
                case "opadajuce":
                    {
                        if (vrstaFiltera == "ime")
                        {
                            studenti = studenti.OrderByDescending(x => x.Ime).ToList();
                        }
                        if (vrstaFiltera == "prezime")
                        {
                            studenti = studenti.OrderByDescending(x => x.Prezime).ToList();
                        }
                        if (vrstaFiltera == "index")
                        {
                            studenti = studenti.OrderByDescending(x => x.BrIndex).ToList();
                        }
                        break;
                    }
                case "rastuce":
                    {

                        if (vrstaFiltera == "ime")
                        {
                            studenti = studenti.OrderBy(x => x.Ime).ToList();
                        }
                        if (vrstaFiltera == "prezime")
                        {
                            studenti = studenti.OrderBy(x => x.Prezime).ToList();
                        }
                        if (vrstaFiltera == "index")
                        {
                            studenti = studenti.OrderBy(x => x.BrIndex).ToList();
                        }
                        
                        break;
                    }
            }
            return View("PregledStudenata", studenti);
        }
    }
}