// <copyright file="Global.asax.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Posco.DC.SCADA
{
    using StackExchange.Profiling.EntityFramework6;
    using StackExchange.Profiling.Mvc;
    using StackExchange.Profiling;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //监测速度
            MiniProfilerEF6.Initialize();
            // 利用autofac实现mvc项目的IOC和DI
            AutofacConfig.Register();
            // 注册区域路由规划
            AreaRegistration.RegisterAllAreas();
            // 注册webapi的路由规则
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // 注册全局过滤器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // 注册网站路由
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // 优化js和css
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //日志文件
            log4net.Config.XmlConfigurator.Configure();

        }
        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)//这里是允许本地访问启动监控,可不写
            {
                MiniProfiler.Start();

            }
        }
        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

    }
}

