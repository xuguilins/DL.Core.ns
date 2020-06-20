﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.CommandFactory
{
    /// <summary>
    /// 命令执行者
    /// </summary>
    public interface ICommandExecutor
    {
        T Execute<T>(ICommand<T> command) where T : class;
    }
}