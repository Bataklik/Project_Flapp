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
        public string Geslacht { get; private set; }
        #endregion

        #region Constructors
        public RijksregisternummerChecker(string r, DateTime gd, string g)
        {
            if (ControleRijksgisternummer(r, gd, g)) {
                this.Nummer = r;
                this.Geboortedatum = gd;
                this.Geslacht = g;
            }
            
        }
        #endregion

        #region ZetMethods
        
        
        #endregion

        #region Methods
        public bool ControleRijksgisternummer(string r, DateTime gd, string g) {
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerCheckerException("Het identificatienummer bevat 11 cijfers"); }
            if (r.Count(e => e == '.') != 3) { throw new RijksregisternummerCheckerException("Het Rijksregisternummer is ongeldig!"); }
            if (r.Count(e => e == '-') != 1) { throw new RijksregisternummerCheckerException("Het Rijksregisternummer is ongeldig!"); }
            if (!ControleEersteGroep(r, gd)) { throw new RijksregisternummerCheckerException("De geboortedatum komt niet overeen met het Rijksregisternummer"); }
            if (!ControleTweedeGroep(r, g)) { throw new RijksregisternummerCheckerException("Het geslacht klopt niet"); }
            return true;
        }

        private bool ControleEersteGroep(string r, DateTime gd) {
            DateTime datetime = gd;
            string datum = datetime.ToString("dd/MM/y");
            
            string rijksnr = r;

            //dd/MM/jj
            string dagDatum = datum[0].ToString() + datum[1].ToString();
            
            string maandDatum = datum[3].ToString() + datum[4].ToString();
   
            string jaarDatum = datum[6].ToString() + datum[7].ToString();
       
            //21.10.02-289.65
            string rijksJaar = rijksnr[0].ToString() + rijksnr[1].ToString();
      
            string rijksMaand = rijksnr[3].ToString() + rijksnr[4].ToString();
       
            string rijksDag = rijksnr[6].ToString() + rijksnr[7].ToString();
        
            if (rijksJaar == jaarDatum && rijksDag == dagDatum && rijksMaand == maandDatum) {
                return true;
            }

            else {
                return false;
            }
        }

        private bool ControleTweedeGroep(string r, string g) {
            //21.10.02-289.65
            string tweedeGroep = r[9].ToString() + r[10].ToString() + r[11].ToString();
            g = g.ToUpper();
            //Man van 001 tot 997
            //Vrouw van 002 tot 998.
            int intTweedeGroep = Convert.ToInt32(tweedeGroep);
            
            if (g == "M" && intTweedeGroep % 2 != 0) {
                return true;
            }
            else if (g == "V" && intTweedeGroep % 2 == 0) {
                return true;
            }
            else {
                return false;
            }

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
