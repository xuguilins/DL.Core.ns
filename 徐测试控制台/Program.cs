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
using System.Linq.Expressions;

namespace 徐测试控制台
{
    internal class Program
    {
        private static ConcurrentQueue<string> quee = new ConcurrentQueue<string>();

        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddPack();
            var sevice = ServiceLocator.Instance.GetService<ISqlServerDbContext>();

            Console.ReadKey();
        }
    }

    public interface IUserSerivce
    {
    }

    public class UserSerivce : BaseSerivce, IUserSerivce
    {
    }

    public class BaseSerivce
    {
        public int MyProperty { get; set; }
    }

    public class MyContext : DbContext
    {
    }

    public enum UserType
    {
        [Description("学生")]
        Student = 0,

        [Description("老师")]
        Teacher = 1,

        [Description("老板")]
        Boss = 2
    }

    public static class EnumeExtensiton
    {
        /// <summary>
        /// 获取当前枚举值的描述
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attbuite = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attbuite == null)
                throw new ArgumentException("当前枚举类型未标记“Description”特性", $"{value.ToString()}");
            return attbuite?.Description;
        }

        /// <summary>
        /// 获取当前枚举值的描述和名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDescripttionAndName(this Enum value)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attbuite = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attbuite == null)
                throw new ArgumentException("当前枚举类型未标记“Description”特性", $"{value.ToString()}");
            pairs.Add(value.ToString(), attbuite.Description);
            return pairs;
        }

        /// <summary>
        /// 获取当前指定的枚举的值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static int GetEnumeValue(this Enum @enum)
        {
            return Convert.ToInt32(@enum);
        }

        /// <summary>
        /// 获取当前枚举值的基本信息
        /// </summary>
        /// <param name="enum">当前枚举值</param>
        /// <returns></returns>
        public static EnumeModel GetEnumModel(this Enum @enum)
        {
            EnumeModel info = new EnumeModel();
            info.EnumeText = @enum.ToString();
            info.EnumeValue = Convert.ToInt32(@enum);
            info.EnumeDesc = GetDescription(@enum);
            return info;
        }

        /// <summary>
        /// 获取当前枚举的文本
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetEmumText(this Enum @enum)
        {
            return @enum.ToString();
        }
    }

    public class EnumeModel
    {
        public string EnumeText { get; set; }
        public int EnumeValue { get; set; }
        public string EnumeDesc { get; set; }
    }
}

public class RequestDto
{
    /// <summary>
    /// 请假人
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 请假天数
    /// </summary>
    public int Day { get; set; }
}

/// <summary>
/// 请假处理者
/// </summary>
public abstract class Approve
{
    public Approve NextStep;

    public abstract void HanderApprove(RequestDto dto);
}

public class Mannger : Approve
{
    public override void HanderApprove(RequestDto dto)
    {
        if (dto.Day >= 7 && dto.Day < 15)
        {
            Console.WriteLine($"我是主管,我可以批{dto.UserName}请的{dto.Day}天假期");
        }
        else if (NextStep != null)
        {
            NextStep.HanderApprove(dto);
        }
    }
}

public class Teacher : Approve
{
    public override void HanderApprove(RequestDto dto)
    {
        if (dto.Day < 7)
        {
            Console.WriteLine($"我是老师,我可以批{dto.UserName}请的{dto.Day}天假期");
        }
        else if (NextStep != null)
        {
            NextStep.HanderApprove(dto);
        }
    }
}

public class Ceo : Approve
{
    public override void HanderApprove(RequestDto dto)
    {
        if (dto.Day >= 15)
        {
            Console.WriteLine($"我是CEO,我可以批{dto.UserName}请的{dto.Day}天假期");
        }
    }
}