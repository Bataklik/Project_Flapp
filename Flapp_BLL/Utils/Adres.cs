using Flapp_BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flapp_BLL.Utils
{
    public class Adres
    {
        private string _straat;
        private int _huisnummer;
        private string _stad;
        private int _postcode;

        public string Straat
        {
            get => _straat;
            private set
            {
                if (string.IsNullOrEmpty(value)) { throw new StraatException("Straatnaam mag niet leeg zijn!"); }
                _straat = value;
            }
        }

        public int Huisnummer
        {
            get => _huisnummer;
            private set
            {
                _huisnummer = value;
            }
        }

        public string Stad
        {
            get => _stad;
            private set
            {
                if (string.IsNullOrEmpty(value)) { throw new StadException("Stadnaam mag niet leeg zijn!"); }
                _stad = value;
            }
        }

        public int Postcode
        {
            get => _postcode; private set
            {
                if (value < 1000 || value > 9999) { throw new PostcodeException("Postcodes zijn groter dan 1000 en kleiner dan 9999"); }
                _postcode = value;
            }
        }

        public Adres(string straat, int huisnummer, string stad, int postcode)
        {
            Straat = straat;
            Huisnummer = huisnummer;
            Stad = stad;
            Postcode = postcode;
        }

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Adres adres &&
                   _straat == adres._straat &&
                   _huisnummer == adres._huisnummer &&
                   _stad == adres._stad &&
                   _postcode == adres._postcode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_straat, _huisnummer, _stad, _postcode);
        }
        #endregion
    }
}