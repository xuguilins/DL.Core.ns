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
}