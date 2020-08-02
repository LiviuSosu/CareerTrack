using CareerTrack.Domain.Entities;

namespace CareerTrack.Persistance.Repository
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(CareerTrackDbContext careerTrackDbContext)
         : base(careerTrackDbContext)
        {
        }
    }
}
