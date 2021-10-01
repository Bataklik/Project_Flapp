using Flapp_BLL.Utils;
using Flapp_BLL.Models;
using System;

namespace Flapp_CNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Rijksregisternummer r = new Rijksregisternummer("99.08.06-289.17");
            Bestuurder b = new Bestuurder("Declerck", "Tibo", DateTime.Now, r, RijbewijsType.B);
            DateTime dt = b.Geboortedatum;
            string datumString = r.datumToString(dt);
            Console.WriteLine(datumString);
            Console.WriteLine(r.ToonNummer());
        }
    }
}
