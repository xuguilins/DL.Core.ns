using DL.Core.ns.Extensiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Entity
{
    public class EntityBase
    {
        public string Id { get; set; } = StrExtensition.GetGuid();

        public DateTime CreatedTime { get; set; } = StrExtensition.GetDateTime();
    }
}