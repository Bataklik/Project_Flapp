using Flapp_BLL.Utils;
using Flapp_BLL.Models;
using System;

namespace Flapp_CNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Rijksregisternummer r1 = new Rijksregisternummer("21.10.02-289.65");
            Rijksregisternummer r2 = new Rijksregisternummer("99.05.12-273.26");

            Adres a1 = new Adres("Frans Uyttenhovestraat", 91, "Gent", 9000);

            Brandstof bs1 = new Brandstof("Elektrisch");

            Voertuig v1 = new Voertuig("Tesla", "X", "123456789", "2-ABC-123", bs1, "Elektrisch", "Zwart", 5);

            Tankkaart t1 = new Tankkaart(123456789, DateTime.Parse("06/08/2025"));

            Bestuurder b1 = new Bestuurder("Declerck", "Tibo", a1,DateTime.Parse("06/08/1999"), r1, RijbewijsType.B, v1, t1);
            Bestuurder b2 = new Bestuurder("Balci", "Burak", DateTime.Parse("12/05/1999"), r2, RijbewijsType.B);

            Console.WriteLine(r1.ToonNummer());
            Console.WriteLine(r2.ToonNummer());
            Console.WriteLine(b1);
            Console.WriteLine(b2);
        }
    }
}
