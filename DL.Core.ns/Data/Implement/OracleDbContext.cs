using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using DL.Core.ns.Configer;
using DL.Core.ns.Logging;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 操作Oracle数据库
    /// 暂不支持！
    /// </summary>
    public class OracleDbContext : DataBaseContext, IOracleDbContext
    {
        public bool BeginTransation { get; set; }
        public override DataBaseType Type => DataBaseType.Oracle;

        //    private OracleConnection _connection = null;
        //    private string connectionStr = string.Empty;
        //    private OracleTransaction _transaction = null;
        //    private bool _isBeginTransaction = false;
        //    private ILogger logger = LogManager.GetLogger<OracleDbContext>();

        //    public OracleDbContext()
        //    {
        //        connectionStr = ConfigerManager.Instance.getCofiger()?.ConnectionString?.OracleDefault;
        //        _connection = new OracleConnection(connectionStr);
        //        _connection.Open();
        //    }

        //    public override DataBaseType Type => DataBaseType.Oracle;

        //    public override int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter)
        //    {
        //        int result = -1;
        //        try
        //        {
        //            if (_connection.State == ConnectionState.Open)
        //            {
        //                using (OracleCommand com = new OracleCommand(sql, _connection))
        //                {
        //                    com.CommandText = sql;
        //                    com.CommandType = type;
        //                    com.Parameters.AddRange(parameter);
        //                    result = com.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error($"执行Oracle-ExecuteNonQuery命令异常,exMes:{ex.Message}，strck:{ex.StackTrace}");
        //            throw;
        //        }
        //        finally
        //        {
        //            if (_connection != null && !_isBeginTransaction)
        //            {
        //                _connection.Close();
        //                _connection.Dispose();
        //            }
        //        }
        //        return result;
        //    }

        //    public override object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter)
        //    {
        //        object result = null;
        //        try
        //        {
        //            if (_connection.State == ConnectionState.Open)
        //            {
        //                using (OracleCommand com = new OracleCommand(sql, _connection))
        //                {
        //                    com.CommandText = sql;
        //                    com.CommandType = type;
        //                    com.Parameters.AddRange(parameter);
        //                    result = com.ExecuteScalar();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error($"执行Oracle-ExecuteScalar命令异常,exMes:{ex.Message}，strck:{ex.StackTrace}");
        //            throw;
        //        }
        //        finally
        //        {
        //            if (_connection != null && !_isBeginTransaction)
        //            {
        //                _connection.Close();
        //                _connection.Dispose();
        //            }
        //        }
        //        return result;
        //    }

        //    public override DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter)
        //    {
        //        DataSet result = new DataSet();
        //        try
        //        {
        //            if (_connection.State == ConnectionState.Open)
        //            {
        //                using (OracleCommand com = new OracleCommand(sql, _connection))
        //                {
        //                    com.CommandText = sql;
        //                    com.CommandType = type;
        //                    com.Parameters.AddRange(parameter);
        //                    using (OracleDataAdapter da = new OracleDataAdapter(com))
        //                    {
        //                        da.Fill(result);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error($"执行Oracle-GetDataSet命令异常,exMes:{ex.Message}，strck:{ex.StackTrace}");
        //            throw;
        //        }
        //        finally
        //        {
        //            if (_connection != null && !_isBeginTransaction)
        //            {
        //                _connection.Close();
        //                _connection.Dispose();
        //            }
        //        }
        //        return result;
        //    }

        //    public override DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter)
        //    {
        //        DataTable result = new DataTable();
        //        try
        //        {
        //            if (_connection.State == ConnectionState.Open)
        //            {
        //                using (OracleCommand com = new OracleCommand(sql, _connection))
        //                {
        //                    com.CommandText = sql;
        //                    com.CommandType = type;
        //                    com.Parameters.AddRange(parameter);
        //                    using (OracleDataAdapter da = new OracleDataAdapter(com))
        //                    {
        //                        da.Fill(result);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error($"执行Oracle-GetDataTable命令异常,exMes:{ex.Message}，strck:{ex.StackTrace}");
        //            throw;
        //        }
        //        finally
        //        {
        //            if (_connection != null && !_isBeginTransaction)
        //            {
        //                _connection.Close();
        //                _connection.Dispose();
        //            }
        //        }
        //        return result;
        //    }

        //    /// <summary>
        //    /// 是否允许开启事务
        //    /// </summary>
        //    public bool BeginTransation
        //    {
        //        get
        //        {
        //            return _isBeginTransaction;
        //        }
        //        set
        //        {
        //            if (value)
        //                _transaction = _connection.BeginTransaction();
        //            _isBeginTransaction = value;
        //        }
        //    }

        //    /// <summary>
        //    /// 事务保存更改
        //    /// </summary>
        //    /// <returns></returns>
        //    public bool SaveTransactionChange()
        //    {
        //        bool result = false;
        //        if (_isBeginTransaction)
        //        {
        //            try
        //            {
        //                _transaction.Commit();
        //                result = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                _transaction.Rollback();
        //                logger.Error($"Oracle-SaveTransactionChange事务提交发生异常,exMes:{ex.Message},Strack:{ex.StackTrace}");
        //                throw;
        //            }
        //            finally
        //            {
        //                if (_transaction != null)
        //                {
        //                    _transaction.Dispose();
        //                }
        //                if (_connection != null)
        //                {
        //                    _connection.Close();
        //                    _connection.Dispose();
        //                }
        //            }
        //        }
        //        return result;
        //    }
        //}
        public override int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter)
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter)
        {
            throw new NotImplementedException();
        }

        public override DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter)
        {
            throw new NotImplementedException();
        }

        public override DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter)
        {
            throw new NotImplementedException();
        }

        public bool SaveTransactionChange()
        {
            throw new NotImplementedException();
        }
    }
}