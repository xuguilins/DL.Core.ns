using DL.Core.ns.Configer;
using System;
using System.Linq;
using DL.Core.ns.Data;
using DL.Core.ns.Table;
using System.Data;
using System.Collections.Generic;
using DL.Core.ns.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ns.Extensiton;
using DL.Core.ns.EFCore;
using DL.Core.ns.Dependency;
using DL.Core.ns.Locator;
using DL.Core.ns.Cacheing;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace 徐测试控制台
{
    internal class Program
    {
        private static ConcurrentQueue<string> quee = new ConcurrentQueue<string>();

        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<ISqlServerDbContext>();
            service.CreateDbConnection("Data Source=.;Initial Catalog=ChatEngine;User ID=sa;Password=0103");
            var sql = string.Format("INSERT INTO ChatUser(Id,CreatedTime,UserId,TargetId,TargetName,ConnectionId,IsRead)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", StrExtensition.GetGuid(), StrExtensition.GetDateTime(), "ds", "sdf", "sdf", "ds", "2");
            service.ExecuteNonQuery(sql, CommandType.Text);
            service.CreateDbConnection("Data Source=.;Initial Catalog=ChatEngine;User ID=sa;Password=0103");
            sql = string.Format("INSERT INTO ChatUser(Id,CreatedTime,UserId,TargetId,TargetName,ConnectionId,IsRead)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", StrExtensition.GetGuid(), StrExtensition.GetDateTime(), "ds", "sdf", "sdf", "ds", "2");
            service.ExecuteNonQuery(sql, CommandType.Text);
            service.Dispose();
            // service.CreateDbConnection("Data Source=.;Initial Catalog=ChatEngine;User ID=sa;Password=0103");
            Console.ReadKey();
        }
    }

    public class MyContext : DbContext
    {
        public MyContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var model = ConfigerManager.Instance.getCofiger().ConnectionString.SqlDefault;
            optionsBuilder.UseSqlServer(model);
        }

        public DbSet<TestUserInfo> TestUser { get; set; }
    }

    public class TestUserInfo : EntityBase
    {
        public string UserName { get; set; }
    }

    public class TestUserInfoConfiguratioin : IEntityTypeConfiguration<TestUserInfo>
    {
        public void Configure(EntityTypeBuilder<TestUserInfo> builder)
        {
            builder.ToTable("TestUserInfO").HasKey(P => P.Id);
        }
    }

    public interface ITestUserInfoService : IRepository<TestUserInfo>, IScopeDependcy
    {
    }

    //[AttbuiteDependency(ServiceLifetime.Scoped)]
    public class TestUserInfoService : Repository<TestUserInfo>, ITestUserInfoService
    {
        public TestUserInfoService(MyContext context) : base(context)
        {
        }
    }
}