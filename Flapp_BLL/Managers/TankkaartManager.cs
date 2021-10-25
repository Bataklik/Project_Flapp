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
    public class TankkaartManager
    {
        private ITankkaartRepo _repo;

        public TankkaartManager(ITankkaartRepo repo) { _repo = repo; }

        public void VoegTankkaartToe(Tankkaart tankkaart)
        {
            if (_repo.BestaatTankkaart(tankkaart)) { throw new Exception("TankkaartManager: VoegTankkaartToe: Tankkaart bestaat al!"); }
            try
            {
                _repo.VoegTankkaartToe(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public void VerwijderTankkaart(Tankkaart tankkaart)
        {
            if (!_repo.BestaatTankkaart(tankkaart)) { throw new Exception("TankkaartManager: VerwijderTankkaart: Tankkaart bestaat niet!"); }
            try
            {
                _repo.VerwijderTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        public void UpdateTankkaart(Tankkaart tankkaart)
        {
            if (!_repo.BestaatTankkaart(tankkaart)) { throw new Exception("TankkaartManager: UpdateTankkaart: Tankkaart bestaat niet!"); }
            try
            {
                _repo.UpdateTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
        IReadOnlyList<Tankkaart> GeefAlleTankkaarten()
        {
            try { return _repo.GeefAlleTankkaarten(); }
            catch (Exception ex) { throw new TankkaartManagerException("TankkaartManager", ex); }
        }
    }
}
