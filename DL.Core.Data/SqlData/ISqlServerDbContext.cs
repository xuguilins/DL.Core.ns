using DL.Core.Data.BaseData;
using DL.Core.utility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DL.Core.Data.SqlData
{
    /// <summary>
    /// SqlServer操作接口
    /// </summary>
    public interface ISqlServerDbContext : IDataBaseContext
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

        /// <summary>
        /// 实体类写入数据库
        /// <see cref="enttiy">实体类结构字段必须与数据库保持一致</see>
        /// </summary>
        /// <typeparam name="TEnttiy"></typeparam>
        /// <param name="enttiy">实体类</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        int InsertEntity<TEnttiy>(TEnttiy enttiy, string tableName = null) where TEnttiy : class;

        /// <summary>
        /// 批量实体写入数据库
        ///<see cref="entities">实体类结构字段必须与数据库保持一致</see>
        /// </summary>
        /// <typeparam name="TEntity">s</typeparam>
        /// <param name="entities">实体集合</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="transation">是否使用事务</param>
        /// <returns></returns>
        int InsertEntityItems<TEntity>(IEnumerable<TEntity> entities, string tableName = null, bool transation = false) where TEntity : class;

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="enttiy">实体信息</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        int DeleteEntity<TEntity>(TEntity enttiy, string tableName = null) where TEntity : EntityBase;

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities">实体集合</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        int DeleteEntityItems<TEntity>(IEnumerable<TEntity> entities, string tableName = null) where TEntity : EntityBase;
    }
}