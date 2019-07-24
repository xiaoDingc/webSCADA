// <copyright file="EnergySysController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using Model;
using NPOI.HSSF.Record.Chart;
using NPOI.SS.Formula.Functions;
using Newtonsoft.Json;

namespace Posco.DC.SCADA.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using CommonHelper;
    using IServices;
    using Newtonsoft.Json;
    using Posco.DC.WebHelper;
    using Repository.Base;

    public class EnergySysController : BaseController
    {
        private IAP_Factory_RealServices factory_realSer;
        private IAP_Station_RealServices station_realSer;
        private IPG_Area_RealServices area_realSer;
        private IAP_Factory_StatisticsServices factory_statisticSer;
        private IAP_AdsorptionDryer_RealServices dryer_RealServices;
        private IAP_Station_HistoryServices station_hisSer;
        private IBB_ChartAliasServices chartAliasServices;
        private IAP_Factory_HistoryServices factory_HisServices;
        private IAP_AdsorptionDryer_RealServices dryerRealServices;
        private IBB_AreaServices bbareaServices;
        private IPG_Area_HistoryServices area_hisSer;
        private IAP_Centrifuge_HistoryServices centrifuge_hisSer;
        private IAP_Centrifuge_RealServices cenrealSer;
        private IAP_AdsorptionDryer_RealServices dryrealSer;
        private IBB_ThresholdServices thresholdSer;
        private ISB_WarnLogServices warnSer;

        //private  DateTime chooseTime=DateTime.Parse("2019.04.12 00:00:00");

        public EnergySysController(IAP_Factory_RealServices factoryser, 
            IAP_Factory_HistoryServices factory_Hisser, 
            IAP_Station_RealServices stationser, 
            IPG_Area_RealServices areaser,
            IAP_Factory_StatisticsServices factorystaser, 
            IAP_Centrifuge_RealServices centrifugeser, 
            IAP_AdsorptionDryer_RealServices dryerSer,
            IBB_ChartAliasServices chartAliasSer,
            IAP_AdsorptionDryer_RealServices dryerRealSer, 
            IBB_AreaServices bbareaSer , 
            IPG_Area_HistoryServices area_hisser,
            IAP_Centrifuge_RealServices cenrealser,
            IAP_Station_HistoryServices station_hisser,
            IAP_AdsorptionDryer_RealServices dryrealser,
            IBB_ThresholdServices thresholdser,
            ISB_WarnLogServices warnser)
        {
            factory_realSer = factoryser;
            station_realSer = stationser;
            area_realSer = areaser;
            factory_statisticSer = factorystaser;
            cenrealSer = centrifugeser;
            dryer_RealServices = dryerSer;
            factory_HisServices = factory_Hisser;
            chartAliasServices = chartAliasSer;
            dryerRealServices = dryerRealSer;
            bbareaServices = bbareaSer;
            area_hisSer = area_hisser;
            cenrealSer = cenrealser;
            station_hisSer = station_hisser;
            dryrealSer = dryrealser;
            thresholdSer = thresholdser;
            warnSer = warnser;
        }
        #region 大数据界面
        /// <summary>
        /// 大数据界面ajax每5分钟
        /// </summary>
        /// <returns>匿名类</returns>
        public ActionResult FiveMHomeIndex()
        {
            try
            {
                DateTime hiscurrtime = DateTime.Now;
                DateTime hisbegtime = hiscurrtime.AddDays(-1);
                DateTime hisFive = hiscurrtime.AddMinutes(-5);

                //upi放散率散点图
                var scatterres = factory_realSer.ScatterSer(hiscurrtime, hisbegtime);

                //放散率分布图
                double[] zzz = new double[] { 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 10.5, 11, 11.5, 12, 12.5, 13, 13.5, 14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19, 19.5, 20, 20.5, 21, 21.5, 22, 25 };
                var freqres = factory_realSer.FrequencySer(hiscurrtime, hisbegtime, zzz);
                //蜘蛛图
                var spidres = station_realSer.SprideSer(hiscurrtime, hisFive);

                var Viewtran = new
                {
                    scatter = scatterres,
                    freq = freqres,
                    spid = spidres,
                };
                return new JsonResult
                {
                    Data = Viewtran,
                    MaxJsonLength = int.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Data + "");
            }
        }

        /// <summary>
        /// 空压站界面ajax
        /// </summary>
        /// <returns>匿名类</returns>
        public ActionResult StationSpider(string stationId)
        {
            try
            {
                DateTime hiscurrtime = DateTime.Now;
                DateTime hisFive = hiscurrtime.AddMinutes(-5);
                //蜘蛛图
                var spidres = station_realSer.SprideSer(hiscurrtime,hisFive, stationId);
                var Viewtran = new
                {
                    spider = spidres,
                };
                return new JsonResult
                {
                    Data = Viewtran,
                    MaxJsonLength = int.MaxValue,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Data + "");
            }
        }
        /// <summary>
        /// 大数据界面ajax每6秒
        /// </summary>
        /// <returns></returns>
        public ActionResult SixSHomeIndex()
        {
            DateTime hiscurrtime = DateTime.Now;
            DateTime hisbegtime = hiscurrtime.AddDays(-1);
            DateTime hissecond = hiscurrtime.AddSeconds(-24);
            DateTime hissecond1 = hiscurrtime.AddSeconds(-6);
            DateTime hisHour = hiscurrtime.AddHours(-1);
            DateTime hismonthtime = hiscurrtime.AddDays(-(hiscurrtime.Day - 1)).Date;
            DateTime hisdate = hiscurrtime.AddMinutes(-1);
            //产气图
            var gasprores = station_realSer.GasProSer(hiscurrtime, hissecond);
            //用气图
            var gasuseres = area_realSer.GasUseSer(hiscurrtime, hissecond);
            //瞬时流量图
            var momres = factory_realSer.MomentSer(hiscurrtime, hissecond, hismonthtime);
            var swires = new object();
            //当天的23：59：59
            DateTime comparetime = hiscurrtime.Date.AddDays(1).AddSeconds(-1);
            //当时时间+6秒//53+6
            DateTime nowcomparetime = hiscurrtime.AddSeconds(6);
            if (nowcomparetime > comparetime)
            {
                swires = cenrealSer.SwitchStackSer(hiscurrtime, hissecond);
            }
            else
            {
                swires = cenrealSer.SwitchSer(hiscurrtime, hissecond);
            }
            //报警信息
            var warn = warnSer.QueryWhere(w => w.WarnTime >= hissecond1 && w.WarnTime <= hiscurrtime).Select(s => new
            {
                content = s.WarnContent,
            }).ToList();
            var transView = new
            {
                gaspro = gasprores,
                gasuse = gasuseres,
                mom = momres,
                swi = swires,
                warnlog = warn//报警
            };
            return new JsonResult
            {
                Data = transView,
                MaxJsonLength = int.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        /// <summary>
        /// 大数据界面开启必要台数实时ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Supply()
        {
            DateTime currtime = DateTime.Now;
            DateTime begtime = currtime.AddSeconds(-24);
            var res = factory_realSer.SupplySer(currtime, begtime);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 大数据界面功率压力流量实时ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult StackProEpq()
        {
            DateTime currtime = DateTime.Now;
            DateTime begtime = currtime.AddSeconds(-24);
            var res = factory_realSer.StackProEpq(currtime, begtime);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 优化指导
        /// <summary>
        /// 优化指导界面
        /// </summary>
        /// <returns></returns>
        public ActionResult SysEnergyAnalsysPage()
        {
            DateTime hiscurrtime = DateTime.Now;
            DateTime hisbegtime = hiscurrtime.AddDays(-7);
            DateTime hissecond = hiscurrtime.AddSeconds(-24); 
             var swires = cenrealSer.SwitchStackSer(hiscurrtime, hissecond);
            ViewBag.onoff = JsonConvert.SerializeObject(swires);
            //过取一周数据统计
            var stares = station_hisSer.SysEnergyAnalsys(hiscurrtime, hisbegtime);
            ViewBag.datapress = JsonConvert.SerializeObject(stares);
            return View();
        }
        /// <summary>
        /// 优化指导开关机提醒
        /// </summary>
        /// <returns></returns>
        public ActionResult SysAnalsysSwitchAjax()
        {
            DateTime hiscurrtime = DateTime.Now;
            DateTime hissecond = hiscurrtime.AddSeconds(-24);
            #region 缓存
            //获取缓存对象，任何阈值设定界面值的修改，都要缓存当前
            List<BB_Threshold> cacheThre= CacheMgr.GetData<List<BB_Threshold>>("threshold");
            //判断是否有缓存存在，不存在重新设置
            if (cacheThre == null)
            {
                List<BB_Threshold> threshold = thresholdSer.QueryWhere().ToList();
                CacheMgr.SetData("threshold", threshold);
                cacheThre = threshold;
            }
            #endregion
            #region 能效排行
            var swires = new object();
            //当天的23：59：59
            DateTime comparetime = hiscurrtime.Date.AddDays(1).AddSeconds(-1);
            //当时时间+6秒//53+6
            DateTime nowcomparetime = hiscurrtime.AddSeconds(6);
            if (nowcomparetime > comparetime)
            {
                swires = cenrealSer.SwitchStackSer(hiscurrtime, hissecond);
            }
            else
            {
                swires = cenrealSer.SwitchSer(hiscurrtime, hissecond);
            }
            #endregion
            //获取当前upi和产气量之和
            var facres = factory_realSer.EnergyAnalsysUPI(hiscurrtime, hissecond);
            //获得upi设定值和可关台数设定值
           decimal UpiK=(decimal) cacheThre.Where(x => x.Name.Equals("UPI设定")).FirstOrDefault().CurrentValue;
           decimal NC = (decimal)cacheThre.Where(x => x.Name.Equals("可关台数值设定")).FirstOrDefault().CurrentValue;
            int onoff = 0;
            double[] darr = new double[4];
            List<int> stanum = new List<int>();
            //判断Upik和facres中的upi
            if (facres[0] > UpiK)
            {
                //计算N值
                var N = facres[2] - Math.Round((decimal)facres[1] / 15000, 2);
                //循环判断N和N设定值的大小
                while (N > NC)
                {
                    double maxn = 0;//最大值
                    int indexof = 0;//索引
                    if (onoff == 0)
                    {
                       List<int> starun= station_realSer.CentriRun_Number(hiscurrtime, hissecond);
                       int n1 = starun[0] - starun[1]/60;
                        int n2 = starun[2] - starun[3]/60;
                        int n3 = starun[4] - starun[5]/60;
                        int n4 = starun[6] - starun[7]/60;
                        darr = new double[4] { n1, n2, n3, n4 };
                        //得到最大值和其索引
                        for (int i = 0; i < darr.Length; i++)
                        {
                            if (darr[i] > maxn)
                            {
                                maxn = darr[i];
                                indexof = i;
                            }
                        }
                        darr[indexof] = darr[indexof] - 1;//最大值减去1
                        stanum.Add(indexof);//空压站号
                    }
                    if (onoff > 0)
                    {
                        for (int i = 0; i < darr.Length; i++)
                        {
                            if (darr[i] > maxn)
                            {
                                maxn = darr[i];
                                indexof = i;
                            }
                        }
                     
                        darr[indexof] = darr[indexof] - 1;
                        stanum.Add(indexof);
                    }
                    double[] test = darr;
                    onoff++;
                    N--;
                }
            }
            stanum.Sort();
            int[] inarr = new int[] { 0,1, 2, 3};
            List<Model.ViewObject.EnergyAnalysysOnOff> all = new List<Model.ViewObject.EnergyAnalysysOnOff>();
            foreach (var item in inarr)
            {
                var dd = stanum.Count(s => s.Equals(item));
                Model.ViewObject.EnergyAnalysysOnOff zz = new Model.ViewObject.EnergyAnalysysOnOff()
                {
                    sta = item,
                    cen = dd
                };
                all.Add(zz);
            }

            return Json(new { swi = swires, Onoff = onoff, OnoffList = all, time = hiscurrtime.ToString() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 趋势分析
        /// <summary>
        /// 趋势分析
        /// </summary>
        /// <returns></returns>
        public ActionResult TrendPage()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                throw new Exception("" + e.Data);
            }
        }
        /// <summary>
        /// 趋势分析工厂实时图
        /// </summary>
        /// <returns></returns>
        public ActionResult TrendFacProducePressSumPower(string id)
        {
            var currtime = DateTime.Now;
            var begtime = currtime.AddHours(-1);
            List<int> numlist = new List<int>();
            List<string> namestr = new List<string>();
            if (id == null)
            {
                numlist.Add(1);
                numlist.Add(2);
                numlist.Add(3);
            }
            else
            {
                numlist = JsonConvert.DeserializeObject<List<int>>(id);
            }
            foreach (var item in numlist)
            {
                namestr.Add(Enum.GetName(typeof(Enums.FacField), item).ToString());
            }

            var res = new object();
            res = factory_realSer.TrendFacProducePressSumPower(currtime, begtime);
            var tran = new
            {
                Res = res,
                namelist = namestr,
                Numlist = numlist
            };
            ViewBag.FacFSP = JsonConvert.SerializeObject(tran);
            return View();
        }
        /// <summary>
        /// 趋势分析工厂实时图ajax
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TrendFacProducePressSumPowerAjax(string id)
        {
            DateTime currtime = DateTime.Now;
            DateTime begtime = currtime.AddSeconds(-24);
            List<int> numlist = new List<int>();
            List<string> namestr = new List<string>();
            numlist = JsonConvert.DeserializeObject<List<int>>(id);
            foreach (var item in numlist)
            {
                namestr.Add(Enum.GetName(typeof(Enums.FacField), item).ToString());
            }
            var res = factory_realSer.TrendFacProducePressSumPowerAjax(currtime, begtime);
            return Json(new { Res = res, namelist = namestr }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 空压站实时趋势分析图
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult TrendStation(string sfieldstr, string stastr)
        {
            DateTime currtime = DateTime.Now;
            DateTime begtime = currtime.AddHours(-1);
            List<string> stalist = new List<string>();
            List<string> stafieldlist = new List<string>();
            if (sfieldstr == null && stastr == null)
            {
                stalist.Add("S001");
                stafieldlist.Add("产气量");
                stafieldlist.Add("电功率");
                stafieldlist.Add("压力");
            }
            else
            {
                stalist = JsonConvert.DeserializeObject<List<string>>(stastr);
                stafieldlist = JsonConvert.DeserializeObject<List<string>>(sfieldstr);
            }
            var res = new object();
            res = station_realSer.TrendStation(currtime, begtime, stalist);
            var tran = new
            {
                stastr = stalist,
                stafieldstr = stafieldlist,
                Res = res
            };
            ViewBag.FacFSP = JsonConvert.SerializeObject(tran);
            return View();
        }
        /// <summary>
        /// 空压站趋势分析实时图ajax
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult TrendStationAjax(string sfieldstr, string sstastr)
        {
            try
            {
                List<string> stalist = JsonConvert.DeserializeObject<List<string>>(sstastr);
                List<string> stafieldlist = JsonConvert.DeserializeObject<List<string>>(sfieldstr); ;


                DateTime currtime = DateTime.Now;
                DateTime begtime = currtime.AddSeconds(-24);
                var res = station_realSer.TrendStationAjax(currtime, begtime, stalist);
                return Json(new { Res = res, stastr=stalist,field=stafieldlist }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception("" + e.Data);
            }
        }
        /// <summary>
        /// 空压机趋势分析实时图
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="sid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult TrendCentri(string cfieldstr, string censtr)
        {
            List<string> cenlist = new List<string>();
            List<string> cenfeildlist = new List<string>();
            if (cfieldstr == null && censtr == null)
            {
                cenlist.Add("S001E001");
                cenfeildlist.Add("IGV开度");
                cenfeildlist.Add("BOV开度");
            }
            else
            {
                cenlist = JsonConvert.DeserializeObject<List<string>>(censtr);
                cenfeildlist = JsonConvert.DeserializeObject<List<string>>(cfieldstr);
            }


            var currtime = DateTime.Now;
            var begtime = currtime.AddHours(-1);
            var res = cenrealSer.TrendCentri(currtime, begtime, cenlist);
            var tran = new
            {
                censtrlist = cenlist,
                cenfieldlist = cenfeildlist,
                Res = res
            };
            ViewBag.CenFSP = JsonConvert.SerializeObject(tran);
            return View();
        }
        /// <summary>
        /// 空压机趋势分析实时图ajax
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="sid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult TrendCentriAjax(string cfieldstr, string ccenstr)
        {


            DateTime currtime = DateTime.Now;
            DateTime begtime = currtime.AddSeconds(-24);
            List<string> cenlist = JsonConvert.DeserializeObject<List<string>>(ccenstr);
            List<string> cenfeildlist = JsonConvert.DeserializeObject<List<string>>(cfieldstr);
            var res = cenrealSer.TrendCentriAjax(currtime, begtime, cenlist);
            return Json(new { Res = res, censtrlist = cenlist, cenfieldlist = cenfeildlist }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 趋势分析干燥机实时图
        /// </summary>
        /// <param name="dfieldstr"></param>
        /// <param name="drystr"></param>
        /// <returns></returns>
        public ActionResult TrendDryer(string dfieldstr, string drystr)
        {
            List<string> drylist = new List<string>();
            List<string> dryfeildlist = new List<string>();
            if (dfieldstr == null && drystr == null)
            {
                drylist.Add("S001E101");
                dryfeildlist.Add("进气压力");
                dryfeildlist.Add("出气压力");
            }
            else
            {
                drylist = JsonConvert.DeserializeObject<List<string>>(drystr);
               dryfeildlist = JsonConvert.DeserializeObject<List<string>>(dfieldstr);
            }


            var currtime = DateTime.Now;
            var begtime = currtime.AddHours(-1);
            var res = dryrealSer.TrendDryer(currtime, begtime, drylist);
            var tran = new
            {
                drystrlist = drylist,
                dryfieldlist = dryfeildlist,
                Res = res
            };
            ViewBag.DryFSP = JsonConvert.SerializeObject(tran);
            return View();
        }
        public ActionResult TrendDryerAjax(string dfieldstr, string drystr)
        {
            DateTime currtime = DateTime.Now;
            DateTime begtime = currtime.AddSeconds(-24);
            List<string> drylist = JsonConvert.DeserializeObject<List<string>>(drystr);
            List<string> dryfeildlist = JsonConvert.DeserializeObject<List<string>>(dfieldstr);
            var res = dryrealSer.TrendDryerAjax(currtime, begtime, drylist);
            return Json(new { Res = res, drystrlist = drylist, dryfieldlist = dryfeildlist }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 用户区域趋势分析实时图
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="aname"></param>
        /// <returns></returns>
        public ActionResult TrendArea(string aid, string aname,string name)
        {


            DateTime currtime = DateTime.Now;
            DateTime begtime = currtime.AddHours(-1);
            List<string> arealist = new List<string>();
            List<string> areafieldlist = new List<string>();
            List<string> areanamelist = new List<string>();
            if (aid == null && aname == null&&name==null)
            {
                arealist.Add("A001");
                areafieldlist.Add("用气量");
                areanamelist.Add("1BF");
            }
            else
            {
                arealist = JsonConvert.DeserializeObject<List<string>>(aid);
                areafieldlist = JsonConvert.DeserializeObject<List<string>>(aname);
                areanamelist = JsonConvert.DeserializeObject<List<string>>(name);
            }
            var res = area_realSer.TrendArea(currtime, begtime, arealist);
            var tran = new { Res = res, Aid = arealist, Aname=areafieldlist,Aidname= areanamelist };
            ViewBag.Area = JsonConvert.SerializeObject(tran);
            return View();
        }
        /// <summary>
        /// 用户区域趋势分析实时图ajax
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ActionResult TrendAreaAjax(string aid, string aname)
        {


            DateTime currtime = DateTime.Now;
            DateTime begtime = currtime.AddSeconds(-24);
            List<string> arealist = JsonConvert.DeserializeObject<List<string>>(aid);
            List<string> areafieldlist = JsonConvert.DeserializeObject<List<string>>(aname);
            var res = area_realSer.TrendAreaAjax(currtime, begtime, arealist);
            var tranres = new
            {
                Res = res,
                area = arealist,
                field = areafieldlist
            };
            return Json(tranres, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// 工厂系统状态图
        /// </summary>
        /// <returns></returns>
        public ActionResult SysStatus()
        {
            DateTime currentTime = DateTime.Now;
            DateTime beforeTime = currentTime.AddMinutes(-5);
            //空压站只要取倒数4条的就行了
            var stations = station_realSer.QueryWhere(x => true && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                .Skip(0).Take(4).Select(x => new
                {
                    Station = x.StationID,
                    DateTime = x.DateTime.ToString(),
                    Main_Q = x.Main_Q/1000,  //由m3/h转换为kM3/h
                    Main_P = x.Main_P,
                }).OrderBy(x => x.Station);
            //22个用户区域
            var areas = area_realSer.QueryWhere(x => true && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                .Skip(0).Take(29).Select(x => new
                {
                    areaId = x.AreaID,
                    DateTime = x.DateTime.ToString(),
                    Inlet_AP = x.Inlet_AP*10,//由MPa转换为bar
                    Inlet_SQ = x.Inlet_SQ,
                }).OrderBy(x => x.areaId).ToList();

            //产气量
            var gaspro = station_realSer.GasProSer(currentTime, beforeTime);
            //用气图
            var gasuse = area_realSer.GasUseSer(currentTime, beforeTime);

            var obj = new { stations, areas, gaspro, gasuse };
            var jsonObj = JsonConvert.SerializeObject(obj);
            ViewBag.infoObject = jsonObj;
            return View();
        }
        /// <summary>
        /// 6s获取工厂图请求函数
        /// </summary>
        /// <param name="factoryId">工厂Id</param>
        /// <returns></returns>
        public ActionResult SixSecondStationAndUserArea(string factoryId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime beforeTime = currentTime.AddMinutes(-5);
            //空压站只要取倒数4条的就行了
            var stations = station_realSer.QueryWhere(x => true && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                .Skip(0).Take(4).Select(x => new
                {
                    Station = x.StationID,
                    DateTime = x.DateTime.ToString(),
                    Main_Q = x.Main_Q / 1000,  //单位转换 由 Nm3转换为kNm3
                    Main_P = x.Main_P,
                }).OrderBy(x => x.Station);
            //用户区域
            var areas = area_realSer.QueryWhere(x => true && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                 .Skip(0).Take(29).Select(x => new
                 {
                     areaId = x.AreaID,
                     DateTime = x.DateTime.ToString(),
                     Inlet_AP = x.Inlet_AP * 10,//由MPa转换为bar,
                     Inlet_SQ = x.Inlet_SQ,
                 }).OrderBy(x => x.areaId).ToList();

            //产气量
            var gaspro = station_realSer.GasProSer(currentTime, beforeTime);
            //用气图
            var gasuse = area_realSer.GasUseSer(currentTime, beforeTime);

            var obj = new { stations, areas, gaspro, gasuse };
            var jsonObj = JsonConvert.SerializeObject(obj);
            return Json(jsonObj, JsonRequestBehavior.AllowGet);
        }
  
        /// <summary>
        /// 用户区域highcharts图
        /// </summary>
        /// <param name="factoryId">工厂Id</param>
        /// <param name="code">编码</param>
        /// <param name="areaId">区域Id</param>
        /// <param name="name">名称</param>
        /// <param name="chartXLegend">X轴图例</param>
        /// <param name="chartYUnit">Y轴单位</param>
        /// <param name="preview">预览</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LineChartArea(string factoryId, int? code, string areaId, string name,
            string factoryName, string areaName, string stationName, string equipName, string fieldName,
            string chartXLegend, string chartYUnit, bool? preview)
        {

            NameReslove.Reslove(ref factoryName, ref areaName, ref stationName, ref equipName, ref fieldName);

            //code 1代表压力  2代表用户区域流量  10*60*24*7 /50 =2016

            var currentTime = DateTime.Now;

            var beforeTime = currentTime.AddDays(-1);
            var infoObject = new object();
            object userAreas = new object();

            //每隔多少行取一条数据
            var countSplit = 1;
            //每间隔22个数据取一次
            var rows = 22;
            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long timezone = StartTime.Ticks;
            var trantimezone = (DateTime.Parse("" + DateTime.Now).AddHours(8).Ticks - timezone) / 10000;
            //1代表用户区域Inlet_AP 用户入口压力
            if (code == 1)
            {
                userAreas = area_realSer.QueryWhere(x => x.FactoryID == factoryId && x.AreaID == areaId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                   .Select(x => new
                   {
                       mId = x.Id,
                       DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                       value = x.Inlet_AP,
                   }).OrderBy(x => x.mId).ToList();
            }
            //2代表用户区域Inlet_SQ 用户入口瞬时流量
            else if (code == 2)
            {
                userAreas = area_realSer.QueryWhere(x => x.FactoryID == factoryId && x.AreaID == areaId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Inlet_SQ,
                 }).OrderBy(x => x.mId).ToList();
            }
            else if (code == 3)
            {
                userAreas = area_realSer.QueryWhere(x => x.FactoryID == factoryId && x.AreaID == areaId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Inlet_SQ,
                     value2 = x.Inlet_AP,
                 }).OrderBy(x => x.mId).ToList();
            }
            //做一次数据转换,可以不用修改前端代码,节省代码量
            var centrifuges = userAreas;
            //如果preview表明为预览,无需从数据库查询配置文件 原值传回
            if (preview.HasValue)
            {
                infoObject = new { chartYUnit, chartXLegend, name = fieldName, factoryId = factoryName, areaId = areaName, data = centrifuges };
            }
            else
            {
                var chartAlias = chartAliasServices.QueryWhere(x => x.factoryId == factoryId && x.AreaId == areaId && x.name == name).FirstOrDefault();
                infoObject = new { chartYUnit = chartAlias.chartYUnit, chartXLegend = chartAlias.chartXLegend, name = chartAlias.FieldName, factoryId = chartAlias.factoryName, areaId = chartAlias.AreaName, data = centrifuges };
            }

            var jsonCentrifuges = JsonConvert.SerializeObject(infoObject);
            ViewBag.infoObject = jsonCentrifuges;
            return View();
        }
        /// <summary>
        ///工厂空压站highcharts图
        /// </summary>
        /// <param name="factoryId">工厂Id</param>
        /// <param name="code">编码</param>
        /// <param name="stationId">区域Id</param>
        /// <param name="name">名称</param>
        /// <param name="chartXLegend">X轴图例</param>
        /// <param name="chartYUnit">Y轴单位</param>
        /// <param name="preview">预览</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LineChartStation(string factoryId, int? code, string stationId, string name,
             string factoryName, string areaName, string stationName, string equipName, string fieldName,
            string chartXLegend, string chartYUnit, bool? preview)
        {
            //!转换为原来的#
            NameReslove.Reslove(ref factoryName, ref areaName, ref stationName, ref equipName, ref fieldName);

            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long timezone = StartTime.Ticks;
            var trantimezone = (DateTime.Parse("" + DateTime.Now).AddHours(8).Ticks - timezone) / 10000;

            //  var getHisTime=(DateTime.Now-chooseTime).Days;
            //  var currentTime = DateTime.Now.AddDays(getHisTime);
            var currentTime = DateTime.Now;

            //code 1代表空压站供气压力  2代表空压站产气量  10*60*24*7 /50 =2016
            //  var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddDays(-1);
            var infoObject = new object();
            object stations = new object();
            //每隔多少行取一条数据
            var countSplit = 1;
            //每间隔22个数据取一次
            var rows = 4;
            //code 1代表空压站供气压力  2代表空压站产气量
            if (code == 1)
            {

                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                   .Select(x => new
                   {
                       mId = x.Id,
                       DateTime = (DateTime.Parse("" +x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                       value = x.Main_P.Value,
                   }).OrderBy(x => x.mId).ToList();

            }
            //code 1代表空压站供气压力  2代表空压站产气量
            else if (code == 2)
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Main_Q.Value / 1000  //单位转换由 Nm3/h转换为kNm3/h,
                 }).OrderBy(x => x.mId).ToList();
            }
            else if (code == 3)
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Main_Q.Value/1000, //单位转换由 Nm3/h转换为kNm3/h
                     value2 = x.Main_P.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            //做一次数据转换,可以不用修改前端代码,节省代码量
            var centrifuges = stations;
            //如果preview表明为预览,无需从数据库查询配置文件 原值传回
            if (preview.HasValue)
            {
                infoObject = new { chartYUnit, chartXLegend, name = fieldName, factoryId = factoryName, areaId = stationName, data = centrifuges };
            }
            else
            {
                var chartAlias = chartAliasServices.QueryWhere(x => x.stationID == stationId && x.stationID == stationId && x.name == name).FirstOrDefault();
                infoObject = new { chartYUnit = chartAlias.chartYUnit, chartXLegend = chartAlias.chartXLegend, name = chartAlias.FieldName, factoryId = chartAlias.factoryName, areaId = chartAlias.stationName, data = centrifuges };
            }

            var jsonCentrifuges = JsonConvert.SerializeObject(infoObject);
            ViewBag.infoObject = jsonCentrifuges;
            return View("LineChartArea");
        }
        /// <summary>
        ///空压站highcharts图
        /// </summary>
        /// <param name="factoryId">工厂Id</param>
        /// <param name="code">编码</param>
        /// <param name="stationId">区域Id</param>
        /// <param name="name">名称</param>
        /// <param name="chartXLegend">X轴图例</param>
        /// <param name="chartYUnit">Y轴单位</param>
        /// <param name="preview">预览</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LineChartSingleStation(string factoryId, int? code, string stationId, string name,
             string factoryName, string areaName, string stationName, string equipName, string fieldName,
            string chartXLegend, string chartYUnit, bool? preview)
        {
            //!转换为原来的#
            NameReslove.Reslove(ref factoryName, ref areaName, ref stationName, ref equipName, ref fieldName);
            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long timezone = StartTime.Ticks;
            var trantimezone = (DateTime.Parse("" + DateTime.Now).AddHours(8).Ticks - timezone) / 10000;

            var currentTime = DateTime.Now;
            if (factoryId == null)
                factoryId = "F001";
            //code 1代表空压站供气压力  2代表空压站产气量  10*60*24*7 /50 =2016
            //  var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddDays(-1);
            var infoObject = new object();
            object stations = new object();
            //每隔多少行取一条数据
            var countSplit = 1;
            //每间隔22个数据取一次
            var rows = 4;
            //code 1代表空压站供气压力  2代表空压站产气量
            if (name == "Main_Q")
            {

                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                   .Select(x => new
                   {
                       mId = x.Id,
                       DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                       value = x.Main_Q.Value,
                   }).OrderBy(x => x.mId).ToList();

            }
            //code 1代表空压站供气压力  2代表空压站产气量
            if (name == "Main_P")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Main_P.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            if (name == "ePower")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.ePower.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            if (name == "UPI")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.UPI.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            if (name == "FLP")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.FLP.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            if (name == "Air_T")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Air_T.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            if (name == "Air_H")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Air_H.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            if (name == "Water_P")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Water_P.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            if (name == "Water_T")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Water_T.Value,
                 }).OrderBy(x => x.mId).ToList();
            }
            if (name == "Water_F")
            {
                stations = station_realSer.QueryWhere(x => x.FactoryID == factoryId && x.StationID == stationId
                              && x.DateTime >= beforeTime && x.DateTime <= currentTime
                              && x.Id / rows % countSplit == 0)
                 .Select(x => new
                 {
                     mId = x.Id,
                     DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                     value = x.Water_F.Value/1000, //单位转换
                 }).OrderBy(x => x.mId).ToList();
            }
            //做一次数据转换,可以不用修改前端代码,节省代码量
            var centrifuges = stations;
            //如果preview表明为预览,无需从数据库查询配置文件 原值传回
            if (preview.HasValue)
            {
                infoObject = new { chartYUnit, chartXLegend, name = fieldName, factoryId = factoryName, areaId = stationName, data = centrifuges };
            }
            else
            {
                var chartAlias = chartAliasServices.QueryWhere(x => x.stationID == stationId && x.stationID == stationId && x.name == name).FirstOrDefault();
                infoObject = new { chartYUnit = chartAlias.chartYUnit, chartXLegend = chartAlias.chartXLegend, name = chartAlias.FieldName, factoryId = chartAlias.factoryName, areaId = chartAlias.stationName, data = centrifuges };
            }

            var jsonCentrifuges = JsonConvert.SerializeObject(infoObject);
            ViewBag.infoObject = jsonCentrifuges;
            return View("LineChartCentri");
        }




        /// <summary>
        /// 空压系统highcharts图表
        /// </summary>
        /// <param name="code">设备码</param>
        /// <param name="stationId">空压站Id</param>
        /// <param name="equipId">设备Id</param>
        /// <param name="name">字段名</param>
        /// <param name="chartXLegend">X轴图例</param>
        /// <param name="chartYUnit">Y轴单位</param>
        /// <param name="preview">预览</param>
        /// <returns></returns>       
        [HttpGet]
        public ActionResult LineChart(int? code, string stationId, string equipId, string name,
                string factoryName, string areaName, string stationName, string equipName, string fieldName,
            string chartXLegend, string chartYUnit, bool? preview)
        {
            //!转换为原来的#
            NameReslove.Reslove(ref factoryName, ref areaName, ref stationName, ref equipName, ref fieldName);
            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long timezone = StartTime.Ticks;
            var trantimezone = (DateTime.Parse("" + DateTime.Now).AddHours(8).Ticks - timezone) / 10000;

            //code 1代表空压机Q  2代表空压机Press  10*60*24*7 /50 =2016
            var currentTime = DateTime.Now;
            //var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddDays(-1);
            var infoObject = new object();
            object centrifuges = new object();
            //每隔多少行取一条数据
            var countSplit = 1;
            //每间隔25个数据取一次
            var rows = 25;
            #region code 编码判断过程
            if (code == 10)
            {

                centrifuges = station_realSer.QueryWhere(x => x.StationID == stationId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                   .Select(x => new
                   {
                       mId = x.Id,
                       DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                       value = x.Main_P.Value,
                   }).OrderBy(x => x.mId).ToList();

            }

            if (code == 1)
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                   .Select(x => new
                   {
                       mId = x.Id,
                       DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                       EquipId = x.EquipID,
                       value = x.Q.Value/1000,//单位转换
                   }).OrderBy(x => x.mId).ToList();

            }
            //2代表空压机Outlet_P
            else if (code == 2)
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                 && x.Id / rows % countSplit == 0)
                  .Select(x => new
                  {
                      mId = x.Id,
                      DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                      EquipId = x.EquipID,
                      value = x.Outlet_P.Value,
                  }).OrderBy(x => x.mId).ToList();
                //3 代表空压机加载效率
            }
            else if (code == 3)
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                 && x.Id / rows % countSplit == 0)
                    .Select(x => new
                    {
                        mId = x.Id,
                        DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                        EquipId = x.EquipID,
                        value = x.LossRatio.Value,
                    }).OrderBy(x => x.mId).ToList();
                //4 代表空压机功率
            }
            else if (code == 4)
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                 && x.Id / rows % countSplit == 0)
                    .Select(x => new
                    {
                        mId = x.Id,
                        DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                        EquipId = x.EquipID,
                        value = x.ePower.Value,
                    }).OrderBy(x => x.mId).ToList();
            } //5 代表空压机UPI
            else if (code == 5)
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                 && x.Id / rows % countSplit == 0)
                    .Select(x => new
                    {
                        mId = x.Id,
                        DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                        EquipId = x.EquipID,
                        value = x.UPI.Value*1000,//单位转换
                    }).OrderBy(x => x.mId).ToList();
            } //6 代表干燥机压差
            else if (code == 6)
            {
                centrifuges = dryer_RealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                 && x.Id / rows % countSplit == 0)
                    .Select(x => new
                    {
                        mId = x.Id,
                        DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                        EquipId = x.EquipID,
                        value = x.CDyer_InOutValue.Value/100,//单位转换
                    }).OrderBy(x => x.mId).ToList();
            }//7 代表干燥机吸干露点
            else if (code == 7)
            {
                centrifuges = dryer_RealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                 && x.Id / rows % countSplit == 0)
                    .Select(x => new
                    {
                        mId = x.Id,
                        DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                        EquipId = x.EquipID,
                        value = x.CDyer_DewPoint.Value,
                    }).OrderBy(x => x.mId).ToList();
            }
            #endregion
            //如果preview表明为预览,无需从数据库查询配置文件 原值传回
            if (preview.HasValue)
            {
                infoObject = new { chartYUnit, chartXLegend, name = fieldName, stationId = stationName, equipId = equipName, data = centrifuges };
            }
            else
            {
                var chartAlias = chartAliasServices.QueryWhere(x => x.stationID == stationId && x.EquipID == equipId && x.name == name).FirstOrDefault();
                infoObject = new { chartYUnit = chartAlias.chartYUnit, chartXLegend = chartAlias.chartXLegend, name = chartAlias.FieldName, stationId = chartAlias.stationName, equipId = chartAlias.EquipName, data = centrifuges };
            }
            var jsonCentrifuges = JsonConvert.SerializeObject(infoObject);
            ViewBag.infoObject = jsonCentrifuges;
            return View();
        }

        public ActionResult LinePreview(string factoryName, string areaName, string fieldName, string stationName, string equipName, string name, string chartXLegend, string chartYUnit)
        {
            var infoObject = new { chartYUnit, chartXLegend, name, stationId = stationName, equipId = equipName, };
            var jsonCentrifuges = JsonConvert.SerializeObject(infoObject);
            ViewBag.infoObject = jsonCentrifuges;
            return View();
        }
        /// <summary>
        /// 空压机数据枚举
        /// </summary>
        public enum CentriCode
        {
            Outlet_P,
            Outlet_T,
            Q,
            Accumulated_Q,
            LossRatio,
            RET_T1,
            RET_T2,
            RET_T3,
            Inlet_dp,
            InletIOpen,
            ePower,
            UPI,
            DiscgargeOpen,
            C,
            T3Pressure,
            PrimaryAir,
            TwoStageAir,
            ThreeStageAir
        }
        /// <summary>
        /// highCharts空压机内部图表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="stationId"></param>
        /// <param name="equipId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult LineChartCentri(int? code, string stationId, string equipId, string name,
               string factoryName, string areaName, string stationName, string equipName, string fieldName,
            string chartXLegend, string chartYUnit, bool? preview)
        {
            //!转换为原来的#
            NameReslove.Reslove(ref factoryName, ref areaName, ref stationName, ref equipName, ref fieldName);
            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long timezone = StartTime.Ticks;
            var trantimezone = (DateTime.Parse("" + DateTime.Now).AddHours(8).Ticks - timezone) / 10000;

            //code 1代表空压机Q  2代表空压机Press  10*60*24*7 /50 =2016
            var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddDays(-1);
            var infoObject = new object();
            object centrifuges = new object();
            //每隔多少行取一条数据
            var countSplit = 1;
            //每间隔25个数据取一次
            var rows = 25;
            // code顺序 采用枚举进行处理 需减去1和枚举对应
            code--;
            #region code 编码判断过程
            #region 空压机判断过程
            if (code == Convert.ToInt16(CentriCode.Outlet_P))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                  .Select(x => new
                  {
                      mId = x.Id,
                      DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                      EquipId = x.EquipID,
                      value = x.Outlet_P.Value,
                  }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.Outlet_T))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                                .Select(x => new
                                {
                                    mId = x.Id,
                                    DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                    EquipId = x.EquipID,
                                    value = x.Outlet_T.Value,
                                }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.Q))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = Math.Round(x.Q.Value / 1000, 2), //单位转换 由Nm3转换为kNm3
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.Accumulated_Q))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.Accumulated_Q.Value / 1000, //单位转换 由Nm3转换为kNm3,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.LossRatio))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                            && x.Id / rows % countSplit == 0)
                            .Select(x => new
                            {
                                mId = x.Id,
                                DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                EquipId = x.EquipID,
                                value = x.LossRatio.Value,
                            }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.RET_T1))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                    && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                    && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.RET_T1.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.RET_T2))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.RET_T2.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.RET_T3))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.RET_T3.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.Inlet_dp))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.Inlet_dp.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.InletIOpen))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.InletIOpen.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.ePower))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.ePower.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (code == Convert.ToInt16(CentriCode.UPI))
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.UPI.Value*1000,
                              }).OrderBy(x => x.mId).ToList();
            }

            else if (name == CentriCode.C.ToString())
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.C.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (name == CentriCode.DiscgargeOpen.ToString())
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.DiscgargeOpen.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            if (name == CentriCode.T3Pressure.ToString())
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.T3Pressure.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (name == CentriCode.PrimaryAir.ToString())
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.PrimaryAir.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (name == CentriCode.TwoStageAir.ToString())
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.TwoStageAir.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            else if (name == CentriCode.ThreeStageAir.ToString())
            {
                centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.ThreeStageAir.Value,
                              }).OrderBy(x => x.mId).ToList();
            }
            #endregion

            #region 干燥机判断过程
            if (name == "CDyer_LeakT")
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                       && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                       && x.Id / rows % countSplit == 0)
                          .Select(x => new
                          {
                              mId = x.Id,
                              DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                              EquipId = x.EquipID,
                              value = x.CDyer_LeakT.Value,
                          }).OrderBy(x => x.mId).ToList();
            }
            if(name== "CDyer_InOutValue")
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                                          && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                                          && x.Id / rows % countSplit == 0)
                                             .Select(x => new
                                             {
                                                 mId = x.Id,
                                                 DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                                 EquipId = x.EquipID,
                                                 value = x.CDyer_InOutValue.Value/100,//单位转换 由kpa转换成bar
                                             }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDryer_InletPre")
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                           && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                           && x.Id / rows % countSplit == 0)
                              .Select(x => new
                              {
                                  mId = x.Id,
                                  DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                  EquipId = x.EquipID,
                                  value = x.CDryer_InletPre.Value/100,  //单位转换 由kpa转换成bar
                              }).OrderBy(x => x.mId).ToList();
            }

            if (name == "CDryer_OutPre")
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDryer_OutPre.Value/100,//单位转换 由kpa转换成bar
                               }).OrderBy(x => x.mId).ToList();
            }

            if (name == "powerPure")
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = Math.Round(decimal.Parse(Math.Sqrt(3).ToString()) * 380 * (x.CDyer_YSJ_A.Value + x.CDyer_JRQ_A.Value) * 85 / 100 / 1000, 2),
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "pressTextPure")//压差
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDyer_InOutValue.Value,
                               }).OrderBy(x => x.mId).ToList();
            }

            if (name == "CDyer_YSJ_A")//组合式干燥机压缩机电流值
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDyer_YSJ_A.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDyer_JRQ_A")//干燥机鼓风机电流值
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDyer_JRQ_A.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDryer_CondPre")//干燥机冷凝压力值
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDryer_CondPre.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDryer_EvapPre")//干燥机蒸发压力值
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDryer_EvapPre.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDyer_INT")//干燥机入口温度
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDyer_INT.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDyer_OutT")//干燥机出口温度
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDyer_OutT.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDyer_ATowerT") //干燥机A塔温度值
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDyer_ATowerT.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDyer_BTowerT") //干燥机B塔温度值
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDyer_BTowerT.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            if (name == "CDyer_DewPoint") //干燥机吸干露点温度值
            {
                centrifuges = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId
                                            && x.DateTime >= beforeTime && x.DateTime <= currentTime
                                            && x.Id / rows % countSplit == 0)
                               .Select(x => new
                               {
                                   mId = x.Id,
                                   DateTime = (DateTime.Parse("" + x.DateTime).AddHours(8).Ticks - timezone) / 10000,
                                   EquipId = x.EquipID,
                                   value = x.CDyer_DewPoint.Value,
                               }).OrderBy(x => x.mId).ToList();
            }
            #endregion

            #endregion
            if (preview.HasValue)
            {
                infoObject = new { chartYUnit, chartXLegend, name = fieldName, stationId = stationName, equipId = equipName, data = centrifuges };
            }
            else
            {
                var chartAlias = chartAliasServices.QueryWhere(x => x.stationID == stationId && x.EquipID == equipId && x.name == name).FirstOrDefault();
                infoObject = new { chartYUnit = chartAlias.chartYUnit, chartXLegend = chartAlias.chartXLegend, name = chartAlias.FieldName, stationId = chartAlias.stationName, equipId = chartAlias.EquipName, data = centrifuges };
            }
            var jsonCentrifuges = JsonConvert.SerializeObject(infoObject);
            ViewBag.infoObject = jsonCentrifuges;
            return View();
        }

        /// <summary>
        /// 空压站系统状态图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StationStatus(string stationID)
        {
            int selectCount = 6;
            if (stationID == null)
            {
                stationID = "S001";
            }
            int sId = Convert.ToInt32(stationID.Substring(3));
            var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddMinutes(-5);
            //干燥机
            var drys = dryer_RealServices.QueryWhere(x => x.StationID == stationID && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
               .Skip(0).Take(selectCount).Select(x => new
               {
                   Station = x.StationID,
                   dryEquipID = x.EquipID,
                   DateTime = x.DateTime.ToString(),
                   CDyer_InOutValue = Math.Round((decimal)x.CDyer_InOutValue / 100,2),//单位转换
                   CDyer_LeakT =Math.Round((decimal)x.CDyer_LeakT,2),
                   CDyer_DewPoint = Math.Round((decimal)x.CDyer_DewPoint,2),
                   x.Dry_Trap,
                   Valve = x.ValveK
               });

            //空压机
            var centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationID && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                .Skip(0).Take(selectCount).Select(x => new
                {
                    Station = x.StationID,
                    EquipID = x.EquipID,
                    DateTime = x.DateTime.ToString(),
                    Q = Math.Round(x.Q.Value / 1000, 2),//单位转换
                    Run = x.Run,
                    DRE = Math.Round((decimal)x.DRE,2),
                    Load = x.Load,
                    Outlet_P = Math.Round((decimal)x.Outlet_P,2),
                    LossRatio = Math.Round((decimal)x.LossRatio,2),
                    EPower = Math.Round((decimal)x.ePower,2),
                    UPI = Math.Round((decimal)x.UPI * 1000,2),
                    x.Ret_Trap1,
                    x.Ret_Trap2,
                    x.Ret_Trap3,
                    Inlet_dp= Math.Round((decimal)x.Inlet_dp,2)
                }).ToList();

            var station = station_realSer.QueryWhere(x => x.StationID == stationID && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                .Skip(0).Take(1).Select(x => new
                {
                    x.StationID,
                    DateTime = x.DateTime.ToString(),
                    Main_Q = x.Main_Q.Value / 1000,//单位转换
                    Main_P= Math.Round((decimal)x.Main_P,2),
                    ePower=Math.Round((decimal)x.ePower,2),
                    UPI = Math.Round((decimal)x.UPI.Value * 1000,2), //单位转换
                    FLP=Math.Round((decimal)x.FLP,2),
                    Water_P=Math.Round((decimal)x.Water_P,2),
                    Water_F = Math.Round((decimal)x.Water_F / 1000,2),//单位转换
                    Water_T= Math.Round((decimal)x.Water_T,2),
                    Air_T=Math.Round((decimal)x.Air_T,2),
                    Air_H=Math.Round((decimal)x.Air_H,2)
                }).FirstOrDefault();
            //蜘蛛图
            var spider = station_realSer.SprideSer(currentTime, beforeTime);

            var obj = new { centrifuges, drys, stationID = sId, spider, station };
            var jsonObj = JsonConvert.SerializeObject(obj);
            ViewBag.infoObject = jsonObj;
            return View();
        }
        /// <summary>
        /// 空压站6s取值程序
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StationPerSecData(string stationID)
        {
            var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddMinutes(-5);
            int selectCount = 6;
            //干燥机
            if (stationID == "S004")
            {
                selectCount = 7;
            }
            var station = station_realSer.QueryWhere(x => x.StationID == stationID && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                .Skip(0).Take(1).Select(x => new
                {
                    x.StationID,
                    DateTime = x.DateTime.ToString(),
                    Main_Q=x.Main_Q/1000,  //单位转换 由 Nm3转换为kNm3
                    x.Main_P,
                    x.ePower,
                    UPI=x.UPI.Value*1000,//单位转换
                    x.FLP,
                    x.Water_P,
                    Water_F = x.Water_F / 1000,//单位转换
                    x.Water_T,
                    x.Air_T,
                    x.Air_H,
                }).FirstOrDefault();
            var drys = dryer_RealServices.QueryWhere(x => x.StationID == stationID && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                .Skip(0).Take(selectCount).Select(x => new
                {
                    Station = x.StationID,
                    dryEquipID = x.EquipID,
                    DateTime = x.DateTime.ToString(),
                    CDyer_InOutValue = x.CDyer_InOutValue/100,//单位转换
                    CDyer_DewPoint = Math.Round((decimal)x.CDyer_DewPoint, 2),
                    CDyer_LeakT = Math.Round((decimal)x.CDyer_LeakT,2),
                    x.Dry_Trap,
                    Valve=x.ValveK,
                });

            //空压机
            var centrifuges = cenrealSer.QueryWhere(x => x.StationID == stationID && x.DateTime >= beforeTime && x.DateTime <= currentTime).OrderByDescending(x => x.Id)
                .Skip(0).Take(selectCount).Select(x => new
                {
                    Station = x.StationID,
                    EquipID = x.EquipID,
                    DateTime = x.DateTime.ToString(),
                    Q = Math.Round(x.Q.Value/1000,2),
                    Run = x.Run,
                    DRE = x.DRE,
                    Load = x.Load,
                    Outlet_P = x.Outlet_P,
                    LossRatio = x.LossRatio,
                    EPower = x.ePower,
                    UPI = x.UPI*1000,//单位转换
                    x.Ret_Trap1,
                    x.Ret_Trap2,
                    x.Ret_Trap3,
                    x.Inlet_dp
                }).ToList();
            //蜘蛛图
            var spider = station_realSer.SprideSer(currentTime, beforeTime, stationID);

            //var obj = new { centrifuges, drys, stationID = sId, spider };
            var obj = new { centrifuges, drys, spider, station };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 空压机内部状态图
        /// </summary>
        /// <param name="stationId">空压站Id</param>
        /// <param name="equipId">设备Id</param>
        /// <returns></returns>
        public ActionResult CentriStatus(string stationId, string equipId)
        {
            var currentTime = DateTime.Now;
            //var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddMinutes(-5);
            if (stationId == null && equipId == null)
            {
                stationId = "S001";
                equipId = "E001";
            }
            int sId = Convert.ToInt32(stationId.Substring(3));
            int eId = Convert.ToInt32(equipId.Substring(3));
            var alarmLines = thresholdSer.QueryWhere(x => x.Name.Contains("能效线")).ToList();

            var obj = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId && x.DateTime >= beforeTime && x.DateTime <= currentTime).
                OrderByDescending(x => x.Id).Skip(0).Take(1)
                .Select(x => new
                {
                    x.Id,
                    x.StationID,
                    x.EquipID,
                    x.DateTime,
                    x.Outlet_P,
                    x.Outlet_T,
                    Q=x.Q.Value/(decimal)1000, //单位转换由 Nm3转换为kNm3
                    x.Accumulated_Q,
                    x.RET_T1,
                    x.RET_T2,
                    x.RET_T3,
                    Inlet_dp=x.Inlet_dp.Value / (decimal)100,   //单位转换由 kpa转换成bar
                    x.InletIOpen,
                    x.LossRatio,
                    x.ePower,
                    UPI=x.UPI*1000,
                    x.DiscgargeOpen,
                    x.C,
                    x.T3Pressure,
                    x.PrimaryAir,
                    x.TwoStageAir,
                    x.ThreeStageAir
                }).ToList();
            var infoObj = new { data = obj, stationId = sId, equipId = eId, alarmLines = alarmLines };
            var jsonObj = JsonConvert.SerializeObject(infoObj);
            ViewBag.infoObject = jsonObj;
            return View();
        }

        /// <summary>
        /// 干燥机内部状态图
        /// </summary>
        /// <param name="stationId">空压站Id</param>
        /// <param name="equipId">干燥机Id</param>
        /// <returns></returns>
        public ActionResult DryerInnerStatus(string stationId, string equipId)
        {
            var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddMinutes(-5);
            if (stationId == null && equipId == null)
            {
                stationId = "S001";
                equipId = "E101";
            }
            int sId = Convert.ToInt32(stationId.Substring(3));
            int eId = Convert.ToInt32(equipId.Substring(3));
            //干燥机设定数据读取
            var alarmLines = thresholdSer.QueryWhere(x => x.Name.Contains("能效线")).ToList();
            var obj = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId && x.DateTime >= beforeTime && x.DateTime <= currentTime).
                OrderByDescending(x => x.Id).Skip(0).Take(1)
                .Select(x => new
                {
                    x.Id,
                    x.StationID,
                    x.EquipID,
                    x.DateTime,
                    CDryer_InletPre= x.CDryer_InletPre.Value/(decimal)100,//进气压力 由kpa转换为bar
                    CDryer_OutPre=x.CDryer_OutPre.Value / (decimal)100,//出气压力 由kpa转换为bar
                    x.CDyer_LeakT,//露点温度值
                    x.CDyer_DewPoint,//吸干露点温度值
                    CDyer_InOutValue = x.CDyer_InOutValue.Value / (decimal)100,//压差 由kpa转换为bar
                    x.CDyer_YSJ_A,  //组合式干燥机压缩机电流值
                    x.CDyer_JRQ_A,//干燥机鼓风机电流值
                    CDryer_CondPre = x.CDryer_CondPre.Value / (decimal)100,  //干燥机冷凝压力值  由kpa转换为bar
                    CDryer_EvapPre = x.CDryer_EvapPre.Value / (decimal)100,  //干燥机蒸发压力值  由kpa转换为bar
                    x.CDyer_INT,               //干燥机入口温度
                    x.CDyer_OutT,              //干燥机出口温度
                    x.CDyer_ATowerT,               //干燥机A塔温度值
                    x.CDyer_BTowerT,            //干燥机B塔温度值
                    Power = Math.Round(decimal.Parse(Math.Sqrt(3).ToString()) * 380 * (x.CDyer_YSJ_A.Value + x.CDyer_JRQ_A.Value) * 85 / 100 / 1000, 2),
                }).ToList();
            var infoObj = new { data = obj, stationId = sId, equipId = eId, alarmLines = alarmLines };
            var jsonObj = JsonConvert.SerializeObject(infoObj);
            ViewBag.infoObject = jsonObj;
            return View();
        }

        /// <summary>
        /// 6s定时获取干燥机内部数据
        /// </summary>
        /// <param name="stationId">空压站id</param>
        /// <param name="equipId">设备id</param>
        /// <returns></returns>
        public ActionResult PerSixSecondDryInnerStatus(string stationId, string equipId)
        {
            var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddMinutes(-5);
            if (stationId == null && equipId == null)
            {
                stationId = "S001";
                equipId = "E001";
            }
            var obj = dryerRealServices.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId && x.DateTime >= beforeTime && x.DateTime <= currentTime).
                OrderByDescending(x => x.Id).Skip(0).Take(1)
                .Select(x => new
                {
                    x.Id,
                    x.StationID,
                    x.EquipID,
                    x.DateTime,
                    CDryer_InletPre = x.CDryer_InletPre.Value / (decimal)100,//进气压力 由kpa转换为bar
                    CDryer_OutPre = x.CDryer_OutPre.Value / (decimal)100,//出气压力 由kpa转换为bar
                    x.CDyer_LeakT,//露点温度值
                    x.CDyer_DewPoint,//吸干露点温度值
                    CDyer_InOutValue = x.CDyer_InOutValue.Value / (decimal)100,//压差 由kpa转换为bar
                    x.CDyer_YSJ_A,  //组合式干燥机压缩机电流值
                    x.CDyer_JRQ_A,//干燥机鼓风机电流值
                    CDryer_CondPre = x.CDryer_CondPre.Value / (decimal)100,  //干燥机冷凝压力值  由kpa转换为bar
                    CDryer_EvapPre = x.CDryer_EvapPre.Value / (decimal)100,  //干燥机蒸发压力值  由kpa转换为bar
                    x.CDyer_INT,               //干燥机入口温度
                    x.CDyer_OutT,              //干燥机出口温度
                    x.CDyer_ATowerT,               //干燥机A塔温度值
                    x.CDyer_BTowerT,            //干燥机B塔温度值
                    Power = Math.Round(decimal.Parse(Math.Sqrt(3).ToString()) * 380 * (x.CDyer_YSJ_A.Value + x.CDyer_JRQ_A.Value) * 85 / 100 / 1000, 2),
                }).ToList();
            var infoObj = new { data = obj };
            var jsonObj = JsonConvert.SerializeObject(infoObj);
            return Json(jsonObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 6s定时获取空压机内部数据
        /// </summary>
        /// <param name="stationId">空压站id</param>
        /// <param name="equipId">设备id</param>
        /// <returns></returns>
        public ActionResult PerSixSecondCentriStatus(string stationId, string equipId)
        {
            var currentTime = DateTime.Now;
            // var currentTime = DateTime.Now;
            var beforeTime = currentTime.AddMinutes(-5);
            if (stationId == null && equipId == null)
            {
                stationId = "S001";
                equipId = "E001";
            }

            var obj = cenrealSer.QueryWhere(x => x.StationID == stationId && x.EquipID == equipId && x.DateTime >= beforeTime && x.DateTime <= currentTime).
                OrderByDescending(x => x.Id).Skip(0).Take(1)
                .Select(x => new
                {
                    x.Id,
                    x.StationID,
                    x.EquipID,
                    x.DateTime,
                    x.Outlet_P,
                    x.Outlet_T,
                    Q = x.Q.Value / (decimal)1000, //单位转换由 Nm3转换为kNm3
                    x.Accumulated_Q,
                    x.LossRatio,
                    x.RET_T1,
                    x.RET_T2,
                    x.RET_T3,
                    Inlet_dp = x.Inlet_dp.Value / (decimal)100,   //单位转换由 kpa转换成bar
                    x.InletIOpen,
                    x.ePower,
                    UPI=x.UPI*1000,//单位转换  
                    x.C,
                    x.DiscgargeOpen,
                    x.T3Pressure,
                    x.PrimaryAir,
                    x.TwoStageAir,
                    x.ThreeStageAir
                }).ToList();
            var infoObj = new { data = obj };
            var jsonObj = JsonConvert.SerializeObject(infoObj);
            return Json(jsonObj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult layUIAlias(int? code, string factoryId, string areaId, string stationId, string equipId, string name)
        {
            if (factoryId == null)
            {
                factoryId = "F001";
            }
            var chartAliasList = CacheMgr.GetData<List<BB_ChartAlias>>("chartAliasList");

            var chartAlias = chartAliasList.Where(x => x.stationID == stationId && x.factoryId == factoryId && x.AreaId == areaId
                  && x.EquipID == equipId && x.name == name).FirstOrDefault();

            var infoObject = new { title = chartAlias.layuiTitle, area = chartAlias.layuiArea, offset = chartAlias.layuiOffset, name = chartAlias.name };
            return Json(infoObject, JsonRequestBehavior.AllowGet);
        }

    }
}