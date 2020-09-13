using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CareerTrack.Domain.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
    }
}
