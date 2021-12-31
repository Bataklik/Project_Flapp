using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Exceptions.ManagerExceptions {
    public class BrandstofManagerException : Exception{
        public BrandstofManagerException(string message) : base(message) {
        }

        public BrandstofManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
