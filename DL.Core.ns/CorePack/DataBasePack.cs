using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ns.Data;

namespace DL.Core.ns.CorePack
{
    public class DataBasePack : PackBase
    {
        public override int StarLevel => 100;

        public override IServiceCollection AddService(IServiceCollection services)
        {
            services.AddScoped<IMySqlDbContext, MySqlDbContext>();
            services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();
            services.AddScoped<IOracleDbContext, OracleDbContext>();
            services.AddScoped<IDataBaseDbContextManager, DataBaseDbContextManager>();
            return services;
        }
    }
}