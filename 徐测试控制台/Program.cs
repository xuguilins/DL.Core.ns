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
using DL.Core.utility.Logging;
using System.IO;
using DL.Core.Data.InitDatabase;
using Microsoft.Extensions.Configuration;
using DL.Core.utility.Configer;
using System.Text;
using DL.Core.utility.Web;
using DL.Core.utility.Table;
using DL.Core.utility.Dependency;
using DL.Core.ns.Locator;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //IServiceCollection services = new ServiceCollection();
            //services.AddEngineDbContextPack<UserContext>();
            //services.AddEnginePack();
            //var provider = services.BuildServiceProvider();
            //var unit = provider.GetService<IUnitOfWork>();
            //// unit.BeginTransaction = true;
            using (var context = new UserContext())
            {
                context.Set<UserInfo>().Add(new UserInfo
                {
                    UserName = "小米手机",
                    UsePass = "1111"
                });
                context.SaveChanges();
            }
            var service = ServiceLocator.Instance.GetService<IUserService>();
            service.CreateUser(new UserInfo
            {
                UserName = "小米手机",
                UsePass = "1111"
            });
            Console.ReadKey();
        }
    }
}

public interface IUserService : IScopeDependcy
{
    void CreateUser(UserInfo info);
}

public class UserService : IUserService
{
    private IRepository<UserInfo> _userRepository;

    public UserService(IRepository<UserInfo> repository)
    {
        _userRepository = repository;
    }

    public void CreateUser(UserInfo info)
    {
        //var context = ServiceLocator.Instance.GetService<IUnitOfWork>();
        // context.BeginTransaction = true;
        // _userRepository.UnitOfWork.BeginTransaction = true;
        _userRepository.AddEntity(info);
    }
}

public class UserContext : DbContextBase<UserContext>
{
    public override string ConnectionString => "Data Source=.;Initial Catalog=UserInfo;User ID=sa;Password=0103";

    public override void RegistConfiguration(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserInfoEntityTypeConfiguration());
    }
}

public class UserInfoEntityTypeConfiguration : ConfigurationBase<UserInfo>
{
    public override Type DbContextType => typeof(UserContext);

    public override void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.ToTable("UserInfo");
    }
}

public class UserInfo : EntityBase
{
    public string UserName { get; set; }
    public string UsePass { get; set; }
}