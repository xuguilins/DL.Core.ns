using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Logging
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public static class LogManager
    {
        private static ConcurrentDictionary<Type, ILogger> parmms = new ConcurrentDictionary<Type, ILogger>();

        /// <summary>
        /// 创建当前应用程序同一个实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger GetLogger<T>()
        {
            if (parmms.ContainsKey(typeof(T)))
            {
                return parmms[typeof(T)];
            }
            else
            {
                var loger = new InternalLogger();
                parmms.TryAdd(typeof(T), loger);
                return loger;
            }
        }

        /// <summary>
        /// 创建当前应用程序同一个实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger GetLogger()
        {
            if (parmms.ContainsKey(typeof(LogManager)))
            {
                return parmms[typeof(LogManager)];
            }
            else
            {
                var loger = new InternalLogger();
                parmms.TryAdd(typeof(LogManager), loger);
                return loger;
            }
        }
    }
}