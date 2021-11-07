using System;
using System.Collections.Generic;
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

        bool BestaatBestuurder(Bestuurder bestuurder); //Moet kunnen kijken of een tankkaart een bestuurder heeft
        //void VoegBestuurderToe(Bestuurder bestuurder);
        //void VerwijderBestuurder(Bestuurder bestuurder);
        //IReadOnlyList<Tankkaart> GeefAlleTankkaarten(); //Voor een lijst voor tankkaarten zonder bestuurders;
    }
}
