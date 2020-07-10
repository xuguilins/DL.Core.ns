using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace DL.Core.ns.Extensiton
{
    /// <summary>
    /// 针对枚举的扩展
    /// </summary>
    public static class EnumExtensiton
    {
        /// <summary>
        /// 获取当前枚举值的描述
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attbuite = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attbuite == null)
                throw new ArgumentException("当前枚举类型未标记“Description”特性", $"{value.ToString()}");
            return attbuite?.Description;
        }

        /// <summary>
        /// 获取当前枚举值的描述和名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDescripttionAndName(this Enum value)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attbuite = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attbuite == null)
                throw new ArgumentException("当前枚举类型未标记“Description”特性", $"{value.ToString()}");
            pairs.Add(value.ToString(), attbuite.Description);
            return pairs;
        }

        /// <summary>
        /// 获取当前指定的枚举的值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static int GetEnumeValue(this Enum @enum)
        {
            return Convert.ToInt32(@enum);
        }

        /// <summary>
        /// 获取当前枚举值的基本信息
        /// </summary>
        /// <param name="enum">当前枚举值</param>
        /// <returns></returns>
        public static EnumeModel GetEnumModel(this Enum @enum)
        {
            EnumeModel info = new EnumeModel();
            info.EnumeText = @enum.ToString();
            info.EnumeValue = Convert.ToInt32(@enum);
            info.EnumeDesc = GetDescription(@enum);
            return info;
        }

        /// <summary>
        /// 获取当前枚举的文本
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetEmumText(this Enum @enum)
        {
            return @enum.ToString();
        }
    }

    public class EnumeModel
    {
        public string EnumeText { get; set; }
        public int EnumeValue { get; set; }
        public string EnumeDesc { get; set; }
    }
}