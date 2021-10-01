
using Flapp_BLL.Exceptions;
using System;

namespace Flapp_BLL.Utils
{
    public class Brandstof
    {
        private string _naam;

        public string Naam { get; }

        public Brandstof(string naam)
        {
            if (string.IsNullOrEmpty(naam)) { throw new BrandstofException("Brandstof naam mag niet leeg zijn"); }
            _naam = naam;
        }

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Brandstof brandstof &&
                   _naam == brandstof._naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_naam);
        }
        #endregion
    }
}
