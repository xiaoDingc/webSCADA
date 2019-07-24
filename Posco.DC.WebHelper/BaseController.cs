// <copyright file="BaseController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Posco.DC.WebHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using CommonHelper;

    public class BaseController : Controller
    {
        #region  封装ajax请求的返回方法
        protected ActionResult WriteSuccess(string msg)
        {
            return Json(new { status = (int)Enums.EAjaxState.Sucess, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult WriteError(string errmsg)
        {
            return Json(new { status = (int)Enums.EAjaxState.Error, msg = errmsg }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult WriteError(Exception ex)
        {
            return Json(new { status = (int)Enums.EAjaxState.Error, msg = ex.Message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
