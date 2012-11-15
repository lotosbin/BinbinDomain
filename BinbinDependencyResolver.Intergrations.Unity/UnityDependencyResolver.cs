using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace BinbinDependencyResolver.Intergrations.Unity
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     <![CDATA[DependencyResolver.SetResolver(new UnityDependencyResolver(container));]]></example>
    public class UnityDependencyResolver : IDependencyResolver, IDisposable
    {
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.Container = container;
        }

        private IUnityContainer Container { get; set; }

        public object GetService(Type serviceType)
        {
            if ((serviceType.IsClass && !serviceType.IsAbstract) || this.Container.IsRegistered(serviceType))
                return this.Container.Resolve(serviceType);
            return null;
        }


        public IEnumerable<object> GetServices(Type serviceType)
        {
            if ((serviceType.IsClass && !serviceType.IsAbstract) || this.Container.IsRegistered(serviceType))
                return this.Container.ResolveAll(serviceType);

            return new object[] {};
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}