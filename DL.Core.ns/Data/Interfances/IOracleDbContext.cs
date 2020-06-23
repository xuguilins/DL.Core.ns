using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// Oracle数据库操作接口
    /// </summary>
    public interface IOracleDbContext : IDataBaseContext
    {
        /// <summary>
        /// 是否允许开启事务
        /// </summary>
        bool BeginTransation { get; set; }

        /// <summary>
        /// 事务保存更改
        /// </summary>
        /// <returns></returns>
        bool SaveTransactionChange();
    }
}