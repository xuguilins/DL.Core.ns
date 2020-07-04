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

namespace 徐测试控制台
{
    internal class Program
    {
        private static ConcurrentQueue<string> quee = new ConcurrentQueue<string>();

        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddPack();

            Task[] task = new Task[3];

            for (int i = 0; i < task.Count(); i++)
            {
                if (i == 0)
                {
                    task[i] = Task.Run(() => InOrder());
                }
                else
                {
                }
            }

            Console.ReadKey();
        }

        public static void InOrder()
        {
            Console.WriteLine("第一批订单入列");
            for (int i = 0; i < 100; i++)
            {
                quee.Enqueue($"编号：{i}入列成功");
            }

            Console.WriteLine("我在入列");
        }
    }

    // [DL.Core.ns.Dependency.AttbuiteDependency(ServiceLifetime.Scoped)]
    public interface IUserSerivce
    {
        void Speak();
    }

    [DL.Core.ns.Dependency.AttbuiteDependency(ServiceLifetime.Scoped)]
    public class UserService : IUserSerivce
    {
        public void Speak()
        {
            Console.WriteLine("我是特性注入");
        }
    }

    public interface TeacherSerivce : IScopeDependcy
    {
        void Speak();
    }

    [DL.Core.ns.Dependency.AttbuiteDependency(ServiceLifetime.Scoped)]
    public class TeacherSerivces : TeacherSerivce
    {
        public void Speak()
        {
            Console.WriteLine("老师我是特性注入");
        }
    }

    public class EfContext : DbContext
    {
    }
}