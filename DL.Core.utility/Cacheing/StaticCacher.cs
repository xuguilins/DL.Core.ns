using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.utility.Cacher
{
    /// <summary>
    /// 静态内存缓存
    /// </summary>
    public static class StaticCacher
    {
        private static ConcurrentDictionary<string, object> pairs = new ConcurrentDictionary<string, object>();

        /// <summary>
        ///  设置静态缓存
        ///  若key存在,则更新原有的缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        public static void SetCacher(string key, object val)
        {
            if (!pairs.ContainsKey(key))
            {
                pairs.TryAdd(key, val);
            }
            else
            {
                pairs[key] = val;
                //throw new ArgumentNullException($"{key}", $"指定的key“{key}”已存在");
            }
        }

        /// <summary>
        /// 更新静态缓存
        /// 若key不存在，抛异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void UpdateCacher(string key, object val)
        {
            if (!pairs.ContainsKey(key))
                throw new Exception($"指定的key “{key}”不存在");
            pairs[key] = val;
        }

        /// <summary>
        /// 检查指定的key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns>t</returns>
        public static bool CheckKeyIsExite(string key)
        {
            return pairs.ContainsKey(key);
        }

        /// <summary>
        /// 获取静态缓存的数据
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static object GetCacher(string key)
        {
            if (CheckKeyIsExite(key))
            {
                return pairs[key];
            }
            else
            {
                return null;
            }
        }
    }
}