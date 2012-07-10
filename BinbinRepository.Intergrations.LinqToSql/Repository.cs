using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BinbinRepository.Intergrations.LinqToSql
{
    public abstract class Repository<TDataContext, TEntity> : IRepository<TEntity, int>
        where TDataContext : DataContext
        where TEntity : class
    {

        public Repository(TDataContext dataContext)
        {
            this.DataContext = dataContext;
        }

        #region Implementation of IRepository<TEntity,in int>

        public virtual List<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return this.DataContext.GetTable<TEntity>().Where(expression).ToList();
        }

        public abstract TEntity Find(int key);

        public virtual void Add(TEntity entity)
        {
            this.DataContext.GetTable<TEntity>().InsertOnSubmit(entity);
            this.DataContext.SubmitChanges();
        }

        public virtual void Remove(TEntity entity)
        {
            this.DataContext.GetTable<TEntity>().DeleteOnSubmit(entity);
            this.DataContext.SubmitChanges();
        }

        public virtual void Save(TEntity entity)
        {
            this.DataContext.SubmitChanges();
        }

        #endregion

        private TDataContext DataContext { get; set; }
    }
}
