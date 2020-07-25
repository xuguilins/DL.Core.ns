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
using DL.Core.ns.Web;
using DL.Core.ns.Tools;
using System.IO;

namespace 徐测试控制台
{
    internal class Program
    {
        private static ConcurrentQueue<string> quee = new ConcurrentQueue<string>();

        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddPack<MyContext>();
            var service = ServiceLocator.Instance.GetService<ITestUserInfoService>();
            service.Create();

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

    public interface ITestUserInfoService //: IScopeDependcy
    {
        void Create();
    }

    //[AttbuiteDependency(ServiceLifetime.Scoped)]
    [AttbuiteDependency(ServiceLifetime.Transient)]
    public class TestUserInfoService : ITestUserInfoService
    {
        private IRepository<TestUserInfo> userinfosevice;

        public TestUserInfoService(IRepository<TestUserInfo> repository)
        {
            userinfosevice = repository;
        }

        public void Create()
        {
            userinfosevice.AddEntity(new TestUserInfo { UserName = "aaaa" });
        }
    }
}