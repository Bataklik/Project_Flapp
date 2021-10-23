using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Exceptions.CheckerExceptions
{
    public class ChassisnummerCheckerException : Exception
    {
        public ChassisnummerCheckerException(string message) : base(message)
        {

        }
    }
}
