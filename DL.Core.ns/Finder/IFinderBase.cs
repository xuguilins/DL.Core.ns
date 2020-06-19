using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Finder
{
    public interface IFinderBase
    {
        /// <summary>
        /// 获取所有类型
        /// </summary>
        List<Type> LoadTypes { get; }

        /// <summary>
        /// 查找所有类型
        /// </summary>
        /// <returns></returns>
        Type[] FinderAll();

        /// <summary>
        /// 查找指定类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Type FinderType(Type type);
    }
}