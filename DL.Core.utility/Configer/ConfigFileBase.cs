using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.utility.Configer
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

        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string FileName { get; }
    }
}