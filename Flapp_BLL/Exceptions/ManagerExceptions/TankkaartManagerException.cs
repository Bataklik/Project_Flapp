using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flapp_BLL.Exceptions.ManagerExceptions {
    public class TankkaartManagerException : Exception{
        public TankkaartManagerException(string message) : base(message) {
        }

        public TankkaartManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
