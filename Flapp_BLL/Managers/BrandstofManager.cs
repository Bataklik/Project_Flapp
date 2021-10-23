using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions.ManagerExceptions;

namespace Flapp_BLL.Managers {
    public class BrandstofManager {
        private IBrandstofRepo _repo;

        public BrandstofManager(IBrandstofRepo _repo) {
            this._repo = _repo;
        }

        public void GeefBrandstof(Brandstof brandstof) {
            try {
                if (!_repo.BestaatBrandstof(brandstof)) { throw new BrandstofManagerException("BrandstofManager: BrandstofType bestaat niet"); }
                _repo.GeefBrandstof(brandstof.Naam);
            }
            catch (Exception ex) { throw new BrandstofManagerException("BrandstofManager", ex); }
        }
    }
}
