// <copyright file="IBaseRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace IRepository.Base
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region 查询相关方法
        DbSqlQuery<TEntity> QueryNoParams(string sql);
        DbSqlQuery<TEntity> QueryParams(string sql, params SqlParameter[] param);
        IQueryable<TEntity> QueryWhere();
        IQueryable<TEntity> QueryWhere(string str);
        IQueryable<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] TableNames);
        TEntity Find(object[] keys);
        List<TEntity> QueryOrderBy<Tkey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> order);
        IQueryable<TEntity> QueryOrderByDescending<TKey>(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TKey>> order);
        IQueryable<TEntity> QueryByPage<TKey>(int pageindex, int pagesize, out int rowcount,
            Expression<Func<TEntity, TKey>> order, Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> QuerySplitPage(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, int pageSize = 10, int pageIndex = 1);
        IQueryable<TEntity> QueryWhereDesc(Expression<Func<TEntity, bool>> where,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order);
     
        #endregion

        #region 3.0  编辑相关方法

        void Edit(TEntity model, string[] propertys);

        void Update(TEntity model);

        int Count();

        int Count(Expression<Func<TEntity, bool>> where);

        #endregion

        #region 4.0  删除相关方法

        void Delete(TEntity model, bool isadded);

        void DeleteList(IEnumerable<TEntity> model);
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
