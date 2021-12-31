using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flapp_BLL.Managers
{
    public class VoertuigManager
    {
        private IVoertuigRepo _repo;

        public VoertuigManager(IVoertuigRepo repo)
        {
            _repo = repo;
        }

        public int VoegVoertuigToe(Voertuig voertuig)
        {
            if (_repo.BestaatVoertuig(voertuig)) { throw new VoertuigException("VoertuigManager: VoegVoertuigToe: Voertuig bestaat al!"); }
            try
            {
                return _repo.VoegVoertuigToe(voertuig);
            }
            catch (Exception ex) { throw new VoertuigManagerException(ex.Message); }
        }

        public void UpdateVoertuig(Voertuig voertuig)
        {
            //if (!_repo.BestaatVoertuig(voertuig)) { throw new VoertuigException("VoertuigManager: UpdateVoertuig: Voertuig bestaat niet!"); }
            try
            {
                _repo.UpdateVoertuig(voertuig);
            }
            catch (Exception ex) { throw new VoertuigManagerException(ex.Message); }
        }

        public void VerwijderVoertuig(Voertuig voertuig)
        {
            // if (!_repo.BestaatVoertuig(voertuig)) { throw new VoertuigException("VoertuigManager: VerwijderVoertuig: Voertuig bestaat niet!"); }
            try
            {
                _repo.VerwijderVoertuig(voertuig);
            }
            catch (Exception ex) { throw new VoertuigManagerException(ex.Message); }
        }

        public Dictionary<int, Voertuig> GeefVoertuigen()
        {
            try
            {
                return _repo.GeefVoertuigen();
            }
            catch (Exception ex) { throw new BestuurderManagerException("VoertuigManager: Geef alle voertuigen:", ex); }
        }

        public Voertuig GeefVoertuigDoorID(int id)
        {
            try
            {
                return _repo.GeefVoertuigDoorID(id);
            }
            catch (Exception ex) { throw new BestuurderManagerException("VoertuigManager: Geef voertuig door ID:", ex); }
        }
        public Dictionary<int, Voertuig> ZoekVoertuig(string merk, string model, string nummerplaat)
        {
            try
            {
                return _repo.ZoekVoertuig(merk, model, nummerplaat);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Dictionary<int, Voertuig> ZoekVoertuigen(string merk, string model)
        {
            try
            {
                return _repo.ZoekVoertuigen(merk, model);
            }
            catch (Exception ex) { throw new VoertuigManagerException("VoertuigManager: ZoekVoertuigen", ex); }
        }

        public IReadOnlyList<string> GeefMerken()
        {
            try
            {
                return _repo.GeefMerken();
            }
            catch (Exception ex) { throw new VoertuigManagerException("VoertuigManager: Geef automerken:", ex); }
        }
        public IReadOnlyList<string> GeefModellenMerk(string merk)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(merk)) { throw new VoertuigManagerException("VoertuigManager: Geef modellen: Merk mag niet leeg zijn"); }
                return _repo.GeefModellen(merk);
            }
            catch (Exception ex) { throw new VoertuigManagerException("VoertuigManager: Geef modellen:", ex); }
        }
    }
}
