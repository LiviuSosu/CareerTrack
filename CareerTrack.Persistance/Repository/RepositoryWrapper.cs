﻿
namespace CareerTrack.Persistance.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private CareerTrackDbContext _careerTrackDbContext;
        private IArticleRepository _account;

        public IArticleRepository Article
        {
            get
            {
                if (_account == null)
                {
                    _account = new ArticleRepository(_careerTrackDbContext);
                }

                return _account;
            }
        }

        public RepositoryWrapper(CareerTrackDbContext CareerTrackDbContext)
        {
            _careerTrackDbContext = CareerTrackDbContext;
        }

        public void Save()
        {
            _careerTrackDbContext.SaveChanges();
        }
    }
}