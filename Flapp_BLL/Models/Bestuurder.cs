using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;
using System;

namespace Flapp_BLL.Models
{
    public class Bestuurder
    {
        private string _naam; //!
        private string _voornaam; //!
        private Adres _adres;
        private DateTime _geboortedatum; //!
        private Rijksregisternummer _rijksregisternummer; //!
        private RijbewijsType _rijbewijs; //!
        private Voertuig _voertuig;
        private Tankkaart _tankkaart;

        public Bestuurder(string naam, string voornaam, DateTime geboortedatum, Rijksregisternummer rijksregisternummer, RijbewijsType rijbewijs)
        {
            _naam = naam;
            _voornaam = voornaam;
            _geboortedatum = geboortedatum;
            _rijksregisternummer = rijksregisternummer;
            _rijbewijs = rijbewijs;
        }

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
        }

        public DateTime Geboortedatum
        {
            get => _geboortedatum;
            set => _geboortedatum = value;
        }

        public Rijksregisternummer Rijksregisternummer { get => _rijksregisternummer; }

        public RijbewijsType Rijbewijs { get => _rijbewijs; set => _rijbewijs = value;; }

        public Voertuig Voertuig
        {
            get => _voertuig;
        }

        public Tankkaart Tankkaart
        {
            get => _tankkaart;
        }
    }
}