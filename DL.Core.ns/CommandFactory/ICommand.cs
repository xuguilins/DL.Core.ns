using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.CommandFactory
{
    public interface ICommand<T>
    {
        /// <summary>
        /// 执行具体的命令
        /// </summary>
        /// <returns></returns>
        T Execute();
    }
}