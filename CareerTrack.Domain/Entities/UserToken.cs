using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CareerTrack.Domain.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        [Key]
        public new Guid UserId { get; set; }
    }
}
