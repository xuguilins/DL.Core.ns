﻿using DL.Core.ns.Configer;
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

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddPack<UserContext>();
            var service = ServiceLocator.Instance.GetService<IMemoryCache>();
            Console.WriteLine("设置缓存。。。");
            service.Set("zll", "666", TimeSpan.FromSeconds(3));
            var value = service.Get("zll");
            Console.WriteLine($"取出缓存：{value}");
            Console.ReadKey();
        }
    }

    // [Table("UserTest")]
    public class UserTest : EntityBase
    {
        public string UserName { get; set; }
        public string UsePass { get; set; }
    }

    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CoreNs;User ID=sa;Password=0103");
        }

        public DbSet<UserTest> UserTest { get; set; }
    }

    public interface IUserService : IRepository<UserTest>, IScopeDependcy
    {
    }

    public class UserService : Repository<UserTest>, IUserService
    {
        public UserService(UserContext context) : base(context)
        {
        }
    }
}