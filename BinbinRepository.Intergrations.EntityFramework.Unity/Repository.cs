using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;

namespace BinbinRepository.Intergrations.EntityFramework.Unity
{
    public class Repository<TDataContext, TEntity> : IRepository<TEntity, int>
        where TDataContext : DbContext
        where TEntity : class
    {
        private TDataContext DataContext { get; set; }
        public Repository([Dependency]TDataContext dataContext)
        {
            this.DataContext = dataContext;
        }
        #region Implementation of IRepository<TEntity,in int>

        public virtual List<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return this.DataContext.Set<TEntity>().Where(expression).ToList();
        }

        public virtual TEntity Find(int key)
        {
            return this.DataContext.Set<TEntity>().Find(key);
        }

        public void Add(TEntity entity)
        {
            this.DataContext.Set<TEntity>().Add(entity);
            this.DataContext.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            this.DataContext.Set<TEntity>().Remove(entity);
            this.DataContext.SaveChanges();
        }

        public void Save(TEntity entity)
        {
            this.DataContext.SaveChanges();
        }

        #endregion
    }
}
