using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.CommandFactory
{
    /// <summary>
    /// 命令执行者
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// 执行具体的命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        T Execute<T>(ICommand<T> command) where T : class;

        /// <summary>
        /// 委托执行命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        T Execute<T>(Func<T> func) where T : class;
    }
}