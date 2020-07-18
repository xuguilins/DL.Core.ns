using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 数据库上下文对象
    /// <see cref="DataBaseContext">操作对象都继承依赖于此</see>
    /// </summary>
    public abstract class DataBaseContext : IDataBaseContext
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public virtual DataBaseType Type { get; }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        public virtual IDbConnection GetDbContext { get; set; }

        /// <summary>
        /// 返回受影响的行数
        /// 用于增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>返回受影响的行数</returns>
        public abstract int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter);

        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>数据表格</returns>
        public abstract DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter);

        /// <summary>
        /// 获取数据内存表格
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>内存数据表</returns>
        public abstract DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter);

        /// <summary>
        /// 获取数据对象
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>数据对象</returns>
        public abstract object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter);
    }
}