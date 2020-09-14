using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Concurrent;
using System.Linq;
using DL.Core.Data.BaseData;
using DL.Core.utility.Logging;
using DL.Core.utility.Extendsition;
using DL.Core.utility.Entity;
using MySqlX.XDevAPI.Relational;
using System.Linq.Expressions;

namespace DL.Core.Data.SqlData
{
    /// <summary>
    /// 操作SqlServer 数据库
    /// </summary>
    public class SqlServerDbContext : DataBaseContext, ISqlServerDbContext
    {
        private bool _beginTransaction = false;
        private string _connectString = string.Empty;
        private static SqlTransaction _transaction = null;
        private static SqlConnection _sqlConnection = null;
        private static ILogger logger = LogManager.GetLogger<SqlServerDbContext>();
        private static ConcurrentDictionary<string, SqlConnection> pairs = new ConcurrentDictionary<string, SqlConnection>();

        public SqlServerDbContext()
        {
        }

        public bool BeginTransation
        {
            get
            {
                return _beginTransaction;
            }
            set
            {
                if (value)
                {
                    if (_transaction == null)
                        _transaction = _sqlConnection.BeginTransaction();
                }
                _beginTransaction = value;
            }
        }

        public void Dispose()
        {
            if (_sqlConnection != null)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
            }
        }

        //public override IDbConnection GetDbContext { get;  }
        public override IDbConnection GetDbContext { get; set; }

        public override DataBaseType Type => DataBaseType.SqlServer;

        public override int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter)
        {
            return ExecuteSqlServer(sql, type, parameter);
        }

        #region [增删改]

        private protected int ExecuteSqlServer(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    using (SqlCommand com = new SqlCommand(sql, _sqlConnection, _transaction))
                    {
                        com.CommandType = type;
                        if (parameter != null)
                            com.Parameters.AddRange(parameter);
                        return com.ExecuteNonQuery();
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"执行SqlServer发生异常,command:ExecuteNonQuery,sqlText:{sql},exMes:{ex.Message} ");
                throw;
            }
        }

        #endregion [增删改]

        public override object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    using (SqlCommand com = new SqlCommand(sql, _sqlConnection, _transaction))
                    {
                        com.CommandType = type;
                        com.Parameters.AddRange(parameter);
                        return com.ExecuteScalar();
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"执行SqlServer发生异常,command:ExecuteScalar,sqlText:{sql},exMes:{ex.Message} ");
                throw;
            }
        }

        public override DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    DataSet ds = new DataSet();
                    using (SqlCommand com = new SqlCommand(sql, _sqlConnection, _transaction))
                    {
                        com.CommandType = type;
                        com.Parameters.AddRange(parameter);
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(ds);
                        }
                    }
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"执行SqlServer发生异常,command:GetDataSet,sqlText:{sql},exMes:{ex.Message} ");
                throw;
            }
        }

        public override DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter)
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    DataTable ds = new DataTable();
                    using (SqlCommand com = new SqlCommand(sql, _sqlConnection, _transaction))
                    {
                        com.CommandType = type;
                        com.Parameters.AddRange(parameter);
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(ds);
                        }
                    }
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"执行SqlServer发生异常,command:GetDataTable,sqlText:{sql},exMes:{ex.Message} ");
                throw;
            }
        }

        public bool SaveTransactionChange()
        {
            try
            {
                bool result = false;
                try
                {
                    _transaction.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    logger.Error($"执行SqlServer事务发生异常,\r\n command:SaveTransactionChange \r\n exMes:{ex.Message} ");
                    result = false;
                    _transaction.Rollback();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建数据连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public SqlConnection CreateDbConnection(string connectionString)
        {
            if (pairs.ContainsKey(connectionString))
            {
                GetDbContext = pairs[connectionString];
                _connectString = connectionString;
                return pairs[connectionString];
            }
            else
            {
                _sqlConnection = new SqlConnection(connectionString);
                _sqlConnection.Open();
                pairs.TryAdd(connectionString, _sqlConnection);
                GetDbContext = _sqlConnection;
                _connectString = connectionString;
                return _sqlConnection;
            }
        }

        /// <summary>
        /// 参数化匿名对象获取数据表格
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="type">SQL语句类型</param>
        /// <param name="parameter">匿名对象 new { }</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, CommandType type, object parameter)
        {
            string newSql = sql;
            if (parameter != null)
            {
                var dictionary = parameter.ToDictionary();
                foreach (var item in dictionary)
                {
                    newSql = sql.Replace($"@{item.Key}", $"'{item.Value}'");
                }
            }
            try
            {
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    DataTable ds = new DataTable();
                    using (SqlCommand com = new SqlCommand(sql, _sqlConnection, _transaction))
                    {
                        com.CommandType = type;
                        using (SqlDataAdapter da = new SqlDataAdapter(com))
                        {
                            da.Fill(ds);
                        }
                    }
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"执行SqlServer发生异常,command:参数化匿名对象获取数据表格 GetDataTable,sqlText:{sql},exMes:{ex.Message} ");
                throw;
            }
        }

        /// <summary>
        /// 实体类写入数据库
        /// <see cref="enttiy">实体类结构字段必须与数据库保持一致</see>
        /// </summary>
        /// <typeparam name="TEnttiy"></typeparam>
        /// <param name="enttiy">实体类</param>
        /// <returns></returns>
        public int InsertEntity<TEnttiy>(TEnttiy enttiy, string tableName = null) where TEnttiy : class
        {
            ConcurrentDictionary<string, object> pairs = new ConcurrentDictionary<string, object>();
            var props = enttiy.GetType().GetProperties();
            if (tableName == null)
                tableName = GetTableNameType(enttiy.GetType());
            foreach (var item in props)
            {
                if (pairs.ContainsKey(item.Name))
                    throw new Exception($"包含重复的属性名,写入失败");
                var propName = item.Name;
                var itemValue = item.GetValue(enttiy, null);
                pairs.TryAdd(propName, itemValue);
            }
            if (pairs.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                var keys = string.Join(",", pairs.Select(x => x.Key));
                var parmas = string.Join(",", pairs.Select(x => "@" + x.Key + ""));
                sb.Append($"INSERT INTO {tableName} ({keys}) VALUES ({parmas})");
                List<SqlParameter> list = new List<SqlParameter>();
                foreach (var item in pairs)
                {
                    list.Add(new SqlParameter($"@{item.Key}", item.Value));
                }
                pairs.Clear();
                return ExecuteSqlServer(sb.ToString(), CommandType.Text, list.ToArray());
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 批量实体写入数据库
        ///<see cref="entities">实体类结构字段必须与数据库保持一致</see>
        /// </summary>
        /// <typeparam name="TEntity">s</typeparam>
        /// <param name="entities">实体集合</param>
        /// <param name="transation">是否开启事务</param>
        /// <param name="tableName">实际表名称</param>
        /// <returns></returns>
        public int InsertEntityItems<TEntity>(IEnumerable<TEntity> entities, string tableName = null, bool transation = false) where TEntity : class
        {
            try
            {
                DataTable dt = new DataTable();
                var flag = false;
                if (transation)
                    BeginTransation = transation;
                if (tableName == null)
                {
                    var entity = entities.FirstOrDefault();
                    tableName = GetTableNameType(entity.GetType());
                }
                using (SqlBulkCopy bluk = new SqlBulkCopy(_sqlConnection, SqlBulkCopyOptions.Default, _transaction))
                {
                    bluk.DestinationTableName = tableName;
                    foreach (var entity in entities)
                    {
                        var property = entity.GetType().GetProperties();
                        if (!flag)
                        {
                            foreach (var perty in property)
                            {
                                dt.Columns.Add(perty.Name);
                                var sqlqMapper = new SqlBulkCopyColumnMapping(perty.Name, perty.Name);
                                bluk.ColumnMappings.Add(sqlqMapper);
                            }
                            flag = true;
                        }
                        DataRow row = dt.NewRow();
                        foreach (var perty in property)
                        {
                            var value = perty.GetValue(entity, null);
                            row[perty.Name] = perty.GetValue(entity, null);
                        }
                        dt.Rows.Add(row);
                    }

                    bluk.WriteToServer(dt);
                    if (_transaction != null && transation)
                        _transaction.Commit();
                }
                return 1;
            }
            catch (Exception ex)
            {
                logger.Error($"批量写入SqlServer数据库异常：{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="enttiy">实体信息</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        public int DeleteEntity<TEntity>(TEntity enttiy, string tableName = null) where TEntity : EntityBase
        {
            var propInfo = enttiy.GetType().GetProperty("Id");
            var primaryKey = propInfo.GetValue(enttiy, null);
            if (primaryKey == null)
                throw new Exception("获取指定属性的值异常");
            if (tableName == null)
            {
                var type = enttiy.GetType();
                tableName = GetTableNameType(type);
            }
            var DelSql = $"DELETE FROM {tableName} WHERE Id=@Id";
            SqlParameter ps = new SqlParameter("@Id", primaryKey.ToString());
            return ExecuteSqlServer(DelSql, CommandType.Text, ps);
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities">实体集合</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        public int DeleteEntityItems<TEntity>(IEnumerable<TEntity> entities, string tableName = null) where TEntity : EntityBase
        {
            string primaryKeys = string.Empty;
            foreach (var item in entities)
            {
                var props = item.GetType().GetProperty("Id");
                var value = props.GetValue(item, null);
                if (value != null)
                    primaryKeys = string.Join(",", $"'{value}'");
            }
            var entity = entities.FirstOrDefault();
            if (tableName == null)
                tableName = GetTableNameType(entity.GetType());
            var DelSql = $"DELETE FROM {tableName} WHERE Id IN ({primaryKeys})";
            return ExecuteSqlServer(DelSql, CommandType.Text);
        }

        private IDbConnection DbContext()
        {
            IDbConnection db = new SqlConnection();
            return db;
        }

        private string GetTableNameType(Type type)
        {
            string tableName = string.Empty;
            var attinfo = type.GetCustomAttributes(typeof(TableAttubite), false);
            if (attinfo != null && attinfo.Length > 0)
            {
                var info = attinfo[0] as TableAttubite;
                tableName = info.TableName;
            }
            else
            {
                tableName = type.Name;
            }
            return tableName;
        }
    }
}