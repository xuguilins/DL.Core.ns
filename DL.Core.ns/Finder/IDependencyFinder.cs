﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Finder
{
    public interface IDependencyFinder : IFinderBase
    {
        Type[] DependencyType { get; }
    }
}