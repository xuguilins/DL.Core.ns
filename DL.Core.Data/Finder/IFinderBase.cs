using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DL.Core.Data.Finder
{
    /// <summary>
    /// 查找器接口
    /// </summary>
    public interface IFinderBase
    {
        /// <summary>
        /// 查找所有Type
        /// </summary>
        /// <returns></returns>
        Type[] FinderAllType();

        /// <summary>
        /// 查找指定表达式的Type
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Type[] FinderExpressition(Func<Type, bool> expression);
    }
}