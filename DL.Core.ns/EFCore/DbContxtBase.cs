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
        public abstract string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            IEntityFinder finder = new EntityFinder();
            var typeList = finder.FinderAll();
            foreach (var item in typeList)
            {
                builder.Entity(item);
            }
            base.OnModelCreating(builder);
        }
    }
}