using Flapp_BLL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        ObservableCollection<Adres> GeefAdressen();
        Dictionary<int, string> GeefAlleSteden();
        ObservableCollection<string> GeefStratenStad(int postcode, string stad);
        ObservableCollection<Adres> ZoekAdressen(int postcode, string stad, string straat);
    }
}
