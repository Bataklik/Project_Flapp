using Flapp_BLL.Exceptions;
using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Managers
{
    class VoertuigManager
    {
        private IVoertuigRepo repo;

        public VoertuigManager(IVoertuigRepo repo)
        {
            this.repo = repo;
        }

        public void AddVehicle(Voertuig vehicle)
        {
            try
            {
                if (!repo.BestaatVoertuig(vehicle))
                {
                    repo.VoegVoertuigToe(vehicle);
                }
                else
                {
                    throw new VoertuigException("VehicleManager - AddVehicle - Vehicle already added");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateVehicle(Voertuig vehicle)
        {
            try
            {
                // Bestaat vehicle met properties al ?


            }
            catch (Exception ex)
            {
                throw new VoertuigException("");
            }
        }

        public void DeleteVehicle(Voertuig vehicle)
        {
            try
            {
                if (repo.BestaatVoertuig(vehicle))
                {
                    repo.VerwijderVoertuig(vehicle);
                }
                else
                {
                    throw new VoertuigException("VehicleManager - DeleteVehicle - Vehicle already deleted");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
