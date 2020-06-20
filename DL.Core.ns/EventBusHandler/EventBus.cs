using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.ns.EventBusHandler
{
    public class EventBus : IEventBus
    {
        private ConcurrentDictionary<Type, List<Type>> Store = null;
        private IEventStore _store;

        public EventBus(IEventStore store)
        {
            _store = store;
            Store = _store.GetStore();
        }

        public void Publish<T>(T @event) where T : EventData
        {
            var type = @event.GetType();
            if (!Store.Any())
                throw new Exception($"EventStore未初始化");
            var handlerList = Store[type];
            if (handlerList != null && handlerList.Any())
            {
                foreach (var handler in handlerList)
                {
                    var method = handler.GetMethod("Execute");
                    var instance = Activator.CreateInstance(handler);
                    if (method != null)
                    {
                        method.Invoke(instance, new object[] { @event });
                    }
                }
            }
        }
    }
}