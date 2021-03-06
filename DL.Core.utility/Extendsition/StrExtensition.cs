﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DL.Core.utility.Extendsition
{
    public static class StrExtensition
    {
        /// <summary>
        /// 字符转INT
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ToInt32(this string data) => Convert.ToInt32(data);

        /// <summary>
        /// 字符串转INT
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ToInt16(this string data) => Convert.ToInt16(data);

        /// <summary>
        /// 字符串转金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string data) => Convert.ToDecimal(data);

        /// <summary>
        /// 字符串转单精度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static float ToFloat(this string data) => Convert.ToSingle(data);

        /// <summary>
        /// 字符串转双精度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double ToDuble(this string data) => Convert.ToDouble(data);

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="fix"></param>
        /// <returns></returns>
        public static string CreateNumber(string fix) => fix + DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random(Guid.NewGuid().GetHashCode()).Next(1000, 10000);

        #region [日期扩展]

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTime() => DateTime.Now;

        /// <summary>
        /// 获取时间信息（可格式化）
        /// </summary>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static string GetDataTime(string formatter = "yyyy-MM-dd") => DateTime.Now.ToString(formatter);

        /// <summary>
        /// 字符串转DateTime
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string data) => Convert.ToDateTime(data);

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="data">时间类型的字符串</param>
        /// <param name="formatter">格式化参数</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string data, string formatter = "yyyy-MM-dd HH:mm:ss") => Convert.ToDateTime(data).ToString(formatter).ToDateTime();

        /// <summary>
        /// 日期格式化[格式化：yyyy-MM-dd]
        /// </summary>
        /// <param name="time">日期</param>
        /// <returns></returns>
        public static string DateToString(this DateTime time) => time.ToString("yyyy-MM-dd");

        #endregion [日期扩展]

        /// <summary>
        /// 字符串分割为成数组
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="fix">分割的字符,默认为【,】</param>
        /// <returns></returns>
        public static string[] ExpenstrToarry(this string data, char fix = ',') => data.Split(fix);

        /// <summary>
        /// 数组转字符串
        /// </summary>
        /// <param name="arry">字符串数组</param>
        /// <param name="fix">分隔的字符默认为【,】</param>
        /// <returns></returns>
        public static string ArryToStr(this string[] arry, string fix = ",") => string.Join(fix, arry);

        /// <summary>
        /// 集合转字符串
        /// </summary>
        /// <param name="list">字符串集合</param>
        /// <param name="fix">拼接字符,默认为【,】</param>
        /// <returns></returns>
        public static string ListToStr(this List<string> list, string fix = ",") => string.Join(fix, list.Select(x => x.ToString()));

        /// <summary>
        /// 移除字符串中后面的X个字符
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ExpenSubstr(this string data, int count = 1) => data.Length > count ? data.Substring(0, data.Length - count) : "移除失败,移除字符个数大于当前字符串长度";

        /// <summary>
        /// 获取guid，（可格式化）
        /// </summary>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static string GetGuid(string formatter = "N") => Guid.NewGuid().ToString(formatter);

        /// <summary>
        /// 仅支持对象T转字典,不允许对象嵌套
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(this object value)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var propList = value.GetType().GetProperties();
            foreach (var item in propList)
            {
                var name = item.Name;
                var val = item.GetValue(value, null);
                dic.Add(name, val);
            }
            return dic;
        }

        /// <summary>
        /// 字典转对象,仅支持一级
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToObject<T>(this Dictionary<string, object> value) where T : class, new()
        {
            var model = new T();
            var propList = model.GetType().GetProperties();
            foreach (var item in value)
            {
                var info = propList.FirstOrDefault(m => m.Name.Equals(item.Key));
                if (info != null)
                {
                    info.SetValue(model, item.Value);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取时间排序的GUID
        /// </summary>
        /// <returns></returns>
        public static string GetDateGuid()
        {
            var time = GetDataTime("ddHHmm");
            long i = 1;
            foreach (var bt in Guid.NewGuid().ToByteArray())
                i *= bt + 1;
            var result = string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper();
            return time + result;
        }

        /// <summary>
        /// 获取可排序的GUID
        /// </summary>
        /// <returns></returns>
        public static string GetXGuid()
        {
            var guid = Guid.NewGuid().ToString("N").ToUpper();
            var bytes = Encoding.UTF8.GetBytes(guid);
            Array.Reverse(bytes);
            string result = string.Empty;
            for (int j = 0; j < 2; j++)
            {
                result += bytes[j];
            }
            var uid = guid.ExpenSubstr(15);
            return result + uid;
        }

        #region 【字符串非空验证】

        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="parms"></param>
        public static void ChekcNotNull(this string parms)
        {
            if (string.IsNullOrWhiteSpace(parms))
                throw new ArgumentNullException($"参数不能为空");
        }

        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="parms"></param>
        public static void ChekcNotNull(this string parms, string data)
        {
            if (string.IsNullOrWhiteSpace(parms))
                throw new ArgumentNullException($"参数{data}不能为空");
        }

        /// <summary>
        /// 检查集合是否为空
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="parms">参数</param>
        public static void CheckListNotNull<T>(this List<T> parms)
        {
            if (!parms.Any())
                throw new Exception($"集合不能为空引用或集合数量不能为0");
        }

        /// <summary>
        /// 检查集合是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parms"></param>
        public static bool CheckIEnumerable<T>(this IEnumerable<T> parms)
        {
            return parms.Any();
        }

        /// <summary>
        /// 检查GUID是否为空
        /// </summary>
        /// <param name="guid"></param>
        public static void CheckGuidNotNull(this Guid guid)
        {
            if (guid == Guid.Empty)
                throw new Exception($"GUID不能为空");
        }

        /// <summary>
        /// 检查字典集合是否为空
        /// </summary>
        /// <param name="parms"></param>
        public static bool CheckDictionaryNotNull(this Dictionary<string, object> parms)
        {
            return parms.Any();
        }

        /// <summary>
        /// 检查对象是否为空
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">对象</param>
        public static void CheckObjectNotNull<T>(this T data)
        {
            if (data == null)
                throw new Exception($"对象不能未空引用");
        }

        /// <summary>
        /// 检查是否为空,返回是：true 否：false
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckPamrsIsNotNull(this string data)
        {
            return string.IsNullOrWhiteSpace(data);
        }

        #endregion 【字符串非空验证】

        #region [字节与流的转换]

        /// <summary>
        /// Strem转字节
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToByte(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// 数据转字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToByte(this string data) => Encoding.UTF8.GetBytes(data);

        /// <summary>
        /// 转字节流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static MemoryStream ToStream(this byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            return ms;
        }

        #endregion [字节与流的转换]
    }
}