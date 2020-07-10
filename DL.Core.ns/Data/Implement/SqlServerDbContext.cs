using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using DL.Core.ns.Configer;
using DL.Core.ns.Logging;
using DL.Core.ns.Extensiton;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 操作SqlServer 数据库
    /// </summary>
    public class SqlServerDbContext : DataBaseContext, ISqlServerDbContext
    {
        private bool _beginTransaction = false;
        private string _connectString = string.Empty;
        private SqlTransaction _transaction = null;
        private SqlConnection _sqlConnection = null;
        private ILogger logger = LogManager.GetLogger<SqlServerDbContext>();

        public SqlServerDbContext()
        {
            _connectString = ConfigerManager.Instance.getCofiger()?.ConnectionString?.SqlDefault;
            _connectString.ChekcNotNull();
            _sqlConnection = new SqlConnection(_connectString);
            _sqlConnection.Open();
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
                    _transaction = _sqlConnection.BeginTransaction();
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

        public override IDbConnection GetDbContext => new SqlConnection(_connectString);
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
            finally
            {
                if (_sqlConnection != null && !_beginTransaction)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }
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
            finally
            {
                if (_sqlConnection != null && !_beginTransaction)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }
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
            finally
            {
                if (_sqlConnection != null && !_beginTransaction)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }
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
            finally
            {
                if (_sqlConnection != null && !_beginTransaction)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }
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
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }
            }
        }

        private IDbConnection DbContext()
        {
            IDbConnection db = new SqlConnection();
            return db;
        }
    }
}