using System;

namespace CareerTrack.Application.Exceptions
{
    public class UserEmailNotConfirmedException : Exception
    {
        public UserEmailNotConfirmedException(string name, object key) : base($"Entity \"{name}\" ({key}) was not confirmed.")
        {
        }
    }
}
