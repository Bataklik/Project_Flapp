
using Flapp_BLL.Exceptions;
using System;

namespace Flapp_BLL.Utils
{
    public class Brandstof
    {
        #region Props
        public string Naam { get; private set; }
        #endregion

        #region Constructors
        public Brandstof(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) { throw new BrandstofException("Brandstof naam mag niet leeg zijn!"); }
            Naam = naam;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"[Brandstof] {Naam}";
        }
        public override bool Equals(object obj)
        {
            return obj is Brandstof brandstof &&
                   Naam == brandstof.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam);
        }

        #endregion
    }
}
