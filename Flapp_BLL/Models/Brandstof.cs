using Flapp_BLL.Exceptions.ModelExpections;
using System;

namespace Flapp_BLL.Models
{
    public class Brandstof
    {
        #region Props
        public int Id { get; private set; }
        public string Naam { get; private set; }
        #endregion

        #region Constructors
        public Brandstof(string brandstofnaam)
        {
            ZetBrandstofNaam(brandstofnaam);
        }

        public Brandstof(int id, string brandstofnaam)
        {
            ZetId(id);
            ZetBrandstofNaam(brandstofnaam);
        }

        #endregion

        #region ZetMethods
        public void ZetId(int id)
        {
            if (id <= 0) { throw new BrandstofException("Brandstof id moet positief zijn!"); }
            Id = id;
        }
        public void ZetBrandstofNaam(string brandstofnaam)
        {
            if (string.IsNullOrWhiteSpace(brandstofnaam)) { throw new BrandstofException("Brandstof naam mag niet leeg zijn!"); }
            Naam = brandstofnaam;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"Brandstof: {Naam}";
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
