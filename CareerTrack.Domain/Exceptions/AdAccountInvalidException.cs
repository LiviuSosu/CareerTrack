using System;

namespace CareerTrack.Domain.Exceptions
{
    public class AdAccountInvalidException : Exception
    {
        public AdAccountInvalidException(string adAccount, Exception ex)
           : base($"AD Account \"{adAccount}\" is invalid.", ex)
        {
        }
    }
}
