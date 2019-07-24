// <copyright file="LoginController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Posco.DC.SCADA.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using CommonHelper;
    using IServices;
    using Model.ModelViews;
    using Posco.DC.WebHelper;
    using System.Threading;
    using Newtonsoft.Json;

    [SkipCheckLogin]
    public class LoginController : BaseController
    {
        // 通过DI的方式,获取SB_User类
        private ISB_UserServices userSer;
        private ISB_OperateHistoryServices operathisSer;
        private IBB_ChartAliasServices chartAliasSer;
        private IAP_Factory_HistoryServices hisSer;
        private IAP_Station_HistoryServices shis;
        private IAP_Centrifuge_HistoryServices cenhis;
        private IBB_AreaServices area;
        public LoginController(ISB_UserServices userService,
            ISB_OperateHistoryServices operaeHistoryServices,
            IBB_ChartAliasServices chartAliasServices,
            IAP_Factory_HistoryServices hisser, IAP_Station_HistoryServices shiss,
            IAP_Centrifuge_HistoryServices cenhiss, IBB_AreaServices areaa)
        {
            userSer = userService;
            operathisSer = operaeHistoryServices;
            chartAliasSer = chartAliasServices;
            hisSer = hisser;
            shis = shiss;
            cenhis = cenhiss;
            area = areaa;
        }
        /// <summary>
        /// Get方式 页面展示
        /// </summary>
        /// <returns></returns>
        // GET: Login
        [HttpGet]
        public ActionResult Login(int Number = 0)
        {
            if (Number == 1)
            {
                Model.SB_User model = (Model.SB_User)Session[Keys.userInfo];
                new OperatRecord().operating(null, "0", "2", "/Home/MainPage", model);
            }


            Model.ModelViews.LoginInfo userinfo = new LoginInfo();
            if (Request.Cookies["UserName"] != null)
            {
                var namecookie = Request.Cookies["UserName"];
                userinfo.ULoginName = namecookie.Value;
            }

            Session["URL"] = "";
            return View(userinfo);
        }

        /// <summary>
        /// 负责接收页面提交过来的数据进行登录处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginInfo model)
        {
            //try
            //{
            // 实体参数合法性验证
            if (ModelState.IsValid == false)
            {
                return WriteError("实体验证失败");
            }

            // 检查用户名和密码的正确性
            var userinfo = userSer.QueryWhere(u => u.Account == model.ULoginName && u.PassWord == model.ULoginPWD).FirstOrDefault();

            if (userinfo == null)
            {
                return WriteError("用户名或者密码错误");
            }

            var c = operathisSer.OperatRecordAdd(null, Convert.ToInt32(Enums.ChildType.登录).ToString(), Session["URL"].ToString(), userinfo, null);
            if (c > 0)
            {
                // 将userinfo存入session
                Session[Keys.userInfo] = userinfo;
                //设置SESSION时间
                Session.Timeout = 20;
                //获取IP
                string ip = GetIpHelper.GetWebClientIp();
                //将ip放入session
                Session[Keys.userIP] = ip;
                //设置cookie内容和时效
                HttpCookie UserInfoCookies = new HttpCookie("UserName");
                UserInfoCookies.Value = model.ULoginName;
                UserInfoCookies.Expires = DateTime.Now.AddDays(20);
                Response.Cookies.Add(UserInfoCookies);
                HttpCookie UserPwdCookies = new HttpCookie("UserPwd");
                UserPwdCookies.Value = model.ULoginPWD;
                UserPwdCookies.Expires = DateTime.Now.AddDays(20);
                Response.Cookies.Add(UserPwdCookies);
                // 返回登录成功消息
                // return WriteSuccess("登录成功");
                var chartAliasList = chartAliasSer.QueryWhere();
                CacheMgr.SetData("chartAliasList", chartAliasList);

                //return RedirectToAction("MainPage", "Home");
                return RedirectToAction("Index", "Home");
            }


            return View(model);



            //}
            //catch (Exception ex)
            //{
            //    // 返回异常
            //    return WriteError(ex);
            //}
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Verification()
        {
            Verification val = new Verification();
            //获得四位的验证码
            string valcode = val.CreateValidateCode(4);
            //把验证码放入Session
            Session["validatecode"] = valcode;
            HttpCookie cookie = new HttpCookie("validatecode");
            //把验证码放入客户端的cookie
            cookie.Value = valcode;
            Response.Cookies.Add(cookie);
            //得到图片字节数组
            byte[] images = val.CreateValidateGraphic(valcode);
            return File(images, "image/jpeg");
        }
        public ActionResult JsonTest(string aaa)
        {
            string nowstr = Session["validatecode"].ToString();
            if (aaa.Equals(nowstr, StringComparison.OrdinalIgnoreCase))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        #region  登出

        [HttpGet]
        public ActionResult Logout()
        {
            // 清空Session[Keys.uinfo]对象
            Session[Keys.userInfo] = null;

            // 跳转到登录页面
            return RedirectToAction("Login", "Login");
        }

        #endregion
    }
}