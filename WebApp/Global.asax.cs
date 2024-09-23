using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApp.Models.Serijalizacija;
using WebApp.Models;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            XmlStudenti studenti = Serijalizacija.DeserijalizujStudente();
            HttpContext.Current.Application["Studenti"] = studenti.Studenti;

            XmlAdministratori administratori = Serijalizacija.DeserijalizujAdministratore();
            HttpContext.Current.Application["Administratori"] = administratori.Administratori;

            XmlProfesori profesori = Serijalizacija.DeserijalizujProfesore();
            HttpContext.Current.Application["Profesori"] = profesori.Profesori;

            XmlIspiti ispiti = Serijalizacija.DeserijalizujIspite();
            HttpContext.Current.Application["Ispiti"] = ispiti.Ispiti;

            XmlRezultatiIspita rezultati = Serijalizacija.DeserijalizujRezultate();
            HttpContext.Current.Application["Rezultati"] = rezultati.Rezultati;
        }
    }
}
