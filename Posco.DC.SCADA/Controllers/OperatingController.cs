using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CommonHelper;
using System.IO;
using NPOI.HSSF.UserModel;

namespace Posco.DC.SCADA.Controllers
{
    public class OperatingController : Controller
    {
        private ISB_OperateHistoryServices operathisSer;
        private ISB_PageServices pageSer;
        private ISB_ClientServices clientSer;
        private IBB_InstrumentServices instrumentSer;
        private IBB_StationServices stationSer;
        private IBB_AreaServices areaSer;
        private IBB_FactoryServices factorySer;
        public OperatingController(ISB_OperateHistoryServices Operathisser, ISB_PageServices PageSer, ISB_ClientServices ClientSer,
            IBB_InstrumentServices InstrumentSer, IBB_StationServices stationser, IBB_AreaServices areaser, IBB_FactoryServices factoryser)
        {
            operathisSer = Operathisser;
            pageSer = PageSer;
            clientSer = ClientSer;
            instrumentSer = InstrumentSer;
            stationSer = stationser;
            areaSer = areaser;
            factorySer = factoryser;
        }
        public OperatingController()
        {

        }
        /// <summary>
        /// 操作日志界面
        /// </summary>
        /// <param name="pageSize">行数</param>
        /// <param name="pageIndex">页数</param>
        /// <returns>视图</returns>
        public ActionResult Index()
        {
            var b = Session["URL"];
            return View();
            
        }
        /// <summary>
        /// 系统操作历史记录表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="name"></param>
        /// <param name="begtime"></param>
        /// <param name="currtime"></param>
        /// <returns></returns>
        public ActionResult SysRecord( int limit,int page,string name,string begtime,string currtime,string type)
        {
            var res = operathisSer.OperatSysRecord(limit, page,name,begtime,currtime,type);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
      public ActionResult OperatRecordAdd()
        {
            return null;
        }
        /// <summary>
        /// 备用导出报表，现不用
        /// </summary>
        /// <param name="time"></param>
        /// <param name="time1"></param>
        /// <returns></returns>
        public ActionResult operatHisExcel(DateTime time,DateTime time1)
        {
            //根据时间查询内容
           var res= operathisSer.QueryWhereDesc(o => o.OperateTime > time1 && o.OperateTime <= time, order => order.OrderByDescending(or => or.ID));
            //标题内容
            string[] strtitle = new string[] { "用户ID", "用户名称", "使用者登录IP", "操作类型", "操作子类型", "操作时间", "操作记录", "访问地址", "地址中文", "客户", "设备", "空压站", "区域", "工厂", "备注" };
            //定义返回的stream流
            MemoryStream stream = new MemoryStream();
            //创建workbook
            var workbook = new HSSFWorkbook();
            //创建sheet
            var sheet = workbook.CreateSheet();
            //创建空行标题头
            var headRow = sheet.CreateRow(0);
            //向空行内添加标题内容
            for(var i = 0; i < strtitle.Length; i++)
            {
                headRow.CreateCell(i).SetCellValue(strtitle[i]);
            }
            //添加内容
            int count = 1;
            foreach(var item in res)
            {
                //新建内容
                var newheadRow = sheet.CreateRow(count);
                //string inname;
                //string faname;
                //string staname;
                //string areaname;
                //string clientname;
                //string parname;
                //string chlidname;
                if (item.InstrumentID != null)
                {
                    var inres=instrumentSer.QueryWhere(i => i.ID == item.InstrumentID).FirstOrDefault();
                    var facres = factorySer.QueryWhere(f => f.ID == item.FactoryID).FirstOrDefault();
                    var stares = stationSer.QueryWhere(s => s.ID == item.StationID).FirstOrDefault();
                    var clires = clientSer.QueryWhere(c => c.ID == item.ClientID).FirstOrDefault();
                }
                newheadRow.CreateCell(0).SetCellValue(item.ID);
                newheadRow.CreateCell(1).SetCellValue(item.UserName);
                newheadRow.CreateCell(2).SetCellValue(item.IPAddress);
                newheadRow.CreateCell(3).SetCellValue(item.OperateType);
                newheadRow.CreateCell(4).SetCellValue(item.OperateChildType);
                newheadRow.CreateCell(5).SetCellValue(item.OperateTime.ToString());
                newheadRow.CreateCell(6).SetCellValue(item.OperateRecord);
                newheadRow.CreateCell(7).SetCellValue(item.PageURL);
                newheadRow.CreateCell(8).SetCellValue(item.PageNameCN);
                newheadRow.CreateCell(9).SetCellValue(item.ClientID.ToString());
                newheadRow.CreateCell(10).SetCellValue(item.InstrumentID.ToString());
                newheadRow.CreateCell(11).SetCellValue(item.StationID.ToString());
                newheadRow.CreateCell(12).SetCellValue(item.AreaID.ToString());
                newheadRow.CreateCell(13).SetCellValue(item.FactoryID.ToString());
                newheadRow.CreateCell(14).SetCellValue(item.Remark);

                count++;
            }

            // workbook写入stream流
            workbook.Write(stream);

            // 清理资源
            stream.Flush();
            stream.Position = 0;
            sheet = null;
            headRow = null;
            workbook = null;
            string filename = DateTime.Now.ToShortDateString() + "操作日志.xls";
            return File(stream, "application/vnd.ms-excel", filename);
        }
       
    }
}