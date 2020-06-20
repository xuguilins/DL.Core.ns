using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Core.ns.Finder;
using Microsoft.Extensions.DependencyInjection;

namespace DL.Core.ns.CorePack
{
    public class DLEnginePack : IDLEnginePack
    {
        private readonly IPackFinder _finder;

        public DLEnginePack()
        {
            LoadPacks = new List<PackBase>();
            _finder = new PackFinder();
            var types = _finder.FinderAll();
            foreach (var item in types)
            {
                var instance = Activator.CreateInstance(item) as PackBase;
                LoadPacks.Add(instance);
            }
        }

        public List<PackBase> LoadPacks { get; private set; }

        public IServiceCollection AddEnginePack(IServiceCollection services)
        {
            var loads = LoadPacks.OrderBy(x => x.StarLevel);
            foreach (var item in loads)
            {
                item.AddService(services);
            }
            return services;
        }
    }
}