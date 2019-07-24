// <copyright file="BaseServices.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Services.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using IRepository.Base;
    using IServices.Base;
    using Repository;

    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        // 1.0 定义数据仓储的接口
        public IBaseRepository<TEntity> baseDal;

        #region 2.0  查询相关方法

        /// <summary>
        /// 根据labmda表达式进行查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
        {
            return baseDal.QueryWhere(where).ToList();
        }

        public List<TEntity> QueryWhere()
        {
            return baseDal.QueryWhere().ToList();
        }
        /// <summary>
        /// 连表操作
        /// </summary>
        /// <param name="where"></param>
        /// <param name="tableNames"></param>
        /// <returns></returns>
        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            return baseDal.QueryJoin(where, tableNames).ToList();
        }

        /// <summary>
        /// 按照条件查询出数据以后，根据外部指定的字段进行升序排列
        /// </summary>
        /// <typeparam name="TKey">表示从TEntity中获取的属性类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序lambda表达式</param>
        /// <returns></returns>
        public List<TEntity> QueryOrderBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order)
        {
            return baseDal.QueryOrderBy(where, order);
        }
        public TEntity Find(object[] keys)
        {
            return baseDal.Find(keys);
        }
        /// <summary>
        /// 按照条件查询出数据以后，根据外部指定的字段进行倒序排列
        /// </summary>
        /// <typeparam name="TKey">表示从TEntity中获取的属性类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序lambda表达式</param>
        /// <returns></returns>
        public List<TEntity> QueryOrderByDescending<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order)
        {
            return baseDal.QueryOrderByDescending(where, order).ToList();
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <typeparam name="TKey">要指定的排序属性名称 Tentity.property</typeparam>
        /// <param name="pageindex">分页页码</param>
        /// <param name="pagesize">页容量</param>
        /// <param name="rowcount">总行数</param>
        /// <param name="order">排序lambda表达式</param>
        /// <param name="where">查询条件lambda表达式</param>
        /// <returns></returns>
        public List<TEntity> QueryByPage<TKey>(int pageindex, int pagesize, out int rowcount, Expression<Func<TEntity, TKey>> order, Expression<Func<TEntity, bool>> where)
        {
            return baseDal.QueryByPage(pageindex, pagesize, out rowcount, order, where).ToList();
        }

        public List<TEntity> QuerySplitPage(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int pageSize = 10, int pageIndex = 1)
        {
            return baseDal.QuerySplitPage(where, order, pageSize, pageIndex).ToList();
        }
        public List<TEntity> QueryWhereDesc(Expression<Func<TEntity, bool>> where,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order)
        {
            return baseDal.QueryWhereDesc(where,order).ToList();
        }
        #endregion

        #region 3.0  编辑相关方法

        public void Edit(TEntity model, string[] propertys)
        {
            baseDal.Edit(model, propertys);
        }

        public void Update(TEntity model)
        {
            baseDal.Update(model);
        }

        public int Count()
        {
            return baseDal.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return baseDal.Count(where);
        }
        #endregion

        #region 4.0  删除相关方法

        public void Delete(TEntity model, bool isadded)
        {
            baseDal.Delete(model, isadded);
        }
        public void DeleteList(IEnumerable<TEntity> model)
        {
            baseDal.DeleteList(model);
        }
        #endregion

        #region 5.0  新增相关方法

        public void Add(TEntity model)
        {
            baseDal.Add(model);
        }

        public void AddRange(List<TEntity> models)
        {
            baseDal.AddRange(models);
        }


        #endregion

        #region 6.0  统一提交

        public int SaveChanges()
        {
           return baseDal.SaveChanges();
        }      

        public void SaveChangesAsync()
        {
            baseDal.SaveChangesAsync();
        }





        #endregion
    }
}
