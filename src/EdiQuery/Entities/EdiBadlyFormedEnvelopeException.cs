using System;

namespace EdiQuery.Entities
{
    public class EdiBadlyFormedEnvelopeException : Exception
    {
        public EdiBadlyFormedEnvelopeException(string msg):base(msg)
        { 
        }
    }
}