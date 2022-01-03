using Flapp_BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Interfaces {
    public interface IVoertuigRepo {
        Dictionary<int, Voertuig> GeefVoertuigen();
        //IReadOnlyList<Voertuig> ZoekVoertuigen(int? vehicleId, string brand, string model, string chassisNumber, string licensePlate, Brandstof fuelType, string vehicleType, string color, int doors, Bestuurder driver);
        Voertuig GeefVoertuigDoorID(int id);
        //Voertuig ZoekVoertuig(int? vehicleId, string brand, string model, string chassisNumber, string licensePlate, Brandstof fuelType, string vehicleType, string color, int doors, Bestuurder driver);
        bool BestaatVoertuig(Voertuig voertuig);
        int VoegVoertuigToe(Voertuig voertuig);
        void UpdateVoertuig(Voertuig voertuig);
        void VerwijderVoertuig(Voertuig voertuig);
        IReadOnlyList<string> GeefMerken();
        IReadOnlyList<string> GeefModellen(string merk);
        Dictionary<int, Voertuig> ZoekVoertuig(string brand, string model, string licensePlate);
        Dictionary<int, Voertuig> ZoekVoertuigen(string merk, string model, string nummerplaat);
        //bool HeeftBestuurder(Bestuurder bestuurder); Moet kunnen kijken of een voertuig een bestuurder heeft
        //IReadOnlyList<Voertuig> GeefAlleVoertuigenZonderBestuurder() Voor een lijst voor voertuigen zonder bestuurders;
    }
}
