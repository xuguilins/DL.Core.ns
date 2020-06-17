using DL.Core.ns.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using DL.Core.ns.Extensiton;

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
    }
}