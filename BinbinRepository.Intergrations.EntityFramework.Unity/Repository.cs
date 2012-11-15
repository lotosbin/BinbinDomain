using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace BinbinRepository.Intergrations.EntityFramework.Unity
{
    public class UnityRepository<TDataContext, TEntity, TKey> : Repository<TDataContext, TEntity, TKey>
        where TDataContext : DbContext
        where TEntity : class
    {
        public UnityRepository([Dependency] TDataContext dataContext) : base(dataContext)
        {
        }
    }
}