using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;

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
        public Brandstof Brandstoftype { get; private set; } // !
        public string VoertuigType { get; private set; } // !

        public string Kleur { get; private set; }
        public int Aantaldeuren { get; private set; }
        public Bestuurder Bestuurder { get; private set; }
        #endregion

        #region Constructors
        public Voertuig(int voertuigID, string merk, string model, string chassisNummer, string nummerPlaat, Brandstof brandstofType, string voertuigType, string kleur, int deuren)
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
            //ZetBestuurder(bestuurder);
        }

        // !
        public Voertuig(string merk, string model, string chassisNummer, string nummerplaat, Brandstof brandstoftype, string voertuigType)
        {
            ZetMerk(merk);
            ZetModel(model);
            ZetChassisNummer(chassisNummer);
            ZetNummerplaat(nummerplaat);
            ZetBrandstofType(brandstoftype);
            ZetVoertuigType(voertuigType);
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
            if(cc.controleChassisnummer(nummer))
                ChassisNummer = nummer;
        }
        public void ZetNummerplaat(string nummerplaat)
        {
            NummerplaatChecker nc = new NummerplaatChecker(nummerplaat);
            
            if (nc.ControleNummerplaat(nummerplaat))
                Nummerplaat = nummerplaat;
        }
        public void ZetBrandstofType(Brandstof brandstof)
        {
            if (brandstof == null) { throw new VoertuigException("Voertuig - brandstof is null!"); }
            Brandstoftype = brandstof;
        }
        public void ZetVoertuigType(string voertuigType)
        {
            if (string.IsNullOrWhiteSpace(voertuigType)) { throw new VoertuigException("Voertuig - Voertuigtype mag niet leeg zijn"); }
            VoertuigType = voertuigType;
        }
        public void ZetKleur(string kleur)
        {
            if (string.IsNullOrWhiteSpace(kleur)) { throw new VoertuigException("Voertuig - Kleur mag niet leeg zijn"); }
            Kleur = kleur;
        }
        public void ZetAantalDeuren(int deuren)
        {
            if (deuren < 0 && deuren > 7) { throw new VoertuigException("Voertuig - Aantal deuren moet tussen 0 en 7 zijn!"); }
            Aantaldeuren = deuren;
        }
        public void ZetBestuurder(Bestuurder bestuurder)
        {
            //if (bestuurder == null) { throw new VoertuigException("Voertuig - Bestuurder dat u wilt zetten is null!"); }
            if (Bestuurder == bestuurder) { throw new VoertuigException("Voertuig - Het is dezelfde bestuurder!"); }
            Bestuurder = bestuurder;
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