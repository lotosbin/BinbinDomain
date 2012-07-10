using System;
using System.Collections.Generic;
using System.Data.Linq;
using BinbinDomain;
using BinbinUnitOfWork;

namespace BinbinDomainImpl
{   /// <summary>
    /// ²Ö´¢»ùÀà
    /// </summary>
    /// <typeparam name="TIEntity"></typeparam>
    public abstract class Repository<TIEntity, TEntity, TDataContext> : Repository<TIEntity, Guid, TEntity, TDataContext>, IRepository<TIEntity>
        where TIEntity : IAggregateRoot
        where TEntity : class,TIEntity
        where TDataContext : DataContext
    {

    }
    public abstract class Repository<  TEntity, TKey, TDataContext> : IRepository<TEntity, TKey>
         
        where TEntity : class
        where TDataContext : DataContext
    {
        protected TDataContext DataContext { get; set; }
        public abstract TEntity GetById(TKey id);
        public abstract bool ExistById(TKey key);
        public abstract List<TEntity> GetByIdRange(List<TKey> ids);

        public virtual void Add(TEntity entity)
        {
            this.DataContext.GetTable<TEntity>().InsertOnSubmit((TEntity)entity);

            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();
            }
        }

        public virtual void AddRange(List<TEntity> entities)
        {
            this.DataContext.GetTable<TEntity>().InsertAllOnSubmit(entities.ConvertAll(e => (TEntity)e));
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();
            }
        }

        public virtual void Delete(TIEntity entity)
        {
            this.DataContext.GetTable<TEntity>().DeleteOnSubmit((TEntity)entity);
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();
            }
        }

        public virtual void DeleteRange(List<TIEntity> entities)
        {
            this.DataContext.GetTable<TEntity>().DeleteAllOnSubmit(entities.ConvertAll(e => (TEntity)e));
            if (UnitOfWorkScope.Current != null)
            {
                UnitOfWorkScope.Current.JoinScope(this.DataContext);
            }
            else
            {
                this.DataContext.SubmitChanges();
            }
        }

        public virtual void Save(TIEntity entity)
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

        public virtual void SaveRange(List<TIEntity> entities)
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


    }
}