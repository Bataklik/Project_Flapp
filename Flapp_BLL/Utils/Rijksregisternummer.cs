using Flapp_BLL.Exceptions;
using System.Linq;

namespace Flapp_BLL.Utils
{
    public class Rijksregisternummer
    {
        private string _nummer;

        public Rijksregisternummer(string r)
        {
            if (r.Count(e => char.IsDigit(e)) != 11) { throw new RijksregisternummerException("Het identificatienummer bevat 11 cijfers"); }
            _nummer = r;
        }

        public string ToonNummer()
        {
            return _nummer;
        }
    }
}
