using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.CommandFactory
{
    public class CommandExecutor : ICommandExecutor
    {
        public T Execute<T>(ICommand<T> command) where T : class
        {
            return command.Execute();
        }
    }
}