// <copyright file="IBaseServices.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace IServices.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBaseServices<TEntity> where TEntity : class
    {
        #region 2.0  查询相关方法

        /// <summary>
        /// 根据labmda表达式进行查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where);

        List<TEntity> QueryWhere();
        /// <summary>
        /// 连表操作
        /// </summary>
        /// <param name="where"></param>
        /// <param name="tableNames"></param>
        /// <returns></returns>
        List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames);
      

        /// <summary>
        /// 按照条件查询出数据以后，根据外部指定的字段进行升序排列
        /// </summary>
        /// <typeparam name="TKey">表示从TEntity中获取的属性类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序lambda表达式</param>
        /// <returns></returns>
        List<TEntity> QueryOrderBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order);
       
        /// <summary>
        /// 按照条件查询出数据以后，根据外部指定的字段进行倒序排列
        /// </summary>
        /// <typeparam name="TKey">表示从TEntity中获取的属性类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序lambda表达式</param>
        /// <returns></returns>
        List<TEntity> QueryOrderByDescending<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order);
        TEntity Find(object[] keys);

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
        List<TEntity> QueryByPage<TKey>(int pageindex, int pagesize, out int rowcount, Expression<Func<TEntity, TKey>> order, Expression<Func<TEntity, bool>> where);

        List<TEntity> QuerySplitPage(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int pageSize = 10, int pageIndex = 1);

        List<TEntity> QueryWhereDesc(Expression<Func<TEntity, bool>> where,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order);
            #endregion

        #region 3.0  编辑相关方法

        void Edit(TEntity model, string[] propertys);

        void Update(TEntity model);

        int Count();

        int Count(Expression<Func<TEntity, bool>> where);

        #endregion

        #region 4.0  删除相关方法
        void DeleteList(IEnumerable<TEntity> model);
        void Delete(TEntity model, bool isadded);

       


        #endregion

        #region 5.0  新增相关方法

        void Add(TEntity model);

        void AddRange(List<TEntity> models);

        #endregion


        #region 6.0  统一提交

        int SaveChanges();
        void SaveChangesAsync();

        #endregion
    }
}
