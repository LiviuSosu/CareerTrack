using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CareerTrack.Persistance.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected CareerTrackDbContext CareerTrackDbContext { get; set; }

        public RepositoryBase(CareerTrackDbContext careerTrackDbContext)
        {
            this.CareerTrackDbContext = careerTrackDbContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.CareerTrackDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.CareerTrackDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.CareerTrackDbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.CareerTrackDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.CareerTrackDbContext.Set<T>().Remove(entity);
        }
    }
}
