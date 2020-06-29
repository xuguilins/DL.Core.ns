using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Dependency
{
    /// <summary>
    ///  特性注入
    ///  目前只支持在服务类中打上注入标记
    ///  且类必须实现接口
    /// </summary>
    public class AttbuiteDependency : Attribute
    {
        public ServiceLifetime Lifetime { get; }

        public AttbuiteDependency(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}