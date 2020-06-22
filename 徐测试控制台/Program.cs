using DL.Core.ns.Configer;
using System;
using System.Linq;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //XmlConfigerManager.Instance.GetConfiger();
            ConfigerManager.Instance.getCofiger();
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