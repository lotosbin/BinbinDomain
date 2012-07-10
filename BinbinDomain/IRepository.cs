using System;
using System.Collections.Generic;
using System.Linq;

namespace BinbinDomain
{
    /// <summary>
    /// 仓储接口，只有聚合根才有仓储
    /// </summary>
    /// <typeparam name="TIEntity"></typeparam>
    public interface IRepository<TIEntity> : IRepository<TIEntity, Guid>
        where TIEntity : class,IAggregateRoot
    {


    }

    public interface IRepository<TIEntity, TKey>
        where TIEntity : class,IAggregateRoot
    {
        TIEntity GetById(TKey id);
        List<TIEntity> GetByIdRange(List<TKey> ids);
        bool ExistById(TKey key);
        void Add(TIEntity entity);
        void AddRange(List<TIEntity> entities);
        void Delete(TIEntity entity);
        void DeleteRange(List<TIEntity> entities);
        void Save(TIEntity entity);
        void SaveRange(List<TIEntity> entities);
        IQueryable<TIEntity> WithId(TKey key);
    }
}