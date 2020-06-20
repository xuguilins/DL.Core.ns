using DL.Core.ns.Entity;
using DL.Core.ns.Extensiton;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DL.Core.ns.EFCore
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        private DbContext _dbContext;
        private DbSet<TEntity> DbSet = null;

        public Repository(DbContext context)
        {
            _dbContext = context;
            DbSet = _dbContext.Set<TEntity>();
        }

        #region [同步方法]

        /// <summary>
        ///跟踪查询实体
        /// </summary>
        public IQueryable<TEntity> TrackEntities => DbSet.AsQueryable();

        /// <summary>
        /// 非跟踪查询实体
        /// </summary>
        public IQueryable<TEntity> Entities => DbSet.AsNoTracking().AsQueryable();

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int AddEntity(TEntity entity)
        {
            DbSet.Add(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int UpdateEntity(TEntity entity)
        {
            DbSet.Update(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int RemoveEntity(TEntity entity)
        {
            DbSet.Remove(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 根据指定表达式获取实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity GetEntityByExpression(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.FirstOrDefault(expression);
        }

        #endregion [同步方法]

        #region [异步方法]

        /// <summary>
        /// 异步新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public async Task<int> AddEntityAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步更新更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public async Task<int> UpdateEntityAsync(TEntity entity)
        {
            DbSet.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步移除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public async Task<int> RemoveEntityAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步根据指定表达式获取实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<TEntity> GetEntityByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }

        #endregion [异步方法]
    }
}