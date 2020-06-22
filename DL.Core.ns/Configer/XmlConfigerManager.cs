using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace DL.Core.ns.Configer
{
    public class XmlConfigerManager : ConfigFileBase
    {
        public static Lazy<XmlConfigerManager> xmlList = new Lazy<XmlConfigerManager>(() => new XmlConfigerManager());
        public override string FileName => "DL.Config.xml";
        public static XmlConfigerManager Instance = xmlList.Value;

        public XmlJsonConfig GetConfiger()
        {
            XmlJsonConfig info = new XmlJsonConfig();
            foreach (var item in BasePath)
            {
                var path = item + FileName;
                if (File.Exists(path))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(path);
                    var nodeParent = xml.GetElementsByTagName("DL");
                    if (xml != null && nodeParent.Count > 0)
                    {
                        var parent = nodeParent[0];
                        if (parent.HasChildNodes)
                        {
                            var chidList = parent.ChildNodes;
                            foreach (XmlNode child in chidList)
                            {
                                switch (child.Name)
                                {
                                    case "DataBase":
                                        var bas = xml.GetElementsByTagName(child.Name);
                                        if (bas != null && bas.Count > 0)
                                        {
                                            var databaseNode = bas[0];
                                            if (databaseNode.HasChildNodes)
                                            {
                                                info.DataBase = ParseDataBaseXml(databaseNode.ChildNodes);
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"无效的XML配置");
                    }
                }
            }
            return info;
        }

        private DataBase ParseDataBaseXml(XmlNodeList list)
        {
            DataBase info = new DataBase();
            foreach (XmlNode item in list)
            {
                switch (item.Name)
                {
                    case "SqlServer":
                        info.SqlServer = item.InnerText.Trim();
                        break;

                    case "MySql":
                        info.MySql = item.InnerText.Trim();
                        break;

                    case "Oracle":
                        info.Oracle = item.InnerText.Trim();
                        break;
                }
            }
            return info;
        }
    }
}