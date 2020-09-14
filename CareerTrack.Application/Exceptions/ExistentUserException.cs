using System;

namespace CareerTrack.Application.Exceptions
{
    public class ExistentUserException : Exception
    {
        public ExistentUserException(string name)
          : base($"The following user \"{name}\" already exists")
        {
        }
    }
}
