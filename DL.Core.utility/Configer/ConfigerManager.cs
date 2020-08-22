using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using DL.Core.utility.Extendsition;

namespace DL.Core.utility.Configer
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class ConfigerManager : ConfigFileBase
    {
        private static Lazy<ConfigerManager> lazyList = new Lazy<ConfigerManager>(() => new ConfigerManager());
        private IConfiguration configuration;

        private ConfigerManager()
        {
            var path = Directory.GetCurrentDirectory();
            configuration = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddJsonFile("appsettings.Development.json", optional: true)
             .Build();
        }

        public override string FileName => "appsettings.json";
        public static ConfigerManager Instance = lazyList.Value;

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        public AppJsonConfig getCofiger()
        {
            AppJsonConfig config = null;
            foreach (var item in BasePath)
            {
                var path = item + FileName;
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

        /// <summary>
        ///直接 获取配置文件的属性的值
        /// 多级如下 code:email:username
        /// 以"："区分多级
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return configuration[key];
        }
    }
}