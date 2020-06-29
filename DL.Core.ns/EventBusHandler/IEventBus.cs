using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.EventBusHandler
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
        /// <summary>
        /// 事件发布
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        void Publish<T>(T @event) where T : EventData;

        /// <summary>
        /// 事件移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        void Remove<T>(T @event) where T : EventData;
    }
}