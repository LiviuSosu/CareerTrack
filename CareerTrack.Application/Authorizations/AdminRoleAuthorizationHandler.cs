using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace CareerTrack.Application.Authorizations
{
    public class AdminRoleAuthorizationHandler : AuthorizationHandler<ClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
        {
            const string roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

            if (!context.User.HasClaim(c => c.Issuer == Common.Configuration.Issuer && c.Type == roleClaim))
            {
                return Task.CompletedTask;
            }

            if (context.User.Identities.ToList().FirstOrDefault().HasClaim(roleClaim, requirement.Role))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class ClaimRequirement : IAuthorizationRequirement
    {
        public ClaimRequirement(string role)
        {
            Role = role;
        }

        public string Role { get; private set; }
    }
}
