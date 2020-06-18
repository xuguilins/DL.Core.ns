using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.EventBus
{
    /// <summary>
    /// 空事件接口
    /// </summary>
    public interface IEventHandler
    {
    }

    public interface IEventHandler<T> : IEventHandler where T : IEventData
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="event"></param>
        void Execute(T @event);
    }

    public interface IEventBus
    {
        void Publish<T>(T @event) where T : EventData;
    }
}