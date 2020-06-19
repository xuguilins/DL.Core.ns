using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ns.Data
{
    /// <summary>
    /// 内置数据库上下文管理器
    /// </summary>
    public class DataBaseDbContextManager : IDataBaseDbContextManager
    {
        private ConcurrentDictionary<Type, IDataBaseContext> _contexts = new ConcurrentDictionary<Type, IDataBaseContext>();

        public IDataBaseContext GetDataBaseDbContext(Type type)
        {
            IDataBaseContext dbContext = null;
            if (_contexts.ContainsKey(type))
                return _contexts[type];
            var instance = Activator.CreateInstance(type) as IDataBaseContext;
            if (instance == null)
                throw new Exception($"创建上下文实例发生异常，请检查当前上下文 “{type}”是否实现 IDataBaseContext");
            switch (instance.Type)
            {
                case DataBaseType.SqlServer:
                    dbContext = new SqlServerDbContext();
                    break;

                case DataBaseType.MySql:
                    dbContext = new MySqlDbContext();
                    break;

                case DataBaseType.Oracle:
                    dbContext = new OracleDbContext();
                    break;
            }
            _contexts.TryAdd(type, dbContext);
            return dbContext;
        }
    }
}