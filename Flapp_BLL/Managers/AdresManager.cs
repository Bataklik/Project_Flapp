using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Managers
{
    public class AdresManager
    {
        private IAdresRepo _repo;

        public AdresManager(IAdresRepo repo)
        {
            _repo = repo;
        }

        public void VoegAdresToe(Adres a)
        {
            try
            {
                if (_repo.BestaatAdres(a)) { throw new Exception("AdresManager: VoegAdresToe: Adres bestaat al!"); }
                _repo.VoegAdresToe(a);
            }
            catch (Exception ex) { throw new Exception("AdresManager", ex); }
        }

        public void VerwijderAdres(Adres a)
        {
            try
            {
                if (!_repo.BestaatAdres(a)) { throw new Exception("AdresManager: VerwijderAdres: Adres bestaat niet!"); }
                _repo.VerwijderAdres(a);
            }
            catch (Exception ex) { throw new Exception("AdresManager", ex); }
        }

        public void UpdateAdres(Adres a)
        {
            try
            {
                if (!_repo.BestaatAdres(a)) { throw new Exception("AdresManager: UpdateAdres: Adres bestaat niet!"); }
                _repo.UpdateAdres(a);
            }
            catch (Exception ex) { throw new Exception("AdresManager", ex); }
        }
    }
}
