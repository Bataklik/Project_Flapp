using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;

namespace Flapp_BLL.Managers
{
    public class AdresManager
    {
        private IAdresRepo _repo;

        public AdresManager(IAdresRepo repo)
        {
            _repo = repo;
        }

        public void VoegAdresToe(Adres adres)
        {
            if (_repo.BestaatAdres(adres)) { throw new AdresManagerException("AdresManager: VoegAdresToe: Adres bestaat al!"); }
            try
            {
                _repo.VoegAdresToe(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager", ex); }
        }

        public void VerwijderAdres(Adres adres)
        {
            if (!_repo.BestaatAdres(adres)) { throw new AdresManagerException("AdresManager: VerwijderAdres: Adres bestaat niet!"); }
            try
            {
                _repo.VerwijderAdres(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager", ex); }
        }
        public void UpdateAdres(Adres adres)
        {
            if (!_repo.BestaatAdres(adres)) { throw new AdresManagerException("AdresManager: UpdateAdres: Adres bestaat niet!"); }
            try
            {
                _repo.UpdateAdres(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager", ex); }
        }

        public Adres GeefAdres(int id)
        {
            if (!_repo.BestaatAdres(id)) { throw new AdresManagerException("AdresManager: GeefAdres: Adres bestaat niet!"); }
            try
            {
                return _repo.GeefAdres(id);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager", ex); }
        }
        public Adres GeefAdres(Adres adres)
        {
            if (!_repo.BestaatAdres(adres)) { throw new AdresManagerException("AdresManager: GeefAdres: Adres bestaat niet!"); }
            try
            {
                return _repo.GeefAdres(adres);
            }
            catch (Exception ex) { throw new AdresManagerException("AdresManager", ex); }
        }
    }
}
