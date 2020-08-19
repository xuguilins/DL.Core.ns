using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.utility.ResultEnttiy
{
    /// <summary>
    ///  返回状态码
    /// </summary>
    public enum ReturnResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        // 错误
        /// </summary>
        Error = 2,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 3,

        /// <summary>
        /// 过期
        /// </summary>
        Expire = 4,

        /// <summary>
        /// 警告
        /// </summary>
        Warn = 5
    }
}