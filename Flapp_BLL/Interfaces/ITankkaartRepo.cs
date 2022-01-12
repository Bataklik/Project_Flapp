using Flapp_BLL.Models;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Interfaces {
    public interface ITankkaartRepo {
        #region Bestaat
        bool BestaatTankkaart(Tankkaart tankkaart);
        bool BestaatTankkaart(int kaart);
        #endregion

        #region Crud
        Tankkaart GeefTankkaart(int kaartnr);
        int VoegTankkaartToe(Tankkaart tankkaart);
        void VerwijderTankkaart(Tankkaart tankkaart);
        void UpdateTankkaart(Tankkaart tankkaart);

        Dictionary<int, Tankkaart> GeefAlleTankkaarten();
        Dictionary<int, Tankkaart> GeefTankkaarten(int? kaartnummer, DateTime? datum, int? bestuurderid, string naam, string voornaam, DateTime? geboortedatum, string rijksregister, bool strikt = true);
        Dictionary<int, Tankkaart> GeefAlleTankkaartenZonderBestuurder(DateTime? startDt, DateTime? endDt);
        #endregion
    }
}
