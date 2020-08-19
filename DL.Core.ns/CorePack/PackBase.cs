using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns
{
    /// <summary>
    /// 模块包基类
    /// </summary>
    public abstract class PackBase
    {
        /// <summary>
        /// 启动级别
        /// 数字越小启动越快
        /// </summary>
        public abstract int StarLevel { get; }

        /// <summary>
        /// 注入基类
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public abstract IServiceCollection AddService(IServiceCollection services);
    }
}