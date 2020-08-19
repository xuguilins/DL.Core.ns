using DL.Core.ns.CorePack;
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
using DL.Core.utility.Logging;

namespace DL.Core.ns.Extensiton
{
    public static class EngineExtensition
    {
        private static ILogger logger = LogManager.GetLogger();

        /// <summary>
        /// 初始化单个数据库上下文
        /// </summary>
        /// <typeparam name="TDbCotnext">EF数据库上下文</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEngineDbContextPack<TDbCotnext>(this IServiceCollection services) where TDbCotnext : DbContext
        {
            services.AddDbContext<TDbCotnext>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TDbCotnext>>();
            var type = typeof(TDbCotnext);
            var db = Activator.CreateInstance(type) as DbContext;
            DbContextManager.InitUnitDbContext(db);
            //设置EF数据实体上下文
            DbContextManager.InitEngityDbContext();
            return services;
        }

        /// <summary>
        /// 初始化多个EF数据库上下文
        /// <see cref="TDbContext1">数据库上下文1</see>
        /// <see cref="TDbContext2">数据库上下文2</see>
        /// </summary>
        /// <typeparam name="TDbContext1">数据库上下文1</typeparam>
        /// <typeparam name="TDbContext2">数据库上下文2</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEngineDbContextPack<TDbContext1, TDbContext2>(this IServiceCollection services)
            where TDbContext1 : DbContext
            where TDbContext2 : DbContext
        {
            services.AddDbContext<TDbContext1>();
            services.AddDbContext<TDbContext2>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext1>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext2>>();
            var type1 = typeof(TDbContext1);
            var db = Activator.CreateInstance(type1) as DbContext;
            DbContextManager.InitUnitDbContext(db);
            var type2 = typeof(TDbContext2);
            var db2 = Activator.CreateInstance(type2) as DbContext;
            DbContextManager.InitUnitDbContext(db2);
            //设置EF数据实体上下文
            DbContextManager.InitEngityDbContext();
            return services;
        }

        /// <summary>
        /// 初始化多个EF数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext1">数据库上下文1</typeparam>
        /// <typeparam name="TDbContext2">数据库上下文2</typeparam>
        /// <typeparam name="TDbContext3">数据库上下文3</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEngineDbContextPack<TDbContext1, TDbContext2, TDbContext3>(this IServiceCollection services)
            where TDbContext1 : DbContext
            where TDbContext2 : DbContext
            where TDbContext3 : DbContext
        {
            services.AddDbContext<TDbContext1>();
            services.AddDbContext<TDbContext2>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext1>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext2>>();
            var type1 = typeof(TDbContext1);
            var db = Activator.CreateInstance(type1) as DbContext;
            DbContextManager.InitUnitDbContext(db);
            var type2 = typeof(TDbContext2);
            var db2 = Activator.CreateInstance(type2) as DbContext;
            DbContextManager.InitUnitDbContext(db2);
            var type3 = typeof(TDbContext3);
            var db3 = Activator.CreateInstance(type3) as DbContext;
            DbContextManager.InitUnitDbContext(db3);
            //设置EF数据实体上下文
            DbContextManager.InitEngityDbContext();
            return services;
        }

        /// <summary>
        /// 模块包注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEnginePack(this IServiceCollection services)
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
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                //服务构建
                IServiceProvider provider = services.BuildServiceProvider();
                //服务集合器设置
                ServiceLocator.Instance.SetServiceCollection(services);
                //服务构建器设置
                ServiceLocator.Instance.SetProvider(provider);
                var autoConfig = utility.Configer.ConfigerManager.Instance.getCofiger().CodeConfig.AutoMigrationEnable;
                sb.Append($"准备检查是否开启自动迁移.【{autoConfig}】\r\n");
                if (autoConfig)
                {
                    var contexts = DbContextManager.GetMeomryDbContxt();
                    sb.Append($"统计数据库上下文数量:{contexts.Count}\r\n");
                    if (contexts.Any())
                    {
                        contexts.ForEach(context =>
                        {
                            var name = context.GetType().Name;
                            sb.Append($"准备迁移数据库上下文[{name}]的数据实体");
                            var result = AutoMigration(context);
                            sb.Append($"上下文：[{name}]" + result + "\r\n");
                        });
                    }
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

        private static IServiceCollection AddService(this IServiceCollection services)
        {
            //添加缓存
            services.AddMemoryCache();
            services.AddScoped<IMemoryCache, MemoryCache>();
            return services;
        }

        /// <summary>
        /// 自动迁移
        /// </summary>
        /// <param name="context"></param>
        private static string AutoMigration(DbContext context)
        {
            try
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                    return "迁移完毕";
                }
                else
                {
                    return "未检测到含有启动迁移的文件或数据实体未发生任何改变,请尝试运行 Add-Migration 指令";
                }
            }
            catch (Exception ex)
            {
                logger.Error($"自动迁移发生异常，异常原因：{ex.Message}");
                throw new Exception($"自动迁移发生异常，异常原因：{ex.Message}");
            }
        }
    }
}