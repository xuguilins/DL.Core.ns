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

        public static DbContext GetDb(Type type)
        {
            if (diclist.Any())
            {
                var dbContext = GetDbContext(type);
                return Activator.CreateInstance(dbContext) as DbContext;
            }
            else
            {
                InitDbContext();
                return null;
            }
        }

        /// <summary>
        /// 初始化数据库实体上下文
        /// </summary>
        public static void InitDbContext()
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

        public static Type GetDbContext(Type type)
        {
            Type dbType = null;
            if (diclist.Any())
            {
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
            else
            {
                return null;
            }
        }
    }
}