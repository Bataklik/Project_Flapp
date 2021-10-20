using Flapp_BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flapp_BLL.Models
{
    public class Adres
    {
        #region Props
        public int Id { get; private set; }
        public string Straat { get; private set; }
        public int Huisnummer { get; private set; }
        public string Stad { get; private set; }
        public int Postcode { get; private set; }
        #endregion

        #region ZetMethods
        public void ZetId(int value)
        {
            if (value <= 0) { throw new AdresException("Adres id moet positief zijn!"); }
            Id = value;
        }
        public void ZetStraat(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { throw new AdresException("Straatnaam mag niet leeg zijn!"); }
            Straat = value;
        }
        public void ZetHuisnummer(int value)
        {
            if (value <= 0) { throw new AdresException("Huisnummer mag niet onder nul zijn!"); }
            Huisnummer = value;
        }
        public void ZetStad(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { throw new AdresException("Stadnaam mag niet leeg zijn!"); }
            Stad = value;
        }
        public void ZetPostcode(int value)
        {
            if (value < 1000 || value > 9999) { throw new AdresException("Postcodes zijn groter dan 1000 en kleiner dan 9999"); }
            Postcode = value;
        }
        #endregion

        #region Construtors
        public Adres(string straat, int huisnummer, string stad, int postcode)
        {
            ZetStraat(straat);
            ZetHuisnummer(huisnummer);
            ZetStad(stad);
            ZetPostcode(postcode);
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"[Adres] {Straat}, {Huisnummer}, {Stad}: {Postcode}";
        }

        public override bool Equals(object obj)
        {
            return obj is Adres adres &&
                   Straat == adres.Straat &&
                   Huisnummer == adres.Huisnummer &&
                   Stad == adres.Stad &&
                   Postcode == adres.Postcode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Straat, Huisnummer, Stad, Postcode);
        }
        #endregion
    }
}