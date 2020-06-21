using DL.Core.ns.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using DL.Core.ns.Extensiton;
using System.Collections.Generic;
using DL.Core.ns.Table;
using System.Data;
using DL.Core.ns.EventBusHandler;
using DL.Core.ns.CommandFactory;
using Microsoft.EntityFrameworkCore;
using DL.Core.ns.Dependency;
using DL.Core.ns.Finder;
using System.Linq;
using DL.Core.ns.CorePack;
using DL.Core.ns.Configer;
using DL.Core.ns.EFCore;
using DL.Core.ns.Entity;
using DL.Core.ns.Tools;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Insert()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IMySqlDbContext, MySqlDbContext>();
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<IMySqlDbContext>();

            //service.BeginTransation = true;
            var sql = "INSERT INTO userdata(ID,Name,Age,CreateTime)values(@ID,@Name,@Age,@CreateTime)";
            MySqlParameter[] ps =
            {
                new MySqlParameter("@ID",StrExtensition.GetGuid()),
                new MySqlParameter("@Name","11"),
                new MySqlParameter("@Aged",5),
                new MySqlParameter("@CreateTime",StrExtensition.GetDateTime())
            };
            var c = service.GetDbContext;
            service.ExecuteNonQuery(sql, System.Data.CommandType.Text, ps);
            var usql = "UPDATE userdata SET NAME='666fffddddf',Age=50,CreateTime='" + StrExtensition.GetDateTime() + "' where ID='91f651004a5849b48aed6df60d474a3c'";
            service.ExecuteNonQuery(usql, System.Data.CommandType.Text);
            //Assert.IsTrue(true);
            //service.SaveTransactionChange();
        }

        [TestMethod]
        public void CheckObject()
        {
            User list = new User
            {
                Name = "sdf",
                Age = "11",
                Pass = "sd"
            };
            var table = list.ToTable();
            foreach (DataRow row in table.Rows)
            {
                var d = row["Name"];
            }
        }

        [TestMethod]
        public void CommmandTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<ICommandExecutor, CommandExecutor>();
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<ICommandExecutor>();
            service.Execute(new UserLoginCommand());
        }

        [TestMethod]
        public void DbContextMethod()
        {
            //IDbContext
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IFinderBase, IntertalContextFinder>();
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<IFinderBase>();
            var type = service.FinderAll();
            //  var context = Activator.CreateInstance(type) as TestDbContext;
        }

        [TestMethod]
        public void DependcyPackLoad()
        {
            ValidateCodeHelper.CreateValidteCode(5, true);
            //PackBase p = new DependencyPack();
            //IServiceCollection services = new ServiceCollection();
            //services.AddDbContext<TestDbContext>();
            //services.AddScoped<IUnitOfWork, UnitOfWork<TestDbContext>>();
            //services.AddPack();
            //IServiceProvider provider = services.BuildServiceProvider();
            //var service = provider.GetService<IChatUserService>();
            //var uni = provider.GetService<IUnitOfWork>();
            //var sqlservice = provider.GetService<ISqlServerDbContext>();
            //sqlservice.ExecuteNonQuery("sd", CommandType.Text);
            //uni.BeginTransaction = true;
            //var data = service.AddEntity(new ChatUser { ConnectionId = StrExtensition.GetGuid(), CreatedTime = StrExtensition.GetDateTime(), Id = StrExtensition.GetGuid(), IsRead = 0, TargetId = "11", TargetName = "老王", UserId = "zzlls" });
            //var a = data;
            //uni.CommitTransaction();
            // p.AddService(services);
        }
    }

    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ConfigerManager.getCofiger();
            if (config != null)
            {
                var sql = config.ConnectionString.Default;
                optionsBuilder.UseSqlServer(sql);
            }
        }

        public DbSet<ChatUser> ChatUser { get; set; }
    }

    public interface IChatUserService : IRepository<ChatUser>, IScopeDependcy
    {
    }

    public class ChatUserService : Repository<ChatUser>, IChatUserService
    {
        //  private TestDbContext context = null;

        public ChatUserService(TestDbContext context) : base(context)
        {
        }
    }
}

public class ChatUser : EntityBase
{
    /// <summary>
    ///
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int IsRead { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string TargetId { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string TargetName { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string UserId { get; set; }
}

public class User
{
    public string Name { get; set; }
    public string Age { get; set; }
    public string Pass { get; set; }
}

public class UserLoginCommand : ICommand<User>
{
    public User Execute()
    {
        return new User { Age = "100", Name = "命令执行者", Pass = "执行命令" };
    }
}