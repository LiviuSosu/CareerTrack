using CareerTrack.Persistance.Repository.ArticleRepository;
using CareerTrack.Persistance.Repository.RoleRepository;
using CareerTrack.Persistance.Repository.UserRepository;
using CareerTrack.Persistance.Repository.UserRoleRepository;
using CareerTrack.Persistance.Repository.UserTokenRepository;
using System.Threading.Tasks;

namespace CareerTrack.Persistance.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private CareerTrackDbContext _careerTrackDbContext;
        private IArticleRepository _article;
        private IUserRepository _user;
        private IUserRoleRepository _userRole;
        private IRoleRepository _role;
        private IUserTokenRepository _userToken;

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

        public IUserRoleRepository UserRole
        {
            get
            {
                if (_userRole == null)
                {
                    _userRole = new UserRoleRepository.UserRoleRepository(_careerTrackDbContext);
                }

                return _userRole;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository.RoleRepository(_careerTrackDbContext);
                }

                return _role;
            }
        }

        public IUserTokenRepository UserToken 
        { 
            get
            {
                if (_userToken == null)
                {
                    _userToken = new UserTokenRepository.UserTokenRepository(_careerTrackDbContext);
                }

                return _userToken;
            }
        }

        public async Task SaveAsync()
        {
            await _careerTrackDbContext.SaveChangesAsync();
        }
    }
}
