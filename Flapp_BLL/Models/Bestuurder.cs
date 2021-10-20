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
        public Bestuurder(string naam, string voornaam, Geslacht geslacht, string geboortedatum, string rijksregisternummer, RijbewijsType rijbewijs)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbewijsType(rijbewijs);
        }

        public Bestuurder(string naam, string voornaam, Geslacht geslacht, Adres adres, string geboortedatum, string rijksregisternummer, RijbewijsType rijbewijs, Voertuig voertuig, Tankkaart tankkaart)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbewijsType(rijbewijs);
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
        public void ZetGeboortedatum(string d)
        {
            DateTime _d;
            if (!DateTime.TryParse(d, out _d)) { throw new BestuurderException("Bestuurder geboortedatum is geen geboortedatum"); }
            if (_d > DateTime.Now) { throw new BestuurderException("Bestuurder geboortedatum is groter dan vandaag!"); }
            Geboortedatum = _d;
        }
        public void ZetRijksregisternummer(string r)
        {
            RijksregisterChecker rc = new RijksregisterChecker(r, Geboortedatum, Geslacht);
            if (r == null) { throw new BestuurderException("Bestuuder rijksregisternummer is null!"); }
            if (rc.ControleRijksgisternummer(r, Geboortedatum, Geslacht)) Rijksregisternummer = r;
        }
        public void ZetRijbewijsType(RijbewijsType rt)
        {
            RijbewijsType = rt;
        }
        public void ZetVoertuigg(Voertuig value)
        {
            // Misschien Fout
            if (value == null) { throw new BestuurderException("Bestuurder: ZetVoertuig: Voertuig bestaat niet!"); }
            if (value == Voertuig) { throw new BestuurderException("Bestuurder: ZetVoertuig: Voertuig is hetzelfde!"); }
            if (value != null) value.zetBestuurder(this);
            Voertuig = value;
        }
        public void ZetTankkaart(Tankkaart tk)
        {
            Tankkaart = tk ?? throw new BestuurderException("Bestuurder tankkaart is null!");
        }

        public void ZetVoertuig(Voertuig nieuwVoertuig)
        {

            if (nieuwVoertuig != null)
            {
                if (this.Voertuig == null)
                {
                    if (!nieuwVoertuig.HeeftBestuurder(this))
                    {
                        nieuwVoertuig.zetBestuurder(this);
                    }
                }
                else if (this.Voertuig != nieuwVoertuig)
                {
                    //
                    if (this.Voertuig.HeeftBestuurder(this))
                    {
                        this.Voertuig.VerwijderBestuurder(); //Als zijn vorige auto nog steeds over de bestuurder beschikt
                    }
                    if (!nieuwVoertuig.HeeftBestuurder(this))
                    {
                        nieuwVoertuig.VerwijderBestuurder();
                        nieuwVoertuig.zetBestuurder(this);
                    }
                }
                Voertuig = nieuwVoertuig;
                if (!nieuwVoertuig.HeeftBestuurder(this))
                {
                    nieuwVoertuig.VerwijderBestuurder();
                    nieuwVoertuig.zetBestuurder(this);
                }
            }
            else
            {
                throw new BestuurderException("Voertuig - zetVoertuig: Nieuw voertuig is null");
            }
        }
        #endregion

        #region Methods
        public void VerwijderVoertuig() //Verwijder voertuig bij bestuurder
        {
            Voertuig = null;
        }
        public bool HeeftVoertuig(Voertuig voertuig) // Heeft de bestuurder al een voertuig?
        {
            if (Voertuig != null)
            {
                if (this.Voertuig == voertuig)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
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

//USE [Project_Flapp_DB];
//CREATE TABLE [dbo].[Bestuurder](
//	[id] [int] IDENTITY(1,1) PRIMARY KEY,
//	[naam] [varchar](50) NOT NULL,
//	[voornaam] [varchar](50) NOT NULL,
//	[geboortedatum] [date] NOT NULL,
//	[rijksregister] [varchar](11) NOT NULL,
//	[rijbewijstype_id] [int] FOREIGN KEY REFERENCES Rijbewijs(id),
//	[adres_id] [int] NOT NULL,
//	[voertuig_id] [int] NULL,
//	[tankkaart_id] [int] NULL,
//	[geslacht] [bit] NOT NULL)