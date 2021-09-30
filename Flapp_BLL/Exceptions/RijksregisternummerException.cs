using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Exceptions
{
    internal class RijksregisternummerException : Exception
    {
        public RijksregisternummerException(string message) : base(message)
        {
        }
    }
}
