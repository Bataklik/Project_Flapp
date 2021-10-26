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
    public class RijbewijsTypeManager
    {
        private IRijbewijsTypeRepo _repo;

        public RijbewijsTypeManager(IRijbewijsTypeRepo _repo)
        {
            this._repo = _repo;
        }

        //public void GeefRijbewijs(string rijbewijs)
        //{
        //    if (!_repo.BestaatRijbewijs(rijbewijs)) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager: GeefRijbewijs: RijbewijsType bestaat niet"); }
        //    try
        //    {
        //        _repo.GeefRijbewijs(rijbewijs);
        //    }
        //    catch (Exception ex) { throw new RijbewijsTypeManagerException("RijbewijsTypeManager", ex); }
        //}
        public void VoegRijbewijsToe(RijbewijsType rijbewijs)
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
