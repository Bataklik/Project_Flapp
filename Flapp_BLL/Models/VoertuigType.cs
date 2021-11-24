using Flapp_BLL.Exceptions.ModelExpections;
using System;

namespace Flapp_BLL.Models
{
    public class VoertuigType
    {
        #region Props
        public int Id { get; private set; }
        public string TypeWagen { get; set; }
        #endregion

        #region Constructors
        public VoertuigType(string type)
        {
            ZetTypeWagen(type);
        }

        public VoertuigType(int id, string type)
        {
            ZetId(id);
            ZetTypeWagen(type);
        }

        #endregion

        #region ZetMethods
        public void ZetId(int id)
        {
            if (id <= 0) { throw new BrandstofException("Brandstof id moet positief zijn!"); }
            Id = id;
        }
        public void ZetTypeWagen(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) { throw new BrandstofException("Brandstof naam mag niet leeg zijn!"); }
            TypeWagen = type;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"{TypeWagen}";
        }
        public override bool Equals(object obj)
        {
            return obj is VoertuigType voertuigtype &&
                   TypeWagen == voertuigtype.TypeWagen;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TypeWagen);
        }

        #endregion
    }
}
