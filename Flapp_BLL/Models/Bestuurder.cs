using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;
using System;
using System.Collections.Generic;

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

        #region Constructors
        public Bestuurder(string naam, string voornaam, DateTime geboortedatum, Rijksregisternummer rijksregisternummer, RijbewijsType rijbewijs)
        {
            Naam = naam;
            Voornaam = voornaam;
            Geboortedatum = geboortedatum;
            Rijksregisternummer = rijksregisternummer;
            Rijbewijs = rijbewijs;
        }
        #endregion

        #region Props
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
            set => _adres = value;
        }

        public DateTime Geboortedatum
        {
            get => _geboortedatum;
            set
            {
                _geboortedatum = value.ToUniversalTime();
            }
        }

        public Rijksregisternummer Rijksregisternummer
        {
            get => _rijksregisternummer;
            private set => _rijksregisternummer = value;
        }

        public RijbewijsType Rijbewijs
        {
            get => _rijbewijs;
            set => _rijbewijs = value;
        }

        public Voertuig Voertuig
        {
            get => _voertuig;
            private set => _voertuig = value;
        }

        public Tankkaart Tankkaart
        {
            get => _tankkaart;
            private set => _tankkaart = value;
        }
        #endregion

        #region Methods

        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Bestuurder bestuurder &&
                   _naam == bestuurder._naam &&
                   _voornaam == bestuurder._voornaam &&
                   EqualityComparer<Adres>.Default.Equals(_adres, bestuurder._adres) &&
                   _geboortedatum == bestuurder._geboortedatum &&
                   EqualityComparer<Rijksregisternummer>.Default.Equals(_rijksregisternummer, bestuurder._rijksregisternummer) &&
                   _rijbewijs == bestuurder._rijbewijs &&
                   EqualityComparer<Voertuig>.Default.Equals(_voertuig, bestuurder._voertuig) &&
                   EqualityComparer<Tankkaart>.Default.Equals(_tankkaart, bestuurder._tankkaart);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_naam, _voornaam, _adres, _geboortedatum, _rijksregisternummer, _rijbewijs, _voertuig, _tankkaart);
        }
        #endregion
    }
}