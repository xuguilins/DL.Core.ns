using DL.Core.utility.Configer.Entities;
using Microsoft.Extensions.Configuration;
using DL.Core.utility.Extendsition;
using System;

namespace DL.Core.utility.Configer
{
    /// <summary>
    /// 配置文件扩展
    /// </summary>
    public static class IConfigurationExtendsition
    {
        /// <summary>
        /// 获取Setting下面的最小级别配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSetting(this IConfiguration configuration, string key)
        {
            return configuration["DL:Setting:" + key];
        }

        /// <summary>
        /// 获取邮件配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Mail GetMailSetting(this IConfiguration configuration)
        {
            var info = new Mail();
            var config = configuration.GetSection("DL:Setting:Mail");
            info.SmtpPort = config["StmpPort"].ToInt32();
            info.SmtpHost = config["StmpHost"];
            info.SmtpPass = config["SmtpPass"];
            info.SendUser = config["SendUser"];
            return info;
        }

        /// <summary>
        /// 获取DbConfig配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static DbConfig GetDbSetting(this IConfiguration configuration)
        {
            var config = configuration.GetSection("DL:Setting:DbConfig");
            var db = new DbConfig();
            db.AutoAdoNetMiagraionEnable = Convert.ToBoolean(config["AutoAdoNetMiagraionEnable"]);
            db.AutoEFMigrationEnable = Convert.ToBoolean(config["AutoEFMigrationEnable"]);
            return db;
        }

        /// <summary>
        /// 获取Swagger配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static SwaggerConfig GetSwaggerSetting(this IConfiguration configuration)
        {
            var swg = new SwaggerConfig();
            var config = configuration.GetSection("DL:Setting:Swagger");
            swg.Enable = Convert.ToBoolean(config["Enable"]);
            swg.SwaggerName = config["SwaggerName"];
            swg.SwaggerDesc = config["SwaggerDesc"];
            swg.Version = config["Version"];
            swg.XmlAssmblyName = config["XmlAssmblyName"];
            return swg;
        }

        /// <summary>
        /// 获取数据库连接字符串配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ConnectionString GetConStrSetting(this IConfiguration configuration)
        {
            var con = new ConnectionString();
            con.Default = configuration["ConnectionString:default"];
            con.SqlDefault = configuration["ConnectionString:SqlDefault"];
            con.MySqlDefault = configuration["ConnectionString:MySqlDefault"];
            con.OracleDefault = configuration["ConnectionString:OracleDefault"];
            return con;
        }
    }
}