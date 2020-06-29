using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ns.Finder;
using System.Linq;
using DL.Core.ns.Dependency;

namespace DL.Core.ns.CorePack
{
    /// <summary>
    /// 依赖注入包
    /// </summary>
    public class DependencyPack : PackBase
    {
        public override int StarLevel => 10;

        public override IServiceCollection AddService(IServiceCollection services)
        {
            IDependencyFinder finder = new DependencyFinder();
            var types = finder.FinderAll();
            foreach (var type in types)
            {
                var interfance = type.GetInterfaces().FirstOrDefault(x => x.IsInterface && !x.IsDefined(typeof(IgnoreDependency), false));
                if (interfance != null)
                {
                    if (typeof(IScopeDependcy).IsAssignableFrom(interfance))
                    {
                        services.AddScoped(interfance, type);
                    }
                    else if (typeof(ITransientDependcy).IsAssignableFrom(interfance))
                    {
                        services.AddTransient(interfance, type);
                    }
                    else if (typeof(ISingletonDependcy).IsAssignableFrom(interfance))
                    {
                        services.AddSingleton(interfance, type);
                    }
                    else
                    {
                        services = AddAttbuiteDependenty(services, interfance, type);
                    }
                }
            }
            return services;
        }

        /// <summary>
        /// 特性注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interfance"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private IServiceCollection AddAttbuiteDependenty(IServiceCollection services, Type interfance, Type type)
        {
            //检查当前类的特性
            var attbuite = type.GetCustomAttributes(false);
            if (attbuite != null && attbuite.Length > 0)
            {
                var attb = attbuite[0] as AttbuiteDependency;
                if (attb != null)
                {
                    switch (attb.Lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(interfance, type);
                            break;

                        case ServiceLifetime.Singleton:
                            services.AddSingleton(interfance, type);
                            break;

                        case ServiceLifetime.Transient:
                            services.AddTransient(interfance, type);
                            break;
                    }
                }
            }
            return services;
        }
    }
}