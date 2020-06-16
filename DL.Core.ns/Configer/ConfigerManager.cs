using DL.Core.ns.Extensiton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DL.Core.ns.Configer
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public static class ConfigerManager
    {
        /// <summary>
        /// 配置路径
        /// </summary>
        private static string[] BasePath = { "./", "./../", "./../../", "./../../../../", "./../../../../../", "./../../../../../../", "./../../../../../../../" };

        private static string Name = "appsettings.json";

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        public static AppJsonConfig getCofiger()
        {
            AppJsonConfig config = null;
            foreach (var item in BasePath)
            {
                var path = item + Name;
                if (File.Exists(path))
                {
                    using (StreamReader stream = new StreamReader(path, Encoding.UTF8))
                    {
                        var jsondata = stream.ReadToEnd().Trim();
                        config = jsondata.FromJson<AppJsonConfig>();
                    }
                }
            }
            return config;
        }
    }
}