using DL.Core.ns.CorePack;
using DL.Core.ns.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DL.Core.ns.Locator;
using DL.Core.ns.Finder;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DL.Core.ns.Extensiton
{
    public static class EngineExtensition
    {
        private static ILogger logger = LogManager.GetLogger();

        public static IServiceCollection AddPack<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DL框架引擎初始化...\r\n");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IDLEnginePack service = new DLEnginePack();
            //添加缓存
            services.AddMemoryCache();
            //模块注入
            service.AddEnginePack(services);
            //上下文注入
            services.AddDbContext<TDbContext>();
            //服务构建
            IServiceProvider provider = services.BuildServiceProvider();
            //服务集合器设置
            ServiceLocator.Instance.SetServiceCollection(services);
            //服务构建器设置
            ServiceLocator.Instance.SetProvider(provider);
            watch.Stop();
            sb.Append($"DL框架引擎初始化完成\r\n");
            sb.Append($"总共花费:{watch.ElapsedMilliseconds}毫秒");
            logger.Info(sb.ToString());
            return services;
        }
    }
}