using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Managers {
    public class TankkaartManager {
        private ITankkaartRepo _repo;

        public TankkaartManager(ITankkaartRepo repo) { _repo = repo; }

        public int VoegTankkaartToe(Tankkaart tankkaart) {
            if (_repo.BestaatTankkaart(tankkaart)) { throw new Exception("TankkaartManager: VoegTankkaartToe: Tankkaart bestaat al!"); }
            try {
                return _repo.VoegTankkaartToe(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public void VerwijderTankkaart(Tankkaart tankkaart) {
            if (!_repo.BestaatTankkaart(tankkaart.Kaartnummer)) { throw new Exception("TankkaartManager: VerwijderTankkaart: Tankkaart bestaat niet!"); }
            try {
                _repo.VerwijderTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public void UpdateTankkaart(Tankkaart tankkaart) {
            if (!_repo.BestaatTankkaart(tankkaart.Kaartnummer)) { throw new Exception("TankkaartManager: UpdateTankkaart: Tankkaart bestaat niet!"); }
            try {
                _repo.UpdateTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public Dictionary<int, Tankkaart> GeefAlleTankkaarten(int kaartnummer, DateTime? geldigheidsdatum) {
            try { return _repo.GeefAlleTankkaarten(kaartnummer, geldigheidsdatum); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public Dictionary<int, Tankkaart> GeefAlleTankkaarten() {
            try { return _repo.GeefAlleTankkaarten(); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public Dictionary<int, Tankkaart> GeefAlleTankkaartenOpKaartnummer(int kaartnummer) {
            try { return _repo.GeefAlleTankkaartenOpKaartnummer(kaartnummer); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public Dictionary<int, Tankkaart> GeefAlleTankkaartenOpGeldigheidsdatum(DateTime geldigheidsdatum) {
            try { return _repo.GeefAlleTankkaartenOpGeldigheidsdatum(geldigheidsdatum); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public bool BestaatBestuurder(Bestuurder bestuurder) {
            try {
                return _repo.BestaatBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public void VoegBestuurderToe(Bestuurder bestuurder) {
            if (_repo.BestaatBestuurder(bestuurder)) { throw new Exception("TankkaartManager: BestaatBestuurder: Bestuurder bestaat al!"); }
            try {
                //_repo.VoegBestuurderToe(bestuurder);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public void VerwijderBestuurder(Bestuurder bestuurder) {
            if (!_repo.BestaatBestuurder(bestuurder)) { throw new TankkaartManagerException("TankkaartManager: VerwijderBestuurder: Bestuurder bestaat niet"); }
            try {
                //_repo.VerwijderBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
    }
}
