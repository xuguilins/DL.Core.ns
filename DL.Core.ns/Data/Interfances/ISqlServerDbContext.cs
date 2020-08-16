using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// SqlServer操作接口
    /// </summary>
    public interface ISqlServerDbContext : IDataBaseContext, IDisposable
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
        SqlConnection CreateDbConnection(string connectionString);

        /// <summary>
        /// 参数化匿名对象获取数据表格
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="type">SQL语句类型</param>
        /// <param name="parameter">匿名对象 new { }</param>
        /// <returns></returns>
        DataTable GetDataTable(string sql, CommandType type, object parameter);
    }
}