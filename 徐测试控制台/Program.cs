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
using DL.Core.Data.InitDatabase;
using Microsoft.Extensions.Configuration;
using DL.Core.utility.Configer;
using System.Text;
using DL.Core.utility.Web;
using DL.Core.utility.Table;

namespace 徐测试控制台
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = "123";
            var t = a.ToByte();

            var stream = t.ToStream();

            var d = stream.ToByte();

            #region [测试]

            //List<MouseInfo> mianList = new List<MouseInfo>();
            //List<BoardInfo> boardList = new List<BoardInfo>();
            //List<ChassisInfo> chassList = new List<ChassisInfo>();
            //List<MonitorInfo> xsqList = new List<MonitorInfo>();
            //List<PhyInfo> phyList = new List<PhyInfo>();
            //List<NetWorkInfo> netList = new List<NetWorkInfo>();
            //List<PowerSourceInfo> plist = new List<PowerSourceInfo>();
            //List<RadiatorInfo> slqlist = new List<RadiatorInfo>();
            //List<SoundCardInfo> sklist = new List<SoundCardInfo>();
            //ISqlServerDbContext dbContext = new SqlServerDbContext();
            //List<VgaInfo> vlist = new List<VgaInfo>();
            //List<VoiceBoxInfo> voicelist = new List<VoiceBoxInfo>();
            ////   dbContext.CreateDbConnection(conStr);
            //for (int i = 1; i < 100; i++)
            //{
            //    voicelist.Add(new VoiceBoxInfo { FuncName = $"显存{i}", Name = $"音箱{i}", PriceRange = $"价格{i}", VoiceBrand = $"品牌{i}", VoiceMater = $"SYSTEM{i}", VoiceSystem = $"SYSTEM{i}", VoiceType = $"LX{i}" });
            //    // vlist.Add(new VgaInfo { VgaRam = $"显存{i}", Name = $"散热器{i}", PriceRange = $"价格{i}", VgaBrand = $"品牌{i}", VgaCenter = $"SYSTEM{i}", VgaIo = $"接口{i}", VgaType = $"LX{i}" });
            //    // sklist.Add(new SoundCardInfo { BusInterfance = $"接口{i}", Name = $"散热器{i}", PriceRange = $"价格{i}", SoundBrand = $"品牌{i}", SoundSystem = $"SYSTEM{i}", SteupType = $"FS{i}", SuitType = $"LX{i}" });
            //    // slqlist.Add(new RadiatorInfo { HotpipesCount = $"热管{i}", Name = $"散热器{i}", PriceRange = $"价格{i}", RadiatorBrand = $"品牌{i}", RadiatorFs = $"FS{i}", RadiatorType = $"LX{i}", TemperatureContrl = $"{i}°" });
            //    //plist.Add(new PowerSourceInfo { Name = $"电源{i}", OutletType = $"出线{i}", PowerBrand = $"品牌{i}", PowerType = $"LX{i}", PriceRange = $"{i}", Certification = $"U{i}", Ratedpower = $"{i}QWH" });
            //    //netList.Add(new NetWorkInfo { AppNetWorkType = $"网络{i}", BusType = $"总线{i}", Name = $"网卡{i}", NetSpeed = $"{i}速率", NetWorkBrand = $"品牌{i}", NetWorkInterfance = $"接口{i}", PriceRange = $"价格{i}" });
            //    //phyList.Add(new PhyInfo { Name = $"物理{i}", Hc = $"{i}缓存", PriceRange = $"价格{i}", PhyBrand = $"品牌{i}", PhyInterType = $"接口{i}", PhySize = $"{i}", Zs = $"{i}转" });
            //    //xsqList.Add(new MonitorInfo { MonitorBrand = $"品牌{i}", MonitorSize = $"{i}*1920", Name = $"显示器{i}", PanelType = $"M10{i}", PriceRange = $"价格{i}", ProductType = $"LX{i}", VideoInterfance = $"接口{i}" });
            //    //mianList.Add(new MouseInfo { MouseBrand = $"品牌{i}", MouseContact = $"LJ{i}", MouseInterfance = $"接口A{i}", SuitType = $"LX{i}", WorkType = $"DDR{i}", Name = $"主板{i}", PriceRange = $"价格{i}" });
            //    //boardList.Add(new BoardInfo { BoardBrand = $"双飞燕{i}", BoardInterfance = $"JK{i}", BoardJs = $"ALO{i}", BoardPositon = "加用", ContactType = $"LJ{i}", Name = $"键盘{i}", PriceRange = $"{i}" });
            //    //chassList.Add(new ChassisInfo { Name = $"机箱{i}", PriceRange = $"价格{i}", ChassisBrand = $"品牌{i}", ChassisLine = $"L{i}", ChassisStr = $"结构{i}", ChassisType = $"LX{i}", PowerType = $"DDD{i}" });
            //}
            //Console.WriteLine("写入");
            //dbContext.InsertEntityItems(voicelist, "VoiceBoxInfo");
            //Console.WriteLine("写入完毕");

            #endregion [测试]

            Console.ReadKey();
        }
    }

    [TableAttubite("MenuInfo")]
    public class MenuInfo : EntityBase
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 父级菜单
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsEnable { get; set; }
    }

    /// <summary>
    /// 数据模型实体
    /// </summary>
    [TableAttubite("SYS_DataModelInfo")]
    public class DataModelEntity : EntityBase
    {
        /// <summary>
        /// 数据模型名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 数据模型代码
        /// </summary>
        public string ModelCode { get; set; }

        /// <summary>
        /// 数据模型描述
        /// </summary>
        [PropAttbilteLength("200")]
        public string ModelDesc { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsEnable { get; set; }
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

    [TableAttubite("UserInfo")]
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