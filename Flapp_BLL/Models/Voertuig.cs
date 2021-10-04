using Flapp_BLL.Exceptions;
using Flapp_BLL.Utils;
using System;
using System.Collections.Generic;

namespace Flapp_BLL.Models    
{
    
    public class Voertuig
    {
        private string _merk;
        private string _model;
        private string _chassisnummer;
        private string _nummerplaat;
        private Brandstof _brandstoftype;
        private string _typewagen;
        private string _kleur;
        private int _aantaldeuren;
        private Bestuurder _bestuurder;

        #region Constructors
        public Voertuig(string merk, string model, string chassisnummer, string nummerplaat, Brandstof brandstoftype, string typewagen, string kleur, int aantaldeuren)
        {
            Merk = merk;
            Model = model;
            Chassisnummer = chassisnummer;
            Nummerplaat = nummerplaat;
            _brandstoftype = brandstoftype;
            TypeWagen = typewagen;
            Kleur = kleur;
            AantalDeuren = aantaldeuren;
        }
        #endregion

        #region Props        
        public string Merk
        {
            get => _merk;
            set => _merk = value;
        }        public string Model
        {
            get => _model;
            set => _model = value;
        }
        public string Chassisnummer
        {
            get => _chassisnummer;
            set => _chassisnummer = value;            
        }
        public string Nummerplaat
        {
            get => _nummerplaat;
            set => _nummerplaat = value;            
        }
        public string TypeWagen
        {
            get => _typewagen;
            set => _typewagen = value;
        }
        public string Kleur
        {
            get => _kleur;
            set => _kleur = value;
        }
        public int AantalDeuren
        {
            get => _aantaldeuren;
            set
            {
                if (value < 0)
                    throw new AantalDeurenException("Aantal deuren moet groter zijn dan 0!");
                if (value > 6)
                    throw new AantalDeurenException("Aantal deuren moet kleiner zijn dan 6!");
                _aantaldeuren = value;
            }
        }
        #endregion

        #region Methods

        #endregion

        #region Overrides
        
        #endregion
    }
}