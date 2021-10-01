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
            string datum = b.Geboortedatum.ToString();
            Console.WriteLine(datum);
        }
    }
}
