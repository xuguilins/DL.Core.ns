using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DL.Core.ns.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ns.Extensiton;
using DL.Core.Data;
using DL.Core.utility.Locator;
using DL.Core.Data.SqlData;
using DL.Core.Data.Extendsition;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEngineDbContextPack<MyContext>();

            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<ISqlServerDbContext>();
            var x = service.ExecuteNonQuery("", CommandType.Text);
            //IServiceCollection services = new ServiceCollection();
            //ISqlServerDbContext context = new SqlServerDbContext();
            //context.CreateDbConnection("Data Source=.;Initial Catalog=CoreNs;User ID=sa;Password=0103");
            //List<TestUserInfo> list = new List<TestUserInfo>();
            //for (int i = 0; i < 100; i++)
            //{
            //    var info = new TestUserInfo { CreatedTime = DateTime.Now, UsePass = "sdfsdfsd" + i, UserName = "新的数据赏上下文" + i };
            //    list.Add(info);
            //}

            //context.InsertEntityItems(list, "UserTest", false);

            Console.ReadKey();
        }
    }

    public class MyContext : DbContextBase<MyContext>
    {
        public override string ConnectionString => "";

        public override void RegistConfiguration(ModelBuilder builder)
        {
        }
    }
}