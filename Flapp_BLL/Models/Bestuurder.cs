using Flapp_BLL.Checkers;
using Flapp_BLL.Exceptions.ModelExpections;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<Rijbewijs> RijbewijsType { get; private set; }// !

        public Adres Adres { get; private set; }
        public Voertuig Voertuig { get; private set; }
        public Tankkaart Tankkaart { get; private set; }
        public Geslacht Geslacht { get; private set; }
        public int Id { get; private set; }
        #endregion

        #region Constructors
        public Bestuurder(int id, string naam, string voornaam, string geboortedatum)
        {
            ZetId(id);
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeboortedatum(geboortedatum);
        }

        public Bestuurder(string naam, string voornaam, Geslacht geslacht, string geboortedatum, string rijksregisternummer, List<Rijbewijs> rijbewijs)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbewijsLijst(rijbewijs);
        }

        public Bestuurder(string naam, string voornaam, Geslacht geslacht, Adres adres, string geboortedatum, string rijksregisternummer, List<Rijbewijs> rijbewijs, Voertuig voertuig, Tankkaart tankkaart)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbewijsLijst(rijbewijs);
            ZetAdres(adres);
            ZetVoertuig(voertuig);
            ZetTankkaart(tankkaart);
        }

        public Bestuurder(int id, string naam, string voornaam, Geslacht geslacht, string geboortedatum, string rijksregisternummer, List<Rijbewijs> rijbewijs)
        {
            ZetId(id);
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbewijsLijst(rijbewijs);
        }

        public Bestuurder(int id, string naam, string voornaam, Geslacht geslacht, Adres adres, string geboortedatum, string rijksregisternummer, List<Rijbewijs> rijbewijs, Voertuig voertuig, Tankkaart tankkaart)
        {
            ZetId(id);
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbewijsLijst(rijbewijs);
            ZetAdres(adres);
            ZetVoertuig(voertuig);
            ZetTankkaart(tankkaart);
        }

        public Bestuurder(string naam, string voornaam, Geslacht geslacht, Adres adres, string geboortedatum, string rijksregisternummer, List<Rijbewijs> rijbewijs) : this(naam, voornaam, geslacht, geboortedatum, rijksregisternummer, rijbewijs)
        {
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetGeslacht(geslacht);
            ZetGeboortedatum(geboortedatum);
            ZetRijksregisternummer(rijksregisternummer);
            ZetRijbewijsLijst(rijbewijs);
            ZetAdres(adres);
        }
        #endregion

        #region ZetMethods
        public void ZetId(int id)
        {
            if (id <= 0) { throw new BestuurderException("Id moet positief zijn!"); }
            Id = id;
        }
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
            //if (a == null) { throw new BestuurderException("Bestuurder adres is null!"); }
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
            RijksregisternummerChecker rc = new RijksregisternummerChecker(r, Geboortedatum, Geslacht);
            if (r == null) { throw new BestuurderException("Bestuuder rijksregisternummer is null!"); }
            if (rc.ControleRijksregisternummer(r, Geboortedatum, Geslacht)) Rijksregisternummer = r;
        }
        public void ZetRijbewijsLijst(List<Rijbewijs> rt)
        {
            if (rt == null) { throw new BestuurderException("Rijbewijs lijst is null!"); }
            RijbewijsType = rt;
        }
        public void ZetTankkaart(Tankkaart tk)
        {
            Tankkaart = tk; //?? throw new BestuurderException("Bestuurder tankkaart is null!");
        }
        public void ZetVoertuig(Voertuig nieuwVoertuig)
        {

            if (nieuwVoertuig != null)
            {
                if (Voertuig == null)
                {
                    if (!nieuwVoertuig.HeeftBestuurder(this))
                    {
                        nieuwVoertuig.ZetBestuurder(this);
                    }
                }
                else if (Voertuig != nieuwVoertuig)
                {
                    //
                    if (Voertuig.HeeftBestuurder(this))
                    {
                        Voertuig.VerwijderBestuurder(); //Als zijn vorige auto nog steeds over de bestuurder beschikt
                    }
                    if (!nieuwVoertuig.HeeftBestuurder(this))
                    {
                        nieuwVoertuig.VerwijderBestuurder();
                        nieuwVoertuig.ZetBestuurder(this);
                    }
                }
                Voertuig = nieuwVoertuig;
                if (!nieuwVoertuig.HeeftBestuurder(this))
                {
                    nieuwVoertuig.VerwijderBestuurder();
                    nieuwVoertuig.ZetBestuurder(this);
                }
            }
            else
            {
                Voertuig = nieuwVoertuig;
                //throw new BestuurderException("Voertuig - zetVoertuig: Nieuw voertuig is null");
            }
        }
        #endregion

        #region Methods
        public void VerwijderTankkaart()
        {
            Tankkaart = null;
        }
        public void VerwijderVoertuig() //Verwijder voertuig bij bestuurder
        {
            Voertuig = null;
        }
        public bool HeeftVoertuig(Voertuig voertuig) // Heeft de bestuurder al een voertuig?
        {
            if (voertuig == null) { throw new VoertuigException("Voertuig: HeeftVoertuig: Voertuig parameter is null!"); }
            if (Voertuig != null)
            {
                if (Voertuig == voertuig) { return true; }
                else { return false; }
            }
            else { return false; }
        }
        public void VoegRijbewijsToe(Rijbewijs rbt)
        {
            if (rbt == null) { throw new BestuurderException("Rijbewijs is null!"); }
            RijbewijsType.Add(rbt);
        }

        public string GeefNaam()
        {
            return $"{Voornaam} {Naam}";
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"\n---------------{GetType().Name}---------\n" +
                    $"{Naam}, {Voornaam}, {Geboortedatum.ToShortDateString()}\n" +
                    $"{Rijksregisternummer}, {string.Join(", ", RijbewijsType.OrderBy(r => r.Naam))}\n" +
                    $"--------------------------------";
        }

        public override bool Equals(object obj)
        {
            return obj is Bestuurder bestuurder &&
                   Naam == bestuurder.Naam &&
                   Voornaam == bestuurder.Voornaam &&
                   Geboortedatum == bestuurder.Geboortedatum &&
                   Rijksregisternummer == bestuurder.Rijksregisternummer &&
                   Id == bestuurder.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam, Voornaam, Geboortedatum, Rijksregisternummer, Id);
        }

        #endregion
    }
}
