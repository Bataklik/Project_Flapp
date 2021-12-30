using System;
using System.Collections.Generic;
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
        public int VoegBestuurderToeZonderAdres(Bestuurder bestuurder) {
            if (_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe: Bestuurder bestaat al!"); }
            try {
                return _repo.VoegBestuurderToeZonderAdres(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToeZonderAdres", ex); }
        }

        public void VerwijderBestuurder(Bestuurder bestuurder) {
            if (!_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VerwijderBestuurder: Bestuurder bestaat niet!"); }
            try {
                _repo.VerwijderBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VerwijderBestuurder", ex); }
        }
        public void VerwijderTankkaartVanBestuurder(Bestuurder bestuurder) {
            try {
                _repo.VerwijderTankkaartVanBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VerwijderBestuurder", ex); }
        }

        public Dictionary<int, Bestuurder> GeefAlleBestuurders(bool heeftVoertuig) {
            try {
                return _repo.GeefAlleBestuurders(heeftVoertuig);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurders", ex); }
        }

        public void UpdateBestuurder(Bestuurder bestuurder) {
            if (!_repo.BestaatBestuurderId(bestuurder.Id)) { throw new BestuurderManagerException("BestuurderManager: UpdateBestuurder: Bestuurder bestaat niet!"); }
            try {
                _repo.UpdateBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: UpdateBestuurder", ex); }
        }
        public void VoegVoertuigToeAanBestuurder(Bestuurder b) {
            if (!_repo.BestaatBestuurderId(b.Id)) { throw new BestuurderManagerException("BestuurderManager: UpdateBestuurder: Bestuurder bestaat niet!"); }
            try {
                _repo.VoegVoertuigToeAanBestuurder(b);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VoegVoertuigToeAanBestuurder", ex); }
        }
        public void VoegTankkaartToeAanBestuurder(Tankkaart t) {
            try {
                _repo.VoegTankkaartToeAanBestuurder(t);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VoegTankkaartToeAanBestuurder", ex); }
        }

        public Dictionary<int, Bestuurder> GeefAlleBestuurders() {
            try {
                return _repo.GeefAlleBestuurders();
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurders", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaam(string naam, bool heeftVoertuig) {
            try {
                return _repo.GeefAlleBestuurdersOpNaam(naam, heeftVoertuig);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpNaam", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpVoornaam(string voornaam, bool heeftVoertuig) {
            try {
                return _repo.GeefAlleBestuurdersOpVoornaam(voornaam, heeftVoertuig);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpVoornaam", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpDatum(DateTime date, bool heeftVoertuig) {
            try {
                return _repo.GeefAlleBestuurdersOpDatum(date, heeftVoertuig);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpDatum", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaam(string naam, string voornaam, bool heeftVoertuig) {
            try {
                return _repo.GeefAlleBestuurdersOpNaamVoornaam(naam, voornaam, heeftVoertuig);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpDatum", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaamDate(string naam, string voornaam, DateTime date, bool heeftVoertuig) {
            try {
                return _repo.GeefAlleBestuurdersOpNaamVoornaamDatum(naam, voornaam, date, heeftVoertuig);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpNaamVoornaamDate", ex); }
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
    }
}
