using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace BinbinRepository.Intergrations.LinqToSql
{
    public abstract class Repository<TDataContext, TEntity, TKey> : IRepository<TEntity, TKey>
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

        public virtual TEntity Find(TKey key)
        {
            var itemParameter = Expression.Parameter(typeof (TEntity), "item");
            var whereExpression = Expression.Lambda<Func<TEntity, bool>>
                (
                    Expression.Equal(
                        Expression.Property(
                            itemParameter,
                            this.GetPrimaryKeyName()
                            ),
                        Expression.Constant(key)
                        ),
                    new[] {itemParameter}
                );
            return this.DataContext.GetTable<TEntity>().Where(whereExpression).Single();
        }

        public string GetPrimaryKeyName()
        {
            var metaType = this.DataContext.Mapping.GetMetaType(typeof (TEntity));
            var primaryKey = metaType.DataMembers.Single(m => m.IsPrimaryKey);
            return primaryKey.Name;
        }

        #endregion

        private TDataContext DataContext { get; set; }
    }
}