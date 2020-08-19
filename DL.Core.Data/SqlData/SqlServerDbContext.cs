using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using DL.Core.ns.Extensiton;
using System.Collections.Concurrent;
using System.Linq;
using DL.Core.Data.BaseData;
using DL.Core.utility.Logging;
using DL.Core.utility.Extendsition;

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
            return Insert(sql, type, parameter);
        }

        #region [增删改]

        private protected int Insert(string sql, CommandType type, params DbParameter[] parameter)
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
        public int InsertEntity<TEnttiy>(TEnttiy enttiy, string tableName) where TEnttiy : class
        {
            ConcurrentDictionary<string, object> pairs = new ConcurrentDictionary<string, object>();
            var props = enttiy.GetType().GetProperties();
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
                return Insert(sb.ToString(), CommandType.Text, list.ToArray());
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
        /// <returns></returns>
        public int InsertEntityItems<TEntity>(IEnumerable<TEntity> entities, string tableName, bool transation = false) where TEntity : class
        {
            try
            {
                DataTable dt = new DataTable();
                var flag = false;
                if (transation)
                    BeginTransation = transation;
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

        private IDbConnection DbContext()
        {
            IDbConnection db = new SqlConnection();
            return db;
        }
    }
}