using Flapp_BLL.Exceptions;
using Flapp_BLL.Models;
using System;
using System.Linq;

namespace Flapp_BLL.Utils
{
    public class Rijksregisternummer
    {
        private string _nummer;
        private string _datum;

        public Rijksregisternummer(string r)
        {
            //Rijksregisternummer
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerException("Het identificatienummer bevat 11 cijfers"); }
            
            _nummer = r;
            
        }

        public Rijksregisternummer(DateTime dt) {
            if (rijksregisterControle(dt)) { throw new RijksregisternummerException("De geboortedatum komt niet overeen met het rijksregisternummer"); }
            _datum = datumToString(dt);
        }

        public string datumToString(DateTime geboorteDatum) {
            _datum = geboorteDatum.ToString("dd-MM-yy");
            return _datum;
        }



        public bool rijksregisterControle(DateTime geboorteDatum) {
            _datum = datumToString(geboorteDatum);

            string dagGeboorte = _datum[0].ToString();
            dagGeboorte += _datum[1].ToString();

            string maandGeboorte = _datum[3].ToString();
            maandGeboorte += _datum[4].ToString();

            string jaarGeboorte = _datum[6].ToString();
            jaarGeboorte += _datum[7].ToString();

            string jaarRijksregister = _nummer[0].ToString();
            jaarRijksregister += _nummer[1];

            string maandRijksregister = _nummer[3].ToString();
            maandRijksregister += _nummer[4];

            string dagRijksregister = _nummer[6].ToString();
            dagRijksregister += _nummer[7];

            if (dagGeboorte == dagRijksregister && maandGeboorte == maandRijksregister && jaarGeboorte == jaarRijksregister)
                return true;
            else return false;
        }
        

        public string ToonNummer()
        {
            return _nummer;
        }

        public Rijksregisternummer VeranderNummer(string r)
        {
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerException("Het identificatienummer bevat 11 cijfers"); }
            _nummer = r;
            return this;
        }

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
