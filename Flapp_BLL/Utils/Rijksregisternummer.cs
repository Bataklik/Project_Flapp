using Flapp_BLL.Exceptions;
using System;
using System.Linq;
using Flapp_BLL.Models;

namespace Flapp_BLL.Utils
{
    public class Rijksregisternummer {
        private string _nummer;
        public Bestuurder _bestuurder;

        public Rijksregisternummer(string r) {
            if (ControleRijksgisternummer(r))
                _nummer = r;
        }
        public string ToonNummer() {
            return _nummer;
        }
        public void VeranderNummer(string r) {
            if (ControleRijksgisternummer(r))
                _nummer = r;
        }
        private bool ControleRijksgisternummer(string r) {
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerException("Het identificatienummer bevat 11 cijfers"); }
            if (r.Count(e => e == '.') != 3) { throw new RijksregisternummerException("Het Rijksregisternummer is ongeldig!"); }
            if (r.Count(e => e == '-') != 1) { throw new RijksregisternummerException("Het Rijksregisternummer is ongeldig!"); }
            return true;
        }
        public void ZetBestuurder(Bestuurder newBestuurder) {
            if (newBestuurder == null) throw new Exception();
            if (newBestuurder == _bestuurder) throw new Exception();
            if (_bestuurder != null)
                _bestuurder = newBestuurder;
        }
        public bool ControleEersteGroep(string r) {
            DateTime datetime = _bestuurder.Geboortedatum;
            string datum = datetime.ToString("dd/MM/y");

            r = _nummer;

            string dagDatum = datum[0].ToString();
            dagDatum += datum[1].ToString();

            string maandDatum = datum[3].ToString();
            maandDatum += datum[4].ToString();

            string jaarDatum = datum[6].ToString();
            jaarDatum += datum[7].ToString();

            //21.10.02-289.65
            string rijksJaar = _nummer[0].ToString();
            rijksJaar += _nummer[1].ToString();
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

        public override string ToString() {
            return $"[Rijksregisternummer] {_nummer}";
        }
        #endregion
    }
}
