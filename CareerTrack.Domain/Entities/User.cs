using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CareerTrack.Domain.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public Guid UserId { get; set; }
    }
}
