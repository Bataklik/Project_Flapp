using Flapp_BLL.Utils;
using Flapp_BLL.Models;
using System;

namespace Flapp_CNS
{
    class Program
    {
        static void Main(string[] args)
        {
            //Rijksregisternummer r1 = new Rijksregisternummer("21.10.02-289.65");
            //RijksregisternummerChecker r2 = new RijksregisternummerChecker("99.05.12-273.26");

            Adres a1 = new Adres("Frans Uyttenhovestraat", 91, "Gent", 9000);

            Brandstof bs1 = new Brandstof("Elektrisch");

            Voertuig v1 = new Voertuig(1, "Tesla", "X", "13245678957903251", "2-ABC-123", bs1, "Stationwagen", "Zwart", 5);

            Tankkaart t1 = new Tankkaart(123456789, DateTime.Parse("06/08/2025"));

            Bestuurder b1 = new Bestuurder("Declerck", "Tibo", Geslacht.M, a1, DateTime.Parse("06/08/1999"), "99.08.06-289.17", RijbewijsType.B, v1, t1);
            //Bestuurder b2 = new Bestuurder("Balci", "Burak", DateTime.Parse("12/05/1999"), r2, RijbewijsType.B);

            //Console.WriteLine(r1.ToonNummer());
            //Console.WriteLine(r2.ToonNummer());
            //Console.WriteLine(b1);
            //Console.WriteLine(b2);

            //Console.WriteLine(b2.Naam);
            //b2.ZetTankkaart(new(1, DateTime.Now.AddDays(1)));
            //Console.WriteLine(b2.Tankkaart);
            //b2.ZetNaam("Buraq");
            //Console.WriteLine(b2.Naam);
        }
    }
}
