using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Configer
{
    /// <summary>
    ///
    /// </summary>
    public class AppJsonConfig
    {
        public string AllowedHosts { get; set; }
        public ConnectionString ConnectionString { get; set; }
        public CodeConfig CodeConfig { get; set; }
    }

    /// <summary>
    /// 日志
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }
    }

    /// <summary>
    /// 日志级别
    /// </summary>
    public class LogLevel
    {
        /// <summary>
        ///
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Microsoft { get; set; }
    }

    /// <summary>
    ///
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

    /// <summary>
    /// 日志存放路径
    /// </summary>
    public class CodeConfig
    {
        /// <summary>
        /// Code日志存放路径
        /// </summary>
        public string LogPath { get; set; }
    }
}