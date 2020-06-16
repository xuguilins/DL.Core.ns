using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 操作MySql数据库
    /// </summary>
    public class MySqlDbContext : DataBaseContext, IMySqlDbContext
    {
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
    }
}