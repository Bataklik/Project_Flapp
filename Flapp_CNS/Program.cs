using Flapp_BLL.Utils;
using System;

namespace Flapp_CNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Rijksregisternummer r = new Rijksregisternummer("90.02.01-999-02");
            Console.WriteLine(r.ToonNummer());
            r.VeranderNummer("90.02.01-888-02");
            Console.WriteLine(r.ToonNummer());
        }
    }
}
