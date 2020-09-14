using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerTrack.Domain.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        [Key]
        public new Guid UserId { get; set; }
    }
}
