using CareerTrack.Common;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace CareerTrack.Application.Authorizations
{
    public class AdminRoleAuthorizationHandler : AuthorizationHandler<ClaimRequirement>
    {
        private readonly IConfiguration configuration;
        public AdminRoleAuthorizationHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Issuer == configuration.JwtIssuer && c.Type == configuration.ExpectedRoleClaim))
            {
                return Task.CompletedTask;
            }

            if (context.User.Identities.ToList().FirstOrDefault().HasClaim(configuration.ExpectedRoleClaim, requirement.Role))
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
