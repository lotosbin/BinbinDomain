using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BinbinRepository
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : class
    {
        List<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        TEntity Find(TKey key);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Save(TEntity entity);
    }
}
