using Flapp_BLL.Checkers;
using Flapp_BLL.Models;
using System;
using Flapp_BLL.Managers;
using Flapp_DAL.Repository;

namespace Flapp_CNS
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = @"Data Source=LAPTOP-BURAQ\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";
            //Rijksregisternummer r1 = new Rijksregisternummer("21.10.02-289.65");
            //Bestuurder b1 = new Bestuurder("Declerck", "Tibo", Geslacht.M, a1, "06/08/1999", "99.08.06-289.17", RijbewijsType.B, v1, t1);
            //Bestuurder b2 = new Bestuurder("Balci", "Burak", Geslacht.M, "12/05/1999", "99.05.12-273.26", RijbewijsType.B);
            //Adres a1 = new Adres(1, "Frans Uyttenhovestraat", "91", "Gent", 9000);
            Brandstof bs1 = new Brandstof("Elektrisch");
            //Voertuig v1 = new Voertuig(1, "Tesla", "X", "13245678957903251", "2-ABC-123", bs1, "Stationwagen", "Zwart", 5);
            //Tankkaart t1 = new Tankkaart(1, DateTime.Parse("06/08/2025"));
            bs1.ZetBrandstofNaam("UMOEDER");
            BrandstofManager bm = new BrandstofManager(new BrandstofRepo(connString));
            //bm.VoegBrandstofToe(new("LPG"));
            bm.VerwijderBrandstof("LPG");
        }
    }
}
