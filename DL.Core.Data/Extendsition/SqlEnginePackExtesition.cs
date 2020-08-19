using DL.Core.Data.BaseData;
using DL.Core.Data.MySqlData;
using DL.Core.Data.SqlData;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();
            services.AddScoped<IMySqlDbContext, MySqlDbContext>();
            services.AddScoped<IDataBaseDbContextManager, DataBaseDbContextManager>();
            return services;
        }
    }
}