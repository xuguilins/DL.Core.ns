using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 操作MySql数据库接口
    /// </summary>
    public interface IMySqlDbContext : IDataBaseContext, IDisposable
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