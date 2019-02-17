using Microsoft.AspNet.SignalR;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prj.IoC
{
    public class SignalRDependencyResolver: DefaultDependencyResolver
    {
        private readonly IContainer _container;
        public SignalRDependencyResolver(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;

            var service = base.GetService(serviceType);
            if (service != null) return service;

            return (!serviceType.IsAbstract && !serviceType.IsInterface && serviceType.IsClass)
                ? _container.GetInstance(serviceType)
                : _container.TryGetInstance(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>().Concat(base.GetServices(serviceType));
        }
    }
}