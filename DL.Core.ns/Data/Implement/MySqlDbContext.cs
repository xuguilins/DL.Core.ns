using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using DL.Core.ns.Configer;
using DL.Core.ns.Logging;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 操作MySql数据库
    /// </summary>
    public class MySqlDbContext : DataBaseContext, IMySqlDbContext
    {
        private MySqlConnection _connection = null;
        private string connectionStr = string.Empty;
        private MySqlTransaction _transaction = null;
        private bool _isBeginTranscation = false;
        private ILogger logger = LogManager.GetLogger<MySqlDbContext>();

        public MySqlDbContext()
        {
        }

        public override DataBaseType Type => DataBaseType.MySql;
        public override IDbConnection GetDbContext => new MySqlConnection(connectionStr);

        public bool BeginTransation
        {
            get
            {
                return _isBeginTranscation;
            }
            set
            {
                if (value)
                    _transaction = _connection.BeginTransaction();
                _isBeginTranscation = value;
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public override int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter)
        {
            int result = -1;
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    using (MySqlCommand com = new MySqlCommand(sql, _connection, _transaction))
                    {
                        com.CommandType = type;
                        com.Parameters.AddRange(parameter);
                        result = com.ExecuteNonQuery();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error($"执行MySql发生异常,command：ExecuteNonQuery\r\nsqlText:{sql}\r\nexMes:{ex.Message}");
                throw;
            }
        }

        public override object ExecuteScalar(string sql, CommandType type, params DbParameter[] parameter)
        {
            object result = null;
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    using (MySqlCommand com = new MySqlCommand(sql, _connection, _transaction))
                    {
                        com.CommandType = type;
                        com.Parameters.AddRange(parameter);
                        result = com.ExecuteScalar();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error($"执行MySql发生异常,command：ExecuteScalar，sqlText:{sql}，exMes:{ex.Message}");
                throw;
            }
        }

        public override DataSet GetDataSet(string sql, CommandType type, params DbParameter[] parameter)
        {
            DataSet result = null;
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    using (MySqlCommand com = new MySqlCommand(sql, _connection, _transaction))
                    {
                        com.CommandType = type;
                        com.Parameters.AddRange(parameter);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(com))
                        {
                            result = new DataSet();
                            da.Fill(result);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error($"执行MySql发生异常,command：GetDataSet，sqlText:{sql}，exMes:{ex.Message}");
                throw;
            }
        }

        public override DataTable GetDataTable(string sql, CommandType type, params DbParameter[] parameter)
        {
            DataTable result = null;
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    using (MySqlCommand com = new MySqlCommand(sql, _connection, _transaction))
                    {
                        com.CommandType = type;
                        com.Parameters.AddRange(parameter);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(com))
                        {
                            result = new DataTable();
                            da.Fill(result);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error($"执行MySql发生异常,command：DataTable，sqlText:{sql}，exMes:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 创建数据连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public MySqlConnection CreateDbConnection(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            return _connection;
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
                    logger.Error($"执行MySql事务提交发生异常，command:SaveTransactionChange，exMes:{ex.Message}");
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
    }
}