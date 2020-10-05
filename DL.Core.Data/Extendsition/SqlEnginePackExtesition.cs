using DL.Core.Data.BaseData;
using DL.Core.Data.MySqlData;
using DL.Core.Data.SqlData;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.utility.Configer;
using DL.Core.Data.InitDatabase;
using DL.Core.utility.Logging;

namespace DL.Core.Data.Extendsition
{
    public static class SqlEnginePackExtesition
    {
        private static ILogger logger = LogManager.GetLogger();

        /// <summary>
        /// 添加SQL语句操作数据的上下文注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlEnginePack(this IServiceCollection services)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append($"初始化SQL引擎实例 \r\n");
                services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();
                services.AddScoped<IMySqlDbContext, MySqlDbContext>();
                services.AddScoped<IDataBaseDbContextManager, DataBaseDbContextManager>();
                sb.Append($"正在读取配置文件 \r\n");
                //自动构建表
                var dbconfig = ConfigerManager.Instance.Configuration.GetDLDbSetting();
                if (dbconfig.AutoAdoNetMiagraionEnable)
                {
                    sb.Append($"已启动ADO.NET自动迁移，Auto【{dbconfig.AutoAdoNetMiagraionEnable}】");
                    ISqlServerDbContext service = new SqlServerDbContext();
                    var conn = ConfigerManager.Instance.Configuration.GetConStrSetting();
                    sb.Append($"正在创建SQL数据库连接。。 \r\n");
                    var context = service.CreateDbConnection(conn.Default ?? conn.SqlDefault);
                    sb.Append($"正在初始化数据表。。。。\r\n");
                    service.CreateSqlServerTable();
                    service.Dispose();
                    sb.Append($"迁移成功。。。\r\n");
                }
                logger.Info($"ADO.NET数据表迁移完毕");
            }
            catch (Exception ex)
            {
                logger.Error($"ADO.NET迁移数据表发生异常，异常信息:{ex.Message},stack：{ex.StackTrace}");
                throw ex;
            }

            return services;
        }
    }
}