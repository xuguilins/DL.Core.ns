using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.ResultFactory
{
    public class ReturnResult<T> where T : class, new()
    {
        public ReturnResult()
        {
        }

        public ReturnResult(ReturnResultCode code, T data = null) : this(code, data, null)
        {
        }

        public ReturnResult(ReturnResultCode code, T data, string message = null)
        {
        }

        /// <summary>
        /// 返回状态码
        /// </summary>
        public ReturnResultCode Code { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据总数(分页时需要)
        /// </summary>
        public int Count { get; set; }
    }

    public class ReturnResult : ReturnResult<object>
    {
        public ReturnResult()
        {
        }

        public ReturnResult(ReturnResultCode code) : this(code, null)
        {
            Code = code;
        }

        public ReturnResult(ReturnResultCode code, object data = null) : this(code, data, null)
        {
            Code = code;
            Data = data;
        }

        public ReturnResult(ReturnResultCode code, object data, string message = null)
        {
            Code = code;
            Data = data;
            Message = message;
        }
    }
}