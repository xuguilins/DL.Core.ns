using DL.Core.ns.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.ns.Finder
{
    public class EntityTypeConfiguraFinder : FinderBase, IEntityTypeConfiguraFinder
    {
        public override Type[] Find()
        {
            var types = LoadTypes.Where(x => !x.IsInterface && !x.IsAbstract && x.BaseType.GetGenericTypeDefinition() == typeof(ConfigurationBase<>)).ToArray();
            return types;
        }
    }
}