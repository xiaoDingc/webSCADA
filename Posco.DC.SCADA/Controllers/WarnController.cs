using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonHelper;
using System.Diagnostics;
using Posco.DC.WebHelper;

namespace Posco.DC.SCADA.Controllers
{
   
    public class WarnController : Controller
    {
        private ISB_UserServices userSer;
        private ISB_WarnLogServices warnlogSer;
        private ISB_ClientServices clientSer;
        private IBB_AreaServices areaSer;
        private IBB_FactoryServices factorySer;
        private IBB_InstrumentServices instrumentSer;
        private IBB_StationServices stationSer;
       
        private IAP_Centrifuge_HistoryServices centriSer;
        public WarnController(ISB_WarnLogServices warnlogser, ISB_UserServices userser, 
            IAP_Centrifuge_HistoryServices centriser,
            ISB_ClientServices clientser,
            IBB_AreaServices areaser,
            IBB_FactoryServices factoryser,
            IBB_InstrumentServices instrumentser,
            IBB_StationServices stationser)
        {
            warnlogSer = warnlogser;
            userSer = userser;
            centriSer = centriser;
            clientSer = clientser;
            areaSer = areaser;
            factorySer = factoryser;
            instrumentSer = instrumentser;
            stationSer = stationser;
        }
        // GET: Warn
        public ActionResult Index()
        {
           // var time = DateTime.Now;
           // var time1 = time.AddHours(-1);
           // List<Model.SB_WarnLog> warnlist = new List<Model.SB_WarnLog>();
           //var res= warnlogSer.QueryWhere(w=>w.Status.Equals("0")).GroupBy(g=>new { g.Status,g.WarnType}).Select(s=>new { id = s.Min(i => i.ID) } );
           // foreach(var item in res)
           // {
           //    var itemres= warnlogSer.Find(new object[] { item.id });
           //     if (itemres != null)
           //     {
           //         warnlist.Add(itemres);
           //     }
           // }        
            return View();
        }
        public ActionResult WarnRecord()
        {
            return View();
        }
        public ActionResult Test()
        {
            var time = DateTime.Now;
            var time1 = time.AddDays(-1);
            TimeCheck span = new TimeCheck();
            span.CheckTicks(time1, time);
            return View();
        }
        /// <summary>
        /// 报警提示
        /// </summary>
        /// <returns></returns>
        public ActionResult WarnTip()
        {
            var time = DateTime.Now;
            var time1 = time.AddSeconds(-6);
           var res= warnlogSer.QueryWhere(w=>w.WarnTime>=time1&&w.WarnTime<=time&&w.Status.Equals("0"));
            List<Model.SB_WarnLog> warnlog = new List<Model.SB_WarnLog>();
            foreach(var item in res)
            {
                var checkres = warnlogSer.QueryWhere(w => w.WarnType.Equals(item.WarnType) && w.Status.Equals("0")&&w.WarnTime<time1).FirstOrDefault();
                if (checkres==null)
                {
                    warnlog.Add(checkres);
                }
            }
            if (warnlog.Count > 0)
            {
                return Json(warnlog, JsonRequestBehavior.AllowGet);
            }
           
            return Json("0", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 报警处理
        /// </summary>
        /// <returns></returns>
        public ActionResult WranDeal( int wid=1)
        {
            //通过传入的id修改此行
            var res = warnlogSer.Find(new object[] { wid });
            if (res != null)
            {
                res.Status = "1";
                warnlogSer.Update(res);
                warnlogSer.SaveChanges();
                //查询所有此类报警重复行删除
                var checkres = warnlogSer.QueryWhere(w => w.WarnType.Equals(res.WarnType) && w.Status.Equals("0"));
                if (checkres.Count > 0)
                {
                    warnlogSer.DeleteList(checkres);
                }
                //添加已修改记录
                Model.SB_WarnLog warnmodel = new Model.SB_WarnLog()
                {
                    AreaID = res.AreaID,
                    FactoryID = res.FactoryID,
                    InstrumentID = res.FactoryID,
                    Remark = res.Remark,
                    StationID = res.StationID,
                    Status = "2",
                    WarnContent = res.WarnContent,
                    WarnTime = DateTime.Now,
                    WarnType = res.WarnType
                };
                warnlogSer.Add(warnmodel);
                int a = warnlogSer.SaveChanges();
                return Json(warnmodel, JsonRequestBehavior.AllowGet);

            }
            return Json("0", JsonRequestBehavior.AllowGet);
           
        }
        public ActionResult Warn(int pagesize=10,int pageindex=1)
        {
            //查询所有状态为1的报警记录取10条
            var res = warnlogSer.QuerySplitPage(w => w.Status.Equals("1"), order => order.OrderByDescending(o => o.ID), pagesize, pageindex);
            Model.PageSplitInfo pagesplit = new Model.PageSplitInfo(pagesize,pageindex,warnlogSer.Count(w=>w.Status.Equals("1")),"Warn","Warn");
            List<Model.ModelViews.WarnWarn_View> modellist = new List<Model.ModelViews.WarnWarn_View>();
            //如果有外键更好，可以直接获得
            foreach(var item in res)
            {
                //获得空压站，工厂，区域，设备内容
                var sta = stationSer.Find(new object[] { item.StationID });
                var fac = factorySer.Find(new object[] { item.FactoryID });
                var area = areaSer.Find(new object[] { item.AreaID });
                var ins = instrumentSer.Find(new object[] { item.InstrumentID });
                //传入后台的类
                Model.ModelViews.WarnWarn_View viewmodel = new Model.ModelViews.WarnWarn_View();
                viewmodel.Area = area;
                viewmodel.Fac = fac;
                viewmodel.Ins = ins;
                viewmodel.Sta = sta;
                viewmodel.Warn = item;
                modellist.Add(viewmodel);
            }
            return View(modellist);
        }
    }
}