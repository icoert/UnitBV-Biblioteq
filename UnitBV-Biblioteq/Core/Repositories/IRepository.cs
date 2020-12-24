using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        bool Add(TEntity entity);

        bool Remove(TEntity entity);
    }
}
