using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.utility.Entity
{
    public class DataSearch
    {
        /// <summary>
        /// 当前页码值
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 排序方式
        /// 升序：Asc
        /// 降序：Desc
        /// </summary>
        public string SortAsc { get; set; }
    }
}