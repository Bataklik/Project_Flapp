using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces
{
    public interface IAdresRepo
    {
        bool BestaatAdres(Adres adres);
        void VoegAdresToe(Adres adres);
        void VerwijderAdres(Adres adres);
        void UpdateAdres(Adres adres);
    }
}
