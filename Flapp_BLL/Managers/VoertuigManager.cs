using Flapp_BLL.Exceptions.ManagerExceptions;
using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Managers
{
    public class VoertuigManager
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

        public Dictionary<int, Voertuig> GeefAlleVoertuigen()
        {
            try
            {
                return _repo.GeefAlleVoertuigen();
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
        public Dictionary<int, Voertuig> SearchVehicle(string brand, string model, string licensePlate)
        {
            try
            {
                return _repo.SearchVehicle(brand, model, licensePlate);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<Voertuig> VoertuigZoeken(string? nummerplaat, string? merk, string? model/*, Klant _klantSave*/)
        {
            try
            {
                return _repo.VoertuigZoeken(nummerplaat, merk, model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public IReadOnlyList<string> geefMerken()
        {
            try
            {
                return _repo.GeefMerken();
            }
            catch (Exception ex) { throw new VoertuigManagerException("VoertuigManager: Geef automerken:", ex); }
        }
        public IReadOnlyList<string> geefModellen(string merk)
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
