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
            PackBase p = new DependencyPack();
            IServiceCollection services = new ServiceCollection();
            services.AddPack();
            // p.AddService(services);
        }
    }

    public class TestDbContext : DbContext
    {
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
            return new User { Age = "100", Name = "√¸¡Ó÷¥––’ﬂ", Pass = "÷¥––√¸¡Ó" };
        }
    }
}