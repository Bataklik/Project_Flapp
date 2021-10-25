using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions.ManagerExceptions;

namespace Flapp_BLL.Managers
{
    public class BrandstofManager
    {
        private IBrandstofRepo _repo;

        public BrandstofManager(IBrandstofRepo _repo)
        {
            this._repo = _repo;
        }

        public void GeefBrandstof(Brandstof brandstof)
        {
            if (!_repo.BestaatBrandstof(brandstof)) { throw new BrandstofManagerException("BrandstofManager: GeefBrandstof: BrandstofType bestaat niet!"); }

            try { _repo.GeefBrandstof(brandstof); }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void GeefBrandstof(int id)
        {
            if (!_repo.BestaatBrandstof(id)) { throw new BrandstofManagerException("BrandstofManager: GeefBrandstof: BrandstofType bestaat niet!"); }

            try { _repo.GeefBrandstof(id); }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void VoegBrandstofToe(Brandstof brandstof)
        {
            if (_repo.BestaatBrandstof(brandstof)) { throw new BrandstofManagerException("BrandstofManager: VoegBrandstofToe: BrandstofType bestaat al!"); }
            try
            {
                _repo.VoegBrandstofToe(brandstof);
            }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void UpdateBrandstof(Brandstof brandstof)
        {
            if (_repo.BestaatBrandstof(brandstof)) { throw new BrandstofManagerException("BrandstofManager: UpdateBrandstof: BrandstofType bestaat al!"); }
            try
            {
                _repo.UpdateBrandstof(brandstof);
            }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
        public void VerwijderBrandstof(int id)
        {
            if (!_repo.BestaatBrandstof(id)) { throw new BrandstofManagerException("BrandstofManager: VerwijderBrandstof: BrandstofType bestaat niet!"); }
            _repo.VerwijderBrandstof(id);
        }
        public void VerwijderBrandstof(string naam)
        {
            if (!_repo.BestaatBrandstof(naam)) { throw new BrandstofManagerException("BrandstofManager: VerwijderBrandstof: BrandstofType bestaat niet!"); }
            _repo.VerwijderBrandstof(naam);
        }
    }
}
