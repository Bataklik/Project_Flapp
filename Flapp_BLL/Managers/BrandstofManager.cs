using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Managers {
    public class BrandstofManager {
        private IBrandstofRepo _repo;

        public BrandstofManager(IBrandstofRepo _repo) {
            this._repo = _repo;
        }

        public void VoegBrandstofToeAanVoertuig(int voertuig, List<Brandstof> brandstof) {
            try {
                brandstof.ForEach(b => _repo.VoegBrandstofToeAanVoertuig(voertuig, b.Id));
            }
            catch (Exception) { throw; }
        }
        public void VoegBrandstofToeAanTankkaart(int tankkaartId, List<Brandstof> brandstof) {
            try {
                brandstof.ForEach(b => _repo.VoegBrandstofToeAanTankkaart(tankkaartId, b.Id));
            }
            catch (Exception) { throw; }
        }
        public void VerwijderBrandstofBijVoertuig(int id) {
            try { _repo.VerwijderBrandstofBijVoertuig(id); }
            catch (Exception) { throw; }
        }
        public Brandstof GeefBrandstof(Brandstof brandstof) {
            if (!_repo.BestaatBrandstof(brandstof)) { throw new BrandstofManagerException("BrandstofManager: GeefBrandstof: BrandstofType bestaat niet!"); }

            try { return _repo.GeefBrandstof(brandstof); }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void GeefBrandstof(int id) {
            if (!_repo.BestaatBrandstof(id)) { throw new BrandstofManagerException("BrandstofManager: GeefBrandstof: BrandstofType bestaat niet!"); }

            try { _repo.GeefBrandstof(id); }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void VoegBrandstofToe(Brandstof brandstof) {
            if (_repo.BestaatBrandstof(brandstof)) { throw new BrandstofManagerException("BrandstofManager: VoegBrandstofToe: BrandstofType bestaat al!"); }
            try {
                _repo.VoegBrandstofToe(brandstof);
            }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void UpdateBrandstof(Brandstof brandstof) {
            if (_repo.BestaatBrandstof(brandstof)) { throw new BrandstofManagerException("BrandstofManager: UpdateBrandstof: BrandstofType bestaat al!"); }
            try {
                _repo.UpdateBrandstof(brandstof);
            }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void UpdateTankkaartBrandstof(List<Brandstof> brandstof, Tankkaart tankkaart) {
            try {
                brandstof.ForEach(b => _repo.UpdateTankkaartBrandstof(b, tankkaart));
            }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void VerwijderBrandstof(int id) {
            if (!_repo.BestaatBrandstof(id)) { throw new BrandstofManagerException("BrandstofManager: VerwijderBrandstof: BrandstofType bestaat niet!"); }
            _repo.VerwijderBrandstof(id);
        }
        public void VerwijderBrandstofBijTankkaart(int tankkaartId) {
            try {
                _repo.VerwijderBrandstofBijTankkaart(tankkaartId);
            }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public IReadOnlyList<Brandstof> GeefAlleBrandstoffen() {
            try {
                return _repo.GeefAlleBrandstoffen();
            }
            catch (Exception ex) { throw new BestuurderManagerException("VoertuigManager: Geef alle Brandstoffen:", ex); }
        }
    }
}
