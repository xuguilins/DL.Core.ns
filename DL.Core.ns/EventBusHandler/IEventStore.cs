using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.EventBusHandler
{
    public interface IEventStore
    {
        /// <summary>
        /// 获取内存中的事件
        /// </summary>
        /// <returns></returns>
        ConcurrentDictionary<Type, List<Type>> GetStore();
    }
}