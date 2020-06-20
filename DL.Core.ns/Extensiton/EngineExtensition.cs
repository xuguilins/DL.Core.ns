using DL.Core.ns.CorePack;
using DL.Core.ns.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DL.Core.ns.Extensiton
{
    public static class EngineExtensition
    {
        private static ILogger logger = LogManager.GetLogger();

        public static IServiceCollection AddPack(this IServiceCollection services)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DL引擎初始化...\r\n");
            // logger.Debug($"DL引擎初始化...");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IDLEnginePack service = new DLEnginePack();
            service.AddEnginePack(services);
            watch.Stop();
            sb.Append($"DL引擎初始化完成\r\n");
            sb.Append($"总共花费:{watch.ElapsedMilliseconds}毫秒");
            logger.Info(sb.ToString());
            return services;
        }
    }
}