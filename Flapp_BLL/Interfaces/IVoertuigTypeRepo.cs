using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Interfaces
{
    public interface IVoertuigTypeRepo
    {
        IReadOnlyList<string> GeefAlleVoertuigTypes();
        //IReadOnlyList<Voertuig> ZoekVoertuigen(int? vehicleId, string brand, string model, string chassisNumber, string licensePlate, Brandstof fuelType, string vehicleType, string color, int doors, Bestuurder driver);
        //Voertuig GeefVoertuigTypeDoorID(int id);
        ////Voertuig ZoekVoertuig(int? vehicleId, string brand, string model, string chassisNumber, string licensePlate, Brandstof fuelType, string vehicleType, string color, int doors, Bestuurder driver);
        //bool BestaatVoertuigType(VoertuigType voertuigtype);
        //void VoegVoertuigTypeToe(VoertuigType voertuigtype);
        //void UpdateVoertuigType(VoertuigType voertuigtype);
        //void VerwijderVoertuigType(VoertuigType voertuig);
    }
}
