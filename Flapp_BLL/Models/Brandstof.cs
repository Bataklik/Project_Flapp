using Flapp_BLL.Exceptions.ModelExpections;
using System;

namespace Flapp_BLL.Models
{
    public class Brandstof
    {
        #region Props
        public string Naam { get; private set; }
        #endregion

        #region Constructors
        public Brandstof(string brandstofnaam)
        {
            ZetBrandstofNaam(brandstofnaam);
        }
        #endregion

        #region ZetMethods
        public void ZetBrandstofNaam(string brandstofnaam)
        {
            if (string.IsNullOrWhiteSpace(brandstofnaam)) { throw new BrandstofException("Brandstof naam mag niet leeg zijn!"); }
            Naam = brandstofnaam;
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
