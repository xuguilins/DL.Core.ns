using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DL.Core.Data.Finder
{
    public abstract class FinderBase : IFinderBase
    {
        /// <summary>
        /// 获取所有类型
        /// </summary>
        public List<Type> LoadTypes { get; private set; }

        public FinderBase()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
           .ToArray();
            var assemblies = files.Select(Assembly.LoadFrom).Distinct().ToArray();
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();
            LoadTypes = types;
        }

        /// <summary>
        /// 查找所有Type
        /// </summary>
        /// <returns></returns>
        public Type[] FinderAllType()
        {
            return FindType();
        }

        /// <summary>
        /// 查找指定表达式的Type
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Type[] FinderExpressition(Func<Type, bool> expression)
        {
            return FindType().Where(expression).ToArray();
        }

        /// <summary>
        /// 重写查找
        /// </summary>
        /// <returns></returns>
        protected abstract Type[] FindType();
    }
}