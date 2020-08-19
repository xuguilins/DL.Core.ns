using DL.Core.Data.BaseData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.Data.MySqlData
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

        /// <summary>
        /// 创建数据连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        MySqlConnection CreateDbConnection(string connectionString);
    }
}