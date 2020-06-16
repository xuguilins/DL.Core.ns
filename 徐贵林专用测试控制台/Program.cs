using System;

namespace 徐贵林专用测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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