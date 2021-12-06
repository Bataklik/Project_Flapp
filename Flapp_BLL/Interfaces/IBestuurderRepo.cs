using System.Collections.Generic;
using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces
{
    public interface IBestuurderRepo
    {
        bool BestaatBestuurder(Bestuurder bestuurder);
        bool BestaatBestuurderId(int id);
        void VoegBestuurderToe(Bestuurder bestuurder);
        void VerwijderBestuurder(Bestuurder bestuurder);
        void UpdateBestuurder(Bestuurder bestuurder);
        Dictionary<int, Bestuurder> GeefAlleBestuurders();
        Dictionary<int, Bestuurder> GeefAlleBestuurders(int top);
        Bestuurder GeefBestuurder(Bestuurder bestuurder);

        //bool HeeftTankkaart(Tankkaart tankkaart); Moet kunnen kijken of een bestuurder een tankkaart heeft
        IReadOnlyList<Bestuurder> GeefAlleBestuurdersZonderTankkaarten();
        void VoegBestuurderToeZonderAdres(Bestuurder bestuurder);
        //bool HeeftVoertuig(Voertuig voertuig); Moet kunnen kijken of een bestuurder een voertuig heeft
    }
}
