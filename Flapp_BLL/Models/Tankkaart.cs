using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Models
{
    public class Tankkaart
    {
        private int _kaartnummer; //!
        private DateTime _geldigheidsdatum; //!
        private int _pincode;
        private Brandstof _brandstoftype;
        private Bestuurder _bestuurder;
        private bool _geblokkeerd;

        #region Constructors
        public Tankkaart(int kaartnummer, DateTime geldigheidsdatum)
        {
            Kaartnummer = kaartnummer;
            Geldigheidsdatum = geldigheidsdatum;
            _geblokkeerd = false;
        }
        #endregion

        #region Props
        public int Kaartnummer
        {
            get => _kaartnummer;
            set => _kaartnummer = value;
        }

        public DateTime Geldigheidsdatum
        {
            get => _geldigheidsdatum;
            set
            {
                if (value < DateTime.Now) { throw new GeldigeheidsdatumException("Geledigheidsdatum is al verlopen!"); }
                _geldigheidsdatum = value;
            }
        }

        public int Pincode
        {
            get => _pincode;
            private set => _pincode = value;
        }

        public Brandstof Brandstoftype
        {
            get => _brandstoftype;
            private set
            {
                if (value == null) { throw new BrandstofException("Brandstof mag niet null zijn"); }
                _brandstoftype = value;
            }
        }

        public Bestuurder Bestuurder
        {
            get => Bestuurder;
            private set
            {
                if (value == null) { throw new BestuurderException("Bestuurder bestaat niet!"); }
                _bestuurder = value;
            }
        }

        public bool Geblokkeerd
        {
            get => _geblokkeerd;
        }
        #endregion

        #region Methods
        public void VeranderBlokeerStatus(bool status)
        {
            _geblokkeerd = status;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Tankkaart tankkaart &&
                   _kaartnummer == tankkaart._kaartnummer &&
                   _geldigheidsdatum == tankkaart._geldigheidsdatum &&
                   _pincode == tankkaart._pincode &&
                   EqualityComparer<Brandstof>.Default.Equals(_brandstoftype, tankkaart._brandstoftype) &&
                   EqualityComparer<Bestuurder>.Default.Equals(_bestuurder, tankkaart._bestuurder) &&
                   _geblokkeerd == tankkaart._geblokkeerd;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_kaartnummer, _geldigheidsdatum, _pincode, _brandstoftype, _bestuurder, _geblokkeerd);
        }
        #endregion
    }
}