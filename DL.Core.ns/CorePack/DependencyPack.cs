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
                    else
                    {
                        services.AddSingleton(interfance, type);
                    }
                }
            }
            return services;
        }
    }
}