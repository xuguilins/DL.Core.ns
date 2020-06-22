using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Configer
{
    public class ConfigFileBase
    {
        /// <summary>
        /// 配置路径
        /// </summary>
        private string[] _basePath = { "./", "./../", "./../../", "./../../../../", "./../../../../../", "./../../../../../../", "./../../../../../../../" };

        public string[] BasePath
        {
            get
            {
                return _basePath;
            }
            set
            {
                _basePath = value;
            }
        }

        public virtual string FileName { get; }
    }
}