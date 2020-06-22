using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Configer
{
    public class XmlJsonConfig
    {
        public DataBase DataBase { get; set; }
    }

    public class DataBase
    {
        public string SqlServer { get; set; }
        public string MySql { get; set; }
        public string Oracle { get; set; }
    }
}