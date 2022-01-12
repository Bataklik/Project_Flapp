using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Managers {
    public class TankkaartManager {
        private ITankkaartRepo _repo;

        public TankkaartManager(ITankkaartRepo repo) { _repo = repo; }

        #region Bestaat
        public bool BestaatTankkaart(int kaartnr) {
            try {
                return _repo.BestaatTankkaart(kaartnr);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager - BestaatTankkaart", ex); }
        }
        public bool BestaatTankkaart(Tankkaart tankkaart) {
            try {
                return _repo.BestaatTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager - BestaatTankkaart", ex); }
        }
        #endregion

        #region Crud
        public Tankkaart GeefTankkaart(int kaartnr) {
            try { return _repo.GeefTankkaart(kaartnr); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager - GeefTankkaart", ex); }
        }
        public int VoegTankkaartToe(Tankkaart tankkaart) {
            if (_repo.BestaatTankkaart(tankkaart)) { throw new TankkaartManagerException("TankkaartManager: VoegTankkaartToe: Tankkaart bestaat al!"); }
            try {
                return _repo.VoegTankkaartToe(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager - VoegTankkaartToe", ex); }
        }
        public void VerwijderTankkaart(Tankkaart tankkaart) {
            if (!_repo.BestaatTankkaart(tankkaart.Kaartnummer)) { throw new TankkaartManagerException("TankkaartManager: VerwijderTankkaart: Tankkaart bestaat niet!"); }
            try {
                _repo.VerwijderTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager - VerwijderTankkaart", ex); }
        }
        public void UpdateTankkaart(Tankkaart tankkaart) {
            if (!_repo.BestaatTankkaart(tankkaart.Kaartnummer)) { throw new TankkaartManagerException("TankkaartManager: UpdateTankkaart: Tankkaart bestaat niet!"); }
            try {
                _repo.UpdateTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager - UpdateTankkaart", ex); }
        }
        

        public Dictionary<int, Tankkaart> GeefAlleTankkaarten() {
            try { return _repo.GeefAlleTankkaarten(); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager - GeefAlleTankkaarten", ex); }
        }
        public Dictionary<int, Tankkaart> GeefTankkaarten(int? kaartnummer, DateTime? datum, int? bestuurderid, string naam, string voornaam, DateTime? geboortedatum, string rijksregister, bool strikt = true) {
            try { return _repo.GeefTankkaarten(kaartnummer, datum, bestuurderid, naam, voornaam, geboortedatum, rijksregister, strikt); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager - GeefTankkaarten", ex); }
        }
        public Dictionary<int, Tankkaart> GeefAlleTankkaartenZonderBestuurder(DateTime? start, DateTime? end) {
            try { return _repo.GeefAlleTankkaartenZonderBestuurder(start, end); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        #endregion
    }
}
