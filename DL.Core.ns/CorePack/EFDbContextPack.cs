using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.ns.Finder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DL.Core.ns.CorePack
{
    public class EFDbContextPack : PackBase
    {
        public override int StarLevel => 15;

        public override IServiceCollection AddService(IServiceCollection services)
        {
            return services;
            //IDbContextFinder finder = new  DbContextFinder();
            //var types = finder.FinderAll();
            //foreach (var item in types)
            //{
            //    var instance = Activator.CreateInstance(item) as DbContext;

            //    //services.AddDbContext<TDbContext>()
            //}
            //throw new NotImplementedException();
        }
    }
}