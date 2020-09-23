using AutoMapper;
using CareerTrack.Domain.Entities;
using CareerTrack.Services.SendGrid;

namespace CareerTrack.Application.Handlers.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRegistrationEmailDTO>();
        }
    }
}
