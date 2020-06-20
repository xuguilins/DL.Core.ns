using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DL.Core.ns.EFCore
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 是否开启事务
        /// </summary>
        bool BeginTransaction { get; set; }

        /// <summary>
        /// 事务同步提交
        /// </summary>
        /// <returns></returns>
        bool CommitTransaction();

        /// <summary>
        /// 事务异步提交
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitTransactionAsync();
    }
}