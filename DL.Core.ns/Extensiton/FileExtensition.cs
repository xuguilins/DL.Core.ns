using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DL.Core.ns.Extensiton
{
    public static class FileExtensition
    {
        /// <summary>
        /// 文件路径移除
        /// </summary>
        /// <param name="data">路径</param>
        /// <param name="exit">字符开始位置,12345678 比如:从6开始移除所有
        ///只会保留从0开始到6这个字符的位置接受且不包含6
        /// </param>
        /// <returns></returns>
        public static string PathRemove(this string data, string exit)
        {
            var endindex = data.IndexOf(exit);
            var result = data.Remove(endindex);
            return result;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="dicName">文件夹名称</param>
        /// <returns>文件夹名称</returns>
        public static string CreateDic(string path, string dicName = null)
        {
            var files = path + dicName;
            if (!Directory.Exists(files))
            {
                Directory.CreateDirectory(files);
                return dicName;
            }
            else
            {
                return dicName;
            }
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static bool CheckDirctoryIsExite(this string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns>true：存在, false:不存在</returns>
        public static bool CheckFileIsExite(this string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 读取txt文件
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public static string ReadText(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            }
            else
            {
                return "未找到指定路径上的文件";
            }
        }
    }
}