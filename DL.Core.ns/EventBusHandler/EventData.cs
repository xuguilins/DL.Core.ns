﻿using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.ns.Extensiton;
using DL.Core.utility.Extendsition;

namespace DL.Core.ns.EventBusHandler
{
    public class EventData : IEventData
    {
        private string _eventId = StrExtensition.GetDateGuid();
        private EventType _type = EventType.Info;
        private DateTime _time = StrExtensition.GetDateTime();

        public virtual string EventId { get { return _eventId; } set { _eventId = value; } }

        public virtual EventType EventType { get { return _type; } set { _type = value; } }
        public virtual DateTime CreatedTime { get { return _time; } set { _time = value; } }
    }
}