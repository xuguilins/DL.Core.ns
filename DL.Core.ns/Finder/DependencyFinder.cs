using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DL.Core.ns.Dependency;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DL.Core.ns.Finder
{
    /// <summary>
    /// 注入查找
    /// </summary>
    public class DependencyFinder : FinderBase, IDependencyFinder
    {
        private Type[] DependencyType = null;

        public DependencyFinder()
        {
            DependencyType = new Type[] { typeof(IScopeDependcy), typeof(ISingletonDependcy), typeof(ITransientDependcy) };
        }

        public override Type[] Find()
        {
            //查找类型:当前类型不是抽象类，不是接口，没有忽略注入特性。且当前类型是实现了 三种生命周期
            var typeList = LoadTypes.Where(x => !x.IsAbstract && !x.IsInterface && !x.IsDefined(typeof(IgnoreDependency)) && DependencyType.Any(m => m.IsAssignableFrom(x))).ToArray();
            return typeList;
        }
    }
}