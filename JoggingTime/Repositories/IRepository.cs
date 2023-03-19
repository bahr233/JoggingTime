using JoggingTime.Models;
using System.Linq.Expressions;

namespace JoggingTime.Repositories
{
    public interface IRepository<T> where T : BaseModel, new()
    {
        T Add(T entity);
        bool Any(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        void Delete(int id);
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetWithDeleted();
        bool IsDeleted(T entity);
        void Update(T entity);
    }
}