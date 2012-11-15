using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace BinbinDependencyResolver.Intergrations.Unity
{
    public class HttpContextLifetimeManager<T> : LifetimeManager, IDisposable
    {
        public void Dispose()
        {
            this.RemoveValue();
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[typeof (T).AssemblyQualifiedName];
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove(typeof (T).AssemblyQualifiedName);
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[typeof (T).AssemblyQualifiedName]
                = newValue;
        }
    }
}