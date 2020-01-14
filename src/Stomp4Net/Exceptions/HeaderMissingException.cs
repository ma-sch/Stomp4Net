namespace Stomp4Net.Exceptions
{
    using System;

    public class HeaderMissingException : Exception
    {
        public HeaderMissingException(string message)
            : base(message)
        {
        }
    }
}
