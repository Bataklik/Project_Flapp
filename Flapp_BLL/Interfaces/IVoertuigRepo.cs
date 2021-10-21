using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Interfaces
{
    public interface IVoertuigRepo
    {
        IReadOnlyList<Voertuig> GeefAlleVoertuigen();
        //IReadOnlyList<Voertuig> ZoekVoertuigen(int? vehicleId, string brand, string model, string chassisNumber, string licensePlate, Brandstof fuelType, string vehicleType, string color, int doors, Bestuurder driver);
        Voertuig GeefVoertuig(int vId);
        //Voertuig ZoekVoertuig(int? vehicleId, string brand, string model, string chassisNumber, string licensePlate, Brandstof fuelType, string vehicleType, string color, int doors, Bestuurder driver);
        bool BestaatVoertuig(Voertuig v);
        void VoegVoertuigToe(Voertuig v);
        void UpdateVoertuig(Voertuig v);
        void VerwijderVoertuig(Voertuig v);
    }
}
