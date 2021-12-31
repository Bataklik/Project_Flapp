using Flapp_BLL.Models;
using System.Collections.Generic;

namespace Flapp_BLL.Interfaces
{
    public interface IBrandstofRepo
    {
        Brandstof GeefBrandstof(Brandstof brandstof);
        Brandstof GeefBrandstof(int id);
        IReadOnlyList<Brandstof> GeefAlleBrandstoffen();
        void VoegBrandstofToeAanVoertuig(int voertuig, int brandstofid);
        void VoegBrandstofToeAanTankkaart(int tankkaartId, int brandstofId);
        void VerwijderBrandstofBijVoertuig(int id);
        bool BestaatBrandstof(Brandstof brandstof);
        bool BestaatBrandstof(int id);
        bool BestaatBrandstof(string brandstof_naam);
        void VoegBrandstofToe(Brandstof brandstof);
        void UpdateBrandstof(Brandstof brandstof);
        void UpdateTankkaartBrandstof(Brandstof brandstof, Tankkaart tankkaart);
        void VerwijderBrandstof(int id);
        void VerwijderBrandstof(string brandstof_naam);
        void VerwijderBrandstofBijTankkaart(int tankkaartId);
        
    }
}
