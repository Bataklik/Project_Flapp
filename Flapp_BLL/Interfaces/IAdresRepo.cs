using Flapp_BLL.Models;
using System.Collections.Generic;

namespace Flapp_BLL.Interfaces
{
    public interface IAdresRepo
    {
        bool BestaatAdres(Adres adres);
        bool BestaatAdres(int id);
        void VoegAdresToe(Adres adres);
        void VerwijderAdres(Adres adres);
        void UpdateAdres(Adres adres);
        Adres GeefAdres(int id);
        Adres GeefAdres(Adres adres);
        List<Adres> GeefAdressen();
    }
}
