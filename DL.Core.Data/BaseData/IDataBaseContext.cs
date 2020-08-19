using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DL.Core.Data.BaseData
{
    /// <summary>
    /// 基类接口
    /// </summary>
    public interface IDataBaseContext
    {
        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        IDbConnection GetDbContext { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        DataBaseType Type { get; }

        /// <summary>
        /// 返回受影响的行数
        /// 用于增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>返回受影响的行数</returns>
        int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter);

        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>数据表格</returns>
        DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter);

        /// <summary>
        /// 获取数据内存表格
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>内存数据表</returns>
        DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter);

        /// <summary>
        /// 获取数据对象
        /// </summary>
        /// <param name="sql">读取数据</param>
        /// <param name="type">执行类型</param>
        /// <param name="parameter">执行参数</param>
        /// <returns>数据对象</returns>
        object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter);
    }
}