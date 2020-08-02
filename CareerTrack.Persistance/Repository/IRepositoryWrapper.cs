
namespace CareerTrack.Persistance.Repository
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        void Save();
    }
}
