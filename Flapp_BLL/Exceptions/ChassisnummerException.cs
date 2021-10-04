using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Exceptions
{
    class ChassisnummerException : Exception
    {
        public ChassisnummerException(string message) : base (message)
        {

        }
    }
}
