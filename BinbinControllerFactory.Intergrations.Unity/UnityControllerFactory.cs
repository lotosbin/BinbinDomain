using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace BinbinControllerFactory.Intergrations.Unity
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// <![CDATA[ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));]]>
    /// </example>
    public class  UnityControllerFactory : DefaultControllerFactory
    {
        IUnityContainer _container;

        public UnityControllerFactory(IUnityContainer container)
        {
            this._container = container;
        }
        public void RegisterControllers()
        {
            //将当前装配件中实现IContrller接口的类注册到Unity


            Type controllerType = typeof(IController);
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (controllerType.IsAssignableFrom(type))
                {
                    this._container.RegisterType(type, type);
                }
            }
        }


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            if (!typeof(IController).IsAssignableFrom(controllerType))
                throw new ArgumentException(string.Format(
                                                          "Type requested is not a controller: {0}", controllerType.Name),
                                            "controllerType");

            return this._container.Resolve(controllerType) as IController;
        }
    }
}
