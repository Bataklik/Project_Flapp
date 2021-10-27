using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        IReadOnlyList<Bestuurder> GeefAlleBestuurders();
        Bestuurder GeefBestuurder(Bestuurder bestuurder);

        //bool HeeftTankkaart(Tankkaart tankkaart); Moet kunnen kijken of een bestuurder een tankkaart heeft
        //IReadOnlyList<Bestuurder> GeefAlleBestuurdersZonderTankkaarten() Voor een lijst voor bestuurders zonder tankkaarten;

        //bool HeeftVoertuig(Voertuig voertuig); Moet kunnen kijken of een bestuurder een voertuig heeft
        //IReadOnlyList<Bestuurder> GeefAlleVoertuigenZonderBestuurder() Voor een lijst voor bestuurders zonder voertuigen;
    }
}
