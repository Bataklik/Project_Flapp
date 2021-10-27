using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces
{
    public interface ITankkaartRepo
    {
        bool BestaatTankkaart(Tankkaart tankkaart);
        bool BestaatTankkaart(int kaart);
        void VoegTankkaartToe(Tankkaart tankkaart);
        void VerwijderTankkaart(Tankkaart tankkaart);
        void UpdateTankkaart(Tankkaart tankkaart);
        IReadOnlyList<Tankkaart> GeefAlleTankkaarten();

        bool HeeftBestuurder(Bestuurder bestuurder); //Moet kunnen kijken of een voertuig een bestuurder heeft
        IReadOnlyList<Voertuig> GeefAlleVoertuigenZonderBestuurder(); //Voor een lijst voor voertuigen zonder bestuurders;
    }
}
