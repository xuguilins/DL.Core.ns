using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using DL.Core.ns.Configer;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 操作SqlServer 数据库
    /// </summary>
    public class SqlServerDbContext : DataBaseContext, ISqlServerDbContext, IDisposable
    {
        private bool _beginTransaction = false;
        private string _connectString = string.Empty;
        private SqlTransaction _transaction = null;
        private SqlConnection _sqlConnection = null;
        private ISqlServerDbContext _sqlDbContext;
        public override DataBaseType Type => DataBaseType.SqlServer;
        public override IDbConnection GetDbContext => DbContext();

        public SqlServerDbContext()
        {
            _connectString = ConfigerManager.getCofiger()?.ConnectionString?.SqlDefault;
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
                _sqlConnection.Dispose();
            }
        }

        public override int ExecuteNonQuery(string sql, CommandType type, params DbParameter[] parameter)
        {
            if (_sqlConnection.State == ConnectionState.Open)
            {
                using (SqlCommand com = new SqlCommand(sql, _sqlConnection, _transaction))
                {
                    com.Parameters.AddRange(parameter);
                    return com.ExecuteNonQuery();
                }
            }
            else
            {
                return -1;
            }
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
            bool result = false;
            try
            {
                _transaction.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                _transaction.Rollback();
            }
            return result;
        }

        private IDbConnection DbContext()
        {
            return _sqlConnection;
        }
    }
}