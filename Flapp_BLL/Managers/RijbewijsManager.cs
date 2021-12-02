using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using Flapp_BLL.Exceptions.ManagerExceptions;
using System.Collections.ObjectModel;

namespace Flapp_BLL.Managers
{
    public class RijbewijsManager
    {
        private IRijbewijsRepo _repo;

        public RijbewijsManager(IRijbewijsRepo _repo)
        {
            this._repo = _repo;
        }

        public bool BestaatRijbewijs(Rijbewijs rijbewijs)
        {
            return _repo.BestaatRijbewijs(rijbewijs);
        }
        public Rijbewijs GeefRijbewijs(Rijbewijs rijbewijs)
        {
            if (!_repo.BestaatRijbewijs(rijbewijs)) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager: GeefRijbewijs: Rijbewijs bestaat niet!"); }
            try
            {
                return _repo.GeefRijbewijs(rijbewijs); ;
            }
            catch (Exception ex) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager", ex); }

        }
        public Rijbewijs GeefRijbewijs(int id)
        {
            if (!_repo.BestaatRijbewijs(id)) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager: GeefRijbewijs: Rijbewijs bestaat niet!"); }
            try
            {
                return _repo.GeefRijbewijs(id); ;
            }
            catch (Exception ex) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager", ex); }
        }
        public List<Rijbewijs> GeefAlleRijbewijzen()
        {
            try
            {
                return _repo.GeefAlleRijbewijzen();
            }
            catch (Exception ex) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager", ex); }
        }
        public void VoegRijbewijsToe(Rijbewijs rijbewijs)
        {
            if (_repo.BestaatRijbewijs(rijbewijs)) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager: VoegRijbewijsToe: RijbewijsType bestaat al"); }
            try
            {
                _repo.VoegRijbewijsToe(rijbewijs);
            }
            catch (Exception ex) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager", ex); }
        }
    }
}
