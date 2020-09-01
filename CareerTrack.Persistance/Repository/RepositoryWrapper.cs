
using CareerTrack.Persistance.Repository.ArticleRepository;
using CareerTrack.Persistance.Repository.UserRepository;
using System.Threading.Tasks;

namespace CareerTrack.Persistance.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private CareerTrackDbContext _careerTrackDbContext;
        private IArticleRepository _article;
        private IUserRepository _user;

        public RepositoryWrapper(CareerTrackDbContext CareerTrackDbContext)
        {
            _careerTrackDbContext = CareerTrackDbContext;
        }
        public IArticleRepository Article
        {
            get
            {
                if (_article == null)
                {
                    _article = new ArticleRepository.ArticleRepository(_careerTrackDbContext);
                }

                return _article;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository.UserRepository(_careerTrackDbContext);
                }

                return _user;
            }
        }

        public async Task SaveAsync()
        {
            await _careerTrackDbContext.SaveChangesAsync();
        }
    }
}
