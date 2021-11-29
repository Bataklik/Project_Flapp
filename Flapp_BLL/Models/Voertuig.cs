using Flapp_BLL.Exceptions.ModelExpections;
using Flapp_BLL.Checkers;
using System.Collections.Generic;
using System.Linq;

namespace Flapp_BLL.Models
{
    public class Voertuig
    {
        #region Props       
        /* Gegevens die hieronder met ! zijn aangeduid,
         * zijn dingen die verplicht in te vullen zijn bij het aanmaken en/of het editeren */
        public int VoertuigID { get; private set; }
        public string Merk { get; private set; } // !
        public string Model { get; private set; } // !
        public string ChassisNummer { get; private set; } // ! 
        public string Nummerplaat { get; private set; } // !
        public List<Brandstof> Brandstof { get; private set; } // !
        public string VoertuigType { get; private set; } // !

        public string Kleur { get; private set; }
        public int Aantaldeuren { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        #endregion

        #region Constructors
        public Voertuig(int voertuigID, string merk, string model, string chassisNummer, string nummerPlaat, List<Brandstof> brandstofType, string voertuigType, string kleur, int deuren, Bestuurder bestuurder)
        {
            ZetVoeruigID(voertuigID);
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerplaat(nummerPlaat);
            ZetBrandstofTypeLijst(brandstofType);
            ZetVoertuigType(voertuigType);
            ZetKleur(kleur);
            ZetAantalDeuren(deuren);
            ZetBestuurder(bestuurder);
        }
        public Voertuig(int voertuigID, string merk, string model, string chassisNummer, string nummerPlaat, List<Brandstof> brandstofType, string voertuigType, string kleur, int deuren)
        {
            ZetVoeruigID(voertuigID);
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerplaat(nummerPlaat);
            ZetBrandstofTypeLijst(brandstofType);
            ZetVoertuigType(voertuigType);
            ZetKleur(kleur);
            ZetAantalDeuren(deuren);
        }

        public Voertuig(string merk, string model, string chassisNummer, string nummerPlaat, List<Brandstof> brandstofType, string voertuigType, string kleur, int deuren)
        {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerplaat(nummerPlaat);
            ZetBrandstofTypeLijst(brandstofType);
            ZetVoertuigType(voertuigType);
            ZetKleur(kleur);
            ZetAantalDeuren(deuren);
        }
        // !
        public Voertuig(string merk, string model, string chassisNummer, string nummerplaat, List<Brandstof> brandstoftype, string voertuigType)
        {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerplaat(nummerplaat);
            ZetBrandstofTypeLijst(brandstoftype);
            ZetVoertuigType(voertuigType);
        }

        public Voertuig(string merk, string model, string chassisNummer, string nummerplaat, string voertuigType, string kleur, int deuren)
        {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerplaat(nummerplaat);

            ZetVoertuigType(voertuigType);
            ZetKleur(kleur);
            ZetAantalDeuren(deuren);
        }


        #endregion

        #region ZetMethods
        public void ZetVoeruigID(int id)
        {
            if (id <= 0) { throw new VoertuigException("Voertuig - ID kleiner dan 0"); }
            VoertuigID = id;
        }
        public void ZetMerk(string merk)
        {
            if (string.IsNullOrWhiteSpace(merk)) { throw new VoertuigException("Voertuig - Merk mag niet leeg zijn"); }
            Merk = merk;
        }
        public void ZetModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model)) { throw new VoertuigException("Voertuig - Model mag niet leeg zijn"); }
            Model = model;
        }
        public void ZetChassisNummer(string nummer)
        {
            ChassisChecker cc = new ChassisChecker(nummer);
            //if (string.IsNullOrWhiteSpace(nummer)) { throw new VoertuigException("Voertuig - Chassisnummer is leeg of null!"); }
            if (cc.controleChassisnummer(nummer))
                ChassisNummer = nummer;
        }
        public void ZetNummerplaat(string nummerplaat)
        {
            NummerplaatChecker nc = new NummerplaatChecker(nummerplaat);

            if (nc.ControleNummerplaat(nummerplaat))
                Nummerplaat = nummerplaat;
        }
        public void ZetBrandstofTypeLijst(List<Brandstof> bt)
        {
            if (bt == null) { throw new VoertuigException("Brandstof lijst is null!"); }
            Brandstof = bt;
        }
        public void ZetVoertuigType(string voertuigType)
        {
            if (voertuigType == null) { throw new VoertuigException("Voertuig - Voertuigtype mag niet leeg zijn"); }
            VoertuigType = voertuigType;
        }
        public void ZetKleur(string kleur)
        {
            if (string.IsNullOrWhiteSpace(kleur)) { throw new VoertuigException("Voertuig - Kleur mag niet leeg zijn"); }
            Kleur = kleur;
        }
        public void ZetAantalDeuren(int deuren)
        {
            if (deuren < 0 || deuren > 7) { throw new VoertuigException("Voertuig - Aantal deuren moet tussen 0 en 7 zijn!"); }
            Aantaldeuren = deuren;
        }
        public void ZetBestuurderr(Bestuurder bestuurder)
        {
            //if (bestuurder == null) { throw new VoertuigException("Voertuig - Bestuurder dat u wilt zetten is null!"); }
            if (Bestuurder == bestuurder) { throw new VoertuigException("Voertuig - Het is dezelfde bestuurder!"); }
            Bestuurder = bestuurder;
        }

        public void ZetBestuurder(Bestuurder nieuweBestuurder)
        {
            if (nieuweBestuurder != null)
            {
                if (Bestuurder == null)
                {
                    if (!nieuweBestuurder.HeeftVoertuig(this)) //heeft de nieuwe bestuurder dit al als vehicle ?
                    {
                        nieuweBestuurder.VerwijderVoertuig();
                        Bestuurder = nieuweBestuurder;
                        nieuweBestuurder.ZetVoertuig(this);
                    }
                    Bestuurder = nieuweBestuurder;
                }
                else if (Bestuurder != nieuweBestuurder) //is huidige bestuurder niet gelijk aan nieuwe driver ?
                {
                    if (Bestuurder.HeeftVoertuig(this))
                    {
                        Bestuurder.VerwijderVoertuig();
                        //verwijder de huidige driver zijn vehicle
                    }
                    if (!nieuweBestuurder.HeeftVoertuig(this)) //heeft de nieuwe bestuurder dit al als vehicle ?
                    {
                        nieuweBestuurder.VerwijderVoertuig();
                        Bestuurder = nieuweBestuurder;
                        nieuweBestuurder.ZetVoertuig(this);
                    }

                }
                else if (Bestuurder == nieuweBestuurder) // is huidige bestuurder wel gelijk aan nieuwe driver? -> exception
                {
                    if (!Bestuurder.HeeftVoertuig(this))
                    {
                        Bestuurder.VerwijderVoertuig();
                        Bestuurder.ZetVoertuig(this);
                    }
                }
            }
            else
            {
                throw new VoertuigException("Vehicle - SetDriver - Driver is null");
            }
        }

        #endregion

        #region Methods

        public bool HeeftBestuurder(Bestuurder bestuurder)
        {
            if (Bestuurder != null)
            {
                if (Bestuurder == bestuurder)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public void VerwijderBestuurder()
        {
            Bestuurder = null;
        }
        public void voegBrandstofToe(Brandstof b)
        {
            //if (Brandstof.Count <= 0) throw new VoertuigException("Voeg brandstof toe - aantal");
            if (Brandstof.Contains(b))
            {
                throw new VoertuigException("Voeg brandstof toe - Deze wagen beschikt al over deze brandstof");
            }
            else
            {
                Brandstof.Add(b);
            }
        }

        public IReadOnlyList<Brandstof> geefBrandstoffen()
        {
            return Brandstof;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"\n---------------{GetType().Name}---------\n" +
                $"{Merk}, {Model}, {ChassisNummer}\n" +
                $"{string.Join(", ", Brandstof.OrderBy(b => b.Naam))}\n" +
                $"{VoertuigType}\n" +
                $"--------------------------------";
        }
        #endregion
    }
}