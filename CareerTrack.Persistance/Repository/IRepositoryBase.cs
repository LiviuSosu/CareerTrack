using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CareerTrack.Persistance.Repository
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        Task<T> FindByIdAsync(Guid Id);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
