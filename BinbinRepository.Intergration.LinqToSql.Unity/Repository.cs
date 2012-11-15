using System.Data.Linq;
using Microsoft.Practices.Unity;

namespace BinbinRepository.Intergrations.LinqToSql.Unity
{
    public class UnityRepository<TDataContext, TEntity, TKey> : Repository<TDataContext, TEntity, TKey>
        where TDataContext : DataContext
        where TEntity : class
    {
        public UnityRepository([Dependency] TDataContext dataContext) : base(dataContext)
        {
        }
    }
}