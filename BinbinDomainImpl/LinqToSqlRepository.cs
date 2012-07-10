using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using Binbin.Linq;
using BinbinDomain;
using BinbinUnitOfWork;
using Microsoft.Practices.Unity;

namespace BinbinDomainImpl
{
    public abstract class LinqToSqlRepository<TDataContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TDataContext : DataContext
        where TEntity : class, IAggregateRoot
    {
        [Dependency]
        public TDataContext DataContext { get; set; }

        public TEntity GetById(TKey id)
        {
            return this.WithId(id).Single();
        }

        public abstract List<TEntity> GetByIdRange(List<TKey> ids);

        public bool ExistById(TKey key)
        {
            return this.WithId(key).IsExist();
        }

        public void Add(TEntity entity)
        {
            this.DataContext.GetTable<TEntity>().InsertOnSubmit(entity);
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();
            }
        }

        public void AddRange(List<TEntity> entities)
        {
            this.DataContext.GetTable<TEntity>().InsertAllOnSubmit(entities);
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            this.DataContext.GetTable<TEntity>().DeleteOnSubmit(entity);
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();
            }
        }

        public void DeleteRange(List<TEntity> entities)
        {
            this.DataContext.GetTable<TEntity>().DeleteAllOnSubmit(entities);
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();
            }
        }

        public void Save(TEntity entity)
        {
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();

            }
        }

        public void SaveRange(List<TEntity> entities)
        {
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();

            }
        }

        public abstract IQueryable<TEntity> WithId(TKey key);
    }
}
