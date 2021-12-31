using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Exceptions.ManagerExceptions {
    public class RijbewijsTypeManagerException : Exception{
        public RijbewijsTypeManagerException(string message) : base(message) {
        }

        public RijbewijsTypeManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
