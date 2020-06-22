using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ns.CommandFactory;

namespace DL.Core.ns.CorePack
{
    public class CommandPack : PackBase
    {
        public override int StarLevel => 40;

        public override IServiceCollection AddService(IServiceCollection services)
        {
            services.AddScoped<ICommandExecutor, CommandExecutor>();
            return services;
        }
    }
}