using CareerTrack.Persistance.Repository.ArticleRepository;
using CareerTrack.Persistance.Repository.RoleRepository;
using CareerTrack.Persistance.Repository.UserRepository;
using CareerTrack.Persistance.Repository.UserRoleRepository;
using CareerTrack.Persistance.Repository.UserTokenRepository;
using System.Threading.Tasks;

namespace CareerTrack.Persistance.Repository
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        IUserRepository User { get; }
        IUserRoleRepository UserRole { get; }
        IRoleRepository Role { get; }
        IUserTokenRepository UserToken { get; }

        Task SaveAsync();
    }
}
