using DL.Core.ns.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.EFCore
{
    public interface IEntityTypeRegiest
    {
        /// <summary>
        /// 指定数据库上下文类型
        /// </summary>
        Type DbContextType { get; }

        Type EntityType { get; }

        void ModelConfigBuild(ModelBuilder builder);
    }

    public abstract class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityTypeRegiest where TEntity : EntityBase
    {
        public ConfigurationBase()
        {
            //DbContextOptions p =
            //  DbContextOptionsBuilder dbContextOptions = new DbContextOptionsBuilder(DbContextOptions)
        }

        public abstract Type DbContextType { get; }
        public Type EntityType => typeof(TEntity);

        public void ModelConfigBuild(ModelBuilder builder)
        {
            builder.ApplyConfiguration(this);
        }

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}