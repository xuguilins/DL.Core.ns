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
            Configuration = configuration;
        }

        public override string FileName => "appsettings.json";
        public static ConfigerManager Instance = lazyList.Value;
        public IConfiguration Configuration { get; set; }

        public IConfiguration Build()
        {
            if (configuration != null)
            {
                return configuration;
            }
            else
            {
                var path = Directory.GetCurrentDirectory();
                return new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddJsonFile("appsettings.Development.json", optional: true)
             .Build();
            }
        }
    }
}