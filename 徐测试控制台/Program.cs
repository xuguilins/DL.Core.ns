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
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ISqlService service = new SqlServer();
            service.GetTable("SELECT * FROM USERINFO WHERE UserName=@UserName", new { UserName = "徐文" });

            Console.ReadKey();
        }
    }

    public interface ISqlService
    {
        DataTable GetTable(string sql, object parameter = null);
    }

    public class SqlServer : ISqlService
    {
        public DataTable GetTable(string sql, object parameter = null)
        {
            var parmas = parameter.toDictory();
            foreach (var item in parmas)
            {
                sql = sql.Replace($"@{item.Key}", $"'{item.Value}'");
            }
            Console.WriteLine(sql);
            return null;
        }
    }

    public static class Extendsiton
    {
        public static Dictionary<string, object> toDictory(this object value)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var propList = value.GetType().GetProperties();
            foreach (var item in propList)
            {
                var name = item.Name;
                var val = item.GetValue(value, null);
                dic.Add(name, val);
            }
            return dic;
        }
    }

    public interface IUserService : IScopeDependcy
    {
        void AddTeacher();
    }

    public class UserService : IUserService
    {
        private IRepository<TeacherInfo> TeachRepository;
        private IRepository<TestUserInfo> TestUserRepository;

        public UserService(IRepository<TeacherInfo> repository, IRepository<TestUserInfo> xrepository)
        {
            TeachRepository = repository;
            TestUserRepository = xrepository;
        }

        public void AddTeacher()
        {
            TeachRepository.AddEntity(new TeacherInfo { CreatedTime = DateTime.Now, TeacherName = "666", TeacherAdderss = "江西省赣州市" });
            TestUserRepository.AddEntity(new TestUserInfo { CreatedTime = DateTime.Now, UsePass = "sdfsdfsd", UserName = "新的数据赏上下文" });
        }
    }

    /// <summary>
    /// 上下文1
    /// </summary>
    public class MyContext : DbContextBase<MyContext>
    {
        public MyContext()
        {
        }

        public override string ConnectionString => "Data Source=.;Initial Catalog=EFTESTDEMO;User ID=sa;Password=0103";

        public override void RegistConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TeacherInfoConfiguration());
        }
    }

    public class NsDbContext : DbContextBase<NsDbContext>
    {
        public override string ConnectionString => "Data Source=.;Initial Catalog=CoreNs;User ID=sa;Password=0103";

        public override void RegistConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TestUserInfoConfiguration());
        }
    }

    public class TestUserInfoConfiguration : ConfigurationBase<TestUserInfo>
    {
        public override Type DbContextType => typeof(NsDbContext);

        public override void Configure(EntityTypeBuilder<TestUserInfo> builder)
        {
            builder.ToTable("UserTest");
        }
    }

    public class TestUserInfo : EntityBase
    {
        public string UserName { get; set; }
        public string UsePass { get; set; }
    }

    public class TeacherInfo : EntityBase
    {
        public string TeacherName { get; set; }
        public string TeacherAdderss { get; set; }
    }

    public class TeacherInfoConfiguration : ConfigurationBase<TeacherInfo>
    {
        public override Type DbContextType => typeof(MyContext);

        public override void Configure(EntityTypeBuilder<TeacherInfo> builder)
        {
            builder.ToTable("TeacherInfo");
            builder.Property(x => x.TeacherName).HasMaxLength(50);
            builder.Property(x => x.Id).HasMaxLength(50).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}