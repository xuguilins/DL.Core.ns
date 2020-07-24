using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.ns.Locator;

namespace DL.Core.ns.EFCore
{
    public static class DbContextManager
    {
        private static DbContext _dbcontext;

        public static DbContext GetDbContext()
        {
            return _dbcontext;
        }

        public static void SetDbContext<T>() where T : DbContext
        {
            var dbContext = ServiceLocator.Instance.GetService(typeof(T));
            if (typeof(DbContext).IsAssignableFrom(dbContext.GetType()))
            {
                _dbcontext = dbContext as DbContext;
            }
        }
    }
}