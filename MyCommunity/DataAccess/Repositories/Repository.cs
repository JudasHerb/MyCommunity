using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace MyCommunity.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly UnitOfWork _entities;

        public Repository(UnitOfWork entities)
        {
            _entities = entities;
        }

        #region IRepository<T> Members

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);

            return query;
        }

        public virtual T Add(T t)
        {
            _entities.Set<T>().Add(t);
            return t;
        }

        public virtual void Delete(T t)
        {
            _entities.Set<T>().Remove(t);
        }

        public virtual void Update(T t)
        {
            _entities.Entry(t).State = EntityState.Modified;
        }

        #endregion
    }
}