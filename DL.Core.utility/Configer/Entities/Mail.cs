using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.utility.Configer.Entities
{
    /// <summary>
    /// 邮件配置
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// 端口
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// 服务器
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// 发送人账号
        /// </summary>
        public string SendUser { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string SmtpPass { get; set; }
    }

    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// 是否开启EF自动迁移
        /// </summary>
        public bool AutoEFMigrationEnable { get; set; }

        /// <summary>
        /// 是否开启ADO.NET自动建表
        /// </summary>
        public bool AutoAdoNetMiagraionEnable { get; set; }
    }

    /// <summary>
    /// 接口文档配置
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string SwaggerName { get; set; }

        /// <summary>
        /// 文档标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string SwaggerDesc { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// xml文件名
        /// </summary>
        public string XmlAssmblyName { get; set; }

        /// <summary>
        /// 是否启用授权
        /// </summary>
        public bool Authorization { get; set; }
    }

    /// <summary>
    /// 数据库链接字符串配置
    /// </summary>
    public class ConnectionString
    {
        /// <summary>
        /// 默认EF数据库连接字符串
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// SqlServer数据库连接字符串
        /// </summary>
        public string SqlDefault { get; set; }

        /// <summary>
        /// MySql数据库连接字符串
        /// </summary>
        public string MySqlDefault { get; set; }

        /// <summary>
        /// Oracle数据库连接字符串
        /// </summary>
        public string OracleDefault { get; set; }
    }
}