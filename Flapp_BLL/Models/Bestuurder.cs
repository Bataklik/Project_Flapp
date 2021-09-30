using Flapp_BLL.Utils;
using System;

namespace Flapp_BLL.Models
{
    public class Bestuurder
    {
        private string _naam;
        private string _voornaam;
        private Adres _adres;
        private DateTime _geboortedatum;
        private string _rijksregisternummer;
        private RijbewijsType _rijbewijs;
        private Voertuig _voertuig;
        private Tankkaart _tankkaart;
    }
}