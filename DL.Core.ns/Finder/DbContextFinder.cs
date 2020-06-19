using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.ns.Finder
{
    /// <summary>
    /// EF上下文查找器
    /// </summary>
    public class DbContextFinder : FinderBase, IDbContextFinder
    {
        public override Type[] Find()
        {
            var types = LoadTypes.Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface);
            return types.ToArray();
        }
    }
}