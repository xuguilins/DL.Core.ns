using DL.Core.ns.Extensiton;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Entity
{
    public class EntityBase
    {
        private string _Id = StrExtensition.GetGuid();
        private DateTime _createTime = StrExtensition.GetDateTime();

        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public DateTime CreatedTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }
    }
}