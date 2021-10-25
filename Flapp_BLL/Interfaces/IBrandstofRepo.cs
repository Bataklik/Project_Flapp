using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces
{
    public interface IBrandstofRepo
    {
        Brandstof GeefBrandstof(Brandstof brandstof);
        Brandstof GeefBrandstof(int id);
        bool BestaatBrandstof(Brandstof brandstof);
        bool BestaatBrandstof(int id);
        bool BestaatBrandstof(string brandstof_naam);
        void VoegBrandstofToe(Brandstof brandstof);
        void UpdateBrandstof(Brandstof brandstof);
        void VerwijderBrandstof(int id);
        void VerwijderBrandstof(string brandstof_naam);
    }
}
