﻿using Flapp_BLL.Exceptions.ModelExpections;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Models
{
    public class Tankkaart
    {
        #region Props
        /* Gegevens die hieronder met ! zijn aangeduid,
        * zijn dingen die verplicht in te vullen zijn bij het aanmaken en/of het editeren */
        public int Kaartnummer { get; private set; } // !
        public DateTime Geldigheidsdatum { get; private set; } // !

        public string Pincode { get; private set; }
        // Tankkaart moet meerdere brandstoffen hebben.
        public Brandstof Brandstof { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        public bool Geblokkeerd { get; private set; }
        #endregion

        #region Constructors
        public Tankkaart(string pincode, DateTime geldigheidsdatum) {
            ZetPincode(pincode);
            ZetGeldigheidsdatum(geldigheidsdatum);
        }

        public Tankkaart(int kaartnummer, DateTime geldigheidsdatum)
        {
            ZetKaartnummer(kaartnummer);
            ZetGeldigheidsdatum(geldigheidsdatum);
        }

        public Tankkaart(int kaartnummer, DateTime geldigheidsdatum, bool isgeblokkeerd)
        {
            ZetKaartnummer(kaartnummer);
            ZetGeldigheidsdatum(geldigheidsdatum);
            ZetGeblokkeerd(isgeblokkeerd);
        }

        public Tankkaart(int kaartnummer, DateTime geldigheidsdatum, Brandstof brandstof){
            ZetKaartnummer(kaartnummer);
            ZetGeldigheidsdatum(geldigheidsdatum);
            ZetBrandstof(brandstof);
        }

        public Tankkaart(int kaartnummer, DateTime geldigheidsdatum, string pincode, Brandstof brandstoftype, bool geblokkeerd) : this(kaartnummer, geldigheidsdatum)
        {
            ZetKaartnummer(kaartnummer);
            ZetPincode(pincode);
            ZetBrandstof(brandstoftype);
            ZetGeblokkeerd(geblokkeerd);
        }

        public Tankkaart(int kaartnummer, DateTime geldigheidsdatum, string pincode, Brandstof brandstoftype, Bestuurder bestuurder, bool geblokkeerd) : this(kaartnummer, geldigheidsdatum)
        {
            ZetKaartnummer(kaartnummer);
            ZetPincode(pincode);
            ZetBrandstof(brandstoftype);
            ZetBestuurder(bestuurder);
            ZetGeblokkeerd(geblokkeerd);
        }

        #endregion

        #region ZetMethods
        public void ZetKaartnummer(int nummer)
        {
            if (nummer < 1) { throw new TankkaartException("Tankkaart: ZetKaartnummer: Tankkaart kaartnummer is kleiner dan 1!"); }
            Kaartnummer = nummer;
        }
        public void ZetGeldigheidsdatum(DateTime datum)
        {
            if (datum < DateTime.Now) { throw new TankkaartException("Tankkaart: ZetGeldigheidsdatum: Tankkaart Geldigheidsdatum mag niet kleiner zijn dan vandaag!"); }
            Geldigheidsdatum = datum;
        }
        public void ZetGeblokkeerd(bool blok)
        {
            Geblokkeerd = blok;
        }
        public void ZetBrandstof(Brandstof brandstof)
        {
            if (brandstof == null) { throw new TankkaartException("Tankkaart: ZetBrandstof: Tankkaart brandstof is null!"); }
            Brandstof = brandstof;
        }
        public void ZetPincode(string pincode)
        {
            if (string.IsNullOrWhiteSpace(pincode)) { throw new TankkaartException("Tankkaart: ZetPincode: Tankkaart pincode is null!"); }
            Pincode = pincode;
        }
        public void ZetBestuurder(Bestuurder bestuurder)
        {
            if (bestuurder == null) { throw new TankkaartException("Tankkaart: ZetBestuurder: Tankkaart bestuurder is null!"); }
            Bestuurder = bestuurder;
        }
        #endregion

        #region Methods
        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            if (bestuurder == null) throw new TankkaartException("Tankkaart: VoegBestuurderToe: Tankkaart bestuurder bestaat niet!");
            if (bestuurder.Tankkaart != null) { throw new TankkaartException("Tankkaart: VoegBestuurderToe: Tankkaart bestuurder bestaat niet!"); }

            if (bestuurder.Tankkaart != this)
            {
                bestuurder.ZetTankkaart(this);
            }
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            if (bestuurder == null) throw new TankkaartException("Tankkaart: VerwijderBestuurder: Tankkaart bestuurder is null!");
            if (bestuurder.Tankkaart != this) throw new TankkaartException("Tankkaart: VerwijderBestuurder: Tankkaart bestuurder is niet hetzelfde als gekozen bestuurder!!");
            bestuurder.VerwijderTankkaart();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"[{GetType().Name}] {Kaartnummer} {Geldigheidsdatum.ToShortDateString()} {Brandstof.Naam}";
        }
        public override bool Equals(object obj)
        {
            return obj is Tankkaart tankkaart &&
                   Kaartnummer == tankkaart.Kaartnummer &&
                   Geldigheidsdatum == tankkaart.Geldigheidsdatum &&
                   Pincode == tankkaart.Pincode &&
                   EqualityComparer<Bestuurder>.Default.Equals(Bestuurder, tankkaart.Bestuurder) &&
                   Geblokkeerd == tankkaart.Geblokkeerd;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Kaartnummer, Geldigheidsdatum, Bestuurder, Geblokkeerd);
        }
        #endregion
    }
}