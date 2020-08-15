using DL.Core.ns.Entity;
using DL.Core.ns.Finder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.ns.EFCore
{
    public abstract class DbContextBase<TDContext> : DbContext where TDContext : DbContext
    {
        public DbContextBase()
        {
        }

        public abstract string ConnectionString { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var type = GetType();
            IEntityTypeConfiguraFinder finder = new EntityTypeConfiguraFinder();
            var entityList = finder.FinderAll().ToList();
            List<IEntityTypeRegiest> list = entityList.Select(type => Activator.CreateInstance(type) as IEntityTypeRegiest).ToList();
            var data = list.Where(x => x.DbContextType == type).ToList();
            foreach (IEntityTypeRegiest regist in data)
            {
                builder.Entity(regist.EntityType);
            }
            RegistConfiguration(builder);
            base.OnModelCreating(builder);
        }

        public abstract void RegistConfiguration(ModelBuilder builder);
    }
}