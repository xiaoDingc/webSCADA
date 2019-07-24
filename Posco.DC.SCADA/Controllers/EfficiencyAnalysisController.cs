﻿namespace Posco.DC.SCADA.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using IServices;
    using Newtonsoft.Json;
    using Posco.DC.WebHelper;
    using Model;
    using System.Web.Script.Serialization;
    using System.Diagnostics;

    public class EfficiencyAnalysisController : Controller
    {
        private IAP_Factory_RealServices factoryRealSer;
        private IAP_Station_RealServices stationRealSer;
        private IPG_Area_RealServices areaRealSer;
        private IAP_Factory_StatisticsServices factoryStatisticSer;
        private IAP_Station_StatisticsServices stationStatisticSer;
        private IAP_Centrifuge_StatisticsServices centriStatisticSer;
        private IAP_Centrifuge_RealServices centrifugeRealSer;
        private IBB_StationServices bbstationSer;
        private IAP_Station_HistoryServices stationHisSer;
        private IAP_Factory_HistoryServices factionHisSer;
        private IAP_Centrifuge_HistoryServices centrifugeHisSer;
        private IAP_Factory_HistoryServices hisSer;
        log4net.ILog log = log4net.LogManager.GetLogger("testApp.Logging");//获取一个日志记录器

        /// <summary>
        /// Initializes a new instance of the <see cref="EfficiencyAnalysisController"/> class.
        /// 构造函数注入参数
        /// </summary>
        /// <param name="factoryser">工厂对象</param>
        /// <param name="stationser">空压站对象</param>
        /// <param name="areaser">区域对象</param>
        /// <param name="factorystaser">工厂统计对象</param>
        /// <param name="centrifugeser">空压机对象</param>
        public EfficiencyAnalysisController(
            IAP_Factory_RealServices factoryser,
            IAP_Station_RealServices stationser,
            IPG_Area_RealServices areaser,
            IAP_Factory_StatisticsServices factorystaser,
            IAP_Centrifuge_RealServices centrifugeser,
            IBB_StationServices bstationser, IAP_Station_HistoryServices stationHisser,
            IAP_Factory_HistoryServices factionHisser,
            IAP_Centrifuge_HistoryServices centrifugeHisser, IAP_Station_StatisticsServices stationStatisticser,
            IAP_Centrifuge_StatisticsServices centriStatisticser, IAP_Factory_HistoryServices hisser)
        {
            factoryRealSer = factoryser;
            stationRealSer = stationser;
            areaRealSer = areaser;
            factoryStatisticSer = factorystaser;
            centrifugeRealSer = centrifugeser;
            bbstationSer = bstationser;
            stationHisSer = stationHisser;
            factionHisSer = factionHisser;
            centrifugeHisSer = centrifugeHisser;
            stationStatisticSer = stationStatisticser;
            centriStatisticSer = centriStatisticser;
            hisSer = hisser;
        }
        CommonHelper.TimeCheck check = new CommonHelper.TimeCheck();
        // GET: EfficiencyAnalysis
        public ActionResult Index()
        {
            return View();
        }
        #region 工厂统计分析
        /// <summary>
        /// 总产气量图界面
        /// </summary>
        /// <returns>返回一个视图</returns>
        public ActionResult ProduceFlowPage()
        {
            DateTime hiscurrtime = DateTime.Now;
            DateTime hisbegtime = hiscurrtime.AddDays(-1);
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = hisbegtime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = hiscurrtime.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                var res = factoryRealSer.Produceflow(hiscurrtime, hisbegtime, 24);
                ViewBag.fpres = JsonConvert.SerializeObject(res);
              
                return View(viewDate);
            }
            catch(Exception e)
            {
                log.Info(DateTime.Now.ToString() + e.Message+e.StackTrace);//写入一条新log
                return View(viewDate);
            }
        }

        /// <summary>
        /// 总产气量图
        /// </summary>
        /// <param name="strJson">json字符串</param>
        /// <param name="time">开始时间</param>
        /// <param name="aftertime">结束时间</param>
        /// <returns>返回json字符串</returns>
        public ActionResult ProduceFlow(string strJson, DateTime begintime, DateTime endtime)
        {
            //当前时间 
            DateTime NowTime = DateTime.Now;
            //当前时间前24小时
            DateTime NowbeforeTime = NowTime.AddHours(-24);
            //判断开始时间begintime是否大于等于NowbeforeTime
            //访问实时表
            if (begintime >= NowbeforeTime)
            {
                var res1 = factoryRealSer.Produceflow(endtime, begintime, 23);
                return Json(res1, JsonRequestBehavior.AllowGet);
            }
            //历史表和统计表访问
            else
            {
                DateTime endTime = endtime;
                DateTime beginTime = begintime;
                var time = check.CheckTicks(beginTime, endTime);
                var timehour = time[1];
                var timeday = time[0];
                var res = hisSer.Produceflow(endTime, beginTime, (int)timehour);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
           
            
            
        }
        /// <summary>
        /// UPI和放散率散点图界面
        /// </summary>            
        /// <returns>返回视图</returns>
        public ActionResult AnalysisScatterPage()
        {
            var hiscurrtime = DateTime.Now;
            var hisbegtime = hiscurrtime.AddDays(-1);
            var res = factoryRealSer.EScatterSer(hiscurrtime, hisbegtime);
            ViewBag.str = JsonConvert.SerializeObject(res);
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = hisbegtime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = hiscurrtime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// UPI和放散率散点图界面历史
        /// </summary>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public ActionResult AnalysisScatter(DateTime begintime,DateTime endtime)
        {
            //当前时间 
            DateTime NowTime = DateTime.Now;
            //当前时间前24小时
            DateTime NowbeforeTime = NowTime.AddHours(-24);
            //判断开始时间begintime是否大于等于NowbeforeTime
            //访问实时表
            if (begintime >= NowbeforeTime)
            {
                var res1 = factoryRealSer.EScatterSer(endtime, begintime);
                return Json(res1, JsonRequestBehavior.AllowGet);
            }
            //访问历史表
           else
            {
                var time = check.CheckTicks(begintime, endtime);
                var timehour = time[1];
                var timeday = time[0];
                var res = hisSer.ScatterSer(endtime, begintime, (int)timehour);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
      
        /// <summary>
        /// 放散率概率分布图界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Loss_RatioPage() {
            var hiscurrtime = DateTime.Now;
            var hisbegtime = hiscurrtime.AddDays(-1);
            double[] zzz = new double[] { 0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12, 12.5, 13, 13.5, 14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19, 19.5, 20, 20.5, 21, 21.5, 22, 25 };
            var res = factoryRealSer.LossSer(hiscurrtime, hisbegtime);
            ViewBag.tit = JsonConvert.SerializeObject(res);
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = hisbegtime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = hiscurrtime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// 放散率概率分布图
        /// </summary>
        /// <param name="strJson">概率分布区间值</param>
        /// <param name="begintime">选择开始时间</param>
        /// <param name="endtime">选择结束时间</param>
        /// <returns></returns>
        public ActionResult Loss_Ratio(string strJson,DateTime begintime,DateTime endtime)
        {
            //当前时间 
            DateTime NowTime = DateTime.Now;
            //当前时间前24小时
            DateTime NowbeforeTime = NowTime.AddHours(-24);
            //判断开始时间begintime是否大于等于NowbeforeTime
            //访问实时表
            if (begintime >= NowbeforeTime)
            {
                var res1 = factoryRealSer.LossSer(endtime, begintime);
                return Json(res1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                double[] zzz = new double[] { 0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12, 12.5, 13, 13.5, 14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19, 19.5, 20, 20.5, 21, 21.5, 22, 25 };
                var res = factionHisSer.LossSer(endtime, begintime, zzz);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ProduceEpowerPage()
        {
            var hiscurrtime = DateTime.Now;
            var hisbegtime = hiscurrtime.AddDays(-1);
            var res = factoryRealSer.ProduceEpower(hiscurrtime, hisbegtime);
            ViewBag.fpe = JsonConvert.SerializeObject(res);
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = hisbegtime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = hiscurrtime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        public ActionResult ProduceEpower(DateTime begintime, DateTime endtime)
        {
            //当前时间 
            DateTime NowTime = DateTime.Now;
            //当前时间前24小时
            DateTime NowbeforeTime = NowTime.AddHours(-24);
            //判断开始时间begintime是否大于等于NowbeforeTime
            //访问实时表
            if (begintime >= NowbeforeTime)
            {
                var res1 = factoryRealSer.ProduceEpower(endtime, begintime);
                return Json(res1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var res = factionHisSer.ProduceEpower(endtime, begintime);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 空压站统计分析
        /// <summary>
        /// 空压站总产气量界面
        /// </summary>
        /// <returns></returns>
        public ActionResult StationProduceFlowPage(string sid) {
            var hiscurrtime = DateTime.Now;
            var hisbegtime = hiscurrtime.AddDays(-1);
            int[] arr = new int[] { 0, 15000, 30000, 45000, 60000, 75000, 90000, 105000};
            var res = stationRealSer.StaProduce(hiscurrtime, hisbegtime,sid);
            ViewBag.StationP = JsonConvert.SerializeObject(res);
            ViewBag.Sid=sid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = hisbegtime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = hiscurrtime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// 空压站总产气量图
        /// </summary>
        /// <param name="strJson">概率分布区间值</param>
        /// <param name="begintime">选择开始时间</param>
        /// <param name="endtime">选择结束时间</param>
        /// <returns></returns>
        public ActionResult StationProduceFlow(string strJson, DateTime begintime, DateTime endtime,string sid) {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            if (begintime >= NowbeforeTime)
            {
                var res = stationRealSer.StaProduce(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int[] arr = new int[] { 0, 15000, 30000, 45000, 60000, 75000, 90000, 105000 };
                var res = stationHisSer.StaProduce(endtime, begintime, arr, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            
        }
        /// <summary>
        /// 空压站单耗放散率
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Station_UPI_LossRatio_MainP_QPage(string sid)
        {
            var hiscurrtime = DateTime.Now;
            var hisbegtime = hiscurrtime.AddDays(-1);
            var res = stationRealSer.StaUpiLossMainpq(hiscurrtime, hisbegtime,sid);
            ViewBag.stationpq = JsonConvert.SerializeObject(res);
            ViewBag.Sid=sid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = hisbegtime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = hiscurrtime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// 空压站单耗放散率压力流量图
        /// </summary>
        /// <param name="begintime">选择开始时间</param>
        /// <param name="endtime">选择结束时间</param>
        /// <returns>返回一个集合给ajax</returns>
        public ActionResult S_UPI_Loss_MainP_Q(DateTime begintime, DateTime endtime,string sid)
        {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            if (begintime >= NowbeforeTime)
            {
                var res = stationRealSer.StaUpiLossMainpq(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var res = stationHisSer.StaUpiLossMainpq(endtime, begintime,sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 空压站压力功率界面
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult StationEpowerMainPPage(string sid )
        {
            DateTime endTime = DateTime.Now ;
            DateTime begTime = endTime.AddDays(-1);
            var res = centrifugeRealSer.StaMainpq(endTime, begTime,sid);
            ViewBag.stationep = JsonConvert.SerializeObject(res);
            ViewBag.Sid=sid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = begTime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = endTime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// 空压站压力功率图
        /// </summary>
        /// <param name="begintime">选择开始时间</param>
        /// <param name="endtime">选择结束时间</param>
        /// <returns>返回json到ajax</returns>
        public ActionResult StationEpowerMainP(DateTime begintime, DateTime endtime,string sid)
        {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            if (begintime >= NowbeforeTime)
            {
                var res = centrifugeRealSer.StaMainpq(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var res = centrifugeHisSer.StaMainpq(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
           
            
        }
        /// <summary>
        /// 空压站放散率概率分布界面
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult StationLossRatioProbablyPage(string sid)
        {
            var hiscurrtime = DateTime.Now;
            var hisbegtime = hiscurrtime.AddDays(-1);
            double[] zzz = new double[] {3,6,9,12,15,18,21,2,42,45,50};
            var res = stationRealSer.StaLoss(hiscurrtime, hisbegtime, zzz,sid);
            ViewBag.stationloss = JsonConvert.SerializeObject(res);
            ViewBag.Sid=sid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = hisbegtime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = hiscurrtime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// 空压站放散率概率图
        /// </summary>
        /// <param name="strJson">放散率区间值</param>
        /// <param name="begintime">选择开始时间</param>
        /// <param name="endtime">选择结束时间</param>
        /// <returns></returns>
        public ActionResult StationLossRatioProbably(string strJson, DateTime begintime, DateTime endtime,string sid)
        {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            double[] zzz = new double[2];
            if (begintime >= NowbeforeTime)
            {
                var res = stationRealSer.StaLoss(endtime, begintime,zzz, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var res = stationHisSer.StaLoss(endtime, begintime,zzz, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 空压站气量功率
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult StaProduceEpowerPage(string sid)
        {
            var hiscurrtime = DateTime.Now;
            var hisbegtime = hiscurrtime.AddDays(-1);
            var res = stationRealSer.StaProduceEpower(hiscurrtime, hisbegtime,sid);
            ViewBag.stape = JsonConvert.SerializeObject(res);
            ViewBag.Sid = sid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = hisbegtime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = hiscurrtime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        public ActionResult StaProduceEpower(DateTime begintime, DateTime endtime, string sid)
        {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            if (begintime >= NowbeforeTime)
            {
                var res = stationRealSer.StaProduceEpower(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var res = stationHisSer.StaProduceEpower(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 空压机统计分析
        /// <summary>
        /// 空压机产气量功率图
        /// </summary>
        /// <returns></returns>
        public ActionResult CentriProduceEpowerPage( string sidcid)
        {
            DateTime endTime = DateTime.Now;
            DateTime begTime = endTime.AddDays(-1);
            var res = centrifugeRealSer.CentriProduceEpower(endTime, begTime, sidcid);
            ViewBag.faceres = JsonConvert.SerializeObject(res);
            ViewBag.Fsidcid =sidcid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = begTime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = endTime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// 空压机产气量功率Ajax
        /// </summary>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult CentriProduceEpower(DateTime begintime,DateTime endtime,string sid)
        {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            if (begintime >= NowbeforeTime)
            {
                var res = centrifugeRealSer.CentriProduceEpower(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int[] arr = new int[] { 0, 15000, 30000, 45000, 60000, 75000, 90000, 105000 };
                var res = centrifugeHisSer.CentriProduceEpower(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 空压机放散率BOV打开度界面
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult CentrifugeLossRatioPage(string sidcid)
        {
            var endTime = DateTime.Now;
            var begTime = endTime.AddDays(-1);
            var res = centrifugeRealSer.CentriBovLoss(endTime, begTime, sidcid);
            ViewBag.faceres = JsonConvert.SerializeObject(res);
            ViewBag.Fsidcid = sidcid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = begTime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = endTime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// 空压机放散率BOV打开度图
        /// </summary>
        /// <param name="JsonStr">放散率区间值</param>
        /// <param name="begintime">选择开始时间</param>
        /// <param name="endtime">选择结束时间</param>
        /// <returns></returns>
        public ActionResult CentrifugeLossRatio(string JsonStr, DateTime begintime, DateTime endtime,string sid)
        {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            if (begintime >= NowbeforeTime)
            {
                var res = centrifugeRealSer.CentriBovLoss(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int[] arr = new int[] { 0, 15000, 30000, 45000, 60000, 75000, 90000, 105000 };
                var res = centrifugeHisSer.CentriBovLoss(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 空压机upi放散率界面  暂时没做
        /// </summary>
        /// <returns></returns>
        public ActionResult Centrifuge_Upi_Loss_Outlet_PQPage( string sidcid)
        {
            var endTime = DateTime.Now;
            var begTime = endTime.AddDays(-1);
            var res = centrifugeRealSer.CentriUpiLoss(endTime, begTime, sidcid);
            ViewBag.faceres = JsonConvert.SerializeObject(res);
            ViewBag.Fsidcid = sidcid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = begTime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = endTime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        /// <summary>
        /// 空压机upi放散率图
        /// </summary>
        /// <param name="begintime">选择开始时间</param>
        /// <param name="endtime">选择结束时间</param>
        /// <returns></returns>
        public ActionResult CentrifugeUpiLossOutletpQ(DateTime begintime, DateTime endtime,string sid)
        {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            if (begintime >= NowbeforeTime)
            {
                var res = centrifugeRealSer.CentriUpiLoss(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int[] arr = new int[] { 0, 15000, 30000, 45000, 60000, 75000, 90000, 105000 };
                var res = centrifugeHisSer.CentriUpiLoss(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 压力功率界面 
        /// </summary>
        /// <returns></returns>
        public ActionResult CentrifugePePowerPage( string sidcid)
        {
            var endTime = DateTime.Now;
            var begTime = endTime.AddDays(-1);
            var res = centrifugeRealSer.CentriPEpower(endTime, begTime,sidcid);
            ViewBag.faceres = JsonConvert.SerializeObject(res);
            ViewBag.Fsidcid = sidcid;
            Model.ModelViews.DateTime_View viewDate = new Model.ModelViews.DateTime_View();
            viewDate.Time1 = begTime.ToString("yyyy-MM-dd HH:mm:ss");
            viewDate.Time2 = endTime.ToString("yyyy-MM-dd HH:mm:ss");
            return View(viewDate);
        }
        public ActionResult CentrifugePePower(DateTime begintime, DateTime endtime,string sid)
        {
            //当前时间前24小时
            DateTime NowbeforeTime = DateTime.Now.AddHours(-24);
            if (begintime >= NowbeforeTime)
            {
                var res = centrifugeRealSer.CentriPEpower(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int[] arr = new int[] { 0, 15000, 30000, 45000, 60000, 75000, 90000, 105000 };
                var res = centrifugeHisSer.CentriPEpower(endtime, begintime, sid);
                return Json(res, JsonRequestBehavior.AllowGet);
            };
        }
        #endregion
    }
}
