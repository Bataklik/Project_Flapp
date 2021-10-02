using Flapp_BLL.Exceptions;
using System;
using System.Linq;

namespace Flapp_BLL.Utils
{
    public class Rijksregisternummer
    {
        private string _nummer;
        //private string _datum;

        public Rijksregisternummer(string r)
        {
            // 90.02.01-999.02
            if (ControleRijksgisternummer(r))
                _nummer = r;
        }
        public string ToonNummer()
        {
            return _nummer;
        }

        public void VeranderNummer(string r)
        {
            if (ControleRijksgisternummer(r))
                _nummer = r;
        }
        private bool ControleRijksgisternummer(string r)
        {
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerException("Het identificatienummer bevat 11 cijfers"); }
            if (r.Count(e => e == '.') != 3) { throw new RijksregisternummerException("Het Rijksregisternummer is ongeldig!"); }
            return true;
        }
        #region Code
        //public Rijksregisternummer(DateTime dt)
        //{
        //    /* Rijksregisternummer kun je niet zomaar uitrekenen,
        //       want groep twee heb je drie cijfers nodig die dienen om personen te herkennen die op dezelfde dag geboren zijn.
        //       bv: Tibo (01,01,1999) dan krijg je 001, Raf ook zelfde dag geboren krijgt hij 003 (mannen krijgen oneven cijfers/getallen)
        //     */
        //    if (rijksregisterControle(dt)) { throw new RijksregisternummerException("De geboortedatum komt niet overeen met het rijksregisternummer"); }
        //    _datum = datumToString(dt);
        //}
        //public string datumToString(DateTime geboorteDatum)
        //{
        //    _datum = geboorteDatum.ToString("dd-MM-yy");
        //    return _datum;
        //}
        //public bool rijksregisterControle(DateTime geboorteDatum)
        //{
        //    _datum = datumToString(geboorteDatum);

        //    string dagGeboorte = _datum[0].ToString();
        //    dagGeboorte += _datum[1].ToString();

        //    string maandGeboorte = _datum[3].ToString();
        //    maandGeboorte += _datum[4].ToString();

        //    string jaarGeboorte = _datum[6].ToString();
        //    jaarGeboorte += _datum[7].ToString();

        //    string jaarRijksregister = _nummer[0].ToString();
        //    jaarRijksregister += _nummer[1];

        //    string maandRijksregister = _nummer[3].ToString();
        //    maandRijksregister += _nummer[4];

        //    string dagRijksregister = _nummer[6].ToString();
        //    dagRijksregister += _nummer[7];

        //    if (dagGeboorte == dagRijksregister && maandGeboorte == maandRijksregister && jaarGeboorte == jaarRijksregister)
        //        return true;
        //    else
        //        return false;
        //}
        #endregion

        #region Override
        public override bool Equals(object obj)
        {
            return obj is Rijksregisternummer rijksregisternummer &&
                   _nummer == rijksregisternummer._nummer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_nummer);
        }
        #endregion
    }
}
