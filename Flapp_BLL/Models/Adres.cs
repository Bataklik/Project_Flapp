using Flapp_BLL.Exceptions.ModelExpections;
using System;

namespace Flapp_BLL.Models
{
    public class Adres
    {
        #region Props
        public int Id { get; private set; }
        public string Straat { get; private set; }
        public string Huisnummer { get; private set; }
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
        public void ZetHuisnummer(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { throw new AdresException("Huisnummer mag niet leeg zijn!"); }
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
        public Adres(string straat, string huisnummer, string stad, int postcode)
        {
            ZetStraat(straat);
            ZetHuisnummer(huisnummer);
            ZetStad(stad);
            ZetPostcode(postcode);
        }
        public Adres(int id, string straat, string huisnummer, string stad, int postcode)
        {
            ZetId(id);
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

//USE[Project_Flapp_DB];
//CREATE TABLE[dbo].[Adres](
//   [Id][int] IDENTITY(1, 1) PRIMARY KEY,
//   [Straat] [varchar](50) NOT NULL,
//   [Huisnummer] [varchar](50) NOT NULL,
//   [Stad] [varchar](50) NOT NULL,
//   [Postcode] [int] NOT NULL);