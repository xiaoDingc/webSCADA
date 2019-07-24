// <copyright file="BaseRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Data.SqlClient;

namespace Repository.Base
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.Remoting.Messaging;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository.Base;
    using System.Data.SqlClient;

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private BaseDBContext Db
        {
            get
            {
                object obj = CallContext.GetData("BaseDBContext");
                if (obj == null)
                {
                    obj = new BaseDBContext();
                    CallContext.SetData("BaseDBContext", obj);
                }

                return obj as BaseDBContext;
            }
        }

        private DbSet<TEntity> _dbSet;
        public BaseRepository()
        {
            _dbSet = Db.Set<TEntity>();  
        }
        /// <summary>
        /// 不带参数查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DbSqlQuery<TEntity> QueryNoParams(string sql)
        {
            return _dbSet.SqlQuery(sql);
        }
        /// <summary>
        /// 带参数的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DbSqlQuery<TEntity> QueryParams(string sql, SqlParameter[] param)
        {
            return _dbSet.SqlQuery(sql, param);
        }
        #region 查询相关方法

        public IQueryable<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
        {
       
            return _dbSet.Where(where);
        }
        public TEntity Find(object[] keys)
        {
            return _dbSet.Find(keys);
        }
        public IQueryable<TEntity> QueryWhere()
        {
            return _dbSet;
        }
        public IQueryable<TEntity> QueryWhere(string str)
        {
            return _dbSet;
        }
        public IQueryable<TEntity> QueryWhereDesc(Expression<Func<TEntity, bool>> where,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order)
        {
            return order(_dbSet.Where(where));
        }
         
        public IQueryable<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] TableNames)
        {
            if (TableNames.Any())
            {
                throw new Exception("连表操作的表名称至少要有一个");
            }

            DbQuery<TEntity> query = _dbSet;
            foreach (var tableName in TableNames)
            {
                query = query.Include(tableName);
            }

            return query.Where(where);
        }

        public List<TEntity> QueryOrderBy(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, bool>> order)
        {
            return _dbSet.Where(where).OrderBy(order).ToList();
        }

        public IQueryable<TEntity> QueryOrderByDescending<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order)
        {
            return _dbSet.Where(where).OrderByDescending(order);
        }

        public IQueryable<TEntity> QueryByPage<TKey>(int pageindex, int pagesize, out int rowcount, Expression<Func<TEntity, TKey>> order, Expression<Func<TEntity, bool>> where)
        {
            // 1.0 计算当前分页要跳过的数据行数
            int skipCount = (pageindex - 1) * pagesize;

            // 2.0 获取当前满足条件的所有数据总条数
            rowcount = _dbSet.Count(where);

            // 3.0 获取分页数据
            return _dbSet.Where(where).OrderByDescending(order).Skip(skipCount).Take(pagesize);
        }

        public IQueryable<TEntity> QuerySplitPage(Expression<Func<TEntity,bool>> where, Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>order, int pageSize = 10,int pageIndex = 1)
        {
            var query = _dbSet.Where(where);
            if (order == null)
            {
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else {
                query = order(query);
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize); }
            
        }
       
        #endregion

        #region 3.0  编辑相关方法

        public void Edit(TEntity model, string[] propertys)
        {
            if (model == null)
            {
                throw new Exception("实体不能为空");
            }

            if (propertys.Any() == false)
            {
                throw new Exception("要修改的属性至少要有一个");
            }

            // 2.0 将model追击到EF容器
            System.Data.Entity.Infrastructure.DbEntityEntry entry = Db.Entry(model);

            entry.State = EntityState.Unchanged;
            foreach (var item in propertys)
            {
                entry.Property(item).IsModified = true;
            }

            // 3.0 关闭EF对于实体的合法性验证参数
            Db.Configuration.ValidateOnSaveEnabled = false;
        }
        /// <summary>
        /// 更改实体信息
        /// </summary>
        /// <param name="model"></param>
        public void Update(TEntity model)
        {
            _dbSet.Attach(model);
            Db.Entry<TEntity>(model).State = EntityState.Modified;
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public int Count(Expression<Func<TEntity,bool>> where)
        {
            return _dbSet.Count(where);

        }

        #endregion

        #region 4.0  删除相关方法

        public void Delete(TEntity model, bool isadded)
        {
            // (!isadded)表示当前model没有追加到EF容器中
            
                _dbSet.Attach(model);
            
            _dbSet.Remove(model);
        }      
        public void DeleteList(IEnumerable<TEntity> model)
        {
            _dbSet.RemoveRange(model);
        }


        #endregion

        #region 5.0  新增相关方法

        public void Add(TEntity model)
        {
            _dbSet.Add(model);
        }

        public void AddRange(List<TEntity> models)
        {
            _dbSet.AddRange(models);
        }

        #endregion

        #region 6.0  统一提交

        public int SaveChanges()
        {           
           return Db.SaveChanges();
        }        
        public void SaveChangesAsync()
        {
            Db.SaveChangesAsync();
        }

        public List<TEntity> QueryOrderBy<Tkey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> order)
        {
            return _dbSet.Where(where).OrderBy(order).ToList();
        }
        #endregion

    }
}