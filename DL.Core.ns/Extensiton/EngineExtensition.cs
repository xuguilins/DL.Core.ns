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
using Microsoft.Extensions.Caching.Memory;
using DL.Core.ns.EFCore;

namespace DL.Core.ns.Extensiton
{
    public static class EngineExtensition
    {
        private static ILogger logger = LogManager.GetLogger();

        /// <summary>
        /// 是否设置自动迁移
        /// </summary>
        private static bool IsAutoMigration { get; set; }

        /// <summary>
        /// 包含EF上下文
        /// </summary>
        /// <typeparam name="TDbContext">EF上下文</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPack<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("DL框架引擎初始化...\r\n");
                Stopwatch watch = new Stopwatch();
                watch.Start();
                IDLEnginePack service = new DLEnginePack();
                services.AddService();
                //模块注入
                service.AddEnginePack(services);
                //上下文注入
                services.AddDbContext<TDbContext>();
                services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                //服务构建
                IServiceProvider provider = services.BuildServiceProvider();
                //服务集合器设置
                ServiceLocator.Instance.SetServiceCollection(services);
                //服务构建器设置
                ServiceLocator.Instance.SetProvider(provider);
                //设置EF数据上下文
                DbContextManager.SetDbContext<TDbContext>();
                sb.Append($"准备检查是否开启自动迁移.【{IsAutoMigration}】\r\n");
                if (IsAutoMigration)
                {
                    var result = AutoMigration(typeof(TDbContext));
                    sb.Append(result + "\r\n");
                }
                watch.Stop();
                sb.Append($"DL框架引擎初始化完成\r\n");
                sb.Append($"总共花费:{watch.ElapsedMilliseconds}毫秒");
                logger.Info(sb.ToString());
                return services;
            }
            catch (Exception ex)
            {
                logger.Error($"初始化DL框架发生异常[AddPack<TDbContext>]，异常信息：{ex.Message}");
                throw;
            }
        }

        /// <summary>
        ///不包含数据库上下文
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPack(this IServiceCollection services)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("DL框架引擎初始化...\r\n");
                Stopwatch watch = new Stopwatch();
                watch.Start();
                IDLEnginePack service = new DLEnginePack();
                services.AddService();
                //模块注入
                service.AddEnginePack(services);
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
            catch (Exception ex)
            {
                logger.Error($"初始化DL框架发生异常[AddPack()]，异常信息：{ex.Message}");
                throw;
            }
        }

        private static IServiceCollection AddService(this IServiceCollection services)
        {
            //添加缓存
            services.AddMemoryCache();
            services.AddScoped<IMemoryCache, MemoryCache>();

            return services;
        }

        /// <summary>
        /// 启用自动迁移需要在<see cref="AddPack{TDbContext}(IServiceCollection)"/>之前
        /// </summary>
        /// <param name="services"></param>
        /// <param name="flag"></param>
        public static void EnableMigration(this IServiceCollection services, bool flag = false)
        {
            IsAutoMigration = flag;
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="services"></param>
        public static void Test(this IServiceCollection services)
        {
        }

        /// <summary>
        /// 自动迁移
        /// </summary>
        /// <param name="context"></param>
        private static string AutoMigration(Type context)
        {
            try
            {
                DbContext dbcontext = Activator.CreateInstance(context) as DbContext;
                if (dbcontext.Database.GetPendingMigrations().Any())
                {
                    dbcontext.Database.Migrate();
                    return "启动迁移完毕";
                }
                else
                {
                    return "未检测到含有启动迁移的文件或数据实体未发生任何改变,请尝试运行 Add-Migration 指令";
                }
            }
            catch (Exception ex)
            {
                logger.Error($"自动迁移发生异常，异常原因：{ex.Message}");
                throw;
            }
        }
    }
}