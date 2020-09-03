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

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }

    /// <summary>
    /// CPU管理
    /// </summary>
    [TableAttubite("MouseInfo")]
    public class CpuInfo : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// 推荐品牌
        /// </summary>
        public string MouseBrand { get; set; }

        /// <summary>
        /// 价格区间
        /// </summary>
        public string PriceRange { get; set; }

        /// <summary>
        ///适用类型
        /// </summary>
        public string SuitType { get; set; }

        /// <summary>
        ///连接方式
        /// </summary>
        public string MouseContact { get; set; }

        /// <summary>
        ///鼠标接口
        /// </summary>
        public string MouseInterfance { get; set; }

        /// <summary>
        ///工作方式
        /// </summary>
        public string WorkType { get; set; }
    }

    #region [职责链模式]

    public abstract class ManagerBoss
    {
        public ManagerBoss NextStep { get; set; }

        public abstract void WriteMessage(int count);
    }

    public class Student : ManagerBoss
    {
        public override void WriteMessage(int count)
        {
            if (count > 3)
            {
                if (NextStep == null) throw new Exception("请指定职责链");
                NextStep.WriteMessage(count);
            }
            else
            {
                Console.WriteLine("我可以直接消费");
            }
        }
    }

    public class Teacher : ManagerBoss
    {
        public override void WriteMessage(int count)
        {
            if (count >= 3 && count <= 7)
            {
                Console.WriteLine("我是老师，我可以直接消费");
            }
            else
            {
                NextStep.WriteMessage(count);
            }
        }
    }

    #endregion [职责链模式]

    public class UserTest : EntityBase
    {
        //  public int MyProperty { get; set; }
    }

    [TableAttubite("abc")]
    public class UserInfo : EntityBase
    {
        public string UserName { get; set; }
        public string UserPass { get; set; }
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