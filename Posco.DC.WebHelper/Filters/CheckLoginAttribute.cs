// <copyright file="CheckLoginAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Posco.DC.WebHelper.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Autofac;
    using CommonHelper;
    using System.Reflection;
    using Model;
   

    public  class CheckLoginAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var url = filterContext.HttpContext.Request.RawUrl.ToString().ToLower();
            var parames = filterContext.HttpContext.Request.QueryString.ToString();
           var bb= filterContext.RouteData.Values["controller"].ToString();
            var cc= filterContext.RouteData.Values["action"].ToString();
            var dd = filterContext.ActionParameters;
            filterContext.HttpContext.Session["URL"] = "/"+bb+"/"+cc;
            // 在Action上判断是否有跳过检查登陆的特性
            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckLogin),false))
            {
                return;
            }
            // 在Contorller上判断是否有跳过检查登陆的特性
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLogin),false))
            {
                return;
            }

            // 1.0 判断session是否为null
            if (filterContext.HttpContext.Session[Keys.userInfo] == null)
            {
                    // 跳转到登陆页面               
                    ToLogin(filterContext);
            }                        
        }

        /// <summary>
        /// 跳转到登陆界面
        /// </summary>
        /// <param name="filterContext"></param>
        private static void ToLogin(ActionExecutingContext filterContext)
        {
            // 对象初始化
            ViewResult view = new ViewResult() {ViewName = "/Views/Shared/Tip.cshtml"};
            filterContext.Result = view;

        }
    }
}
