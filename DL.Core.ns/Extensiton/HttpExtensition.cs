using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Extensiton
{
    public static class HttpExtensition
    {
        /// <summary>
        /// 是否为HTTP请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            string data = string.Empty;
            if (request != null)
            {
                data = request.Headers["x-requested-with"].ToString();
            }
            if (!string.IsNullOrEmpty(data))
            {
                return data.Contains("XMLHttpRequest") ? true : false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证传入的数据是否为json数据
        /// </summary>
        /// <returns></returns>
        public static bool IsHttpJsonContextType(this HttpRequest request)
        {
            if (request != null)
            {
                var flag = request.Headers?["Content-Type"].ToString()
                            .IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1
                        || request.Headers?["Content-Type"].ToString()
                            .IndexOf("text/json", StringComparison.OrdinalIgnoreCase) > -1;
                if (flag)
                    return true;
                flag = request.Headers?["Accept"].ToString()
                           .IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1
                       || request.Headers?["Accept"].ToString().IndexOf("text/json", StringComparison.OrdinalIgnoreCase) >
                       -1;
                return flag;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证是否含有授权认证头部
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsHaveAuthorization(this HttpRequest request)
        {
            return request.Headers.ContainsKey("Authorization");
        }

        /// <summary>
        /// 获取当前请求的Content-Type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetContentType(this HttpRequest request)
        {
            return request.ContentType;
        }
    }
}