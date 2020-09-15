using DL.Core.Data.BaseData;
using DL.Core.Data.MySqlData;
using DL.Core.Data.SqlData;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.utility.Configer;
using DL.Core.Data.InitDatabase;

namespace DL.Core.Data.Extendsition
{
    public static class SqlEnginePackExtesition
    {
        /// <summary>
        /// 添加SQL语句操作数据的上下文注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlEnginePack(this IServiceCollection services)
        {
            try
            {
                services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();
                services.AddScoped<IMySqlDbContext, MySqlDbContext>();
                services.AddScoped<IDataBaseDbContextManager, DataBaseDbContextManager>();
                //自动构建表
                var dbconfig = ConfigerManager.Instance.Configuration.GetDbSetting();
                if (dbconfig.AutoAdoNetMiagraionEnable)
                {
                    ISqlServerDbContext service = new SqlServerDbContext();
                    var conn = ConfigerManager.Instance.Configuration.GetConStrSetting();
                    var context = service.CreateDbConnection(conn.Default ?? conn.SqlDefault);
                    service.CreateSqlServerTable();
                    service.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return services;
        }
    }
}