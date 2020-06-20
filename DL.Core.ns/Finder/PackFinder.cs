using DL.Core.ns.CorePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.ns.Finder
{
    public class PackFinder : FinderBase, IPackFinder
    {
        public override Type[] Find()
        {
            var types = LoadTypes.Where(x => !x.IsAbstract && x.IsClass && typeof(PackBase).IsAssignableFrom(x)).ToArray();
            return types;
        }
    }
}