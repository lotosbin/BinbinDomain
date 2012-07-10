using System.Linq;
using System.Text;

namespace BinbinDomain
{
    /// <summary>
    /// 工厂接口，只有聚合根才有工厂
    /// </summary>
    /// <typeparam name="TIEntity"></typeparam>
    public interface IFactory<TIEntity>
        where TIEntity : IAggregateRoot
    {
        /// <summary>
        /// 创建默认聚合根
        /// </summary>
        /// <returns></returns>
        TIEntity CreateDefault();
    }
}
