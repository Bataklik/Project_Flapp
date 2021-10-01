using Flapp_BLL.Exceptions;
using System;

namespace Flapp_BLL.Models
{
    public class Tankkaart
    {
        private int _kaartnummer; //!
        private DateTime _geldigheidsdatum; //!
        private int _pincode;
        private string _brandstoftype;
        private Bestuurder _bestuurder;
        private bool _geblokkeerd;



        public int Kaartnummer { get => _kaartnummer; set => _kaartnummer = value; }
        public DateTime Geldigheidsdatum
        {
            get => _geldigheidsdatum;
            set
            {
                if (value < DateTime.Now) { throw new GeldigeheidsdatumException("Geledigheidsdatum is al verlopen!"); }
                _geldigheidsdatum = value;
            }
        }
        public int Pincode { get => _pincode; }
        public string Brandstof { get => _brandstoftype; }
    }
}