using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Exceptions
{
    internal class PostcodeException : Exception
    {
        public PostcodeException(string message) : base(message)
        {
        }
    }
}
