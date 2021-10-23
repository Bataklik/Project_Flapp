using System;
using System.Collections.Generic;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;

namespace Flapp_BLL.Managers
{
    public class BestuurderManager
    {
        private IBestuurderRepo _repo;

        public BestuurderManager(IBestuurderRepo repo)
        {
            _repo = repo;
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            try
            {
                if (_repo.BestaatBestuurder(bestuurder)) { throw new Exception("BestuurderManager: Bestuurder bestaat al!"); }
                _repo.VoegBestuurderToe(bestuurder);
            }
            catch (Exception ex) { throw new Exception("BestuurderManager", ex); }
        }
        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            try
            {
                if (!_repo.BestaatBestuurder(bestuurder)) { throw new Exception("BestuurderManager: Bestuurder bestaat niet!"); }
                _repo.VerwijderBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new Exception("BestuurderManager", ex); }
        }
        public void UpdateBestuurder(Bestuurder bestuurder)
        {
            try
            {
                if (!_repo.BestaatBestuurder(bestuurder)) { throw new Exception("BestuurderManager: Bestuurder bestaat niet!"); }
                _repo.UpdateBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new Exception("BestuurderManager", ex); }
        }
        public IReadOnlyList<Bestuurder> GeefAlleBestuurders()
        {
            try
            {
                return _repo.GeefAlleBestuurders();
            }
            catch (Exception ex) { throw new Exception("BestuurderManager", ex); }
        }
    }
}
