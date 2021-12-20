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

        public int VoegBestuurderToe(Bestuurder bestuurder)
        {
            if (_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe: Bestuurder bestaat al!"); }
            try
            {
                return _repo.VoegBestuurderToe(bestuurder);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe", ex); }
        }
        public int VoegBestuurderToeZonderAdres(Bestuurder bestuurder)
        {
            if (_repo.BestaatBestuurder(bestuurder)) { throw new BestuurderManagerException("BestuurderManager: VoegBestuurderToe: Bestuurder bestaat al!"); }
            try
            {
                return _repo.VoegBestuurderToeZonderAdres(bestuurder);
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
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaam(string naam)
        {
            try
            {
                return _repo.GeefAlleBestuurdersOpNaam(naam);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpNaam", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpVoornaam(string voornaam)
        {
            try
            {
                return _repo.GeefAlleBestuurdersOpVoornaam(voornaam);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpVoornaam", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpDatum(DateTime date)
        {
            try
            {
                return _repo.GeefAlleBestuurdersOpDatum(date);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpDatum", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaam(string naam, string voornaam)
        {
            try
            {
                return _repo.GeefAlleBestuurdersOpNaamVoornaam(naam, voornaam);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpDatum", ex); }
        }
        public Dictionary<int, Bestuurder> GeefAlleBestuurdersOpNaamVoornaamDate(string naam, string voornaam, DateTime date)
        {
            try
            {
                return _repo.GeefAlleBestuurdersOpNaamVoornaamDatum(naam, voornaam, date);
            }
            catch (Exception ex) { throw new BestuurderManagerException("BestuurderManager: GeefAlleBestuurdersOpNaamVoornaamDate", ex); }
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
