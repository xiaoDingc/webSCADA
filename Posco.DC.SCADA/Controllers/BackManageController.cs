using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using IServices;
using Newtonsoft.Json;
using Posco.DC.WebHelper;
using Model;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Web.Helpers;
using CommonHelper;
using Model.BussionObject;

using NPOI.HSSF.Util;
using System.Text;
using NPOI.SS.Util;

namespace Posco.DC.SCADA.Controllers
{
    
    public class BackManageController : BaseController
    {
        private ISB_UserServices userSer;
        private ISB_ClientServices clientSer;
        private IBB_FactoryServices factorySer;
        private IBB_StationServices stationSer;
        private IBB_MeasureMeterServices measuremeterSer;
        private IBB_InstrumentServices instrumentSer;
        private IBB_AreaServices areaSer;
        private ISB_OperateHistoryServices operathisSer;
        private ISB_PageServices pageSer;
        private IBB_ChartAliasServices chartAliasSer;
        private IAP_Station_StatisticsServices statisticSer;
        private IAP_Centrifuge_StatisticsServices centritisticSer;
        public BackManageController(ISB_UserServices UserSer, ISB_ClientServices ClientSer, IBB_FactoryServices FactorySer
            , IBB_StationServices StationSer, IBB_MeasureMeterServices MeasureMeterSer, 
            IBB_InstrumentServices InstrumentSer, IBB_AreaServices AreaSer, ISB_OperateHistoryServices OperathisSer,
            ISB_PageServices PageSer,IBB_ChartAliasServices chartAliasServices, IAP_Station_StatisticsServices statisticser,
            IAP_Centrifuge_StatisticsServices centritisticser)
        {
            userSer = UserSer;
            clientSer = ClientSer;
            factorySer = FactorySer;
            stationSer = StationSer;
            measuremeterSer = MeasureMeterSer;
            instrumentSer = InstrumentSer;
            areaSer = AreaSer;
            operathisSer = OperathisSer;
            pageSer = PageSer;
            chartAliasSer=chartAliasServices;
            statisticSer = statisticser;
            centritisticSer = centritisticser;
        }
     
        /// <summary>
        /// 客户列表
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult CoustomList()
        {                           
            return View();
        }
        public ActionResult CoustomListJson(int page, int limit, int? id, string name, string contact, string state)
        {
            //id有，用id，联系人和名称联合查，如果这三个都没有，有state用state查
            List<Model.SB_Client> coustomList=null;
            int count=0;
            if (state != "" && state != null)
            {
                if (state == "有效")
                {
                    state = "1";
                }
                else if (state == "无效")
                {
                    state = "2";
                }
            }
            //有id直接用id查
            if (id != null)
            {
                count = clientSer.Count(c => c.ID == id);
                coustomList = clientSer.QuerySplitPage((c => c.ID == id), order => order.OrderBy(o => o.ID), limit, page);
            }
            //id没有
            else if (id == null)
            {//name 有
                if (name != "" && name != null)
                {//contact有
                    if (contact != "" && contact != null)
                    {
                        if (state != "" && state != null)
                        {//name contact state有
                            count = clientSer.Count(c => c.Name.Equals(name) && c.Contacts.Equals(contact) && c.state.Equals(state));
                            coustomList = clientSer.QuerySplitPage(c => c.Name.Equals(name) && c.Contacts.Equals(contact) && c.state.Equals(state), order => order.OrderBy(o => o.ID), limit, page);
                        }
                        else
                        {
                            //name 和contact有 state没有
                            count = clientSer.Count(c => c.Name.Equals(name) && c.Contacts.Equals(contact));
                            coustomList = clientSer.QuerySplitPage(c => c.Name.Equals(name) && c.Contacts.Equals(contact), order => order.OrderBy(o => o.ID), limit, page);
                        }

                    }
                    //contact没有
                    else
                    {
                        if (state != "" && state != null)
                        {//name state 有 contact没有
                            count = clientSer.Count(c => c.Name.Equals(name) && c.state.Equals(state));
                            coustomList = clientSer.QuerySplitPage(c => c.Name.Equals(name) && c.state.Equals(state), order => order.OrderBy(o => o.ID), limit, page);
                        }
                        else
                        {
                            //name有，其他两个没有
                            //name 和contact有
                            count = clientSer.Count(c => c.Name.Equals(name));
                            coustomList = clientSer.QuerySplitPage(c => c.Name.Equals(name), order => order.OrderBy(o => o.ID), limit, page);
                        }
                    }
                }
                //name没有
                else
                {
                    //contact有
                    if (contact != "" && contact != null)
                    {
                        if (state != "" && state != null)
                        {//name没有 contact state有
                            count = clientSer.Count(c => c.Contacts.Equals(contact) && c.state.Equals(state));
                            coustomList = clientSer.QuerySplitPage(c => c.Contacts.Equals(contact) && c.state.Equals(state), order => order.OrderBy(o => o.ID), limit, page);
                        }
                        else
                        {
                            //name没有 和contact有 state没有
                            count = clientSer.Count(c => c.Contacts.Equals(contact));
                            coustomList = clientSer.QuerySplitPage(c => c.Contacts.Equals(contact), order => order.OrderBy(o => o.ID), limit, page);
                        }

                    }
                    //contact没有
                    else
                    {
                        if (state != "" && state != null)
                        {//name没有 state 有 contact没有
                            count = clientSer.Count(c => c.state.Equals(state));
                            coustomList = clientSer.QuerySplitPage(c => c.state.Equals(state), order => order.OrderBy(o => o.ID), limit, page);
                        }
                        else
                        {
                            //都没有
                            count = clientSer.Count();
                            coustomList = clientSer.QuerySplitPage(c => true, order => order.OrderBy(o => o.ID), limit, page);
                        }
                    }
                }
            }

            var res = new { code = 0, msg = "success", data = coustomList, count };
            return Json(res, JsonRequestBehavior.AllowGet);
        }
   
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <returns></returns>
        public ActionResult OutputExcel(DateTime time)
        {
            #region 日期获得以及月份天数
            int month = time.Month;
            DateTime monthinit = time.AddDays(-time.Day + 1).Date;
            int year = time.Year;
            int number = 0;
         
                if (month == 4 || month == 6 || month == 9 || month == 11)
                {
                    number = 30;
                }
            else if(month==2)
            {
                if (year % 4 == 0)
                {
                    number = 29;
                }
                else
                {
                    number = 28;
                }
            }
            else
            {
                number = 31;
            }
            DateTime monthend = monthinit.AddDays(number);
            #endregion
            var centritislist =centritisticSer.QueryWhere(c => c.DateTime >= monthinit && c.DateTime <= monthend && c.YMDH.Equals("B")).Select(s => new { s.ePower, s.StationID, s.EquipID,s.DateTime }).GroupBy(g => new { g.StationID, g.EquipID }).ToList();
           var statislist= statisticSer.QueryWhere(s => s.DateTime >= monthinit && s.DateTime <= monthend && s.YMDH.Equals("B")).Select(c => new
            {
                c.Sum_Power,
                c.Main_Q,
                c.Water_F,
                c.StationID,
                c.DateTime
            }).GroupBy(g => new { g.StationID }).ToList();
        
            // 定义返回的stream流
            MemoryStream stream = new MemoryStream();
            // 创建一个workbook
            HSSFWorkbook workbook = new HSSFWorkbook();
            // 创建一个sheet页
            ISheet sheet = workbook.CreateSheet();
            sheet.ForceFormulaRecalculation = true;//单元格计算开关
            sheet.SetColumnWidth(0, 30 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.FillForegroundColor = 47;
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 1));//设置单元格合并
            IRow headrow = sheet.CreateRow(0);
            headrow.CreateCell(0).SetCellValue("空压站汇总");
            headrow.CreateCell(1).SetCellValue("总表");
            // 创建空行
            IRow headRow = sheet.CreateRow(1);
            headRow.CreateCell(0).SetCellValue("");
            headRow.CreateCell(1).SetCellValue("单位");
            #region 设置单元格样式
            ICellStyle style = workbook.CreateCellStyle();//创建样式对象        
            IFont font = workbook.CreateFont(); //创建一个字体样式对象        
            font.FontName = "宋体"; //和excel里面的字体对应        
            font.Color = HSSFColor.Brown.Index;//颜色参考NPOI的颜色对照表(替换掉PINK())        
            //font.IsItalic = true; //斜体       
            font.FontHeightInPoints = 14;//字体大小        
            //font.Boldweight = short.MaxValue;//字体加粗        
            style.SetFont(font); //将字体样式赋给样式对象    
            #endregion
            for (int i = 0; i < number; i++)
            {
                ICell cell4 = headRow.CreateCell(i * 2 + 2);
                cell4.CellStyle = style;
                cell4.SetCellValue(i + 1 + "夜班");
                ICell cell5 = headRow.CreateCell(i * 2 + 3);
                cell5.CellStyle = style;
                cell5.SetCellValue(i + 1 + "白班");
            }
            ICell Finalcell = headRow.CreateCell(number * 2 + 2);
            Finalcell.CellStyle = style;
            #region 获取合计最后位置
            //获取最后索引
            int bbb = Finalcell.ColumnIndex;
            int system = 26;
            char[] digArray = new char[100];
            int ii = 0;
            while (bbb > 0)
            {
                int mod = bbb % system;
                if (mod == 0)
                    mod = system;
                digArray[ii++] = (char)(mod - 1 + 'A');
                bbb = (bbb - 1) / 26;
            }
            StringBuilder sb = new StringBuilder(ii);
            for (int j = ii - 1; j >= 0; j--)

            {
                sb.Append(digArray[j]);
            }
            //合计前一列的列名
            string ccc = sb.ToString();
            #endregion
            //
            Finalcell.SetCellValue("合计");  
            ICell cell2 = headRow.CreateCell(1);
            #region 单位和名称集合
            cell2.CellStyle = style; //把样式赋给单元格
            string[] str1 = new string[] { "1#空压站汇总", "供气量", "1#空压机用电量", "2#空压机用电量", "3#空压机用电量", "4#空压机用电量", "5#空压机用电量", "6#空压机用电量", "1#变压器", "2#变压器", "1#高压进线", "2#高压进线", "1#低压用电量", "2#低压用电量", "工业水补水量", "旁滤装置用水量", "强制排污量", "1#空压站月度供气总量", "1#空压站月度耗电量", "1#空压站电耗", "1#空压站水耗", "单日用电量", "单日电耗", "单日高压用电量", "单日高压电耗", "" };
            string[] str2 = new string[] { "2#空压站汇总", "供气量", "1#空压机用电量", "2#空压机用电量", "3#空压机用电量", "4#空压机用电量", "5#空压机用电量", "6#空压机用电量", "1#变压器", "2#变压器", "1#高压进线", "2#高压进线", "1#低压用电量", "2#低压用电量", "工业水补水量", "旁滤装置用水量", "强制排污量", "2#空压站月度供气总量", "2#空压站月度耗电量", "2#空压站电耗", "2#空压站水耗", "单日用电量", "单日电耗", "单日高压用电量", "单日高压电耗", "" };
            string[] str3 = new string[] { "3#空压站汇总", "供气量", "1#空压机用电量", "2#空压机用电量", "3#空压机用电量", "4#空压机用电量", "5#空压机用电量", "6#空压机用电量", "1#变压器", "2#变压器", "1#高压进线", "2#高压进线", "1#低压用电量", "2#低压用电量", "工业水补水量", "旁滤装置用水量", "强制排污量", "3#空压站月度供气总量", "3#空压站月度耗电量", "3#空压站电耗", "3#空压站水耗", "单日用电量", "单日电耗", "单日高压用电量", "单日高压电耗", "" };
            string[] str4 = new string[] { "4#空压站汇总", "供气量", "1#空压机用电量", "2#空压机用电量", "3#空压机用电量", "4#空压机用电量", "5#空压机用电量", "6#空压机用电量", "7#空压机用电量", "1#变压器", "2#变压器", "1#高压进线", "2#高压进线", "1#低压用电量", "2#低压用电量", "工业水补水量", "旁滤装置用水量", "强制排污量", "4#空压站月度供气总量", "4#空压站月度耗电量", "4#空压站电耗", "4#空压站水耗", "单日用电量", "单日电耗", "单日高压用电量", "单日高压电耗", "" };
            string[] unit123 = new string[] { "", "N km3", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "m3", "m3", "m3", "N km3", "KW", "KW/N km3", "T//N km3", "KW", "KW/N km3", "KW", "KW/N km3" ,""};
            string[] unit4 = new string[] { "", "N km3", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "KW", "m3", "m3", "m3", "N km3", "KW", "KW/N km3", "T//N km3", "KW", "KW/N km3", "KW", "KW/N km3", "" };
            #endregion
            #region 1号空压站
            for (int i = 0; i < str1.Length; i++)
            {
                IRow headRow1 = sheet.CreateRow(i + 2);
                if (i == 0)//第一行
                {
                    headRow1.CreateCell(0).SetCellValue(str1[i]);
                    headRow1.CreateCell(1).SetCellValue(unit123[i]);
                }
              else if(i==1)//第二行供气量
                {
                    headRow1.CreateCell(0).SetCellValue(str1[i]);
                    headRow1.CreateCell(1).SetCellValue(unit123[i]);
                    int j = 0;
                    foreach(var item in statislist[0])
                    {
                        headRow1.CreateCell(j + 2).SetCellValue((double)item.Main_Q);
                        j++;
                    }
                    for(int jj = 0; jj < number*2-statislist[0].Count(); jj++)
                    {
                        headRow1.CreateCell(jj + statislist[0].Count()+2).SetCellValue(0);
                    }
                    headRow1.CreateCell(number * 2 + 2).SetCellFormula("sum(C" + (i + 2) + ":" + ccc + (i + 2) + ")");
                }
                else if(i>1&&i<=7)
                {
                    headRow1.CreateCell(0).SetCellValue(str1[i]);
                    headRow1.CreateCell(1).SetCellValue(unit123[i]);
                    int j = 0;
                    foreach(var item in centritislist[j])
                    {
                        headRow1.CreateCell(j + 2).SetCellValue((double)item.ePower);
                        j++;
                    }
                    for (int jj = 0; jj < number * 2 - centritislist[0].Count(); jj++)
                    {
                        headRow1.CreateCell(jj + centritislist[0].Count() + 2).SetCellValue(1);
                    }
                    headRow1.CreateCell(number * 2 + 2).SetCellFormula("sum(C" + (i + 2) + ":" + ccc + (i + 2) + ")");
                }
                else
                {
                    headRow1.CreateCell(0).SetCellValue(str1[i]);
                    headRow1.CreateCell(1).SetCellValue(unit123[i]);
                }
                
            }
            #endregion
            #region 2号空压站
            for (int i = 0; i < str2.Length; i++)
            {
                IRow headRow1 = sheet.CreateRow(i + 1+str1.Length);
                headRow1.CreateCell(0).SetCellValue(str2[i]);
                headRow1.CreateCell(1).SetCellValue(unit123[i]);
                headRow1.CreateCell(number * 2 + 2).SetCellFormula("sum(C" + (i+ str1.Length + 2) + "," +ccc+ (i+ str1.Length + 2) + ")");
            }
            #endregion
            #region 3号空压站
            for (int i = 0; i < str3.Length; i++)
            {
                IRow headRow1 = sheet.CreateRow(i + 1+str1.Length+str2.Length);
                headRow1.CreateCell(0).SetCellValue(str3[i]);
                headRow1.CreateCell(1).SetCellValue(unit123[i]);
                headRow1.CreateCell(number * 2 + 2).SetCellFormula("sum(C" + (i+str1.Length+str2.Length + 2) + "," +ccc+ (i+str1.Length+str2.Length + 2) + ")");
            }
            #endregion
            #region 4号空压站
            for (int i = 0; i < str4.Length; i++)
            {
                IRow headRow1 = sheet.CreateRow(i + 1+str1.Length+str2.Length+str3.Length);
                headRow1.CreateCell(0).SetCellValue(str4[i]);
                headRow1.CreateCell(1).SetCellValue(unit4[i]);
                headRow1.CreateCell(number * 2 + 2).SetCellFormula("sum(C" + (i+str1.Length+ str2.Length+ str3.Length + 2) + "," +ccc+ (i+str1.Length + str2.Length + str3.Length + 2) + ")");
            }
            #endregion
            // 将excel写入stream流
            workbook.Write(stream);

            // 清理资源
            stream.Flush();
            stream.Position = 0;
            sheet = null;
            headRow = null;
            workbook = null;
            return File(stream, "application/vnd.ms-excel", "月报表.xls");
        }
        
        /// <summary>
        /// 人员列表
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult UserList()
        {
            
            return View();      
        }
        /// <summary>
        /// 人员图表JSON
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="contact"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult UserListJson(int limit,int page, string account, string name, string contact, string state)
        {
            if (account == "")
            {
                account = null;
            }
            if (name == "")
            {
                name = null;
            }
            if (contact == "")
            {
                contact = null;
            }
            if (state == "")
            {
                state = null;
            }
            var res= userSer.UserList(limit, page,account,name,contact,state);
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 人员信息修改
        /// </summary>
        /// <param name="uid">传入的人员id</param>
        /// <param name="type"></param>
        /// <returns>返回一个视图</returns>
        public ActionResult UserModiInfo(Guid uid)
        {
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            //性别
            Model.ViewObject.Sex sex1 = new Model.ViewObject.Sex("1", "男");
            Model.ViewObject.Sex sex2 = new Model.ViewObject.Sex("0", "女");
            List<Model.ViewObject.Sex> sexList = new List<Model.ViewObject.Sex>();
            sexList.Add(sex1);
            sexList.Add(sex2);
            SelectList selectlistSex = new SelectList(sexList, "numstring", "showstring");
            ViewBag.Sex = selectlistSex;
            //状态
            Model.ViewObject.Sex state1 = new Model.ViewObject.Sex("1", "在职");
            Model.ViewObject.Sex state2 = new Model.ViewObject.Sex("0", "离职");
            List<Model.ViewObject.Sex> stateliList = new List<Model.ViewObject.Sex>();
            stateliList.Add(state1);
            stateliList.Add(state2);
            SelectList selectlistState = new SelectList(stateliList, "numstring", "showstring");
            ViewBag.State = selectlistState;
            var res = userSer.ModiUser(uid);
            return View(res);
        }
        /// <summary>
        /// 提交后的人员信息修改
        /// </summary>
        /// <param name="model">修改后的人员信息</param>
        /// <returns>跳转到列表</returns>
        [HttpPost]
        public ActionResult UserModiInfo(Model.BussionObject.UserShow_BO model)
        {
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            //性别
            Model.ViewObject.Sex sex1 = new Model.ViewObject.Sex("1", "男");
            Model.ViewObject.Sex sex2 = new Model.ViewObject.Sex("0", "女");
            List<Model.ViewObject.Sex> sexList = new List<Model.ViewObject.Sex>();
            sexList.Add(sex1);
            sexList.Add(sex2);
            SelectList selectlistSex = new SelectList(sexList, "numstring", "showstring");
            ViewBag.Sex = selectlistSex;
            //状态
            Model.ViewObject.Sex state1 = new Model.ViewObject.Sex("1", "在职");
            Model.ViewObject.Sex state2 = new Model.ViewObject.Sex("0", "离职");
            List<Model.ViewObject.Sex> stateliList = new List<Model.ViewObject.Sex>();
            stateliList.Add(state1);
            stateliList.Add(state2);
            SelectList selectlistState = new SelectList(stateliList, "numstring", "showstring");
            ViewBag.State = selectlistState;

            if (ModelState.IsValid)
            {
               var res= userSer.ModiUser(model);
                if (res > 0)
                {
                    var UserList = userSer.QueryAll();
                    CacheMgr.SetData("UserList", UserList);
                    return Json("OK",JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View(model);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet); 
        }
        /// <summary>
        /// 用户人员增加
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAdd()
        {
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.ClientaDD = selectlistClient;
        //    Model.BussionObject.UserShow_BO model = new Model.BussionObject.UserShow_BO();
            return View();
        }
        /// <summary>
        /// 提交用户添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回视图</returns>
        [HttpPost]
        public ActionResult UserAdd(Model.BussionObject.UserShow_BO model)
        {
            if (ModelState.IsValid)
            {
                var result = userSer.ValidAccount(model.boAccount);
                if (result==0)
                {
                 var res=userSer.AddUser(model);
                    if (res > 0)
                    {
                        return Json("OK", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("E", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("RE", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("E", JsonRequestBehavior.AllowGet);
            }
            
        }
        /// <summary>
        /// 验证账号重复问题
        /// </summary>
        /// <param name="json">传入账号</param>
        /// <returns>返回视图</returns>
        public ActionResult AccountAjax(string json)
        {
            var res=userSer.ValidAccount(json);
            if (res==0)
            {
                return Json("没", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("有", JsonRequestBehavior.AllowGet);
            }       
        }

         public ActionResult FactoryList()
        {
            return View();      
        }
        /// <summary>
        /// 工厂列表
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult FactoryListJSON(int page, int limit )
        {
        
            var pageSplitList = factorySer.QuerySplitPage((c => true), order => order.OrderBy(o => o.ID), limit, page);
            int count = factorySer.Count();
            List<Model.ViewObject.FactoryList_VO> pageInfoList = new List<Model.ViewObject.FactoryList_VO>();
            foreach (var item in pageSplitList)
            {
                string Statename = null;
                if (item.Valid == "1")
                {
                    Statename = "有效";
                }
                if (item.Valid == "0")
                {
                    Statename = "无效";
                }
                var res = clientSer.QueryWhere(c => c.ID == item.ClientID).FirstOrDefault();

                Model.ViewObject.FactoryList_VO vo = new Model.ViewObject.FactoryList_VO();
                vo.VliadName = Statename;
                vo.factory = item;
                vo.ClientName = res.Name;
                pageInfoList.Add(vo);
            }
            Model.PageSplitInfo pageSplitInfo = new PageSplitInfo(limit, page, factorySer.Count(), "FactoryList", "BackManage");
            Model.PageInfo<Model.ViewObject.FactoryList_VO> clientList = new PageInfo<Model.ViewObject.FactoryList_VO>();
            clientList.DataModel = pageInfoList;
            clientList.PageSplit = pageSplitInfo;
            clientList.showPageSize = limit;
       
            var resTest= clientList.DataModel.Select(x=>new      
                {   x.factory.ID,
                    x.factory.code,
                    x.factory.Name,
                    x.factory.Introduction,
                    x.factory.Product,
                    x.factory.AnnualOutput,
                    x.factory.Latitude,
                    x.factory.Longtitude,
                     x.factory.Altitude,
                    x.factory.Size,
                    x.ClientName,
                    x.factory.Remark,
                    x.factory.Valid,
                   }).ToList();
            var tranres = new
            {
                code = 0,
                msg = "success",
                data =resTest,
                count
            };
            return Json(tranres,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 空压站列表
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult StationList()
        {
            return View();
        }
        /// <summary>
        ///空压站列表Json
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult StationListJson(int limit,int page,string code)
        {
            if (code == "")
            {
                code = null;
            }
            var res = stationSer.StationListJson(limit, page,code);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 空压站编辑
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult StationModi(int uid)
        {
            //客户列表
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            //工厂列表
            List<Model.BB_Factory> factoryList = factorySer.QueryAll();
            SelectList selectlistFactory = new SelectList(factoryList, "ID", "Name");
            ViewBag.Factory = selectlistFactory;
           var res= stationSer.ModiStation(uid);
            return View(res);
        }
        /// <summary>
        /// 空压站编辑提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StationModi(Model.BussionObject.StationShow_BO model)
        {
            if (ModelState.IsValid)
            {
               var res= stationSer.ModiStation(model);
                if (res > 0)
                {
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View(model);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 空压站添加
        /// </summary>
        /// <returns></returns>
        public ActionResult StationAdd()
        {
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            //工厂列表
            List<Model.BB_Factory> factoryList = factorySer.QueryAll();
            SelectList selectlistFactory = new SelectList(factoryList, "ID", "Name");
            ViewBag.Factory = selectlistFactory;
            return View();
        }
        /// <summary>
        /// 空压站添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StationAdd(Model.BussionObject.StationShow_BO model)
        {
            if (ModelState.IsValid)
            {
                var res=stationSer.AddStation(model);
                if (res > 0)
                {
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return Json("E", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 空压设备列表Get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult InstrumentList()
        {
            return  View();
        }

        /// <summary>
        /// 空压设备列表Post请求
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult InstrumentListJSON(int page, int limit)
        {
            var query = instrumentSer.QueryWhere();
            int count = query.Count();
            var res = query.OrderBy(o => o.ID).Skip((page - 1) * limit).Take(limit).ToList().Select(x => new
            {  
                    x.ID,
                    x.Code,
                    x.Type,
                    x.Rated_Q,
                    x.DesignPressure,
                    x.Rated_V,
                    x.Rated_A,
                    x.Rated_Power,
                    x.Dewpoint,
                    x.Manufacture,
                    x.RunYear,
                    ClientID=clientSer.Find(new object[] { x.ClientID }).Name,
                    FactoryID=factorySer.Find(new object[]{x.FactoryID }).Name,
                    StationID=stationSer.Find(new object[]{x.StationID }).Name,
                    DateOfProductionTime=x.DateOfProductionTime.ToString(),
                    x.SerialNumber,
                    x.ExFlangeSize,
                    x.ShapeSize,
                    x.Refrigerantype,
                    x.AdsorbentType,
                    x.PrimaryIntercoolerPower,
                    x.SecondaryIntercoolerPower,
                    x.ThirdintercoolerPower,
                    x.Remark,
                    x.Valid,
                   }).ToList();
            var tranres = new
            {
                code = 0,
                msg = "success",
                data =res,
                count
            };
             return  Json(tranres,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 空压设备添加 Get请求
        /// </summary>
        /// <returns></returns>
       public ActionResult InstrumentAdd()
        {
            List<Model.SB_Client> clientList = clientSer.QueryWhere();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            List<Model.BB_Factory> factoryList = factorySer.QueryWhere();
            SelectList selectlistFactory = new SelectList(factoryList, "ID", "Name");
            ViewBag.Fac = selectlistFactory;
            List<Model.BB_Station> stationList =stationSer.QueryWhere();
            SelectList selectlistStation = new SelectList(stationList, "ID", "Name");
            ViewBag.Sta = selectlistStation;
            return View();
        }
        /// <summary>
        /// 空压设备添加 POST请求
        /// </summary>
        /// <param name="model">视图模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InstrumentAdd(Model.BussionObject.CentrifugeShow_BO model)
        {
            if (ModelState.IsValid)
            {
                Model.BB_Instrument viewmodel = new BB_Instrument()
                {
                    AdsorbentType = model.AdsorbentType,
                    Rated_A = model.Rated_A,
                    Rated_Power = model.Rated_Power,
                    Rated_Q = model.Rated_Q,
                    Rated_V = model.Rated_V,
                    Refrigerantype = model.Refrigerantype,
                    Remark = model.Remark,
                    RunYear = model.RunYear,
                   // GasConsumptionRate = model.GasConsumptionRate,
                    ClientID = model.ClientID,
                    Code = model.Code,
                    DateOfProductionTime = model.DateOfProductionTime,
                    DesignPressure = model.DesignPressure,
                    Dewpoint = model.Dewpoint,
                    FactoryID = model.FactoryID,
                   // FilterNum = model.FilterNum,
                    ExFlangeSize = model.ExFlangeSize,
                    Manufacture = model.Manufacture,
                    SerialNumber = model.SerialNumber,
                    ShapeSize = model.ShapeSize,
                    StationID = model.StationID,
                    PrimaryIntercoolerPower = model.PrimaryIntercoolerPower,
                    SecondaryIntercoolerPower = model.SecondaryIntercoolerPower,
                    ThirdintercoolerPower = model.ThirdintercoolerPower,
                    Type = model.Type,
                    Valid = model.Valid
                };
                   instrumentSer.Add(viewmodel);
                   int a = instrumentSer.SaveChanges();
                   if (a>0)
                   {
                       return  Json("OK",JsonRequestBehavior.AllowGet);
                   }
                        
            }
            return  Json("Error",JsonRequestBehavior.AllowGet);           
        }
          /// <summary>
        /// 空压设备编辑
        /// </summary>
        /// <param name="uid">根据ID进行编辑</param>
        /// <returns></returns>
        public ActionResult InstrumentModiInfo(int uid)
        {
            //客户列表
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            //工厂列表
            List<Model.BB_Factory> factoryList = factorySer.QueryAll();
            SelectList selectlistFactory = new SelectList(factoryList, "ID", "Name");
            ViewBag.Factory = selectlistFactory;
            //空压站列表
               List<Model.BB_Station> stationList = stationSer.QueryWhere().ToList();
            SelectList stationListFactory = new SelectList(stationList, "ID", "Name");
            ViewBag.Station = stationListFactory;

           var model= instrumentSer.QueryWhere(x => x.ID == uid).FirstOrDefault() ;
           CentrifugeShow_BO centrifugeShow=new CentrifugeShow_BO()
           {
                ID = model.ID,
                Code = model.Code,
                Type = model.Type,
                Rated_Q = model.Rated_Q,
                DesignPressure = model.DesignPressure,
                Rated_V = model.Rated_V,
                Rated_A = model.Rated_A,
                Rated_Power = model.Rated_Power,
                Dewpoint = model.Dewpoint,
                Manufacture = model.Manufacture,
                RunYear = model.RunYear,
                Remark = model.Remark,
                StationID = model.StationID,
                FactoryID = model.FactoryID,
                ClientID = model.ClientID,
                Valid = model.Valid,
                DateOfProductionTime = model.DateOfProductionTime,
                SerialNumber = model.SerialNumber,
                ExFlangeSize = model.ExFlangeSize,
                ShapeSize = model.ShapeSize,
                Refrigerantype = model.Refrigerantype,
                AdsorbentType = model.AdsorbentType,
                PrimaryIntercoolerPower = model.PrimaryIntercoolerPower,
                SecondaryIntercoolerPower = model.SecondaryIntercoolerPower,
                ThirdintercoolerPower = model.ThirdintercoolerPower,
           };
            return View(centrifugeShow);
        }
        /// <summary>
        /// 空压设备编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InstrumentModiInfo(Model.BussionObject.CentrifugeShow_BO model)
        {
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            //工厂列表
            List<Model.BB_Factory> factoryList = factorySer.QueryAll();
            SelectList selectlistFactory = new SelectList(factoryList, "ID", "Name");
            ViewBag.Factory = selectlistFactory;

             List<Model.BB_Station> stationList = stationSer.QueryWhere().ToList();
            SelectList stationListFactory = new SelectList(stationList, "ID", "Name");
            ViewBag.Station = stationListFactory;
            if (ModelState.IsValid)
            {
            BB_Instrument instrument=new BB_Instrument()
            {
                ID = model.ID,
                Code = model.Code,
                Type = model.Type,
                Rated_Q = model.Rated_Q,
                DesignPressure = model.DesignPressure,
                Rated_V = model.Rated_V,
                Rated_A = model.Rated_A,
                Rated_Power = model.Rated_Power,
                Dewpoint = model.Dewpoint,
                Manufacture = model.Manufacture,
                RunYear = model.RunYear,
                Remark = model.Remark,
                StationID = model.StationID,
                FactoryID = model.FactoryID,
                ClientID = model.ClientID,
                Valid = model.Valid,
                DateOfProductionTime = model.DateOfProductionTime,
                SerialNumber = model.SerialNumber,
                ExFlangeSize = model.ExFlangeSize,
                ShapeSize = model.ShapeSize,
                Refrigerantype = model.Refrigerantype,
                AdsorbentType = model.AdsorbentType,
                PrimaryIntercoolerPower = model.PrimaryIntercoolerPower,
                SecondaryIntercoolerPower = model.SecondaryIntercoolerPower,
                ThirdintercoolerPower = model.ThirdintercoolerPower,
            };

               instrumentSer.Update(instrument);
               instrumentSer.SaveChanges();
               return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 区域列表get获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult AreaList()
        {
            return  View();
        }
        /// <summary>
        /// 区域列表
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult AreaListJSON(int page , int limit)
        {
           // var pageSplitList = areaSer.QuerySplitPage((c => true), order => order.OrderBy(o => o.ID), limit, page);
            var pageSplitList = areaSer.QueryWhere(x=>true).OrderBy(x=>x.ID).Skip((page-1)*limit).Take(limit).ToList();
            int count = areaSer.QueryWhere(x=>true).Count();
            List<Model.ViewObject.AreaList_VO> pageInfoList = new List<Model.ViewObject.AreaList_VO>();
            foreach (var item in pageSplitList)
            {
                string Statename = null;
                if (item.Valid == "1")
                {
                    Statename = "有效";
                }
                if (item.Valid == "0")
                {
                    Statename = "无效";
                }
                var res = clientSer.QueryWhere(c => c.ID == item.ClientID).FirstOrDefault();
                var factory = factorySer.QueryWhere(f => f.ID == item.FactoryID).FirstOrDefault();
                if (res != null && factory != null)
                {
                    Model.ViewObject.AreaList_VO vo = new Model.ViewObject.AreaList_VO();
                    vo.VliadName = Statename;
                    vo.area = item;
                    vo.ClientName = res.Name;
                    vo.FactoryName = factory.Name;
                    pageInfoList.Add(vo);
                }             
            }
            Model.PageSplitInfo pageSplitInfo = new PageSplitInfo(limit, page, stationSer.Count(), "AreaList", "BackManage");
            Model.PageInfo<Model.ViewObject.AreaList_VO> clientList = new PageInfo<Model.ViewObject.AreaList_VO>();
            clientList.DataModel = pageInfoList;
            clientList.PageSplit = pageSplitInfo;
            clientList.showPageSize = limit;
            var jsonResult=new
            {
                code = 0,
                msg = "success",
                data = clientList.DataModel.Select(x=>new
                {
                    x.area.ID,
                    x.area.Code,
                    x.area.Name,
                    x.area.Introduction,
                    x.area.Latitude,
                    x.area.Longtitude,
                    x.area.Remark,
                    x.FactoryName,
                    x.ClientName,
                    x.area.Valid,
                    x.area.InputSize
                }),
                count
            };
            return Json(jsonResult,JsonRequestBehavior.AllowGet);
        }

            /// <summary>
        /// 用户人员增加
        /// </summary>
        /// <returns></returns>
        public ActionResult AreaAdd()
        {
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;

            List<Model.BB_Factory> factoryList = factorySer.QueryWhere();
            SelectList selectlistFactory = new SelectList(factoryList, "ID", "Name");
            ViewBag.Factory = selectlistFactory;

            //List<Model.BB_Station> stationList =stationSer.QueryWhere();
            //SelectList selectlistStation = new SelectList(stationList, "ID", "Name");
            //ViewBag.Sta = selectlistStation;
            return View();
        }
        /// <summary>
        /// 提交用户添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回视图</returns>
        [HttpPost]
        public ActionResult AreaAdd(BB_Area model)
        {
            if (ModelState.IsValid)
            {
                 areaSer.Add(model);
                 areaSer.SaveChanges();   
                 return Json("OK", JsonRequestBehavior.AllowGet);                                   
            }
            else
            {
                return Json("E", JsonRequestBehavior.AllowGet);
            }
            
        }
               /// <summary>
        /// 工厂修改
        /// </summary>
        /// <param name="uid">传值</param>
        /// <returns>视图</returns>
        [HttpGet]
        public ActionResult AreaModiInfo(int uid)
        {
            List<Model.SB_Client> clientList = clientSer.QueryWhere();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;

            ViewBag.Factory=new SelectList(factorySer.QueryWhere(),"ID","Name");

            var model = areaSer.QueryWhere(u => u.ID == uid).FirstOrDefault();          
            return View(model);
        }
        /// <summary>
        /// 工厂修改
        /// </summary>
        /// <param name="model">传值</param>
        /// <returns>跳转</returns>
        [HttpPost]
        public ActionResult AreaModiInfo(BB_Area model)
        {
            //数据校验是否有问题
            if (ModelState.IsValid)
            {
                areaSer.Update(model);
                areaSer.SaveChanges();
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 检测仪表列表
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult MeasureMeterList()
        {
            return View();
        }
        public ActionResult MeasureMeterListJson(int limit,int page)
        {
            var res=measuremeterSer.MeasureMeterList(limit, page);
            return Json(res,JsonRequestBehavior.AllowGet);
        }
        public ActionResult MeasureMeterModi(int id)
        {
            //客户列表
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            //工厂列表
            List<Model.BB_Factory> factoryList = factorySer.QueryAll();
            SelectList selectlistFactory = new SelectList(factoryList, "ID", "Name");
            ViewBag.Factory = selectlistFactory;
            //区域列表
            List<Model.BB_Area> areaList = areaSer.QueryWhere();
            SelectList selectlistArea = new SelectList(areaList, "ID", "Name");
            ViewBag.Area = selectlistArea;
            //状态
            Model.ViewObject.Sex state1 = new Model.ViewObject.Sex("1", "有效");
            Model.ViewObject.Sex state2 = new Model.ViewObject.Sex("0", "无效");
            List<Model.ViewObject.Sex> stateliList = new List<Model.ViewObject.Sex>();
            stateliList.Add(state1);
            stateliList.Add(state2);
            SelectList selectlistState = new SelectList(stateliList, "numstring", "showstring");
            ViewBag.State = selectlistState;
            var res = measuremeterSer.MeasureMeterModi(id);
            return View(res);
        }
        [HttpPost]
        public ActionResult MeasureMeterModi(Model.BussionObject.MeasureMeterShow_BO model)
        {
            if (ModelState.IsValid)
            {
                var res = measuremeterSer.MeasureMeterModi(model);
                if (res > 0)
                {
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View(model);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
        public ActionResult MeasureMeterAdd()
        {
            //客户列表
            List<Model.SB_Client> clientList = clientSer.QueryAll();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            //工厂列表
            List<Model.BB_Factory> factoryList = factorySer.QueryAll();
            SelectList selectlistFactory = new SelectList(factoryList, "ID", "Name");
            ViewBag.Factory = selectlistFactory;
            //区域列表
            List<Model.BB_Area> areaList = areaSer.QueryWhere();
            SelectList selectlistArea = new SelectList(areaList, "ID", "Name");
            ViewBag.Area = selectlistArea;
            //状态
            Model.ViewObject.Sex state1 = new Model.ViewObject.Sex("1", "有效");
            Model.ViewObject.Sex state2 = new Model.ViewObject.Sex("0", "无效");
            List<Model.ViewObject.Sex> stateliList = new List<Model.ViewObject.Sex>();
            stateliList.Add(state1);
            stateliList.Add(state2);
            SelectList selectlistState = new SelectList(stateliList, "numstring", "showstring");
            ViewBag.State = selectlistState;
            return View();
        }
        [HttpPost]
        public ActionResult MeasureMeterAdd(Model.BussionObject.MeasureMeterShow_BO model)
        {
            if (ModelState.IsValid)
            {
                var res = measuremeterSer.MeasureMeterAdd(model);
                if (res > 0)
                {
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View(model);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 客户信息修改
        /// </summary>
        /// <param name="uid">传入的客户id</param>
        /// <param name="type"></param>
        /// <returns>返回一个视图</returns>
        public ActionResult CoustomModiInfo(int? uid,int? type)
        {          
                var res = clientSer.QueryWhere(c => c.ID == uid).FirstOrDefault();
                Model.BussionObject.CustomShow_BO model = new Model.BussionObject.CustomShow_BO()
                {
                    boID = res.ID,
                    boAddress = res.Address,
                    boCompanyMail = res.CompanyMail,
                    boContacts = res.Contacts,
                    boContactsCellone = res.ContactsCellone,
                    boContactsMail = res.ContactsMail,
                    boName = res.Name,
                    boRemark = res.Remark,
                    bostate = res.state,
                    boTelephone = res.Telephone,
                    boType = res.Type
                };
                return View(model);                                
        }
        [HttpPost]
        public ActionResult CoustomModiInfoAjax(Model.BussionObject.CustomShow_BO model)
        {
            Model.SB_Client clientmodel = new SB_Client()
            {
                ID = model.boID,
                Address = model.boAddress,
                CompanyMail = model.boCompanyMail,
                Contacts = model.boContacts,
                ContactsCellone = model.boContactsCellone,
                Name = model.boName,
                Type = model.boType,
                state = model.bostate,
                ContactsMail = model.boContactsMail,
                Remark = model.boRemark,
                Telephone = model.boTelephone
            };
            clientSer.Update(clientmodel);
            int a = clientSer.SaveChanges();
            if (a > 0)
            {
                var ClientList = clientSer.QueryWhere();
                CacheMgr.SetData("ClientList", ClientList);
                return WriteSuccess("OK");
            }
            else
            {
                return WriteError("erro");
            }
        }

        /// <summary>
        /// 提交后的请求
        /// </summary>
        /// <param name="model">提交的模型</param>
        /// <returns>跳转到列表界面</returns>
        [HttpPost]
        public ActionResult CoustomModiInfo(Model.BussionObject.CustomShow_BO model)
        {
            if (ModelState.IsValid)
            {
                string state;
                if (model.bostate == "正常")
                {
                    state = "1";
                }
                else
                {
                    state = "2";
                }
                Model.SB_Client clientmodel = new SB_Client()
                {
                    ID=model.boID,
                    Address = model.boAddress,
                    CompanyMail = model.boCompanyMail,
                    Contacts = model.boContacts,
                    ContactsCellone = model.boContactsCellone,
                    Name = model.boName,
                    Type = model.boType,
                    state = state,
                    ContactsMail = model.boContactsMail,
                    Remark = model.boRemark,
                    Telephone = model.boTelephone
                };
                clientSer.Update(clientmodel);
                int a = clientSer.SaveChanges();
                if (a > 0)
                {
                    return RedirectToAction("CoustomList", "BackManage");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            
        }
    
        /// <summary>
        /// 真正删除客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CoustomDelete(int? uid)
        {
           
            var test = clientSer.QueryWhere(c => c.ID == uid).FirstOrDefault();
           
            clientSer.Delete(test, true);
            int result = clientSer.SaveChanges();
            if (result > 0)
            {
                return Json(result,JsonRequestBehavior.AllowGet);
            }

            return Json(0,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 客户增加
        /// </summary>
        /// <returns>返回视图</returns>
        [HttpGet]
        public ActionResult CoustomAdd()
        {            
            Model.BussionObject.CustomShow_BO mdoelbo = new Model.BussionObject.CustomShow_BO();                    
            return View(mdoelbo);
        }
        /// <summary>
        /// 客户增加提交
        /// </summary>
        /// <param name="model">提交模型</param>
        /// <returns>返回视图</returns>
        [HttpPost]
       public ActionResult CustomAdd(Model.BussionObject.CustomShow_BO model)
        {
            if (ModelState.IsValid)
            {
                Model.SB_Client clientmodel = new SB_Client()
                {                    
                    Address = model.boAddress,
                    CompanyMail = model.boCompanyMail,
                    Contacts = model.boContacts,
                    ContactsCellone = model.boContactsCellone,
                    Name = model.boName,
                    Type = model.boType,
                    state = "1",
                    ContactsMail = model.boContactsMail,
                    Remark = model.boRemark,
                    Telephone = model.boTelephone
                };
                clientSer.Add(clientmodel);
                int res = clientSer.SaveChanges();
                if (res > 0)
                {
                    return Json(new { mess="成功",value="1"},JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { mess = "失败", value = "0" }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { mess = "失败", value = "0" }, JsonRequestBehavior.AllowGet);
        }
        
      
        /// <summary>
        /// 人员信息删除
        /// </summary>
        /// <param name="uid">传入的人员id</param>
        /// <param name="type"></param>
        /// <returns>返回视图</returns>
        public ActionResult UserDelete(Guid uid,int type)
        {
            var res = userSer.QueryWhere(c => c.ID == uid).FirstOrDefault();
            Model.BussionObject.UserShow_BO model = new Model.BussionObject.UserShow_BO()
            {
                boAccount = res.Account,
                boCellphone = res.Cellphone,
                boClientID = res.ClientID,
                boEmail = res.Email,
                boID = res.ID,
                boIDNo = res.IDNo,
                boName = res.Name,
                boPassWord = res.PassWord,
                boRemark = res.Remark,
                boSex = res.Sex,
                boState = res.State,
                boUserType = res.UserType
            };

            return View(model);
        }
        /// <summary>
        /// 提交用户删除
        /// </summary>
        /// <param name="model">传入的用户信息模型</param>
        /// <returns>返回视图</returns>
        [HttpPost]
        public ActionResult UserDelete(Model.BussionObject.UserShow_BO model)
        {
            var res = userSer.QueryWhere(u => u.ID == model.boID).FirstOrDefault();
            userSer.Delete(res, true);
            int val = userSer.SaveChanges();
            if (val > 0)
            {
                return RedirectToAction("UserList", "BackManage");
            }
            else
            {
                return View(model);
            }
        }
        
        
        /// <summary>
        /// 工厂添加
        /// </summary>
        /// <returns>视图</returns>
        public ActionResult FactoryAdd()
        {
            List<Model.SB_Client> clientList = clientSer.QueryWhere();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            return View();
        }
        /// <summary>
        /// 工厂添加
        /// </summary>
        /// <param name="model">传值</param>
        /// <returns>跳转</returns>
        [HttpPost]
        public ActionResult FactoryAdd(Model.BussionObject.FactoryShow_BO model)
        {
            if (ModelState.IsValid)
            {

                Model.BB_Factory usremodel = new BB_Factory()
                {
                    Altitude = model.boAltitude,
                    AnnualOutput = model.boAnnualOutput,
                    ClientID = model.boClientID,
                    code = model.bocode,
                    Introduction = model.boIntroduction,
                    Latitude = model.boLatitude,
                    Longtitude = model.boLongtitude,
                    Name = model.boName,
                    Product = model.boProduct,
                    Remark = model.boRemark,
                    Size = model.boSize,
                    Valid = model.boValid
                };
                                          
                    factorySer.Add(usremodel);
                    int res = factorySer.SaveChanges();
                    if (res > 0)
                    {
                        return Json("OK", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                          return Json("Error", JsonRequestBehavior.AllowGet);
                    }                                                               
            }

            return View(model);          
        }
        /// <summary>
        /// 工厂修改
        /// </summary>
        /// <param name="uid">传值</param>
        /// <returns>视图</returns>
        public ActionResult FactoryModiInfo(int uid)
        {
            List<Model.SB_Client> clientList = clientSer.QueryWhere();
            SelectList selectlistClient = new SelectList(clientList, "ID", "Name");
            ViewBag.Client = selectlistClient;
            var model = factorySer.QueryWhere(u => u.ID == uid).FirstOrDefault();
            Model.BussionObject.FactoryShow_BO showmodel = new Model.BussionObject.FactoryShow_BO()
            {
                boAltitude = model.Altitude,
                boAnnualOutput = model.AnnualOutput,
                boClientID = model.ClientID,
                bocode = model.code,
                boIntroduction = model.Introduction,
                boLatitude = model.Latitude,
                boLongtitude = model.Longtitude,
                boName = model.Name,
                boProduct = model.Product,
                boRemark = model.Remark,
                boSize = model.Size,
                boValid = model.Valid,
                boID=model.ID             
            };
            return View(showmodel);
        }
        /// <summary>
        /// 工厂修改
        /// </summary>
        /// <param name="model">传值</param>
        /// <returns>跳转</returns>
        [HttpPost]
        public ActionResult FactoryModiInfo(Model.BussionObject.FactoryShow_BO model)
        {
            if (ModelState.IsValid)
            {
                Model.BB_Factory usermodel = new BB_Factory()
                {
                    Altitude = model.boAltitude,
                    AnnualOutput = model.boAnnualOutput,
                    ClientID = model.boClientID,
                    code = model.bocode,
                    Introduction = model.boIntroduction,
                    Latitude = model.boLatitude,
                    Longtitude = model.boLongtitude,
                    Name = model.boName,
                    Product = model.boProduct,
                    Remark = model.boRemark,
                    Size = model.boSize,
                    Valid = model.boValid,
                    ID = model.boID
                };
                factorySer.Update(usermodel);
                int res = factorySer.SaveChanges();
                if (res > 0)
                {
                   return Json("OK", JsonRequestBehavior.AllowGet);
                }
                else
                {
                   return Json("E", JsonRequestBehavior.AllowGet);
                }
            }
            return View(model);
        }
        /// <summary>
        /// 工厂删除
        /// </summary>
        /// <param name="uid">传值</param>
        /// <returns>视图</returns>
        public ActionResult FactoryDelete(int uid)
        {
            var model = factorySer.QueryWhere(u => u.ID == uid).FirstOrDefault();
            Model.BussionObject.FactoryShow_BO showmodel = new Model.BussionObject.FactoryShow_BO()
            {
                boAltitude = model.Altitude,
                boAnnualOutput = model.AnnualOutput,
                boClientID = model.ClientID,
                bocode = model.code,
                boIntroduction = model.Introduction,
                boLatitude = model.Latitude,
                boLongtitude = model.Longtitude,
                boName = model.Name,
                boProduct = model.Product,
                boRemark = model.Remark,
                boSize = model.Size,
                boValid = model.Valid,
                boID = model.ID

            };
            return View(showmodel);
        }
        /// <summary>
        /// 工厂删除
        /// </summary>
        /// <param name="model">值</param>
        /// <returns>跳转</returns>
        [HttpPost]
        public ActionResult FactoryDelete(Model.BussionObject.FactoryShow_BO model)
        {
            var res = factorySer.QueryWhere(u => u.ID == model.boID).FirstOrDefault();
            factorySer.Delete(res, true);
            int val = factorySer.SaveChanges();
            if (val > 0)
            {
                return RedirectToAction("FactoryList", "BackManage");
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// 图表配置界面列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult ChartAliasList()
        {
          
            return  View();           
        }
        /// <summary>
        /// 图表添加时候的Get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult ChartAliasAdd()
        {
             BB_ChartAlias chartAlias = new BB_ChartAlias();                    
            return View(chartAlias);
        }

          /// <summary>
        /// 图表添加增加提交
        /// </summary>
        /// <param name="model">提交模型</param>
        /// <returns>返回视图</returns>
        [HttpPost]
       public ActionResult ChartAliasAdd(BB_ChartAlias model)
        {
            if (ModelState.IsValid)
            {
                BB_ChartAlias chartAlias = new BB_ChartAlias()
                {                    
                     DateTime = model.DateTime,
                    code = model.code,
                    factoryId = model.factoryId,
                    factoryName = model.factoryName,
                    stationID = model.stationID,
                    stationName = model.stationName,
                    EquipID = model.EquipID,
                    EquipName = model.EquipName,
                    AreaId = model.AreaId,
                    AreaName = model.AreaName,
                    name = model.name,
                    FieldName = model.FieldName,
                    layuiTitle = model.layuiTitle,
                    layuiArea = model.layuiArea,                   
                    layuiOffset = model.layuiOffset,                   
                    chartYUnit = model.chartYUnit,
                    chartXLegend = model.chartXLegend
                };
                chartAliasSer.Add(chartAlias);
                int res = chartAliasSer.SaveChanges();
                if (res > 0)
                {
                    return Json(new { mess="成功",value="1"},JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { mess = "失败", value = "0" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { mess = "失败", value = "0" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ChartAliasGet(int page, int limit, int? id, int? code,string name)
        {
            int count;
            var res = new object();
            var resAliasesList = new object();
            List<object> list = new List<object>();
            if (id != null)
            {
                resAliasesList = chartAliasSer.QuerySplitPage(c => c.Id==id, order => order.OrderBy(o => o.Id), limit, page).Select(c => new
                {
                    c.Id,
                    c.code,
                    c.factoryId,
                    c.factoryName,
                    c.stationID,
                    c.stationName,
                    c.EquipID,
                    c.EquipName,
                    c.AreaId,
                    c.AreaName,
                    c.name,
                    c.FieldName,
                    c.layuiTitle,
                    c.layuiArea,
                    c.layuiOffset,
                    c.chartXLegend,
                    c.chartYUnit
                });

                count = chartAliasSer.Count(c=>c.Id==id);
                res = new { code = 0, msg = "success", data = resAliasesList, count };
            }
            else if (id == null && code != null)
            {
                resAliasesList = chartAliasSer.QuerySplitPage(c => c.code==code, order => order.OrderBy(o => o.Id), limit, page).Select(c => new
                {
                    c.Id,
                    c.code,
                    c.factoryId,
                    c.factoryName,
                    c.stationID,
                    c.stationName,
                    c.EquipID,
                    c.EquipName,
                    c.AreaId,
                    c.AreaName,
                    c.name,
                    c.FieldName,
                    c.layuiTitle,
                    c.layuiArea,
                    c.layuiOffset
                });

                count = chartAliasSer.Count(c=>c.code==code);
                res = new { code = 0, msg = "success", data = resAliasesList, count };
            }
            else if (id == null && code == null && name != ""&&name!=null)
            {
                resAliasesList = chartAliasSer.QuerySplitPage(c => c.EquipName.Equals(name), order => order.OrderBy(o => o.Id), limit, page).Select(c => new
                {
                    c.Id,
                    c.code,
                    c.factoryId,
                    c.factoryName,
                    c.stationID,
                    c.stationName,
                    c.EquipID,
                    c.EquipName,
                    c.AreaId,
                    c.AreaName,
                    c.name,
                    c.FieldName,
                    c.layuiTitle,
                    c.layuiArea,
                    c.layuiOffset
                });

                count = chartAliasSer.Count(c=>c.EquipName.Equals(name));
                res = new { code = 0, msg = "success", data = resAliasesList, count };
            }
            else if(id == null && code != null && name != "")
            {
                resAliasesList = chartAliasSer.QuerySplitPage(c => c.code==code&&c.EquipName.Equals(name), order => order.OrderBy(o => o.Id), limit, page).Select(c => new
                {
                    c.Id,
                    c.code,
                    c.factoryId,
                    c.factoryName,
                    c.stationID,
                    c.stationName,
                    c.EquipID,
                    c.EquipName,
                    c.AreaId,
                    c.AreaName,
                    c.name,
                    c.FieldName,
                    c.layuiTitle,
                    c.layuiArea,
                    c.layuiOffset
                });

                count = chartAliasSer.Count(c=>c.code==code&&c.EquipName.Equals(name));
                res = new { code = 0, msg = "success", data = resAliasesList, count };
            }
            else
            {
                resAliasesList = chartAliasSer.QuerySplitPage(c => true, order => order.OrderBy(o => o.Id), limit, page).Select(c => new
                {
                    c.Id,
                    c.code,
                    c.factoryId,
                    c.factoryName,
                    c.stationID,
                    c.stationName,
                    c.EquipID,
                    c.EquipName,
                    c.AreaId,
                    c.AreaName,
                    c.name,
                    c.FieldName,
                    c.layuiTitle,
                    c.layuiArea,
                    c.layuiOffset
                });

                count = chartAliasSer.QueryWhere().Count();
                res = new { code = 0, msg = "success", data = resAliasesList, count };
            }
            System.Collections.Specialized.NameValueCollection keyVals = Request.QueryString;
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChartExcel()
        {
            //返回的输出流
            MemoryStream stream = new MemoryStream();
            //创建book
            var workbook = new HSSFWorkbook();
            //创建sheet
            var sheet = workbook.CreateSheet();
            //标题行
            var headRow = sheet.CreateRow(0);
            //定义标题行
            string[] str = new string[] { "Id", "编码", "工厂编号", "工厂名称", "空压站编号", "空压站名称", " 设备编号", " 设备名称", " 区域编号", "区域名称", "名称", "显示名称", "界面标题", "界面大小", "界面位置" };
            for(int i = 0; i < str.Length; i++)
            {
                headRow.CreateCell(i).SetCellValue(str[i]);
            }
            //内容
            var con = chartAliasSer.QueryWhere();
            //第一行
            int count = 1;
            //填充内容
            foreach(var item in con)
            {
                var newheadRow = sheet.CreateRow(count);
                newheadRow.CreateCell(0).SetCellValue(item.Id);
                newheadRow.CreateCell(1).SetCellValue((double)item.code);
                newheadRow.CreateCell(2).SetCellValue(item.factoryId);
                newheadRow.CreateCell(3).SetCellValue(item.factoryName);
                newheadRow.CreateCell(4).SetCellValue(item.stationID);
                newheadRow.CreateCell(5).SetCellValue(item.stationName);
                newheadRow.CreateCell(6).SetCellValue(item.EquipID);
                newheadRow.CreateCell(7).SetCellValue(item.EquipName);
                newheadRow.CreateCell(8).SetCellValue(item.AreaId);
                newheadRow.CreateCell(9).SetCellValue(item.AreaName);
                newheadRow.CreateCell(10).SetCellValue(item.name);
                newheadRow.CreateCell(11).SetCellValue(item.FieldName);
                newheadRow.CreateCell(12).SetCellValue(item.layuiTitle);
                newheadRow.CreateCell(13).SetCellValue(item.layuiOffset);
                newheadRow.CreateCell(14).SetCellValue(item.layuiArea);
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
            string filename = DateTime.Now.ToShortDateString() + "图表配置列表.xls";
            return File(stream, "application/vnd.ms-excel", filename);
        }
    
        [HttpGet]
        public  ActionResult ChartGet()
        {
            int count;
            var res = new object();
            var resAliasesList = new object();
            //默认传送page limit
            var page = Convert.ToInt32(Request["page"]);
            var limit = Convert.ToInt32(Request["limit"]);
            int id;
            //根据搜索条件再附加上key[id]
            var obj = Request["key[id]"];
            //如果obj为null,说明没有搜索
            if (obj == null)
            {
                resAliasesList = chartAliasSer.QueryWhere(x =>true).Skip((page - 1) * limit).Take(limit).Select(c => new
                {
                    c.Id,
                    c.name,
                    c.FieldName
                });
                count =chartAliasSer.Count();
                res = new { code = 0, msg = "success", data = resAliasesList, count };
            }
            //根据搜索条件判断筛选内容
            else
            {
                var idParse = int.TryParse(Request["key[id]"], out id);
                if (idParse)
                {
                    resAliasesList = chartAliasSer.QueryWhere(x => x.Id == id).Select(c => new
                    {
                        c.Id,
                        c.name,
                        c.FieldName
                    });
                    count = chartAliasSer.QueryWhere(x => x.Id == id).Count();
                    res = new { code = 0, msg = "success", data = resAliasesList, count = count };
                }
                else
                {
                    res = new { code = 0, msg = "查询错误", data = 0, count = 0 };
                } 
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public  ActionResult ChartAliasEdit(int? id)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (chartAliasSer.QueryWhere(x => x.Id == id.Value).FirstOrDefault() == null)
            {
                return HttpNotFound();
            }
          var res=chartAliasSer.QueryWhere(x => x.Id == id.Value).FirstOrDefault();
            return View(chartAliasSer.QueryWhere(x => x.Id == id.Value).FirstOrDefault());
        }
        [HttpPost]
         public  ActionResult ChartAliasEdit(BB_ChartAlias bbChart)
        {
            //更新图表列里面的内容,同时更新缓存
            chartAliasSer.Update(bbChart);
            chartAliasSer.SaveChanges();
            var chartAliasList = chartAliasSer.QueryWhere();
            CacheMgr.SetData("chartAliasList", chartAliasList);
            return WriteSuccess("OK");
        }

    }
}