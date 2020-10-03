using System;

namespace CareerTrack.Application.Exceptions
{
    public class NoRolesAssignedException : Exception
    {
        public NoRolesAssignedException(string name)
         : base($"The following user \"{name}\" does not have any assigned roles.")
        {
        }
    }
}
