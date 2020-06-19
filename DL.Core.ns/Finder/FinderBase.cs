using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DL.Core.ns.Finder
{
    /// <summary>
    /// 查找器基类
    /// </summary>
    public abstract class FinderBase : IFinderBase
    {
        public FinderBase()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
           .ToArray();
            var assemblies = files.Select(Assembly.LoadFrom).Distinct().ToArray();
            var assmably = assemblies.Select(x => x);
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();
            LoadTypes = types;
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        public List<Type> LoadTypes { get; private set; }

        public Type[] FinderAll()
        {
            return Find();
        }

        public Type FinderType(Type type)
        {
            var typeService = Find().FirstOrDefault(x => x == type);
            return typeService;
        }

        public abstract Type[] Find();
    }
}