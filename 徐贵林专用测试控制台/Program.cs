using DL.Core.ns.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace 徐专用测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IDataBaseContext, SqlServerDbContext>();
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<IDataBaseContext>();
            // service.BeginTransation = false;
            var sql = "INSERT INTO UserInfO(ID,Name,Age,CreateTime)VALUES(@ID,@Name,@Age,@CreateTime)";
            SqlParameter[] ps =
            {
                 new SqlParameter("@ID",Guid.NewGuid()),
                 new SqlParameter("@Name","测试"),
                 new SqlParameter("@Age",10),
                 new SqlParameter("@CreateTime",DateTime.Now)
            };
            var data = service.ExecuteNonQuery(sql, System.Data.CommandType.Text, ps);
            //service.SaveTransactionChange();
            Console.ReadKey();
        }
    }

    #region [命令模式简要Demo]

    public abstract class Command
    {
        public abstract void Execute();
    }

    public class UserRegistCommand : Command
    {
        public override void Execute()
        {
            Console.WriteLine("我是用户注册命令");
        }
    }

    public class UserLoginCommand : Command
    {
        public override void Execute()
        {
            Console.WriteLine("我是用户登录命令");
        }
    }

    public class CommandExecuter
    {
        public void ExecuteCmd(Command command)
        {
            command.Execute();
        }
    }

    #endregion [命令模式简要Demo]
}