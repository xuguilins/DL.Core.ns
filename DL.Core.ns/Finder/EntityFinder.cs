using DL.Core.ns.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.ns.Finder
{
    public class EntityFinder : FinderBase, IEntityFinder
    {
        public override Type[] Find()
        {
            return LoadTypes.Where(x => typeof(EntityBase).IsAssignableFrom(x) && x != typeof(EntityBase) && !x.IsAbstract).ToArray();
        }
    }
}