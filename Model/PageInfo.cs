// <copyright file="PageInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 分页泛型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInfo<T>
    {
        // 分页标签信息
        public PageSplitInfo PageSplit { get; set; }

        // 分页数据
        public IEnumerable<T> DataModel { get; set; }
        public int showPageSize { get; set; }
        public int type { get; set; }
        public string time { get; set; }
        public string time1 { get; set; }
    }
}
