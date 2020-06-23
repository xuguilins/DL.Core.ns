using DL.Core.ns.Configer;
using System;
using System.Linq;
using DL.Core.ns.Data;
using DL.Core.ns.Table;
using System.Data;
using System.Collections.Generic;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sql = "SELECT * FROM UserInfO ";
            ISqlServerDbContext context = new SqlServerDbContext();
            DataTable Table = context.GetDataTable(sql, System.Data.CommandType.Text);
            var list = Table.ToObjectList<UserInfo>();

            Console.ReadKey();
        }
    }

    public interface ItestA : ITestB
    {
    }

    public interface ITestB
    {
    }

    public class ResultData
    {
    }

    public abstract class Command
    {
        public abstract void Execute();
    }

    public class UserLoginCommand : Command
    {
        public override void Execute()
        {
            Console.WriteLine("我在执行方法");
        }
    }

    public class CommandExecuter
    {
        public void ExeCom<T>(T command) where T : Command
        {
            var typeinfo = command.GetType();
            if (typeof(Command).IsAssignableFrom(typeinfo))
            {
                var method = typeinfo.GetMethod("Execute");
                if (method != null)
                {
                    //创建执行对象
                    var instance = Activator.CreateInstance(typeinfo);
                    method.Invoke(instance, null);
                }
            }

            //command.Execute();
        }
    }

    public class UserInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateTime { get; set; }
    }
}