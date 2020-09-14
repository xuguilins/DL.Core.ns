using DL.Core.utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.Data.Finder
{
    public interface IEntityFinder : IFinderBase
    {
    }

    public class EntityFinder : FinderBase, IEntityFinder
    {
        protected override Type[] FindType()
        {
            var entityList = LoadTypes.Where(x => typeof(EntityBase).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface && typeof(EntityBase) != x);
            return entityList.ToArray();
        }
    }
}