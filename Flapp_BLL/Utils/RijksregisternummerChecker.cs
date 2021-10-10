using Flapp_BLL.Exceptions;
using System;
using System.Linq;
using Flapp_BLL.Models;
using System.Collections.Generic;

namespace Flapp_BLL.Utils
{
    public class RijksregisternummerChecker
    {
        #region Props
        public string Nummer { get; private set; }
        public DateTime Geboortedatum { get; private set; }
        #endregion

        #region Constructors
        public RijksregisternummerChecker(string r, DateTime geboortedatum)
        {
            if (ControleRijksgisternummer(r, geboortedatum)) {
                this.Nummer = r;
                this.Geboortedatum = geboortedatum;
            }
            
        }
        #endregion

        #region ZetMethods
        
        
        #endregion

        #region Methods
        public bool ControleRijksgisternummer(string r, DateTime geboortedatum) {
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerCheckerException("Het identificatienummer bevat 11 cijfers"); }
            if (r.Count(e => e == '.') != 3) { throw new RijksregisternummerCheckerException("Het Rijksregisternummer is ongeldig!"); }
            if (r.Count(e => e == '-') != 1) { throw new RijksregisternummerCheckerException("Het Rijksregisternummer is ongeldig!"); }
            if (!ControleEersteGroep(r, geboortedatum)) { throw new RijksregisternummerCheckerException("De geboortedatum komt niet overeen met het Rijksregisternummer"); }
            return true;
        }

        private bool ControleEersteGroep(string r, DateTime geboortedatum) {
            DateTime datetime = geboortedatum;
            string datum = datetime.ToString("dd/MM/y");
            
            string rijksnr = r;

            //dd/MM/jj
            string dagDatum = datum[0].ToString();
            dagDatum += datum[1].ToString();

            string maandDatum = datum[3].ToString();
            maandDatum += datum[4].ToString();

            string jaarDatum = datum[6].ToString();
            jaarDatum += datum[7].ToString();

            //21.10.02-289.65
            string rijksJaar = rijksnr[0].ToString();
            rijksJaar += rijksnr[1].ToString();

            string rijksMaand = rijksnr[3].ToString();
            rijksMaand += rijksnr[4].ToString();

            string rijksDag = rijksnr[6].ToString();
            rijksDag += rijksnr[7].ToString();

            if (rijksJaar == jaarDatum && rijksDag == dagDatum && rijksMaand == maandDatum)
                return true;
            else
                return false;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"[Rijksregisternummer] {Nummer}";
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Nummer);
        }
        #endregion
    }
}
