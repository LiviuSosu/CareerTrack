using CareerTrack.Models.Users;
using CareerTrack.Persistance;
using System.Linq;

namespace CarerrTrack.Services.User
{
    //public class UserService : IUserService
    //{
    //    private CareerTrackDbContext _context;

    //    public UserService(CareerTrackDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public LoginResponse Authenticate(AuthenticateRequest model, string ipAddress)
    //    {
    //        var user = _context.Users.SingleOrDefault(x => x.UserName == model.Username /*&& x.Password == model.Password*/);

    //        if (user == null) return null;

    //        return null;
    //    }
    //}
}
