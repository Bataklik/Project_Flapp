using System;
using System.Runtime.Serialization;

namespace Flapp_BLL.Exceptions
{
    internal class GeldigeheidsdatumException : Exception
    {
        public GeldigeheidsdatumException(string message) : base(message)
        {
        }
    }
}