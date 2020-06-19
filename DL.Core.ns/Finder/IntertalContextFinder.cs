using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Core.ns.Data;

namespace DL.Core.ns.Finder
{
    /// <summary>
    ///  内置操作MySql、SqlServer、Oracle的数据库上下文
    /// </summary>
    public class IntertalContextFinder : FinderBase, IIntertalContextFinder
    {
        public override Type[] Find()
        {
            var type = LoadTypes.Where(x => typeof(DataBaseContext).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface).ToArray();
            return type;
        }
    }
}