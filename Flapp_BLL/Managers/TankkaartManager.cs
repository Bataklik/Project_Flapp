using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;

namespace Flapp_BLL.Managers
{
    public class TankkaartManager
    {
        private ITankkaartRepo _repo;

        public TankkaartManager(ITankkaartRepo repo) { _repo = repo; }

        public void VoegTankkaartToe(Tankkaart tankkaart)
        {
            try
            {
                if (_repo.BestaatTankkaart(tankkaart)) { throw new Exception(); }
                _repo.VoegTankkaartToe(tankkaart);
            }
            catch (Exception ex) { throw new Exception("TankkaartManager", ex); }
        }
        public void VerwijderTankkaart(Tankkaart tankkaart)
        {
            try
            {
                if (_repo.BestaatTankkaart(tankkaart)) { throw new Exception(); }
                _repo.VerwijderTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new Exception("TankkaartManager", ex); }
        }
        public void UpdateTankkaart(Tankkaart tankkaart)
        {
            try
            {
                if (_repo.BestaatTankkaart(tankkaart)) { throw new Exception(); }
                _repo.UpdateTankkaart(tankkaart);
            }
            catch (Exception ex) { throw new Exception("TankkaartManager", ex); }
        }
        IReadOnlyList<Tankkaart> GeefAlleTankkaarten()
        {
            try { return _repo.GeefAlleTankkaarten(); }
            catch (Exception ex) { throw new Exception("TankkaartManager", ex); }
        }
    }
}
