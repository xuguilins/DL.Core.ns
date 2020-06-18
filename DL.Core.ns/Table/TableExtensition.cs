using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DL.Core.ns.Table
{
    /// <summary>
    /// 表格的扩展
    /// </summary>
    public static class TableExtensition
    {
        /// <summary>
        /// 对象集合转DataTable
        /// </summary>
        /// <typeparam name="T">对象,必须是类(class)</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DataTable ToTable<T>(this List<T> obj) where T : class, new()
        {
            var type = obj.GetType();
            if (type.GetGenericTypeDefinition() == typeof(List<>))
            {
                DataTable dt = new DataTable();
                //设置表格的头部
                var model = new T();
                var list = model.GetType().GetProperties();
                foreach (var item in list)
                {
                    dt.Columns.Add(new DataColumn { ColumnName = item.Name });
                }
                //设置行数据
                foreach (T item in obj)
                {
                    DataRow row = dt.NewRow();
                    foreach (var colume in list)
                    {
                        var value = colume.GetValue(item);
                        row[colume.Name] = value;
                    }
                    dt.Rows.Add(row);
                }
                return dt;
            }
            else
            {
                throw new Exception($"当前对象不是List<>类型");
            }
        }

        /// <summary>
        /// 对象转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DataTable ToTable<T>(this T obj) where T : class, new()
        {
            var type = obj.GetType();
            if (type.IsClass)
            {
                DataTable dt = new DataTable();
                //设置表格的头部
                var model = new T();
                var list = model.GetType().GetProperties();
                foreach (var item in list)
                {
                    dt.Columns.Add(new DataColumn { ColumnName = item.Name });
                }
                DataRow row = dt.NewRow();
                foreach (var colume in list)
                {
                    var value = colume.GetValue(obj);
                    row[colume.Name] = value;
                }
                dt.Rows.Add(row);
                return dt;
            }
            else
            {
                throw new Exception($"当前对象不是List<>类型");
            }
        }

        /// <summary>
        /// DataTable转对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToObjectList<T>(this DataTable dt) where T : class, new()
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                List<T> list = new List<T>();
                T model = null;
                var proList = model.GetType().GetProperties();
                foreach (DataRow row in dt.Rows)
                {
                    model = new T();
                    foreach (var pro in proList)
                    {
                        if (dt.Columns.Contains(pro.Name))
                        {
                            pro.SetValue(model, row[pro.Name], null);
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// DataTable转实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ToObject<T>(this DataTable dt) where T : class, new()
        {
            var model = new T();
            //获取实体中所有属性
            var portity = model.GetType().GetProperties();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in portity)
                    {
                        if (dt.Columns.Contains(item.Name))
                        {
                            //取出值
                            var value = row[item.Name];
                            if (value != DBNull.Value)
                            {
                                //赋值
                                item.SetValue(model, value, null);
                            }
                        }
                    }
                }
                return model;
            }
            else
            {
                return model;
            }
        }
    }
}