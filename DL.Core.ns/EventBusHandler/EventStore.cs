using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DL.Core.ns.EventBusHandler
{
    public class EventStore : IEventStore
    {
        public ConcurrentDictionary<Type, List<Type>> GetStore()
        {
            ConcurrentDictionary<Type, List<Type>> dic = new ConcurrentDictionary<Type, List<Type>>();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                //检查当前类型是否实现了IEventHandler接口
                if (typeof(IEventHandler).IsAssignableFrom(type))
                {
                    //获取当前实现当前接口泛型接口
                    var fType = type.GetInterface("IEventHandler`1");
                    if (fType != null)
                    {
                        var parms = fType.GetGenericArguments()[0];
                        if (dic.ContainsKey(parms))
                        {
                            List<Type> evtType = new List<Type>();
                            evtType.Add(type);
                            dic[parms] = evtType;
                        }
                        else
                        {
                            List<Type> evtType = new List<Type>();
                            evtType.Add(type);
                            dic.TryAdd(parms, evtType);
                        }
                    }
                }
            }
            return dic;
        }
    }
}