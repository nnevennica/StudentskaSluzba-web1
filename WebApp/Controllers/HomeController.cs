using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.Serijalizacija;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Serijalizacija.DodajPodatke();
            return View("Login");
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["ulogovanKorisnik"] = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string korisnickoIme = form.Get("korisnik");
            string lozinka = form.Get("lozinka");

            if (lozinka == "" || korisnickoIme == "")
            {
                ViewBag.Message = "Podaci nisu popunjeni!";
                return View();
            }

            List<Student> studenti = HttpContext.Application["Studenti"] as List<Student>;
            List<Profesor> profesori = HttpContext.Application["Profesori"] as List<Profesor>;
            List<Administrator> administratori = HttpContext.Application["Administratori"] as List<Administrator>;

            Student student = studenti.Find(x => x.KorisnickoIme == korisnickoIme && x.Lozinka == lozinka);
            if(student == null)
            {
                ViewBag.Message = "Neispravno korisnicko ime ili lozinka.";
            }
            else
            {
                Session["ulogovanKorisnik"] = (Korisnik)student;
                return View("DashBoard");
            }

            Profesor profesor = profesori.Find(x => x.KorisnickoIme == korisnickoIme && x.Lozinka == lozinka);
            if (profesor == null)
            {
                ViewBag.Message = "Neispravno korisnicko ime ili lozinka.";
            }
            else
            {
                Session["ulogovanKorisnik"] = (Korisnik)profesor;
                return View("DashBoard");
            }

            Administrator administrator = administratori.Find(x => x.KorisnickoIme == korisnickoIme && x.Lozinka == lozinka);
            if (administrator == null)
            {
                ViewBag.Message = "Neispravno korisnicko ime ili lozinka.";
            }
            else
            {
                Session["ulogovanKorisnik"] = (Korisnik)administrator;
                return View("DashBoard");
            }
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}