using System;

namespace Flapp_BLL.Exceptions
{
    internal class PostcodeException : Exception
    {
        public PostcodeException(string message) : base(message)
        {
        }
    }
}
