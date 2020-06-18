using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Cacheing
{
    /// <summary>
    /// Core基本缓存的封装
    /// </summary>
    public static class MemoreyCacher
    {
        private static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 100, CompactionPercentage = 0.2, ExpirationScanFrequency = TimeSpan.FromMinutes(1) });

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="span">绝对过期时间</param>
        public static void SetCacher(string key, object val, TimeSpan span)
        {
            Cache.Set(key, val, span);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCacher(string key)
        {
            return Cache.Get(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="span">绝对过期时间</param>
        public static void SetCacher<T>(string key, T val, TimeSpan span) where T : class
        {
            Cache.Set<T>(key, val, span);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCacher<T>(string key) where T : class
        {
            return Cache.Get<T>(key);
        }
    }
}