using Flapp_BLL.Exceptions;
using System;
using System.Linq;
using Flapp_BLL.Models;
using System.Collections.Generic;

namespace Flapp_BLL.Utils
{
    public class Rijksregisternummer
    {
        #region Constructors
        public Rijksregisternummer(string r)
        {
            if (ControleRijksgisternummer(r))
                Nummer = r;
        }
        #endregion

        #region Props
        public string Nummer { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        #endregion

        #region ZetMethods
        public void ZetNummer(string nummer)
        {
            if (ControleRijksgisternummer(nummer))
                Nummer = nummer;
        }
        public void ZetBestuurder(Bestuurder newBestuurder)
        {
            if (newBestuurder == null) throw new Exception();
            if (newBestuurder == Bestuurder) throw new Exception();
            if (Bestuurder != null)
                Bestuurder = newBestuurder;
        }
        #endregion

        #region Methods
        private bool ControleRijksgisternummer(string r)
        {
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerException("Het identificatienummer bevat 11 cijfers"); }
            if (r.Count(e => e == '.') != 3) { throw new RijksregisternummerException("Het Rijksregisternummer is ongeldig!"); }
            if (r.Count(e => e == '-') != 1) { throw new RijksregisternummerException("Het Rijksregisternummer is ongeldig!"); }
            return true;
        }
        public bool ControleEersteGroep(string r)
        {
            DateTime datetime = Bestuurder.Geboortedatum;
            string datum = datetime.ToString("dd/MM/y");

            r = Nummer;

            string dagDatum = datum[0].ToString();
            dagDatum += datum[1].ToString();

            string maandDatum = datum[3].ToString();
            maandDatum += datum[4].ToString();

            string jaarDatum = datum[6].ToString();
            jaarDatum += datum[7].ToString();

            //21.10.02-289.65
            string rijksJaar = Nummer[0].ToString();
            rijksJaar += Nummer[1].ToString();

            return false;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"[Rijksregisternummer] {Nummer}";
        }

        public override bool Equals(object obj)
        {
            return obj is Rijksregisternummer rijksregisternummer &&
                   Nummer == rijksregisternummer.Nummer &&
                   EqualityComparer<Bestuurder>.Default.Equals(Bestuurder, rijksregisternummer.Bestuurder);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nummer, Bestuurder);
        }
        #endregion
    }
}
