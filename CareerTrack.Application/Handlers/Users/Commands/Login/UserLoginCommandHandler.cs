using CareerTrack.Application.Exceptions;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommandHandler : BaseHandler<UserLoginCommand, LoginResponseDTO>, IRequestHandler<UserLoginCommand, LoginResponseDTO>
    {
        public UserLoginCommandHandler(CareerTrackDbContext context) : base(context) { }

        public new async Task<LoginResponseDTO> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await request.UserManager.FindByNameAsync(request.Username);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    if (await request.UserManager.CheckPasswordAsync(user, request.Password))
                    {
                        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.JWTConfiguration.JwtSecretKey));

                        var token = new JwtSecurityToken(
                               issuer: request.JWTConfiguration.JwtIssuer,
                               audience: request.JWTConfiguration.JwtAudience,
                               expires: DateTime.UtcNow.AddHours(Convert.ToInt16(request.JWTConfiguration.JwtLifeTime)),
                               claims: await GetRolesAsClaim(request.UserManager, user),
                               signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                               );

                        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                        var tok = new IdentityUserToken<Guid>
                        {
                            UserId = user.Id,
                            LoginProvider = "WIF",
                            Name = user.Id.ToString(),
                            Value = tokenValue
                        };
                        //_repoWrapper.UserToken.Create(tok);
                        //await _repoWrapper.SaveAsync();

                        return new LoginResponseDTO
                        {
                            token = tokenValue,
                            expiration = token.ValidTo,
                            UserId = user.Id,
                            Username = user.UserName
                        };
                    }
                    else
                    {
                        throw new LoginFailedException();
                    }
                }
                else
                {
                    throw new UserEmailNotConfirmedException("user.EmailConfirmed", user);
                }
            }
            else
            {
                throw new NotFoundException(request.Username, user);
            }
        }

        private async Task<List<Claim>> GetRolesAsClaim(UserManager<User> userManager, User user)
        {
            var result = new List<Claim>();

            var rolesString = await GetRoles(user);

            result.Add(new Claim("roles", rolesString));
            return result;
        }

        private async Task<string> GetRoles(User user)
        {
            var userRolesIds = _repoWrapper.UserRole.FindByCondition(r => r.UserId == user.Id).ToList();

            if (userRolesIds.Count != 0)
            {
                var stringRoles = new StringBuilder();

                foreach (var roleId in userRolesIds)
                {
                    stringRoles.Append((await _repoWrapper.Role.FindByIdAsync(roleId.RoleId)).Name);
                    stringRoles.Append(',');
                }
                stringRoles.Remove(stringRoles.Length - 1, 1);

                return stringRoles.ToString();
            }
            else
            {
                throw new NoRolesAssignedException(user.Email);
            }
        }
    }
}
