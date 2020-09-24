using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.Data.Finder;
using DL.Core.Data.SqlData;
using DL.Core.utility.Logging;

namespace DL.Core.Data.InitDatabase
{
    /// <summary>
    /// 创建数据表的扩展
    /// </summary>
    public static class CreateTable
    {
        private static ILogger logger = LogManager.GetLogger();

        /// <summary>
        /// 调用此扩展方法前需先创建数据库链接
        /// </summary>
        /// <param name="context"></param>
        public static void CreateSqlServerTable(this ISqlServerDbContext context)
        {
            try
            {
                logger.Info($"Core.Data 初始化数据表。。。");
                //查找所有实体
                IEntityFinder finder = new EntityFinder();
                var types = finder.FinderAllType();
                //遍历所有实体，并且创建数据表结构
                logger.Info($"准备解析实体。。。。");
                foreach (var item in types)
                {
                    StringBuilder sb = new StringBuilder();
                    //获取当前实体是否设置特性
                    var attb = item.GetCustomAttributes(typeof(TableAttubite), false);
                    string tableName = item.Name;
                    if (attb != null && attb.Length > 0)
                    {
                        var att = attb[0] as TableAttubite;
                        if (att != null)
                        {
                            tableName = att.TableName;
                        }
                    }
                    var props = item.GetProperties();
                    sb.Append($"CREATE TABLE {tableName}");
                    sb.Append("(");
                    foreach (var p in props)
                    {
                        #region [解析特性]

                        var ats = p.GetCustomAttributes(typeof(PropAttbilteLength), false);
                        string length = string.Empty;
                        if (ats != null && ats.Length > 0)
                        {
                            var ps = ats[0] as PropAttbilteLength;
                            if (ps != null)
                            {
                                length = ps.PropLength;
                            }
                        }

                        #endregion [解析特性]

                        #region [解析长度]

                        if (p.Name == "Id")
                        {
                            sb.Append($"{p.Name} {ParsePropType(p.PropertyType.Name, length)} primary key not null,");
                        }
                        else
                        {
                            sb.Append($"{p.Name} {ParsePropType(p.PropertyType.Name, length)},");
                        }

                        #endregion [解析长度]
                    }
                    sb.Append(")");
                    logger.Info($"实体解析完毕。。。。准备创建数据表【{tableName}】");
                    int data = context.ExecuteNonQuery(sb.ToString(), System.Data.CommandType.Text);
                    logger.Info($"数据表【{tableName}】创建完毕");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Core.Data迁移数据表发生异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 解析字段类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static string ParsePropType(string typeName, string length)
        {
            string result = string.Empty;
            switch (typeName)
            {
                case "String":
                    result = (string.IsNullOrWhiteSpace(length) ? "varchar(100)" : $"varchar({length})");
                    break;

                case "Int32":
                    result = "int";
                    break;

                case "Decimal":
                    result = (string.IsNullOrWhiteSpace(length) ? "decimal(18, 2)" : $"decimal({length})");
                    break;

                case "DateTime":
                    result = "datetime";
                    break;
            }
            return result;
        }
    }
}