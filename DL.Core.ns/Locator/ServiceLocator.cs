using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Locator
{
    /// <summary>
    /// 服务定位器
    /// </summary>
    public sealed class ServiceLocator
    {
        private IServiceProvider _provider = null;
        private IServiceCollection _services = null;
        private static readonly Lazy<ServiceLocator> Locator = new Lazy<ServiceLocator>(() => new ServiceLocator());
        public static ServiceLocator Instance => Locator.Value;

        /// <summary>
        /// 设置服务提供者
        /// </summary>
        /// <param name="provider"></param>
        public void SetProvider(IServiceProvider provider)
        {
            this._provider = provider;
        }

        /// <summary>
        /// 设置服务集合
        /// </summary>
        /// <param name="services"></param>
        public void SetServiceCollection(IServiceCollection services)
        {
            this._services = services;
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            if (_provider != null)
                return _provider.GetService<T>();
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetService(Type type)
        {
            return _provider.GetService(type);
        }
    }
}