using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.ns.EventBusHandler;
using Microsoft.Extensions.DependencyInjection;

namespace DL.Core.ns.CorePack
{
    public class EventBusPack : PackBase
    {
        public override int StarLevel => 20;

        public override IServiceCollection AddService(IServiceCollection services)
        {
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventBus, EventBus>();
            return services;
        }
    }
}