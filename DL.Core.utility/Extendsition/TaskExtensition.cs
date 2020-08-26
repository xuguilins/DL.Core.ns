using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DL.Core.utility.Extendsition
{
    public static class TaskExtensition
    {
        public static Task<T> toTask<T>(this T data)
        {
            return Task.FromResult(data);
        }
    }
}