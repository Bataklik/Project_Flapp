using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Exceptions
{
    class AantalDeurenException : Exception
    {
        public AantalDeurenException(string message) : base(message)
        {

        }
    }
}
