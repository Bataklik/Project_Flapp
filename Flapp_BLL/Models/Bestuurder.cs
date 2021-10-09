using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Models
{
    public class Bestuurder
    {
        #region Constructors
        public Bestuurder(string naam, string voornaam, DateTime geboortedatum, Rijksregisternummer rijksregisternummer, RijbewijsType rijbewijs)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbijsType(rijbewijs);
        }

        public Bestuurder(string naam, string voornaam, Adres adres, DateTime geboortedatum, Rijksregisternummer rijksregisternummer, RijbewijsType rijbewijs, Voertuig voertuig, Tankkaart tankkaart)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbijsType(rijbewijs);
            ZetAdres(adres);
            ZetVoertuig(voertuig);
            ZetTankkaart(tankkaart);
        }
        #endregion

        #region Props
        public string Naam { get; private set; }
        public string Voornaam { get; private set; }
        public Adres Adres { get; private set; }
        public DateTime Geboortedatum { get; private set; }
        public Rijksregisternummer Rijksregisternummer { get; private set; }
        public RijbewijsType RijbewijsType { get; private set; }
        public Voertuig Voertuig { get; private set; }
        public Tankkaart Tankkaart { get; private set; }
        #endregion

        #region ZetMethods
        public void ZetNaam(string n)
        {
            if (string.IsNullOrWhiteSpace(n)) { throw new BestuurderException("Naam mag niet leeg zijn!"); }
            Naam = n;
        }
        public void ZetVoornaam(string n)
        {
            if (string.IsNullOrWhiteSpace(n)) { throw new BestuurderException("Naam mag niet leeg zijn!"); }
            Voornaam = n;
        }
        public void ZetAdres(Adres a)
        {
            if (a == null) { throw new BestuurderException("Bestuurder adres is null!"); }
            Adres = a;
        }
        public void ZetGeboortedatum(DateTime d)
        {
            Geboortedatum = d;
        }
        public void ZetRijksregisternummer(Rijksregisternummer r)
        {
            if (r == null) { throw new BestuurderException("Bestuuder rijksregisternummer is null!"); }
            Rijksregisternummer = r;
        }
        public void ZetRijbijsType(RijbewijsType rt)
        {
            RijbewijsType = rt;
        }
        public void ZetVoertuig(Voertuig v)
        {
            if (v == null) { throw new BestuurderException("Bestuuder voertuig is null!"); }
            Voertuig = v;
        }
        public void ZetTankkaart(Tankkaart tk)
        {
            Tankkaart = tk ?? throw new BestuurderException("Bestuurder tankkaart is null!");
        }
        #endregion

        #region Methods

        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Bestuurder bestuurder &&
                   Naam == bestuurder.Naam &&
                   Voornaam == bestuurder.Voornaam &&
                   EqualityComparer<Adres>.Default.Equals(Adres, bestuurder.Adres) &&
                   Geboortedatum == bestuurder.Geboortedatum &&
                   EqualityComparer<Rijksregisternummer>.Default.Equals(Rijksregisternummer, bestuurder.Rijksregisternummer) &&
                   RijbewijsType == bestuurder.RijbewijsType &&
                   EqualityComparer<Voertuig>.Default.Equals(Voertuig, bestuurder.Voertuig) &&
                   EqualityComparer<Tankkaart>.Default.Equals(Tankkaart, bestuurder.Tankkaart);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam, Voornaam, Adres, Geboortedatum, Rijksregisternummer, RijbewijsType, Voertuig, Tankkaart);
        }
        #endregion
    }
}