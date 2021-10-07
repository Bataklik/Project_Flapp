using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Models    
{
    
    public class Voertuig
    {
        public int _VoertuigID { get; private set; }
        public string _Merk { get; private set; }
        public string _Model { get; private set; }
        public string _ChassisNummer { get; private set; }
        public string _Nummerplaat { get; private set; }
        public Brandstof _Brandstoftype { get; private set; }
        public string _VoertuigType { get; private set; }
        public string _Kleur { get; private set; }
        public int _Aantaldeuren { get; private set; }
        public Bestuurder _Bestuurder { get; private set; }

        #region Constructors
        public Voertuig(int voertuigID, string merk, string model, string chassisNummer, string nummerPlaat, Brandstof brandstofType, string voertuigType, string kleur, int deuren, Bestuurder bestuurder)
        {
            ZetVoeruigId(voertuigID);
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
        public void ZetVoeruigId(int id)
        {
            if (id > 0)
            {
                _VoertuigID= id;
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
                _Merk = merk;
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
                _Model = model;
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
                    _ChassisNummer = nummer;
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
                _Nummerplaat = nummerplaat;
            }
            else
            {
                throw new VoertuigException("Voertuig - Nummerplaat mag niet leeg zijn");
            }
        }
        public void ZetBrandstofType(Brandstof brandstof)
        {
            
             _Brandstoftype = brandstof;
            
        }
        public void ZetVoertuigType(string voertuigType)
        {
            if (!string.IsNullOrWhiteSpace(voertuigType))
            {
                _VoertuigType = voertuigType;
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
                _Kleur = kleur;
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
                _Aantaldeuren = deuren;
            }
            else
            {
                throw new VoertuigException("Voertuig - Aantal deuren moet tussen 0 en 7 zijn");
            }
        }
        public void ZetBestuurder(Bestuurder bestuurder)
        {
            if (_Bestuurder != null) // heeft vehicle al een driver?
            {
                if (_Bestuurder != bestuurder)
                {
                    _Bestuurder = bestuurder;
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
                    _Bestuurder = bestuurder;
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
            return $"[Voertuig] {_VoertuigType}, {_Merk}, {_Model}, {_ChassisNummer}, {_Nummerplaat}, {_Brandstoftype}, {_VoertuigType}, {_Kleur}, {_Aantaldeuren}, {_Bestuurder}";
        }
        #endregion
    }
}