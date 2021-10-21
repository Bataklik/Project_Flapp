using Flapp_BLL.Models;

namespace Flapp_BLL.Interfaces
{
    public interface IAdresRepo
    {
        bool BestaatAdres(Adres a);
        void VoegAdresToe(Adres a);
    }
}
