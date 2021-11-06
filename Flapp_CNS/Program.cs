using Flapp_BLL.Checkers;
using Flapp_BLL.Models;
using System;
using Flapp_BLL.Managers;
using Flapp_DAL.Repository;
using System.Collections.Generic;

namespace Flapp_CNS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connectie string
            string connStringBurak = @"Data Source=LAPTOP-BURAQ\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";

            // Managers
            BrandstofManager bm = new BrandstofManager(new BrandstofRepo(connStringBurak));
            RijbewijsTypeManager rbtm = new RijbewijsTypeManager(new RijbewijsTypeRepo(connStringBurak));

            // Models
            Adres a1 = new Adres(1, "Frans Uyttenhovestraat", "91", "Gent", 9000);



            Brandstof bs1 = new Brandstof("Elektrisch");

            Voertuig Auto1 = new Voertuig(1, "Tesla", "X", "13245678957903251", "2-ABC-123", bs1, "Stationwagen", "Zwart", 5);
            Voertuig Auto2 = new Voertuig(1, "Golf", "Supreme", "13245678957903251", "2-ABC-113", bs1, "Stationwagen", "Zwart", 5);

            Tankkaart t1 = new Tankkaart(1, DateTime.Parse("06/08/2025"));
            List<RijbewijsType> rt = new List<RijbewijsType>();
            rt.Add(new RijbewijsType("B"));
            rt.Add(new RijbewijsType("A"));
            //(string naam, string voornaam, Geslacht geslacht, string geboortedatum, string rijksregisternummer, List<RijbewijsType> rijbewijs)
            Bestuurder b1 = new Bestuurder("Declerck", "Tibo", Geslacht.M, "06/08/1999", "99.08.06-289.17", rt);
            Bestuurder b2 = new Bestuurder("Kcrekced", "Obit", Geslacht.M, "06/08/1999", "99.08.06-289.17", rt);

            b1.ZetVoertuig(Auto1);

            Console.WriteLine(b1.Voertuig.Bestuurder);
            Console.WriteLine(Auto1.Bestuurder);

            Auto1.zetBestuurder(b2);
            Console.WriteLine(Auto1.Bestuurder);
            Console.WriteLine(b2.Voertuig.Bestuurder);

            //b2.ZetVoertuig(Auto1);
            //Console.WriteLine(b2.Voertuig.Bestuurder);
            //Console.WriteLine(Auto1.Bestuurder);
        }
    }
}
