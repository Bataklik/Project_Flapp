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
            string connStringBurak = @"Data Source=LAPTOP-BURAQ\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True"; ;
            BestuurderRepo repo = new BestuurderRepo(connStringBurak);
            var b = repo.GeefBestuurder(2);
            //Geven
            //RijbewijsGevenId(connStringBurak);
            //RijbewijsGevenObj(connStringBurak);
            //AdresGevenId(connStringBurak);
            //AdresGevenObj(connStringBurak);

            //Toevoegen
            //AdresToevoegen(connStringBurak);
            //BrandstofToevoegen(connStringBurak);
            //RijbewijsToevoegen(connStringBurak);
        }

        #region Toevoegen
        private static void RijbewijsToevoegen(string conn)
        {
            RijbewijsManager rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(conn));
            Rijbewijs rijbewijs1 = new Rijbewijs("C");
            rijbewijsManager.VoegRijbewijsToe(rijbewijs1);
        }
        private static void AdresToevoegen(string conn)
        {
            AdresManager adresManager = new AdresManager(new AdresRepo(conn));
            Adres adres1 = new Adres("Pannekoekstraat", "7", "Pannekoek", 1111);

            adresManager.VoegAdresToe(adres1);

        }
        private static void BrandstofToevoegen(string conn)
        {
            BrandstofManager brandstofManager = new BrandstofManager(new BrandstofRepo(conn));
            Brandstof brandstof1 = new Brandstof("Gas");

            brandstofManager.VoegBrandstofToe(brandstof1);
        }
        #endregion

        #region Geef
        private static Rijbewijs RijbewijsGevenId(string conn)
        {
            RijbewijsManager rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(conn));
            Rijbewijs result = rijbewijsManager.GeefRijbewijs(4);
            Console.WriteLine(result);
            return result;
        }
        private static Rijbewijs RijbewijsGevenObj(string conn)
        {
            RijbewijsManager rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(conn));
            Rijbewijs result = rijbewijsManager.GeefRijbewijs(new Rijbewijs("B"));
            Console.WriteLine(result);
            return result;
        }
        private static Adres AdresGevenId(string conn)
        {
            AdresManager adresManager = new AdresManager(new AdresRepo(conn));
            Adres result = adresManager.GeefAdres(2);
            Console.WriteLine(result);
            return result;
        }
        private static Adres AdresGevenObj(string conn)
        {
            AdresManager adresManager = new AdresManager(new AdresRepo(conn));
            Adres result = adresManager.GeefAdres(new Adres("Keizerstraat", "1", "Gent", 9000));
            Console.WriteLine(result);
            return result;
        }
        #endregion
    }
}
