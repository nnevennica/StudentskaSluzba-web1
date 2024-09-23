using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RezultatIspita
    {
        public Ispit Ispit { get; set; }
        public Student Student { get; set; }
        public int Ocena { get; set; }
        public string IdRez { get; set; }

        public RezultatIspita() {
        IdRez = Guid.NewGuid().ToString();
        }
    }

    public class XmlRezultatiIspita
    {
        public List<RezultatIspita> Rezultati { get; set; }

        public XmlRezultatiIspita()
        {
            Rezultati = new List<RezultatIspita>();
        }
    }
}