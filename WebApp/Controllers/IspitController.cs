using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.Serijalizacija;

namespace WebApp.Controllers
{
    public class IspitController : Controller
    {
        // GET: Ispit
        public ActionResult Index()
        {
            return View();
        }

        #region SORT_FILTER_STUD
        [HttpPost]
        public ActionResult PretragaStudent(FormCollection form)
        {
            string ispitniRok = form.Get("ispitFil");
            string ocena = form.Get("ocenaFil");
            string predmet = form.Get("predmetFil");

            List<RezultatIspita> rezultati = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            Korisnik korisnik = (Korisnik)Session["ulogovanKorisnik"];
            rezultati = rezultati.FindAll(x => x.Student.KorisnickoIme == korisnik.KorisnickoIme && x.Ispit.VremeOdrzavanja < DateTime.Now).ToList(); //pronadji ispit na kom je bio student i da je u proslosti tj da je zavrsen
            

            if (ispitniRok != "")    
            {
                Enum.TryParse(ispitniRok, out NazivRoka nazivIspitnogRoka);
                rezultati = rezultati.FindAll(x => x.Ispit.NazivIspitnogRoka == nazivIspitnogRoka);
            }
            if(ocena!="")
            {
                rezultati = rezultati.FindAll(x => x.Ocena == Int32.Parse(ocena));
            }
            if(predmet!="")
            {
                rezultati = rezultati.FindAll(x => x.Ispit.Predmet == predmet);
            }

            return View("PregledIspitaStudent", rezultati);
        }


        [HttpPost]
        public ActionResult SortiranjeStudent(FormCollection form)
        {
            string vrstaFiltera = form.Get("vrstaFiltera");
            string vrednostFiltera = form.Get("vrednostFiltera");
            List<RezultatIspita> rezultati = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            Korisnik korisnik = (Korisnik)Session["ulogovanKorisnik"];
            rezultati = rezultati.FindAll(x => x.Student.KorisnickoIme == korisnik.KorisnickoIme && x.Ispit.VremeOdrzavanja < DateTime.Now).ToList(); //pronadji ispit na kom je bio student i da je u proslosti tj da je zavrsen

            switch (vrednostFiltera)
            {
                case "opadajuce":
                    {
                        if (vrstaFiltera == "ispitR")
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Ispit.NazivIspitnogRoka).ToList();
                        }
                        else if (vrstaFiltera == "predmet")
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Ispit.Predmet).ToList();
                        }
                        else
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Ocena).ToList();
                        }
                        break;
                    }
                case "rastuce":
                    {
                        if (vrstaFiltera == "ispitR")
                        {
                            rezultati = rezultati.OrderBy(x => x.Ispit.NazivIspitnogRoka).ToList();
                        }
                        else if (vrstaFiltera == "predmet")
                        {
                            rezultati = rezultati.OrderBy(x => x.Ispit.Predmet).ToList();
                        }
                        else
                        {
                            rezultati = rezultati.OrderBy(x => x.Ocena).ToList();
                        }
                        break;
                    }
            }
            return View("PregledIspitaStudent", rezultati);
        }
        #endregion

        #region SORT_FILT_PROF

        [HttpPost]
        public ActionResult PretragaProfesor(FormCollection form)
        {
            string ispitniRok = form.Get("ispitFil");
            string ocena = form.Get("ocenaFil");
            string predmet = form.Get("predmetFil");
            string ime = form.Get("imeFil");
            string prezime = form.Get("prezimeFil");
            string index = form.Get("indexFil");

            List<RezultatIspita> rezultati = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            Korisnik korisnik = (Korisnik)Session["ulogovanKorisnik"];
            rezultati = rezultati.FindAll(x => x.Ispit.Profesor == korisnik.KorisnickoIme).ToList(); //pronadji ispit na kom je bio student i da je u proslosti tj da je zavrsen


            if (ispitniRok != "")
            {
                Enum.TryParse(ispitniRok, out NazivRoka nazivIspitnogRoka);
                rezultati = rezultati.FindAll(x => x.Ispit.NazivIspitnogRoka == nazivIspitnogRoka);
            }
            if (ocena != "")
            {
                rezultati = rezultati.FindAll(x => x.Ocena == Int32.Parse(ocena));
            }
            if (predmet != "")
            {
                rezultati = rezultati.FindAll(x => x.Ispit.Predmet == predmet);
            }
            if(ime != "")
            {
                rezultati = rezultati.FindAll(x => x.Student.Ime.Contains(ime));
            }
            if (prezime != "")
            {
                rezultati = rezultati.FindAll(x => x.Student.Prezime.Contains(prezime));
            }
            if (index != "")
            {
                rezultati = rezultati.FindAll(x => x.Student.BrIndex.Contains(index));
            }

            return View("PregledIspitaProfesor", rezultati);
        }


        [HttpPost]
        public ActionResult SortiranjeProfesor(FormCollection form)
        {
            string vrstaFiltera = form.Get("vrstaFiltera");
            string vrednostFiltera = form.Get("vrednostFiltera");
            List<RezultatIspita> rezultati = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            Korisnik korisnik = (Korisnik)Session["ulogovanKorisnik"];
            rezultati = rezultati.FindAll(x => x.Ispit.Profesor == korisnik.KorisnickoIme).ToList(); //pronadji ispit na kom je bio student i da je u proslosti tj da je zavrsen

            switch (vrednostFiltera)
            {
                case "opadajuce":
                    {
                        if (vrstaFiltera == "ispitR")
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Ispit.NazivIspitnogRoka).ToList();
                        }
                        else if (vrstaFiltera == "predmet")
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Ispit.Predmet).ToList();
                        }
                        else if (vrstaFiltera == "ime")
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Student.Ime).ToList();
                        }
                        else if (vrstaFiltera == "prezime")
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Student.Prezime).ToList();
                        }
                        else if (vrstaFiltera == "index")
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Student.BrIndex).ToList();
                        }
                        else
                        {
                            rezultati = rezultati.OrderByDescending(x => x.Ocena).ToList();
                        }
                        break;
                    }
                case "rastuce":
                    {
                        if (vrstaFiltera == "ispitR")
                        {
                            rezultati = rezultati.OrderBy(x => x.Ispit.NazivIspitnogRoka).ToList();
                        }
                        else if (vrstaFiltera == "predmet")
                        {
                            rezultati = rezultati.OrderBy(x => x.Ispit.Predmet).ToList();
                        }
                        else if (vrstaFiltera == "ime")
                        {
                            rezultati = rezultati.OrderBy(x => x.Student.Ime).ToList();
                        }
                        else if (vrstaFiltera == "prezime")
                        {
                            rezultati = rezultati.OrderBy(x => x.Student.Prezime).ToList();
                        }
                        else if (vrstaFiltera == "index")
                        {
                            rezultati = rezultati.OrderBy(x => x.Student.BrIndex).ToList();
                        }
                        else
                        {
                            rezultati = rezultati.OrderBy(x => x.Ocena).ToList();
                        }
                        break;
                    }
            }
            return View("PregledIspitaProfesor", rezultati);
        }

        #endregion

        public ActionResult OcenjivanjeIspita()
        {
            List<RezultatIspita> rezultati = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            Korisnik korisnik = (Korisnik)Session["ulogovanKorisnik"];
            rezultati = rezultati.FindAll(x => x.Ispit.Profesor == korisnik.KorisnickoIme && x.Ocena == 0 && x.Ispit.VremeOdrzavanja < DateTime.Now);
            HttpContext.Application["IspitZaOceniti"] = rezultati;
            return View();
        }
        [HttpPost]
        public ActionResult OcenjivanjeIspita(FormCollection form)
        {
            string idRez = form.Get("idRez");
            int ocena = Int32.Parse(form.Get("ocena"));
            List<RezultatIspita> rezultati = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            RezultatIspita rezultat = rezultati.Find(x => x.IdRez == idRez);
            rezultati.RemoveAll(x => x.IdRez == idRez);
            rezultat.Ocena = ocena;
            rezultati.Add(rezultat);

            Serijalizacija.IzmeniRezultat(rezultat);
            HttpContext.Application["IspitZaOceniti"] = rezultati;
            return RedirectToAction("PregledIspitaProfesor");
        }

        public ActionResult PregledIspitaProfesor()
        {
            List<RezultatIspita> rezultati = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            Korisnik korisnik = (Korisnik)Session["ulogovanKorisnik"];
            rezultati = rezultati.FindAll(x => x.Ispit.Profesor == korisnik.KorisnickoIme).ToList();
            return View(rezultati);
        }

        public ActionResult PregledIspitaStudent()
        {
            List<RezultatIspita> rezultati = HttpContext.Application["Rezultati"] as List<RezultatIspita>;
            Korisnik korisnik = (Korisnik)Session["ulogovanKorisnik"];
            rezultati = rezultati.FindAll(x => x.Student.KorisnickoIme == korisnik.KorisnickoIme && x.Ispit.VremeOdrzavanja < DateTime.Now).ToList(); //pronadji ispit na kom je bio student i da je u proslosti tj da je zavrsen
            return View(rezultati);
        }


        public ActionResult PrijavaIspita()
        {
            ViewBag.Message = "";
            List<Ispit> ispiti = HttpContext.Application["Ispiti"] as List<Ispit>;
            ispiti = ispiti.FindAll(x => x.VremeOdrzavanja > DateTime.Now);
            HttpContext.Application["IspitiZaPrijavu"] = ispiti;
            return View();
        }

        [HttpPost]
        public ActionResult PrijaviIspit(FormCollection form)
        {
            string IdIspita = form.Get("IdIspita");
            List<Ispit> ispiti = HttpContext.Application["Ispiti"] as List<Ispit>;
            RezultatIspita rezultat = new RezultatIspita();
            List<RezultatIspita> rezultatiIspita = HttpContext.Application["Rezultati"] as List<RezultatIspita>;

            Ispit ispit = ispiti.Find(x => x.IdIspita == IdIspita);
            rezultat.Ispit = ispit;

            Korisnik korisnik = (Korisnik)Session["ulogovanKorisnik"];
            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;
            Student student = studenti.Find(x => x.KorisnickoIme == korisnik.KorisnickoIme);

            if (rezultatiIspita.Any(x=>x.Ispit.IdIspita == ispit.IdIspita && x.Student.KorisnickoIme == korisnik.KorisnickoIme))
            {
                ViewBag.Message = "Vec ste prijavili ispit";
                return View("PrijavaIspita");
            }

            rezultat.Student = student;
            rezultat.Ocena = 0; //da razlikujem NEPREGLEDANE od POLOZENIH i NEPOLOZENIH, dok profa ne oceni bice 0

            Serijalizacija.DodajRezultat(rezultat);
            rezultatiIspita.Add(rezultat);
            HttpContext.Application["Rezultati"] = rezultatiIspita;
            ViewBag.Message = "Ispit prijavljen";
            return View("PrijavaIspita");

        }

        public ActionResult KreiranjeIspita()
        {
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult KreiranjeIspita(FormCollection form)
        {
            //List<Profesor> profesori = HttpContext.Application["Profesori"] as List<Profesor>;
            Korisnik korisnik = Session["ulogovanKorisnik"] as Korisnik; //izvlacim profesoro korisnicko ime da upisem u ispit

            string nazivRoka = form.Get("nazivIspitnogRoka");
            string predmet = form.Get("predmet");
            string vremeIspita = form.Get("vremeIspita");
            string nazivUcionice = form.Get("nazivUcionice");

            
            List<Ispit> ispiti = HttpContext.Application["Ispiti"] as List<Ispit>;

            string idIspita = string.Format($"{predmet}+{nazivRoka}");
            if (ispiti.Any(x => x.IdIspita == idIspita)){
                ViewBag.Message = "Ispit iz odabranog predmeta je vec postavljen za odabrani ispitni rok.";
                return View();
            }

            Enum.TryParse(nazivRoka, out NazivRoka nazivIspitnogRoka);

            Ispit ispit = new Ispit(korisnik.KorisnickoIme, predmet, DateTime.Parse(vremeIspita), nazivUcionice, nazivIspitnogRoka);
            Serijalizacija.DodajIspit(ispit);

            ispiti.Add(ispit);
            HttpContext.Application["Ispiti"] = ispiti;
            ViewBag.Message = "Ispit uspesno kreiran";
            return View();
        }
    }
}