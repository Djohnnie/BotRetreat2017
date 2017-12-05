using System;

namespace BotRetreat2017.Client.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException(String message) : base(message) { }
    }
}