using System;

namespace Flapp_BLL.Exceptions.ManagerExceptions
{
    class BestuurderManagerException : Exception
    {
        public BestuurderManagerException(string message) : base(message)
        {
        }

        public BestuurderManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
