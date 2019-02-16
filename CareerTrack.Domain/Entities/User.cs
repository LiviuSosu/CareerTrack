using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CareerTrack.Domain.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public Guid UserId { get; set; }
    }
}
