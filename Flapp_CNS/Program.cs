using Flapp_BLL.Utils;
using Flapp_BLL.Models;
using System;

namespace Flapp_CNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Rijksregisternummer r = new Rijksregisternummer("21.10.02-289.1");
            Bestuurder b = new Bestuurder("Declerck", "Tibo", DateTime.Now, r, RijbewijsType.B);
            DateTime dt = b.Geboortedatum;
            Console.WriteLine(r.datumToString(dt));
            Console.WriteLine(r.ToonNummer());
            Console.WriteLine(r.rijksregisterControle(dt));
        }
    }
}
