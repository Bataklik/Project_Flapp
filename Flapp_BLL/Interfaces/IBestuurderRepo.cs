using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces {
    public interface IBestuurderRepo {
        bool BestaatBestuurder(Bestuurder bestuurder);
        bool BestaatBestuurderId(int id);
        bool HeeftBestuurderTankkaart(Bestuurder bestuurder);

        Bestuurder GeefBestuurder(Bestuurder bestuurder);
        Dictionary<int, Bestuurder> GeefBestuurders(string naam = null, string voornaam = null, DateTime? geboorte = null, bool? heeftVoertuig = false);

        //Dictionary<int, Bestuurder> GeefAlleBestuurders();
        //Dictionary<int, Bestuurder> GeefBestuurders(string naam, string voornaam);
        //Dictionary<int, Bestuurder> GeefAlleBestuurders(bool heeftVoertuig);
        //Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaam(string naam, bool heeftVoertuig);
        //Dictionary<int, Bestuurder> GeefAlleBestuurdersOpVoornaam(string voornaam, bool heeftVoertuig);
        //Dictionary<int, Bestuurder> GeefAlleBestuurdersOpDatum(DateTime geboorte, bool heeftVoertuig);
        //Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaam(string naam, string voornaam, bool heeftVoertuig);
        //Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaamDatum(string naam, string voornaam, DateTime geboorte, bool heeftVoertuig);
        Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderVoertuig();
        Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderTankkaarten();

        int VoegBestuurderToe(Bestuurder bestuurder);


        void UpdateBestuurder(Bestuurder bestuurder);
        void VerwijderBestuurder(Bestuurder bestuurder);
        void UpdateBestuurder_voertuigId(int VoertuigID);
    }
}
