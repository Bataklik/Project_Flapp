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
            Rijksregisternummer r2 = new Rijksregisternummer("99%05.12-273.26");

            Bestuurder b1 = new Bestuurder("Declerck", "Tibo", DateTime.Now, r1, RijbewijsType.B);
            Bestuurder b2 = new Bestuurder("Balci", "Burak", DateTime.Parse("12/05/1999"), r2, RijbewijsType.B);
            // DateTime dt = b.Geboortedatum;
            // Console.WriteLine(r.datumToString(dt));
            Console.WriteLine(r1.ToonNummer());
            Console.WriteLine(r2.ToonNummer());
            // Console.WriteLine(r.rijksregisterControle(dt));
        }
    }
}
