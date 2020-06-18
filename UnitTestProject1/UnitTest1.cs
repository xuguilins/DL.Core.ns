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
using DL.Core.ns.EventBus;

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
        public void EventTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IEventBus, EventBus>();
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<IEventBus>();
            service.Publish(new LoginData());
        }
    }

    public class User : EventData
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Pass { get; set; }
    }
}