using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;

namespace Flapp_BLL.Managers
{
    class VoertuigManager
    {
        private IVoertuigRepo _repo;

        public VoertuigManager(IVoertuigRepo repo)
        {
            _repo = repo;
        }

        public void VoegVoertuigToe(Voertuig voertuig)
        {
            if (_repo.BestaatVoertuig(voertuig)) { throw new VoertuigException("VoertuigManager: VoegVoertuigToe: Voertuig bestaat al!"); }
            try
            {
                _repo.VoegVoertuigToe(voertuig);
            }
            catch (Exception ex) { throw new VoertuigManagerException(ex.Message); }
        }

        public void UpdateVoertuig(Voertuig voertuig)
        {
            if (!_repo.BestaatVoertuig(voertuig)) { throw new VoertuigException("VoertuigManager: UpdateVoertuig: Voertuig bestaat niet!"); }
            try
            {
                _repo.UpdateVoertuig(voertuig);
            }
            catch (Exception ex) { throw new VoertuigManagerException(ex.Message); }
        }

        public void VerwijderVoertuig(Voertuig voertuig)
        {
            if (!_repo.BestaatVoertuig(voertuig)) { throw new VoertuigException("VoertuigManager: VerwijderVoertuig: Voertuig bestaat niet!"); }
            try
            {
                _repo.VerwijderVoertuig(voertuig);
            }
            catch (Exception ex) { throw new VoertuigManagerException(ex.Message); }
        }
    }
}
