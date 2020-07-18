using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using DL.Core.ns.Configer;
using DL.Core.ns.Logging;
using DL.Core.ns.Extensiton;
using System.Collections.Concurrent;

namespace DL.Core.ns.Data
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
            try
            {
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    using (SqlCommand com = new SqlCommand(sql, _sqlConnection, _transaction))
                    {
                        com.CommandType = type;
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
                return pairs[connectionString];
            }
            else
            {
                _sqlConnection = new SqlConnection(connectionString);
                _sqlConnection.Open();
                pairs.TryAdd(connectionString, _sqlConnection);
                GetDbContext = _sqlConnection;
                return _sqlConnection;
            }
        }

        private IDbConnection DbContext()
        {
            IDbConnection db = new SqlConnection();
            return db;
        }
    }
}