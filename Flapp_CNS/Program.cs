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
            // Connectie string
            string connStringBurak = @"Data Source=LAPTOP-BURAQ\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True";

            // Managers
            BrandstofManager bm = new BrandstofManager(new BrandstofRepo(connStringBurak));
            RijbewijsTypeManager rbtm = new RijbewijsTypeManager(new RijbewijsTypeRepo(connStringBurak));

            // Models
            Adres a1 = new Adres(1, "Frans Uyttenhovestraat", "91", "Gent", 9000);

            RijbewijsType rbtB = new RijbewijsType("B");

            Brandstof bs1 = new Brandstof("Elektrisch");

            Voertuig v1 = new Voertuig(1, "Tesla", "X", "13245678957903251", "2-ABC-123", bs1, "Stationwagen", "Zwart", 5);

            Tankkaart t1 = new Tankkaart(1, DateTime.Parse("06/08/2025"));

            Bestuurder b1 = new Bestuurder("Declerck", "Tibo", Geslacht.M, a1, "06/08/1999", "99.08.06-289.17", rbtB, v1, t1);

            rbtm.VoegRijbewijsToe(rbtB);
        }
    }
}
