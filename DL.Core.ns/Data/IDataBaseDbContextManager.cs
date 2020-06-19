using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 内置数据库上下文管理器
    /// </summary>
    public interface IDataBaseDbContextManager
    {
        /// <summary>
        /// 获取指定的数据库上下文
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IDataBaseContext GetDataBaseDbContext(Type type);
    }
}