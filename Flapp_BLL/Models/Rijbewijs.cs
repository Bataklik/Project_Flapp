using Flapp_BLL.Exceptions.ModelExpections;
using System;
using System.Linq;

namespace Flapp_BLL.Models
{
    public class Rijbewijs
    {
        #region Props
        public int Id { get; private set; }
        public string Naam { get; private set; }
        #endregion

        #region Constructor
        public Rijbewijs(int id, string naam)
        {
            ZetId(id);
            ZetNaam(naam);
        }
        public Rijbewijs(string naam)
        {
            ZetNaam(naam);
        }
        #endregion

        #region Zetmethods
        public void ZetId(int id)
        {
            if (id <= 0) { throw new RijbewijsException("RijbewijsType: ZetId: Id moet positief zijn!"); }
            Id = id;
        }
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) { throw new RijbewijsException("RijbewijsType: ZetNaam: Naam mag niet leeg zijn!"); }
            //if (!naam.Any(char.IsUpper)) { throw new RijbewijsException("RijbewijsType: ZetNaam: Naam moet niet hoofdletter!"); }
            Naam = naam;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"{Naam} ";
        }

        public override bool Equals(object obj)
        {
            return obj is Rijbewijs type &&
                   Naam == type.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam);
        }
        #endregion
    }
}