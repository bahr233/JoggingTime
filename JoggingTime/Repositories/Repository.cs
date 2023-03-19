using JoggingTime.Models;
using JoggingTime.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JoggingTime.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel, new()
    {
        public readonly DbSet<T> dbSet;
        protected readonly DbContext context;

        protected string[] defaultExcludedEditProperties = new string[]
        { "IsDeleted" };

        private readonly IUnitOfWork _unitOfWork;

        public int UserID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            dbSet = _unitOfWork.context.Set<T>();

        }


        public T Add(T entity)
        {
            _unitOfWork.context.Set<T>().Add(entity);
            return entity;
        }
        public bool IsDeleted(T entity)
        {
            return GetWithDeleted().FirstOrDefault(e => e.ID == entity.ID)?.IsDeleted ?? true;
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            SaveIncluded(entity, nameof(entity.IsDeleted));
        }
        
        public void Delete(int id)
        {
            T entity = new()
            {
                ID = id
            };
            Delete(entity);
        }

        public IQueryable<T> Get()
        {
            return _unitOfWork.context.Set<T>().Where(entity => !entity.IsDeleted);
        }
        public IQueryable<T> GetWithDeleted()
        {
            return _unitOfWork.context.Set<T>();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Get().Where(predicate);
        }
        public T Get(int id)
        {
            return Get().FirstOrDefault(item => item.ID == id);
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return Get().Any(predicate);
        }

        public void Update(T entity)
        {
            if (entity.ID == 0)
                return;

            SaveExcluded(entity, nameof(BaseModel.ID) , nameof(BaseModel.IsDeleted));
        }

        public virtual void SaveExcluded(T entity, params string[] properties)
        {
            if (entity.ID == 0)
                return;
            List<string> excludedProperties = properties.ToList();
            excludedProperties.Add(nameof(BaseModel.ID));
            excludedProperties.Add(nameof(BaseModel.IsDeleted));
            var entry = _unitOfWork.context.Entry(entity);
            foreach (var prop in entry.Properties)
            {
                if (excludedProperties.Contains(prop.Metadata.Name))
                    prop.IsModified = false;
                else
                    prop.IsModified = true;
            }
        }
        public virtual void SaveIncluded(T entity, params string[] properties)
        {
            var entry = _unitOfWork.context.Entry(entity);
            foreach (var prop in entry.Properties)
            {
                if (properties.Contains(prop.Metadata.Name))
                    prop.IsModified = true;
                else
                    prop.IsModified = false;
            }
        }
    }

}
