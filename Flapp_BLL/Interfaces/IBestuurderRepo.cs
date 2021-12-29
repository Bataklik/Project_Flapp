using System;
using System.Collections.Generic;
using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces {
    public interface IBestuurderRepo {
        bool BestaatBestuurder(Bestuurder bestuurder);
        bool BestaatBestuurderId(int id);
        bool HeeftBestuurderTankkaart(Bestuurder bestuurder);

        Dictionary<int, Bestuurder> GeefAlleBestuurders();
        Dictionary<int, Bestuurder> GeefAlleBestuurders(bool heeftVoertuig);

        Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaam(string naam, bool heeftVoertuig);
        Dictionary<int, Bestuurder> GeefAlleBestuurdersOpVoornaam(string voornaam, bool heeftVoertuig);
        Dictionary<int, Bestuurder> GeefAlleBestuurdersOpDatum(DateTime geboorte, bool heeftVoertuig);
        Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaam(string naam, string voornaam, bool heeftVoertuig);
        Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaamDatum(string naam, string voornaam, DateTime geboorte, bool heeftVoertuig);
        void VoegVoertuigToeAanBestuurder(Bestuurder b);
        Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderVoertuig();
        Bestuurder GeefBestuurder(Bestuurder bestuurder);
        IReadOnlyList<Bestuurder> GeefAlleBestuurdersZonderTankkaarten();

        int VoegBestuurderToe(Bestuurder bestuurder);
        int VoegBestuurderToeZonderAdres(Bestuurder bestuurder);

        void UpdateBestuurder(Bestuurder bestuurder);
        void VerwijderBestuurder(Bestuurder bestuurder);

        void VoegTankkaartToeAanBestuurder(Tankkaart t);
        void VerwijderTankkaartVanBestuurder(Bestuurder bestuurder);
        //bool HeeftTankkaart(Tankkaart tankkaart); Moet kunnen kijken of een bestuurder een tankkaart heeft
        //bool HeeftVoertuig(Voertuig voertuig); Moet kunnen kijken of een bestuurder een voertuig heeft
    }
}
