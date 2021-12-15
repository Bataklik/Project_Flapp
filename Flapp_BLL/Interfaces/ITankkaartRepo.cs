using Flapp_BLL.Models;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Interfaces
{
    public interface ITankkaartRepo
    {
        bool BestaatTankkaart(Tankkaart tankkaart);
        bool BestaatTankkaart(int kaart);
        int VoegTankkaartToe(Tankkaart tankkaart);
        void VerwijderTankkaart(Tankkaart tankkaart);
        void UpdateTankkaart(Tankkaart tankkaart);

        bool BestaatBestuurder(Bestuurder bestuurder); //Moet kunnen kijken of een tankkaart een bestuurder heeft
        //void VoegBestuurderToe(Bestuurder bestuurder);
        //void VerwijderBestuurder(Bestuurder bestuurder);
        Dictionary<int, Tankkaart> GeefAlleTankkaarten(int? kaartnummer, DateTime? geldigheidsdatum); //Voor een lijst voor tankkaarten;
        Dictionary<int, Tankkaart> GeefAlleTankkaarten(); //Voor een lijst voor tankkaarten;
    }
}
