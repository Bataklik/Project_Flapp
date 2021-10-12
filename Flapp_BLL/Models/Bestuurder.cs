using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Models
{
    public class Bestuurder
    {
        #region Props
        /* Gegevens die hieronder met ! zijn aangeduid,
        * zijn dingen die verplicht in te vullen zijn bij het aanmaken en/of het editeren */
        public string Naam { get; private set; } // !
        public string Voornaam { get; private set; } // ! 
        public DateTime Geboortedatum { get; private set; } // !
        public string Rijksregisternummer { get; private set; } // !
        public RijbewijsType RijbewijsType { get; private set; } // !

        public Adres Adres { get; private set; }
        public Voertuig Voertuig { get; private set; }
        public Tankkaart Tankkaart { get; private set; }
        public Geslacht Geslacht { get; private set; }
        #endregion

        #region Constructors
        public Bestuurder(string naam, string voornaam, Geslacht geslacht, DateTime geboortedatum, string rijksregisternummer, RijbewijsType rijbewijs)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbijsType(rijbewijs);
        }

        public Bestuurder(string naam, string voornaam, Geslacht geslacht, Adres adres, DateTime geboortedatum, string rijksregisternummer, RijbewijsType rijbewijs, Voertuig voertuig, Tankkaart tankkaart)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbijsType(rijbewijs);
            ZetAdres(adres);
            ZetVoertuig(voertuig);
            ZetTankkaart(tankkaart);
        }
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
        public void ZetGeslacht(Geslacht g)
        {
            Geslacht = g;
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
        public void ZetRijksregisternummer(string r)
        {
            RijksregisterChecker rc = new RijksregisterChecker(r, Geboortedatum, Geslacht);
            if (r == null) { throw new BestuurderException("Bestuuder rijksregisternummer is null!"); }
            if (rc.ControleRijksgisternummer(r, Geboortedatum, Geslacht)) Rijksregisternummer = r;
        }
        public void ZetRijbijsType(RijbewijsType rt)
        {
            RijbewijsType = rt;
        }
        public void ZetVoertuig(Voertuig v)
        {
            if (v == null) { throw new VoertuigException("Bestuurder voertuig is leeg"); }
            if (v.Bestuurder != null) { throw new VoertuigException("Bestuurder voertuig heeft al een bestuurder!"); }
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
                   Rijksregisternummer == bestuurder.Rijksregisternummer &&
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