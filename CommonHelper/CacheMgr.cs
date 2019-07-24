// <copyright file="CacheMgr.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CommonHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

   public class CacheMgr
    {
        /// <summary>
        /// 根据cacheKey获取缓存的泛型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static T GetData<T>(string cacheKey)
        {
            return (T)HttpRuntime.Cache[cacheKey];
        }

        /// <summary>
        /// 存入的数据不过期(在IIS重启的时候才消失)
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="val"></param>
        public static void SetData(string cacheKey, object val)
        {
            HttpRuntime.Cache[cacheKey] = val;
        }
    }
}
