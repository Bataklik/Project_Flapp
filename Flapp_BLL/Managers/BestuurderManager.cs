using System;
using System.Collections.Generic;
using Flapp_BLL.Exceptions.ManagerExceptions;
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
            if (_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe: Bestuurder bestaat al!"); }
            try
            {
                _repo.VoegBestuurderToe(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe", ex); }
        }
        public void VoegBestuurderToeZonderAdres(Bestuurder bestuurder)
        {
            if (_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe: Bestuurder bestaat al!"); }
            try
            {
                _repo.VoegBestuurderToeZonderAdres(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToeZonderAdres", ex); }
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            if (!_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VerwijderBestuurder: Bestuurder bestaat niet!"); }
            try
            {
                _repo.VerwijderBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VerwijderBestuurder", ex); }
        }
        public void UpdateBestuurder(Bestuurder bestuurder)
        {
            if (!_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: UpdateBestuurder: Bestuurder bestaat niet!"); }
            try
            {
                _repo.UpdateBestuurder(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: UpdateBestuurder", ex); }
        }

        public Dictionary<int, Bestuurder> GeefAlleBestuurders()
        {
            try
            {
                return _repo.GeefAlleBestuurders();
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurders", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurders(int top)
        {
            try
            {
                return _repo.GeefAlleBestuurders(top);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersTOP", ex); }
        }
        public IReadOnlyList<Bestuurder> GeefAlleBestuurdersZonderTankkaarten()
        {
            try
            {
                return _repo.GeefAlleBestuurdersZonderTankkaarten();
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersZonderTankkaarten", ex); }

        }
    }
}
