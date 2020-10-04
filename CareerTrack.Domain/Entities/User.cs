using System;
using Microsoft.AspNetCore.Identity;

namespace CareerTrack.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
    }
}
