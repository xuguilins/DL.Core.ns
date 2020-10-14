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
            

            Console.ReadKey();
        }
    }
}
public class iDemoProduct:EntityBase
{
    public string ProdCode { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    public string Unit { get; set; }
    public string UpdateDate { get; set; }
}