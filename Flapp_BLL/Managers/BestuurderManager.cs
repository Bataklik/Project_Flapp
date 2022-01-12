using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;

namespace Flapp_BLL.Managers {
    public class BestuurderManager {
        private IBestuurderRepo _repo;

        public BestuurderManager(IBestuurderRepo repo) {
            _repo = repo;
        }

        public bool HeeftBestuurderTankkaart(Bestuurder bestuurder) {
            try {
                return _repo.HeeftBestuurderTankkaart(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VerwijderBestuurder", ex); }
        }

        public int VoegBestuurderToe(Bestuurder bestuurder) {
            if (_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe: Bestuurder bestaat al!"); }
            try {
                return _repo.VoegBestuurderToe(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe", ex); }
        }

        public void VerwijderBestuurder(Bestuurder bestuurder) {
            if (!_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VerwijderBestuurder: Bestuurder bestaat niet!"); }
            try {
                _repo.VerwijderBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VerwijderBestuurder", ex); }
        }



        public void UpdateBestuurder(Bestuurder bestuurder) {
            if (!_repo.BestaatBestuurderId(bestuurder.Id)) { throw new BestuurderManagerException("BestuurderManager: UpdateBestuurder: Bestuurder bestaat niet!"); }
            try {
                _repo.UpdateBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: UpdateBestuurder", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurders(string naam = null, string voornaam = null, DateTime? geboorte = null, bool heeftVoertuig = false) {
            try {
                return _repo.GeefBestuurders(naam, voornaam, geboorte, heeftVoertuig);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurders", ex); }
        }
        public Dictionary<int, Bestuurder> GeefBestuurders(string naam, string voornaam) {
            try {
                return _repo.GeefBestuurders(naam, voornaam);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefBestuurders", ex); }
        }

        public Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderTankkaarten() {
            try {
                return _repo.GeefAlleBestuurdersZonderTankkaarten();
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersZonderTankkaarten", ex); }

        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersZonderVoertuig() {
            try {
                return _repo.GeefAlleBestuurdersZonderVoertuig();
            }
            catch (Exception ex) { throw new BestuurderManagerException(ex.Message); }

        }
        public void UpdateBestuurder_voertuigId(int VoertuigID)
        {
            try
            {
                _repo.UpdateBestuurder_voertuigId(VoertuigID);
            }
            catch (Exception ex) { throw new BestuurderManagerException(ex.Message); }
        }
    }
}
