using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flapp_BLL.Managers {
    public class AdresManager {
        private IAdresRepo _repo;

        public AdresManager(IAdresRepo repo) {
            _repo = repo;
        }

        public bool BestaatAdres(Adres adres) {
            try {
                return _repo.BestaatAdres(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: BestaatAdres", ex); }
        }

        public Dictionary<int, string> GeefAlleSteden() {
            try {
                return _repo.GeefAlleSteden();
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: GeefAlleSteden", ex); }
        }

        public void VoegAdresToe(Adres adres) {
            if (_repo.BestaatAdres(adres)) { throw new AdresManagerException("AdresManager: VoegAdresToe: Adres bestaat al!"); }
            try {
                _repo.VoegAdresToe(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: VoegAdresToe", ex); }
        }

        public void VerwijderAdres(Adres adres) {
            if (!_repo.BestaatAdres(adres)) { throw new AdresManagerException("AdresManager: VerwijderAdres: Adres bestaat niet!"); }
            try {
                _repo.VerwijderAdres(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: VerwijderAdres", ex); }
        }

        public ObservableCollection<Adres> GeefAdressen() {
            try {
                return _repo.GeefAdressen();
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: GeefAdressen", ex); }
        }

        public void UpdateAdres(Adres adres) {
            if (!_repo.BestaatAdres(adres)) { throw new AdresManagerException("AdresManager: UpdateAdres: Adres bestaat niet!"); }
            try {
                _repo.UpdateAdres(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: UpdateAdres", ex); }
        }

        public ObservableCollection<string> GeefStratenStad(int postcode, string stad) {
            return _repo.GeefStratenStad(postcode, stad);
        }

        public Adres GeefAdres(int id) {
            if (!_repo.BestaatAdres(id)) { throw new AdresManagerException("AdresManager: GeefAdres: Adres bestaat niet!"); }
            try {
                return _repo.GeefAdres(id);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: GeefAdres(id)", ex); }
        }

        public ObservableCollection<Adres> ZoekAdressen(string stad, string straat) {
            try {
                return _repo.ZoekAdressen(stad, straat);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: ZoekAdressen", ex); }
        }

        public Adres GeefAdres(Adres adres) {
            if (!_repo.BestaatAdres(adres)) { throw new AdresManagerException("AdresManager: GeefAdres: Adres bestaat niet!"); }
            try {
                return _repo.GeefAdres(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager: GeefAdres(adres)", ex); }
        }
    }
}
