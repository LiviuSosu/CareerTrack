using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CareerTrack.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<UserToken> UserTokens { get; set; }
    }
}
