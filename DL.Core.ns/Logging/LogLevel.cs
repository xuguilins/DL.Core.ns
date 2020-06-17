using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Logging
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 正常消息
        /// </summary>
        Info = 0,

        /// <summary>
        /// 错误消息
        /// </summary>
        Error = 1,

        /// <summary>
        /// 警告消息
        /// </summary>
        Warn = 2,

        /// <summary>
        /// 成功消息
        /// </summary>
        Success = 3,

        /// <summary>
        /// 调试消息
        /// </summary>
        Debug = 4
    }
}