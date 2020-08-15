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
            if (_provider == null)
                throw new Exception("无效的服务提供者");
            return _provider.GetService<T>();
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

        /// <summary>
        /// 获取多个服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetServices<T>()
        {
            if (_provider == null)
                throw new Exception("无效的服务提供者");
            return _provider.GetServices<T>();
        }
    }
}