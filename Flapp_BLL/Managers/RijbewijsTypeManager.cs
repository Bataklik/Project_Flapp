using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions.ManagerExceptions;

namespace Flapp_BLL.Managers {
    public class RijbewijsTypeManager {
        private IRijbewijsTypeRepo _repo;

        public RijbewijsTypeManager(IRijbewijsTypeRepo _repo) {
            this._repo = _repo;
        }

        public void GeefRijbewijs(RijbewijsType rijbewijsType) {
            try {
                if (!_repo.BestaatRijbewijs(rijbewijsType)) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager: RijbewijsType bestaat niet"); }
                _repo.GeefRijbewijs(rijbewijsType);
            }
            catch (Exception ex) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager", ex); }
        }
    }
}
