using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.Data
{
    /// <summary>
    /// 实体映射表标记，指定表名
    /// </summary>
    public class TableAttubite : Attribute
    {
        public string TableName { get; private set; }

        public TableAttubite(string tableName)
        {
            TableName = tableName;
        }
    }

    /// <summary>
    /// 字段指定长度
    /// </summary>
    public class PropAttbilteLength : Attribute
    {
        public string PropLength { get; set; }

        /// <summary>
        /// 字段长度
        /// int类型，如：PropAttbilteLength（“15”） //表示长度15
        /// decail 如：PropAttbilteLength（“18，2”）
        /// </summary>
        /// <param name="length"></param>
        public PropAttbilteLength(string length)
        {
            PropLength = length;
        }
    }
}