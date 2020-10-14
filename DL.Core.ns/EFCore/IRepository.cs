using DL.Core.utility.Dependency;
using DL.Core.utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DL.Core.ns.EFCore
{
    [IgnoreDependency]
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// 当前工作单元对象
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }
        #region [同步方法]

        /// <summary>
        ///跟踪查询实体
        /// </summary>
        IQueryable<TEntity> TrackEntities { get; }

        /// <summary>
        /// 非跟踪查询实体
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        int AddEntity(TEntity entity);

        /// <summary>
        /// 新增批量实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int AddEntityItems(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        int UpdateEntity(TEntity entity);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        int RemoveEntity(TEntity entity);

        /// <summary>
        /// 根据指定表达式获取实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        TEntity GetEntityByExpression(Expression<Func<TEntity, bool>> expression);

        #endregion [同步方法]

        #region [异步方法]

        /// <summary>
        /// 异步新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        Task<int> AddEntityAsync(TEntity entity);

        /// <summary>
        /// 异步新增批量实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> AddEntityItemsAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步更新更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        Task<int> UpdateEntityAsync(TEntity entity);

        /// <summary>
        /// 异步移除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        Task<int> RemoveEntityAsync(TEntity entity);

        /// <summary>
        /// 异步根据指定表达式获取实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> GetEntityByExpressionAsync(Expression<Func<TEntity, bool>> expression);

        #endregion [异步方法]
    }
}