// <copyright file="PageSplitInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Model
{    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

/// <summary>
/// 分页标签信息公共类
/// </summary>
    public class PageSplitInfo
    {
        public PageSplitInfo(int pagesize, int pageindex, int totalrecord, string actionname, string controllername)
        {
            PageSize = pagesize;
            PageIndex = pageindex;
            TotalRecord = totalrecord;
            TotalPage = TotalRecord % PageSize == 0 ? TotalRecord / PageSize : TotalRecord / PageSize + 1;
            ActionName = actionname;
            ControllerName = controllername;
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRecord { get; set; }

        public int TotalPage { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }
    
}
}
