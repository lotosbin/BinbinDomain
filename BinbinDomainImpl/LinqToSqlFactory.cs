using System;
using System.Data.Linq;
using BinbinDomain;

namespace BinbinDomainImpl
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIEntity"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDataContext"></typeparam>
    public class LinqToSqlFactory<TIEntity, TEntity, TDataContext> : IFactory<TIEntity>
        where TIEntity : IAggregateRoot
        where TEntity : class,TIEntity
        where TDataContext : DataContext
    {

        public virtual TIEntity CreateDefault()
        {
            throw new NotImplementedException();
        }
    }
}