// <copyright file="Keys.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CommonHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

  public  class Keys
    {
        /// <summary>
        /// 用于存放登录成功的用户实体的session  key
        /// </summary>
        public const string userInfo = "userInfo";

        /// <summary>
        /// 用于存放登录成功以后的用户id的cookie key        
        /// </summary>
        public const string IsMember = "IsMember";

        /// <summary>
        /// 用于缓存整个autofac的容器对象的 缓存key
        /// </summary>
        public const string AutofacContainer = "autofacContainer";
        /// <summary>
        /// 用于客户IP
        /// </summary>
        public const string userIP = "userIP";
    }
}
