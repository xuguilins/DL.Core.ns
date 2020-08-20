# DL.Core.ns
采用模块化的思想编写基于EFCore的操作方法以及.NetCore的自动注入

## DL.Core.ns 

 **DL.Core.ns**  是一个快速开发的一个类库，它针对EFCore操作SQLSERVER数据库进行了封装，对所有SERVICE服务进行自动注入，已经内置的使用
原生SQL语句来操作数据库的类库
 
*** 
* DL.Core.Data
 
 ```
   >>此类类库是使用原生的SQL语句来操作数据
   **操作SQLSERVER数据库**
        ISqlServerDbContext service = new SqlServerDbContext();
       service.CreateDbConnection("");  //创建数据库连接
       service.BeginTransation = false; //是否开启事务
       service.ExecuteNonQuery("", CommandType.Text); //创建、修改、删除 操作
       service.GetDataSet("", CommandType.Text);// 读取数据到内存表
       service.GetDataTable("", CommandType.Text); // 读取数据到table
       // 注意： 数据实体必须字段结构必须要与数据库表字段一致
       // 此操作不需要像【InsertEntityItems】方法一样，需要传入是否开启事务
       // 若要使用操作事务，则调用  【service.BeginTransation】 方法即可
       service.InsertEntity("数据实体", "数据表名称");
       service.InsertEntityItems("数据实体", "数据表名称", "是否开启事务"); // 注意： 数据实体必须字段结构必须要与数据库表字段一致
   **操作MySQL数据库**
   与操作SQLSERVER数据库类似，但不包含批量实体或单个实体写入

 ```

* DL.Core.ns

```
   >> 此类类库针对EFCore进行再一次封装,采用模块化的思想进行设置
   >> 其中包含模块注入、命令模式是设计、多个数据库上下文操作、事件发布、自动注入（特性注入，接口标记注入）以及服务定位器
      IServiceCollection services = new ServiceCollection();
      // 引入框架初始化
	  // MyContext 数据上下文必须继承【DbContextBase<MyContext>】
	  // 必须进行实体配置，
      services.AddEngineDbContextPack<MyContext>();  //初始化数据库上下文，最多支持3个
      services.AddEnginePack();// 模块注入，包含内置的事件、命令、仓储注入，或者后续的服务实现类的注入

	**案例**
	public class UserTest:EntityBase
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
    public class UserConfiguration : ConfigurationBase<UserTest>
    {
        public override Type DbContextType => //您的上下文

        public override void Configure(EntityTypeBuilder<UserTest> builder)
        {
            // 实体配置
        }
    }
  
```
## DL.Core.ns实际操作SQLSERVER数据库

```

using System;
using DL.Core.utility.Entity;
using DL.Core.Data.Extendsition;
using DL.Core.Data.SqlData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DL.Core.ns.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DL.Core.ns.Extensiton;
using DL.Core.utility.Dependency;
using DL.Core.ns.Locator;

namespace SQL_ITMES
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEngineDbContextPack<UserContext, TeacherContext>();
            services.AddEnginePack();
            var service = ServiceLocator.Instance.GetService<IUserService>();
            service.CreateUser(new UserInfo { UsePass = "6666", UserName = "王二狗" });
            Console.ReadKey();
        }
    }

    #region [实体]

    public class UserInfo : EntityBase
    {
        public string UserName { get; set; }
        public string UsePass { get; set; }
    }

    public class TeacherInfo : EntityBase

    {
        public string TeacherName { get; set; }
        public string TeacherAdderss { get; set; }
    }

    #endregion [实体]

    #region [上下文]

    public class UserContext : DbContextBase<UserContext>
    {
        public override string ConnectionString => "Data Source=.;Initial Catalog=CoreNs;Integrated Security=True";

        public override void RegistConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserInfoConfigurtion());
        }
    }

    public class TeacherContext : DbContextBase<TeacherContext>
    {
        public override string ConnectionString => "Data Source=.;Initial Catalog=EFTESTDEMO;Integrated Security=True";

        public override void RegistConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TeacherConfiguration());
        }
    }

    #endregion [上下文]

    #region [实体配置]

    public class UserInfoConfigurtion : ConfigurationBase<UserInfo>
    {
        public override Type DbContextType => typeof(UserContext);

        public override void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("UserInfo");
        }
    }

    public class TeacherConfiguration : ConfigurationBase<TeacherInfo>
    {
        public override Type DbContextType => typeof(TeacherContext);

        public override void Configure(EntityTypeBuilder<TeacherInfo> builder)
        {
            builder.ToTable("TeacherInfo");
        }
    }

    #endregion [实体配置]

    #region [服务]

    public interface IUserService : IScopeDependcy
    {
        void CreateUser(UserInfo info);
    }

    public class UserService : IUserService
    {
        private IRepository<UserInfo> _user;

        public UserService(IRepository<UserInfo> repository)
        {
            _user = repository;
        }

        public void CreateUser(UserInfo info)
        {
            _user.AddEntity(info);
        }
    }

    #endregion [服务]
}

```




* DL.Core.utility
 
```
 它是一个工具类库

```



