using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;

namespace Flapp_BLL.Models
{
    public class Voertuig
    {
        #region Constructors
        public Voertuig(int voertuigID, string merk, string model, string chassisNummer, string nummerPlaat, Brandstof brandstofType, string voertuigType, string kleur, int deuren, Bestuurder bestuurder)
        {
            ZetVoeruigID(voertuigID);
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerplaat(nummerPlaat);
            ZetBrandstofType(brandstofType);
            ZetVoertuigType(voertuigType);
            ZetKleur(kleur);
            ZetAantalDeuren(deuren);
            ZetBestuurder(bestuurder);
        }
        #endregion

        #region Props        
        public int VoertuigID { get; private set; }
        public string Merk { get; private set; }
        public string Model { get; private set; }
        public string ChassisNummer { get; private set; }
        public string Nummerplaat { get; private set; }
        public Brandstof Brandstoftype { get; private set; }
        public string VoertuigType { get; private set; }
        public string Kleur { get; private set; }
        public int Aantaldeuren { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        #endregion

        #region ZetMethods
        public void ZetVoeruigID(int id)
        {
            if (id > 0)
            {
                VoertuigID = id;
            }
            else
            {
                throw new VoertuigException("Voertuig - ID kleiner dan 0");
            }
        }
        public void ZetMerk(string merk)
        {
            if (!string.IsNullOrWhiteSpace(merk))
            {
                Merk = merk;
            }
            else
            {
                throw new VoertuigException("Voertuig - Merk mag niet leeg zijn");
            }
        }
        public void ZetModel(string model)
        {
            if (!string.IsNullOrWhiteSpace(model))
            {
                Model = model;
            }
            else
            {
                throw new VoertuigException("Voertuig - Model mag niet leeg zijn");
            }
        }
        public void ZetChassisNummer(string nummer)
        {
            if (!string.IsNullOrWhiteSpace(nummer))
            {
                if (nummer.Length == 3 + 6 + 8)
                {
                    ChassisNummer = nummer;
                }
                else
                {
                    throw new VoertuigException("Voertuig - Chassisnummer moet 17 karakters lang zijn");
                }

            }
            else
            {
                throw new VoertuigException("Voertuig - Chassisnummer mag niet leeg zijn");
            }
        }
        public void ZetNummerplaat(string nummerplaat)
        {
            if (!string.IsNullOrWhiteSpace(nummerplaat))
            {
                Nummerplaat = nummerplaat;
            }
            else
            {
                throw new VoertuigException("Voertuig - Nummerplaat mag niet leeg zijn");
            }
        }
        public void ZetBrandstofType(Brandstof brandstof)
        {

            Brandstoftype = brandstof;

        }
        public void ZetVoertuigType(string voertuigType)
        {
            if (!string.IsNullOrWhiteSpace(voertuigType))
            {
                VoertuigType = voertuigType;
            }
            else
            {
                throw new VoertuigException("Voertuig - Voertuigtype mag niet leeg zijn");
            }
        }
        public void ZetKleur(string kleur)
        {
            if (!string.IsNullOrWhiteSpace(kleur))
            {
                Kleur = kleur;
            }
            else
            {
                throw new VoertuigException("Voertuig - Kleur mag niet leeg zijn");
            }
        }
        public void ZetAantalDeuren(int deuren)
        {
            if (deuren > 0 && deuren <= 7)
            {
                Aantaldeuren = deuren;
            }
            else
            {
                throw new VoertuigException("Voertuig - Aantal deuren moet tussen 0 en 7 zijn");
            }
        }
        public void ZetBestuurder(Bestuurder bestuurder)
        {
            if (Bestuurder != null) // heeft vehicle al een driver?
            {
                if (Bestuurder != bestuurder)
                {
                    Bestuurder = bestuurder;
                }
                else
                {
                    throw new VoertuigException("");
                }
            }
            else
            {
                if (bestuurder != null) // is driver niet null
                {
                    Bestuurder = bestuurder;
                }
                else
                {
                    throw new VoertuigException("Voertuig - Bestuurder mag niet leeg zijn");
                }
            }
        }
        #endregion

        #region Methods

        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"[Voertuig] {VoertuigType}, {Merk}, {Model}, {ChassisNummer}, {Nummerplaat}, {Brandstoftype}, {VoertuigType}, {Kleur}, {Aantaldeuren}, {Bestuurder}";
        }
        #endregion
    }
}