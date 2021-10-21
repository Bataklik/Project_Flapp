using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;
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
        public Brandstof Brandstoftype { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        public bool Geblokkeerd { get; private set; }
        #endregion

        #region Constructors
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
        #endregion

        #region ZetMethods
        public void ZetKaartnummer(int kn)
        {
            if (kn < 1) { throw new TankkaartException("Tankkaart kaartnummer is kleiner dan 1!"); }
            Kaartnummer = kn;
        }
        public void ZetGeldigheidsdatum(DateTime gd)
        {
            if (gd < DateTime.Now) { throw new TankkaartException("Tankkaart Geldigheidsdatum mag niet kleiner zijn dan vandaag!"); }
            Geldigheidsdatum = gd;
        }
        public void ZetGeblokkeerd(bool b)
        {
            if (b == true) { throw new TankkaartException("Tankkaart is al geblokkeerd"); }
            Geblokkeerd = b;
        }
        //public void ZetBrandstofType(Brandstof b)
        //{
        //    if (b == null) { throw new BrandstofException("Tankkaart brandstof mag niet null zijn"); }
        //    Brandstoftype = b;
        //}
        //public void ZetPincode(string p)
        //{
        //    Pincode = p;
        //}
        //public void ZetBestuurder(Bestuurder b)
        //{
        //    if (b == null) { throw new BestuurderException("Tankkaart bestuurder bestaat niet!"); }
        //    Bestuurder = b;
        //}
        #endregion

        #region Methods

        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"[Tankkaart] {Kaartnummer}, {Geldigheidsdatum}";
        }
        public override bool Equals(object obj)
        {
            return obj is Tankkaart tankkaart &&
                   Kaartnummer == tankkaart.Kaartnummer &&
                   Geldigheidsdatum == tankkaart.Geldigheidsdatum &&
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

//USE[Project_Flapp_DB];
//CREATE TABLE[dbo].[Tankkaart](
//   [id][int] IDENTITY(1, 1) PRIMARY KEY,
//   [geldigheidsdatum] [date] NOT NULL,
//   [pincode] [varchar](50) NOT NULL,
//   [brandstof_id] [int] NOT NULL,
//   [bestuurder_id] [int] FOREIGN KEY REFERENCES dbo.Bestuurder(id),
//   [brandstof_type] [int] FOREIGN KEY REFERENCES dbo.Brandstof(id),
//   [voertuig_type] [varchar](50) NOT NULL,
//   [geblokkeerd] [bit]NOT NULL);