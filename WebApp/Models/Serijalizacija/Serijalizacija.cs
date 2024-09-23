using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApp.Models.Serijalizacija
{
    public class Serijalizacija
    {
        private static readonly string administratoriPutanja = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\administratori.xml";
        private static readonly string ispitiPutanja = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\ispiti.xml";
        private static readonly string rezultatiPutanja = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\rezultati.xml";
        private static readonly string profesoriPutanja = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\profesori.xml";
        private static readonly string studentiPutanja = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\studenti.xml";


        #region UbacivanjePodataka

        public static void DodajPodatke() //za pocetno pokretanje jer u zadatku nije navedeno da se dodaju profesori i administratori pa su programski dodati
        {
            Profesor profesor1 = new Profesor("profesor1", "1234", "Marko", "Markovic", "marko.markovic@uns.ac.rs", "08-08-1975");
            Profesor profesor2 = new Profesor("profesor2", "1234", "Ivana", "Ivanovic", "ivana.ivanovic@uns.ac.rs", "07-07-1964");
            profesor1.Uloga = UlogaKorisnika.Profesor;
            profesor1.Predmeti.Add("MISS");
            profesor1.Predmeti.Add("BAZE");
            profesor2.Uloga = UlogaKorisnika.Profesor;
            profesor2.Predmeti.Add("WEB1");
            profesor2.Predmeti.Add("WEB2");

            Administrator administrator1 = new Administrator("admin1", "1234", "Petar", "Petrovic", "08-08-1977");
            administrator1.Uloga = UlogaKorisnika.Administrator;

            Student student1 = new Student("student1", "PR1/2022", "1234", "Janko", "Jankovic", "janko.jankovic@uns.ac.rs", "08-08-2002"); 
            Student student2 = new Student("student2", "PR2/2022", "1234", "Nikolina", "Nikolic", "nikolina.nikolic@uns.ac.rs", "04-11-2003");

            student1.Uloga = UlogaKorisnika.Student;
            student2.Uloga = UlogaKorisnika.Student;

            Serijalizacija.SerijalizujProfesora(profesor1);
            Serijalizacija.SerijalizujProfesora(profesor2);

            Serijalizacija.SerijalizujAdministratora(administrator1);

            Serijalizacija.DodajStudenta(student1);
            Serijalizacija.DodajStudenta(student2);
        } 

        #endregion



        #region STUDENT
        public static XmlStudenti DeserijalizujStudente()
        {
            XmlStudenti studenti  = new XmlStudenti();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlStudenti));

            using (StreamReader reader = new StreamReader(studentiPutanja))
            {
                studenti = (XmlStudenti)xmlSerializer.Deserialize(reader);
            }
            return studenti;
        }
        public static void DodajStudenta(Student student)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlStudenti));

            XmlStudenti studenti = DeserijalizujStudente();
            //XmlStudenti studenti = new XmlStudenti();

            studenti.Studenti.Add(student);

            using (StreamWriter stream = new StreamWriter(studentiPutanja))
            {
                xmlSerializer.Serialize(stream, studenti);
            }
        }

        public static void IzmeniStudenta(Student student)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlStudenti));

            XmlStudenti studenti = DeserijalizujStudente();

            studenti.Studenti.RemoveAll(x => x.KorisnickoIme == student.KorisnickoIme);
            studenti.Studenti.Add(student);

            using (StreamWriter stream = new StreamWriter(studentiPutanja))
            {
                xmlSerializer.Serialize(stream, studenti);
            }
        }

        public static void ObrisiStudenta(string brIndexa)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlStudenti));

            XmlStudenti studenti = DeserijalizujStudente();

            studenti.Studenti.RemoveAll(x => x.BrIndex == brIndexa);

            using (StreamWriter stream = new StreamWriter(studentiPutanja))
            {
                xmlSerializer.Serialize(stream, studenti);
            }
        }
        #endregion



        #region PROFESOR

        //potrebno radi ucitavanja profesora iz baze i prilikom punjenje baze sa podacima o profesorima

        public static XmlProfesori DeserijalizujProfesore()
        {
            XmlProfesori profesori = new XmlProfesori();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlProfesori));

            using (StreamReader reader = new StreamReader(profesoriPutanja))
            {
                profesori = (XmlProfesori)xmlSerializer.Deserialize(reader);
            }
            return profesori;
        }
        public static void SerijalizujProfesora(Profesor profesor)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlProfesori));

            XmlProfesori profesori = DeserijalizujProfesore();
            //XmlProfesori profesori = new XmlProfesori();

            profesori.Profesori.Add(profesor);

            using (StreamWriter stream = new StreamWriter(profesoriPutanja))
            {
                xmlSerializer.Serialize(stream, profesori);
            }
        }


        #endregion

        #region ADMINISTRATOR


        public static XmlAdministratori DeserijalizujAdministratore()
        {
            XmlAdministratori administratori = new XmlAdministratori();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlAdministratori));

            using (StreamReader reader = new StreamReader(administratoriPutanja))
            {
                administratori = (XmlAdministratori)xmlSerializer.Deserialize(reader);
            }
            return administratori;
        }
        public static void SerijalizujAdministratora(Administrator administrator)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlAdministratori));

            XmlAdministratori administratori = DeserijalizujAdministratore();
            //XmlAdministratori administratori = new XmlAdministratori();

            administratori.Administratori.Add(administrator);

            using (StreamWriter stream = new StreamWriter(administratoriPutanja))
            {
                xmlSerializer.Serialize(stream, administratori);
            }
        }


        #endregion


        #region ISPITI


        public static XmlIspiti DeserijalizujIspite()
        {
            XmlIspiti ispiti = new XmlIspiti();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlIspiti));

            using (StreamReader reader = new StreamReader(ispitiPutanja))
            {
                ispiti = (XmlIspiti)xmlSerializer.Deserialize(reader);
            }
            return ispiti;
        }
        public static void DodajIspit(Ispit ispit)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlIspiti));
            XmlIspiti ispiti = DeserijalizujIspite();
            
             ispiti.Ispiti.Add(ispit);

            using (StreamWriter stream = new StreamWriter(ispitiPutanja))
            {
                xmlSerializer.Serialize(stream, ispiti);
            }
        }

        public static XmlRezultatiIspita DeserijalizujRezultate()
        {
            XmlRezultatiIspita ispiti = new XmlRezultatiIspita();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlRezultatiIspita));

            using (StreamReader reader = new StreamReader(rezultatiPutanja))
            {
                ispiti = (XmlRezultatiIspita)xmlSerializer.Deserialize(reader);
            }
            return ispiti;
        }
        public static void DodajRezultat(RezultatIspita rezultat)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlRezultatiIspita));

            XmlRezultatiIspita rezultati = DeserijalizujRezultate();
            //XmlRezultatiIspita rezultati = new XmlRezultatiIspita();
            rezultati.Rezultati.Add(rezultat);

            using (StreamWriter stream = new StreamWriter(rezultatiPutanja))
            {
                xmlSerializer.Serialize(stream, rezultati);
            }
        }
        public static void IzmeniRezultat(RezultatIspita rezultat)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlRezultatiIspita));

            XmlRezultatiIspita rezultati = DeserijalizujRezultate();
            rezultati.Rezultati.RemoveAll(x => x.IdRez == rezultat.IdRez);
            rezultati.Rezultati.Add(rezultat);

            using (StreamWriter stream = new StreamWriter(rezultatiPutanja))
            {
                xmlSerializer.Serialize(stream, rezultati);
            }
        }
        #endregion

    }
}