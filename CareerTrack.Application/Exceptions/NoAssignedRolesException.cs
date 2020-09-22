using System;

namespace CareerTrack.Application.Exceptions
{
    public class NoAssignedRolesException : Exception
    {
        public NoAssignedRolesException(string name)
         : base($"The following user \"{name}\" does not have any assigned roles.")
        {
        }
    }
}
