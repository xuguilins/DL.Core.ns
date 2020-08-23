﻿using System;
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

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>()
            {
                { "UserName" ,"老何"},
                {"UserPass","123465" },
                {"Extend",
                   new { Age = 12, Sex="男"
                   }
                }
                };

            var data = pairs.ToObject<UserInfo>();
            Console.ReadKey();
        }
    }

    public class UserTest : EntityBase
    {
        //  public int MyProperty { get; set; }
    }

    public class UserInfo
    {
        public string UserName { get; set; }
        public string UserPass { get; set; }
        //public UserPops Extend { get; set; }
    }

    public class UserPops
    {
        public int Age { get; set; }
        public string Sex { get; set; }
    }

    public class MyContext : DbContextBase<MyContext>
    {
        public override string ConnectionString => "";

        public override void RegistConfiguration(ModelBuilder builder)
        {
            //IEntityTypeConfiguration
        }
    }

    public class UserContxt : DbContextBase<UserContxt>
    {
        public override string ConnectionString => "";

        public override void RegistConfiguration(ModelBuilder builder)
        {
        }
    }
}