using CareerTrack.Persistance.Repository.ArticleRepository;
using CareerTrack.Persistance.Repository.UserRepository;
using System.Threading.Tasks;

namespace CareerTrack.Persistance.Repository
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        IUserRepository User { get; }

        Task SaveAsync();
    }
}
