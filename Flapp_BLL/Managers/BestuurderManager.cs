using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;

namespace Flapp_BLL.Managers {
    public class BestuurderManager {
        private IBestuurderRepo repo;

        public BestuurderManager(IBestuurderRepo repo) {
            this.repo = repo;
        }

        public void VoegBestuurderToe(Bestuurder bestuurder) {
            try {
                if (repo.BestaatBestuurder(bestuurder)) { throw new Exception(); }
                repo.VoegBestuurderToe(bestuurder);
            }
            catch (Exception) {

                throw;
            }
        }
        public void VerwijderBestuurder(Bestuurder bestuurder) {
            try {
                if (repo.BestaatBestuurder(bestuurder)) { throw new Exception(); }
                repo.VerwijderBestuurder(bestuurder);
            }
            catch (Exception) {

                throw;
            }
        }
        public void UpdateBestuurder(Bestuurder bestuurder) {
            try {
                if (repo.BestaatBestuurder(bestuurder)) { throw new Exception(); }
                repo.UpdateBestuurder(bestuurder);
            }
            catch (Exception) {

                throw;
            }
        }
        IReadOnlyList<Bestuurder> GeefAlleTankkaarten() {
            return;
        }
    }
}
