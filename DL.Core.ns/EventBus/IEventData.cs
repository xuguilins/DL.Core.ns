using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.EventBus
{
    public interface IEventData
    {
        /// <summary>
        /// 事件主键
        /// </summary>
        string EventId { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        EventType EventType { get; set; }

        /// <summary>
        /// 事件创建时间
        /// </summary>
        DateTime CreatedTime { get; set; }
    }
}