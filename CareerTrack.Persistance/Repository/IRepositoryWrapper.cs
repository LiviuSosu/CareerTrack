
using System.Threading.Tasks;

namespace CareerTrack.Persistance.Repository
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        Task SaveAsync();
    }
}
