using Flapp_BLL.Utils;
using System;

namespace Flapp_CNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Rijksregisternummer("90.02.01-999-02").ToonNummer());
        }
    }
}
