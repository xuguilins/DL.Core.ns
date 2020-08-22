using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ns.Extensiton;
using DL.Core.Data;
using DL.Core.Data.SqlData;
using DL.Core.Data.Extendsition;
using DL.Core.utility.Entity;
using DL.Core.ns.EFCore;
using DL.Core.utility.Extendsition;
using System.Linq;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEngineDbContextPack<MyContext, UserContxt>();
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetServices<IUnitOfWork>().ToList();

            Console.ReadKey();
        }
    }

    public class UserTest : EntityBase
    {
    }

    public class MyContext : DbContextBase<MyContext>
    {
        public override string ConnectionString => "";

        public override void RegistConfiguration(ModelBuilder builder)
        {
            //IEntityTypeConfiguration
        }
    }

    public class UserContxt : DbContextBase<UserContxt>
    {
        public override string ConnectionString => "";

        public override void RegistConfiguration(ModelBuilder builder)
        {
        }
    }
}