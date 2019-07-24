// <copyright file="HomeController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Net.Sockets;
using System.Text;
using System.Threading;
using Services;

namespace Posco.DC.SCADA.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using CommonHelper;
    using IServices;
    using Model;
    using Newtonsoft.Json;
    using Posco.DC.WebHelper;
    using NPOI.HSSF.UserModel;
    using System.IO;
    using System.Diagnostics;

    //[SkipCheckLogin]
    public class HomeController : Controller
    {
        private ISB_UserServices userSer;
        private IRE_SysRoleUserServices sysroleuserSer;
        private IRE_SysRolePageServices sysrolepageSer;
        private ISB_PageServices pageSer;
        private ISB_SysRoleServices sysroleSer;
        private IBB_ThresholdServices thresholdSer;
        private IBB_InstrumentServices instrumentSer;
        private ISB_WarnLogServices warnlogSer;
        private IAP_Factory_RealServices factory_realSer;
        private IAP_Factory_HistoryServices factory_hisSer;
        private IAP_Factory_StatisticsServices factory_staticSer;
        private IAP_Station_RealServices station_realSer;
        private IAP_Centrifuge_RealServices centri_realSer;
        private IAP_Centrifuge_StatisticsServices centri_staticSer;
        private IPG_Area_RealServices area_realSer;
        private IAP_Station_HistoryServices stahisSer;
        private IPG_Area_HistoryServices areahisSer;
        private IAP_Centrifuge_HistoryServices cenhisSer; 
          private IOpcHelperItemServices opcHelperItemServices;
        private IUPIEnergyServices upienergySer;

        //删除opc内容
        // OPC opc = new OPC();
        // List<OpcHelperItem> opcItemList = new List<OpcHelperItem>();

        public HomeController(ISB_UserServices userser, IRE_SysRoleUserServices sysser, IRE_SysRolePageServices syspageser,
            ISB_PageServices pser, ISB_SysRoleServices sroleser, IAP_Factory_RealServices factoryser,
            IPG_Area_RealServices areaser, IBB_InstrumentServices InstrumentSer, IBB_ThresholdServices thresholdser, ISB_WarnLogServices warnlogser,
            IAP_Factory_HistoryServices factory_hisser, IAP_Factory_StatisticsServices factory_staticser,
            IAP_Station_RealServices station_realser, IAP_Centrifuge_RealServices centri_realser,
            IAP_Centrifuge_StatisticsServices centri_staticser, IAP_Station_HistoryServices stahisser,
            IPG_Area_HistoryServices areahisser, IAP_Centrifuge_HistoryServices cenhisser, IOpcHelperItemServices opcHelperItemSer,
            IUPIEnergyServices upienergyser)
        {
            userSer = userser;
            sysroleuserSer = sysser;
            sysrolepageSer = syspageser;
            pageSer = pser;
            sysroleSer = sroleser;
            instrumentSer = InstrumentSer;
            thresholdSer = thresholdser;
            warnlogSer = warnlogser;
            factory_realSer = factoryser;
            factory_hisSer = factory_hisser;
            factory_staticSer = factory_staticser;
            station_realSer = station_realser;
            centri_realSer = centri_realser;
            centri_staticSer = centri_staticser;
            area_realSer = areaser;
            stahisSer = stahisser;
            areahisSer = areahisser;
            cenhisSer = cenhisser;
            opcHelperItemServices = opcHelperItemSer;
            upienergySer = upienergyser;
        }
         
        //分配人员
        public ActionResult Member()
        {
            return View();
        }
        //编辑人员
        public ActionResult Menu()
        {
            return View();
        }
        //添加人员
        public ActionResult AddMenu()
        {
            return View();
        }
        //创建新角色
        public ActionResult NewMenu()
        {
            return View();
        }
        public ActionResult CharMenu()
        {
            return View();
        }

        public ActionResult Page()
        {
            return View();
        }     
        /// <summary>
        /// 大数据主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //当前时间
            var currtime = DateTime.Now;
            //六分钟前时间
            
            var time= DateTime.Parse("2019-06-27 00:00:00");
            var trantime = (DateTime.Now - time).Days;
            var hiscurrtime = DateTime.Now;
            var hisbegtime = hiscurrtime.AddDays(-1);
            var hissecond = hiscurrtime.AddSeconds(-24);
            var hisHour = hiscurrtime.AddHours(-1);
            var hismonthtime = hiscurrtime.AddDays(-(hiscurrtime.Day - 1)).Date;
           var hisdate= hiscurrtime.AddMinutes(-1);
            var hisFiveM = hiscurrtime.AddMinutes(-5);
           
            var gasprores = station_realSer.GasProSer(hiscurrtime, hissecond);
            //用气图
            var gasuseres = area_realSer.GasUseSer(hiscurrtime, hissecond);
            //瞬时流量图
            var momres = factory_realSer.MomentSer(hiscurrtime, hissecond, hismonthtime);
            //upi放散率散点图
            var scatterres = factory_realSer.ScatterSer(hiscurrtime, hisbegtime);
            //放散率分布图
            double[] zzz = new double[] { 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12, 12.5, 13, 13.5, 14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19, 19.5, 20, 20.5, 21, 21.5, 22, 25 };
            var freqres = factory_realSer.FrequencySer(hiscurrtime, hisbegtime, zzz);
            //蜘蛛图
            var spidres = station_realSer.SprideSer(hiscurrtime, hisFiveM);
            //供应能力需求气量流量功率图
            var supplystacksumpowerqp = factory_hisSer.SupplyStackSumPowerpq(hiscurrtime, hisHour);
            //开关图
            var swires = centri_realSer.SwitchStackSer(hiscurrtime, hissecond);
            var Viewtran = new
            {
                gaspro = gasprores,
                gasuse = gasuseres,
                mom = momres,
                scatter = scatterres,
                freq = freqres,
                suppstack=supplystacksumpowerqp,
                spid = spidres,
                swi = swires
            };
            ViewBag.tran = JsonConvert.SerializeObject(Viewtran);
            return View();
        }
     
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        //后台管理
        //客户列表-添加客户
        public ActionResult addClient()
        {
            return View();
        }
        //客户列表-编辑客户
        public ActionResult editClient()
        {
            return View();
        }
        //人员列表-添加人员
        public ActionResult addUser()
        {
            return View();
        }
        //人员列表-编辑人员
        public ActionResult editUser()
        {
            return View();
        }
        //工厂列表-添加人员
        public ActionResult addFactory()
        {
            return View();
        }
        //工厂列表-编辑人员
        public ActionResult editFactory()
        {
            return View();
        }

        /// <summary>
        /// 菜单主页显示
        /// </summary>
        /// <returns></returns>
        public ActionResult MainPage()
        {
            int alarmCount = warnlogSer.QueryWhere(w => w.Status.Equals("0")).GroupBy(g => g.WarnType).Count();
                ViewBag.count = alarmCount;
            // 通过Session获得登录后的用户的ID
             SB_User users = (SB_User)Session[Keys.userInfo];
                ViewBag.userInfo = users.Account;
            ViewBag.userId = users.ID;
            // 通过用户的ID查询用户角色表
            var userRolesDistinct = sysroleuserSer.QueryWhere(s => s.UserID == users.ID).Distinct();
            //已获得角色集合,通过角色id去获得角色权限表，多个角色的话，权限可能有重复
            //获取系统校色页面表
            var rolePages = sysrolepageSer.QueryWhere(x => true);
            //获取页面表
            var pages = pageSer.QueryWhere(x => true).OrderBy(x => x.SortN).ToList();
            //通过系统角色ID查询出该角色页面  
            var userPages = userRolesDistinct.Join(rolePages, x => x.SysRoleID, y => y.SysRoleID, (x, y) =>
                   new
                   {
                       x.UserID,
                       x.SysRoleID,
                       y.PageID
                   }).Distinct();
            //获取该角色所有页面
            var query = pages.Join(userPages, x => x.ID, y => y.PageID, (x, y) => x).GroupBy(g=>g.ID).Select(s=>s.FirstOrDefault()).ToList();
            return View(query);
        }
        /// <summary>
        /// 个人信息修改界面
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ActionResult UserSingle(Guid account)
        {
            Model.ViewObject.Sex sex1 = new Model.ViewObject.Sex("1", "男");
            Model.ViewObject.Sex sex2 = new Model.ViewObject.Sex("0", "女");
            List<Model.ViewObject.Sex> sexList = new List<Model.ViewObject.Sex>();
            sexList.Add(sex1);
            sexList.Add(sex2);
            SelectList selectlistSex = new SelectList(sexList, "numstring", "showstring");
            ViewBag.Sex = selectlistSex;
            Model.BussionObject.UserShow_BO res =userSer.ModiUser(account);
            return View(res);
        }
        /// <summary>
        /// 个人信息修改界面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserSingle(Model.BussionObject.UserShow_BO model)
        {
            try
            {
                Model.ViewObject.Sex sex1 = new Model.ViewObject.Sex("1", "男");
                Model.ViewObject.Sex sex2 = new Model.ViewObject.Sex("0", "女");
                List<Model.ViewObject.Sex> sexList = new List<Model.ViewObject.Sex>();
                sexList.Add(sex1);
                sexList.Add(sex2);
                SelectList selectlistSex = new SelectList(sexList, "numstring", "showstring");
                ViewBag.Sex = selectlistSex;
                if (ModelState.IsValid)
                {
                    var cres = userSer.ValidAccount(model.boAccount);
                    if (cres > 0)
                    {
                        ViewBag.cc = JsonConvert.SerializeObject(cres);
                        return View(model);
                    }
                    else
                    {
                        int res = userSer.ModiUser(model);
                        var userinfo = userSer.Find(new object[] { model.boID });
                        Session[Keys.userInfo] = userinfo;
                        return RedirectToAction("UserSingle", "Home", new { account = model.boID });
                    }
                   
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
            
            
        }
        /// <summary>
        /// 权限界面显示
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfigPre()
        {          
            List<Model.ModelViews.MainPage_View> pageList = new List<Model.ModelViews.MainPage_View>();           
            var queryparentList = pageSer.QueryWhere(p => p.ParentID == 0);
            foreach(var item in queryparentList)
            {
                var res = pageSer.QueryWhere(p => p.ParentID == item.ID);
                Model.ModelViews.MainPage_View view = new Model.ModelViews.MainPage_View() {
                        childPage=res,
                        Page=item
                };
                pageList.Add(view);                                               
            }
            Model.VO.AuthorityPage_VO authorityPage = new Model.VO.AuthorityPage_VO();
            authorityPage.SysRole = sysroleSer.QueryWhere();
            authorityPage.viewList = pageList;
            return View(authorityPage);
        }
        public ActionResult TestConfig()
        {
            return View();
        }
        /// <summary>
        /// 角色拥有人员
        /// </summary>
        /// <param name="Roleid"></param>
        /// <returns></returns>
        public ActionResult ConfigPreRoleUser(int Roleid,int page,int limit,string account)
        {
            if (account =="")
            {
                account = null;
            }
            var res=sysroleuserSer.RoleUser(Roleid,page,limit,account);
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 角色拥有权限
        /// </summary>
        /// <param name="Roleid"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ActionResult ConfigPreRolePage(int Roleid,int page,int limit,string showname)
        {
            if (showname == "")
            {
                showname = null;
            }
            var res = sysrolepageSer.RolePage(Roleid,page,limit,showname);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加新的角色名称
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public ActionResult RoleList(string str)
        {
           var res= sysroleSer.AddRole(str);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改角色名称
        /// </summary>
        /// <param name="str"></param>
        /// <param name="newStr"></param>
        /// <returns></returns>
        public ActionResult RoleModiInfo(string str, string newStr)
        {
            Model.SB_SysRole role = sysroleSer.QueryWhere(ss => ss.Name == str).FirstOrDefault();
            role.Name = newStr;
            sysroleSer.Update(role);
            sysroleSer.SaveChanges();
            return Json(role.Name, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 角色人员删除
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult AjaxDelete(int roleid,string uaccount)
        {
           var res= sysroleuserSer.DeleteRoleUser(uaccount, roleid);
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 角色权限删除
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public ActionResult AjaxRoleDelete(int roleid,int pageId,int parentId)
        {
           var res= sysrolepageSer.RolePageDelete(roleid, pageId, parentId);
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 角色增加人员
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public ActionResult AddUserLayer(int id)
        {
            ViewBag.rr = id;
            return View() ;
        }
        /// <summary>
        /// 角色增加人员表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public ActionResult AddUserLayerJson(int limit, int page,int roleid = 1)
        {
            var res = sysroleuserSer.AddRoleUser(roleid,limit,page);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 将选中的人员添加到对应表中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
       public ActionResult ajaxAddUser(string str,int roleid)
        {
            List<Guid> useridlist = JsonConvert.DeserializeObject<List<Guid>>(str);
            var res = sysroleuserSer.AddRangeUserRole(useridlist, roleid);

            return Json(1,JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddPageLayer(int roleid)
        {
            var res = sysrolepageSer.AddPageLayer(roleid);
            ViewBag.rrid = roleid;
            return View(res);
        }
        public ActionResult SavePage(string strint, int roleid)
        {
            List<int> useint = JsonConvert.DeserializeObject<List<int>>(strint);
            var res = sysrolepageSer.SavePage(useint, roleid);
            return Json("res",JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 设备信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult InstrumentInfo()
        {
          var Centri=  instrumentSer.QueryWhere(i => i.Type == "01").ToList();
            var Drys = instrumentSer.QueryWhere(i => i.Type == "02").ToList();
            var Filter = instrumentSer.QueryWhere(i => i.Type == "03").ToList();
            var Colddrys = instrumentSer.QueryWhere(i => i.Type == "04").ToList();
            Model.ModelViews.Instrument_View viewList = new Model.ModelViews.Instrument_View();
            viewList.centri = Centri;
            viewList.drys = Drys;
            viewList.fliter = Filter;
            viewList.colddrys = Colddrys;

            return View(viewList);
        }
        /// <summary>
        /// 设备信息导出excel
        /// </summary>
        /// <param name="form"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult ExportInstrumentExcel(FormCollection form,string type)
        {
            MemoryStream stream = new MemoryStream();
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet();
            //konghang
            var headRow = sheet.CreateRow(0);
            List<Model.BB_Instrument> res;
            int count = 1;
            string[] columnname = new string[] { "空压机", "形式", "额定排气量", "额定功率", "额定电流", "额定压力", " 品牌", " 安装日期", " 机组编号" };
            string[] columnname1 = new string[] { "干燥机", "形式", "额定处理量", "额定功率", "额定压力", "再生耗气率", " 品牌", " 安装日期", " 机组编号" };
            string[] columnname2 = new string[] { "前置过滤器", "形式", "额定处理量", "滤芯数量", " 品牌", " 安装日期", " 机组编号" };
            string[] columnname3 = new string[] { "冷干机", "形式", "额定处理量", "额定功率", "额定压力", " 品牌", " 安装日期", " 机组编号" };
            if (type == "01")
            {
                 res = instrumentSer.QueryWhere(i => i.Type == type).ToList();
                for(int i=0;i<columnname.Length;i++)
                {
                    headRow.CreateCell(i).SetCellValue(columnname[i]);
                }
                foreach(var item in res)
                {
                    var newrow = sheet.CreateRow(count);
                    newrow.CreateCell(0).SetCellValue(item.Code);
                    newrow.CreateCell(1).SetCellValue(item.Type);
                    newrow.CreateCell(2).SetCellValue(item.Rated_A);
                    newrow.CreateCell(3).SetCellValue(item.Rated_Power);
                    newrow.CreateCell(4).SetCellValue(item.Rated_A);
                    newrow.CreateCell(5).SetCellValue(item.DesignPressure);
                    newrow.CreateCell(6).SetCellValue(item.Manufacture);
                    newrow.CreateCell(7).SetCellValue(item.RunYear);
                    newrow.CreateCell(8).SetCellValue(item.SerialNumber);
                    count++;
                }
                // 将excel写入stream流
                workbook.Write(stream);

                // 清理资源
                stream.Flush();
                stream.Position = 0;
                sheet = null;
                headRow = null;
                workbook = null;
                string filename = DateTime.Now.ToShortDateString() + "空压机列表.xls";
                return File(stream, "application/vnd.ms-excel", filename);

            }
            else if (type == "02")
            {
                res = instrumentSer.QueryWhere(i => i.Type == type).ToList();
                for (int i = 0; i < columnname1.Length; i++)
                {
                    headRow.CreateCell(i).SetCellValue(columnname1[i]);
                }
                foreach (var item in res)
                {
                    var newrow = sheet.CreateRow(count);
                    newrow.CreateCell(0).SetCellValue(item.Code);
                    newrow.CreateCell(1).SetCellValue(item.Type);
                    newrow.CreateCell(2).SetCellValue(item.Rated_A);
                    newrow.CreateCell(3).SetCellValue(item.Rated_Power);
                    newrow.CreateCell(4).SetCellValue(item.DesignPressure);
                    newrow.CreateCell(5).SetCellValue(item.GasConsumptionRate.ToString());
                    newrow.CreateCell(6).SetCellValue(item.Manufacture);
                    newrow.CreateCell(7).SetCellValue(item.RunYear);
                    newrow.CreateCell(8).SetCellValue(item.SerialNumber);
                    count++;
                }
                // 将excel写入stream流
                workbook.Write(stream);

                // 清理资源
                stream.Flush();
                stream.Position = 0;
                sheet = null;
                headRow = null;
                workbook = null;
                string filename = DateTime.Now.ToShortDateString() + "干燥机列表.xls";
                return File(stream, "application/vnd.ms-excel", filename);
            }
            else if (type == "03")
            {
                res = instrumentSer.QueryWhere(i => i.Type == type).ToList();
                for (int i = 0; i < columnname2.Length; i++)
                {
                    headRow.CreateCell(i).SetCellValue(columnname2[i]);
                }
                foreach (var item in res)
                {
                    var newrow = sheet.CreateRow(count);
                    newrow.CreateCell(0).SetCellValue(item.Code);
                    newrow.CreateCell(1).SetCellValue(item.Type);
                    newrow.CreateCell(2).SetCellValue(item.Rated_A);
                    newrow.CreateCell(3).SetCellValue(item.FilterNum.ToString());                
                    newrow.CreateCell(4).SetCellValue(item.Manufacture);
                    newrow.CreateCell(5).SetCellValue(item.RunYear);
                    newrow.CreateCell(6).SetCellValue(item.SerialNumber);
                    count++;
                }
                // 将excel写入stream流
                workbook.Write(stream);

                // 清理资源
                stream.Flush();
                stream.Position = 0;
                sheet = null;
                headRow = null;
                workbook = null;
                string filename = DateTime.Now.ToShortDateString() + "前置过滤器列表.xls";
                return File(stream, "application/vnd.ms-excel", filename);
            }
            else
            {
                res = instrumentSer.QueryWhere(i => i.Type == type).ToList();
                for (int i = 0; i < columnname3.Length; i++)
                {
                    headRow.CreateCell(i).SetCellValue(columnname3[i]);
                }
                foreach (var item in res)
                {
                    var newrow = sheet.CreateRow(count);
                    newrow.CreateCell(0).SetCellValue(item.Code);
                    newrow.CreateCell(1).SetCellValue(item.Type);
                    newrow.CreateCell(2).SetCellValue(item.Rated_A);
                    newrow.CreateCell(3).SetCellValue(item.Rated_Power);                  
                    newrow.CreateCell(4).SetCellValue(item.DesignPressure);
                    newrow.CreateCell(5).SetCellValue(item.Manufacture);
                    newrow.CreateCell(6).SetCellValue(item.RunYear);
                    newrow.CreateCell(7).SetCellValue(item.SerialNumber);
                    count++;
                }
                // 将excel写入stream流
                workbook.Write(stream);

                // 清理资源
                stream.Flush();
                stream.Position = 0;
                sheet = null;
                headRow = null;
                workbook = null;
                string filename = DateTime.Now.ToShortDateString() + "冷干机列表.xls";
                return File(stream, "application/vnd.ms-excel", filename);
            }          
        }
       /// <summary>
       /// 阈值设定显示界面
       /// </summary>
       /// <returns></returns>
        public ActionResult Threshold()
        {
            var res=thresholdSer.QueryWhere();
           
            return View(res);
        }
   

        public ActionResult AjaxThresholdModi(decimal setvalue,int id )
        { 
             SocketHelper.GetInstance();
             SocketHelper.InitSocket();
             SocketHelper.SendMessage(setvalue.ToString());
             Thread.Sleep(2000);
             if (SocketHelper.Flag==true)
             {

             }else
             {

             }

             var res = thresholdSer.QueryWhere(t => t.ID == id).FirstOrDefault();
            if (setvalue == res.CurrentValue)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }            
            res.CurrentValue = setvalue;
            res.SetValue = setvalue;
            thresholdSer.Update(res);
            if(thresholdSer.SaveChanges()>0)
            {
            //     #region 直接传入数据到opc,现在是测试,能使用,但是需要进一步加入到场景之中才有具体意义
            //opcItemList = opcHelperItemServices.QueryWhere();
            //opc.OPCList.AddRange(opcItemList);
            ////获取DBContext
            //string serverName = "Kepware.KEPServerEX.V6";
            //string serverNode = "192.168.14.113";
            //opc.Connect(serverName, serverNode);
            //#endregion
            //     opc.AsyncWriteOpcItems(TagList.SimTest_F005_A001_Inlet_SQ.ToString(), setvalue);
            //    opc.SyncReadOpcItems();
            //    var getValue= opc.OPCList[(int)TagList.SimTest_F005_A001_Inlet_SQ].Value;
            //    if(setvalue==Convert.ToDecimal(getValue))
            //        return Json(setvalue,JsonRequestBehavior.AllowGet);
            }
            return Json(0,JsonRequestBehavior.AllowGet);
        }
    }
}