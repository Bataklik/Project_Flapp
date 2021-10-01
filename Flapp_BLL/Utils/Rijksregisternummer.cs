using Flapp_BLL.Exceptions;
using Flapp_BLL.Models;
using System;
using System.Linq;

namespace Flapp_BLL.Utils
{
    public class Rijksregisternummer
    {
        private string _nummer;

        public Rijksregisternummer(string r)
        {
            //Rijksregisternummer
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerException("Het identificatienummer bevat 11 cijfers"); }
            _nummer = r;
        }

        public string ToonNummer()
        {
            return _nummer;
        }

        public Rijksregisternummer VeranderNummer(string r)
        {
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerException("Het identificatienummer bevat 11 cijfers"); }
            _nummer = r;
            return this;
        }

        #region Override
        public override bool Equals(object obj)
        {
            return obj is Rijksregisternummer rijksregisternummer &&
                   _nummer == rijksregisternummer._nummer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_nummer);
        }
        #endregion
    }
}
