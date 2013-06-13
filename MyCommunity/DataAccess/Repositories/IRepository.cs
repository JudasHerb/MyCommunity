using System;
using System.Linq;
using System.Linq.Expressions;

namespace MyCommunity.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T t);
        void Delete(T t);
        void Update(T t);
    }
}