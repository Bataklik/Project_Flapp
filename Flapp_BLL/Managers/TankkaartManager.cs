using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;

namespace Flapp_BLL.Managers {
    public class TankkaartManager {
        private ITankkaartRepo repo;

        public TankkaartManager(ITankkaartRepo repo) {
            this.repo = repo;
        }

        public void VoegTankkaartToe(Tankkaart tankkaart) {
            try {
                if(repo.BestaatTankkaart(tankkaart)) { throw new Exception(); }
                repo.VoegTankkaartToe(tankkaart);
            }
            catch (Exception) {

                throw;
            }
        }
        public void VerwijderTankkaart(Tankkaart tankkaart) {
            try {
                if(repo.BestaatTankkaart(tankkaart)) { throw new Exception(); }
                repo.VerwijderTankkaart(tankkaart);
            }
            catch (Exception) {

                throw;
            }
        }
        public void UpdateTankkaart(Tankkaart tankkaart) {
            try {
                if (repo.BestaatTankkaart(tankkaart)) { throw new Exception(); }
                repo.UpdateTankkaart(tankkaart);
            }
            catch (Exception) {

                throw;
            }
        }
        IReadOnlyList<Tankkaart> GeefAlleTankkaarten() {
            return;
        }
    }
}
