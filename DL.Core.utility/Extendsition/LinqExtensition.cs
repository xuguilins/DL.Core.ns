using DL.Core.utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DL.Core.utility.Extendsition
{
    public static class LinqExtensition
    {
        /// <summary>
        /// where的扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="expression">表达式</param>
        /// <param name="result">如果为true，则执行上述表达式</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> data, Expression<Func<T, bool>> expression, bool result)
        {
            return result ? data.Where(expression) : data;
        }

        /// <summary>
        /// 排序的扩展
        /// </summary>
        /// <typeparam name="T">排序数据</typeparam>
        /// <typeparam name="TKey">lambda表达式</typeparam>
        /// <param name="data"></param>
        /// <param name="expression"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByPage<T, TKey>(this IQueryable<T> data, Expression<Func<T, TKey>> expression, DataSearch search)
        {
            if (search.SortAsc.CheckPamrsIsNotNull())
            {
                return data.OrderByDescending(expression).Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize);
            }
            else
            {
                if (search.SortAsc == "Asc")
                {
                    return data.OrderBy(expression).Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize);
                }
                else
                {
                    return data.OrderByDescending(expression).Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize);
                }
            }
        }
    }
}