using System;
using System.Linq;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var type = typeof(UserInfo);
            var list = type.GetInterfaces().FirstOrDefault();
            Console.WriteLine(list.Name);
            var d = list.GetInterfaces().FirstOrDefault();
            Console.WriteLine(d.Name);
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"name:{item.Name},fullname:{item.FullName}");
            //}
            Console.ReadKey();
        }
    }

    public interface ItestA : ITestB
    {
    }

    public interface ITestB
    {
    }

    public class UserInfo : ItestA, ITestB
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
}