using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.EventBusHandler
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 成功事件
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败事件
        /// </summary>
        Error = 1,

        /// <summary>
        /// 警告事件
        /// </summary>
        Warn = 2,

        /// <summary>
        /// 正常事件
        /// </summary>
        Info = 3
    }
}