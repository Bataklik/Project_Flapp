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

        public void VoegVoertuigToe(Voertuig voertuig)
        {
            try
            {
                if (!repo.BestaatVoertuig(voertuig))
                {
                    repo.VoegVoertuigToe(voertuig);
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

        public void UpdateVoertuig(Voertuig voertuig)
        {
            try
            {
                // Bestaat voertuig met properties al ?


            }
            catch (Exception ex)
            {
                throw new VoertuigException(ex.Message);
            }
        }

        public void VerwijderVoertuig(Voertuig voertuig)
        {
            try
            {
                if (repo.BestaatVoertuig(voertuig))
                {
                    repo.VerwijderVoertuig(voertuig);
                }
                else
                {
                    throw new VoertuigException("VoeruigManager - VerwijderVoertuig - Voertuig bestaat al");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
