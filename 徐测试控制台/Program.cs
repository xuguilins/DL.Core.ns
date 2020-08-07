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
using DL.Core.ns.Finder;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.EnableMigration(true);
            services.AddPack<MyContext>();
            var service = ServiceLocator.Instance.GetService<IUserService>();
            service.AddTeacher();
            Console.ReadKey();
        }
    }

    public interface IUserService : IScopeDependcy
    {
        void AddTeacher();
    }

    public class UserService : IUserService
    {
        private IRepository<TeacherInfo> TeachRepository;

        public UserService(IRepository<TeacherInfo> repository)
        {
            TeachRepository = repository;
        }

        public void AddTeacher()
        {
            TeachRepository.AddEntity(new TeacherInfo { CreatedTime = DateTime.Now, TeachName = "666" });
        }
    }

    public class MyContext : DbContextBase<MyContext>
    {
        public MyContext()
        {
        }

        public override string ConnectionString { get; set; } = "Data Source=.;Initial Catalog=EFTESTDEMO;User ID=sa;Password=0103";
    }

    public class TeacherInfo : EntityBase
    {
        public string TeachName { get; set; }
    }
}