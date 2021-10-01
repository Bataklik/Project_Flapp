using Flapp_BLL.Exceptions;
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

        public string Naam
        {
            get => _naam;
            set
            {
                if (string.IsNullOrEmpty(value)) { throw new LeegException("Naam mag niet leeg zijn!"); }
                _naam = value;
            }
        }

        public string Voornaam
        {
            get => _voornaam;
            set
            {
                if (string.IsNullOrEmpty(value)) { throw new LeegException("Voornaam mag niet leeg zijn!"); }
                _voornaam = value;
            }
        }

        public Adres Adres
        {
            get => _adres;
            set
            {
                _adres = value ?? throw new AdresException("Adres mag niet leeg zijn!");
            }
        }


    }
}