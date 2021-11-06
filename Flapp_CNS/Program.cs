using Flapp_BLL.Checkers;
using Flapp_BLL.Models;
using System;
using Flapp_BLL.Managers;
using Flapp_DAL.Repository;
using System.Collections.Generic;

namespace Flapp_CNS
{
    class Program
    {

        static void Main(string[] args)
        {
            string connStringBurak = @"Data Source=LAPTOP-BURAQ\SQLEXPRESS;Initial Catalog=Project_Flapp_DB;Integrated Security=True"; ;

            //AdresToevoegen(connStringBurak);
            //BrandstofToevoegen(connStringBurak);
            //RijbewijsToevoegen(connStringBurak);
        }

        private static void RijbewijsToevoegen(string connStringBurak)
        {
            RijbewijsManager rijbewijsManager = new RijbewijsManager(new RijbewijsRepo(connStringBurak));
            Rijbewijs rijbewijs1 = new Rijbewijs("AM");
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
    }
}
