using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.ns.Locator;
using System.Collections.Concurrent;
using DL.Core.ns.Finder;
using System.Linq;

namespace DL.Core.ns.EFCore
{
    public static class DbContextManager
    {
        private static ConcurrentDictionary<Type, List<IEntityTypeRegiest>> diclist = new ConcurrentDictionary<Type, List<IEntityTypeRegiest>>();
        private static List<DbContext> dbContextList = new List<DbContext>();

        /// <summary>
        /// 根据指定的实体类型获取上下文
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbContext GetDb(Type type)
        {
            if (!diclist.Any())
                throw new AggregateException("未设置对应的实体配置且未设置实体对应的数据上下文");
            var dbContext = GetDbContext(type);
            return Activator.CreateInstance(dbContext) as DbContext;
        }

        /// <summary>
        /// 初始化数据库实体上下文
        /// </summary>
        public static void InitEngityDbContext()
        {
            IEntityTypeConfiguraFinder finder = new EntityTypeConfiguraFinder();
            var entityList = finder.FinderAll().ToList();
            List<IEntityTypeRegiest> list = entityList.Select(type => Activator.CreateInstance(type) as IEntityTypeRegiest).ToList();
            var result = list.GroupBy(x => x.DbContextType).ToList();
            foreach (IGrouping<Type, IEntityTypeRegiest> item in result)
            {
                List<IEntityTypeRegiest> f = new List<IEntityTypeRegiest>();
                if (diclist.ContainsKey(item.Key))
                {
                    var temp = diclist[item.Key];
                    temp.AddRange(item.ToList());
                }
                else
                {
                    diclist.TryAdd(item.Key, item.ToList());
                }
            }
        }

        /// <summary>
        /// 获取上下文
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetDbContext(Type type)
        {
            Type dbType = null;
            if (!diclist.Any())
                throw new Exception("未设置对应的实体配置且未设置实体对应的数据上下文");
            foreach (var item in diclist)
            {
                if (item.Value.Any(x => x.EntityType == type))
                {
                    dbType = item.Key;
                    return dbType;
                }
            }
            return dbType;
        }

        /// <summary>
        /// 上下文初始化
        /// </summary>
        /// <param name="type"></param>
        public static void InitUnitDbContext(DbContext type)
        {
            dbContextList.Add(type);
        }

        /// <summary>
        /// 获取内存设定好的数据库上下文
        /// </summary>
        /// <returns></returns>
        public static List<DbContext> GetMeomryDbContxt()
        {
            return dbContextList;
        }
    }
}