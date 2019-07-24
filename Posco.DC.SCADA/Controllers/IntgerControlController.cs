using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IServices;
using CommonHelper;
using Model;
using Newtonsoft.Json;
using System.Threading;

namespace Posco.DC.SCADA.Controllers
{
    public class IntgerControlController : Controller
    {
        private IAP_PressDesign_RealServices pressSer;
        private IAP_PressDesign_ConclusionServieces pressconSer;
        private IAP_Factory_HistoryServices factoryHisSer;
        public IntgerControlController(IAP_PressDesign_RealServices pressser,
            IAP_PressDesign_ConclusionServieces pressconser,
            IAP_Factory_HistoryServices factoryHisser)
        {
            pressSer = pressser;
            pressconSer = pressconser;
            factoryHisSer = factoryHisser;
        }
        // GET: IntgerControl
        /// <summary>
        /// 智能控制界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 压力设定界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PressDesign()
        {
            //获得最新一条
            AP_PressDesign_Conclusion res = pressconSer.FisrtModel();
            string rescon = res.Conclusion;
            //获取时间
            DateTime histime = Convert.ToDateTime(res.Time);
            DateTime beforetime = Convert.ToDateTime(pressconSer.Find(new object[] { res.Id - 1 }).Time);
            //定住时间
            DateTime fixtime = DateTime.Parse("2019-06-27 00:00:00");
            int tranday = (histime - fixtime).Days;
            DateTime currtime = histime;
            beforetime = currtime.AddHours(-5);
            double upi =Math.Round(factoryHisSer.PressDesignUPISer(currtime, beforetime, 5)*1000,2);//单位转换
            string time=DateTime.Parse(""+res.Time).ToString("yyyy/MM/dd HH:mm:ss");
            string[] resarr = rescon.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
            
            List<double> doublelist1 = new List<double>();
            List<double> doublelist2 = new List<double>();
            List<double> doublelist3 = new List<double>();
            List<double> doublelist4 = new List<double>();
            for (int i = 0; i < 7; i++)
            {
                if (i < 6)
                {
                    doublelist1.Add(Convert.ToDouble(resarr[i]));//0//1//2//3//4//5
                    doublelist2.Add(Convert.ToDouble(resarr[i + 6]));//6//7//8//9//10//11
                    doublelist3.Add(Convert.ToDouble(resarr[i + 12]));//12//13//14//15//16//17
                    doublelist4.Add(Convert.ToDouble(resarr[i + 18]));//18
                }
                else
                {
                    doublelist4.Add(Convert.ToDouble(resarr[i + 18]));
                }
            }
            //doublelist1.RemoveAll(x => x.ToString() == "0");
            //doublelist2.RemoveAll(x => x.ToString() == "0");
            //doublelist3.RemoveAll(x => x.ToString() == "0");
            //doublelist4.RemoveAll(x => x.ToString() == "0");
            List<double> dlist1 = doublelist1.Where(x => x.ToString() != "0").ToList();
            List<double> dlist2 = doublelist2.Where(x => x.ToString() != "0").ToList();
            List<double> dlist3 = doublelist3.Where(x => x.ToString() != "0").ToList();
            List<double> dlist4 = doublelist4.Where(x => x.ToString() != "0").ToList();
            var list1 = dlist1.Distinct().ToList();

            int count1 = list1.Count();
            if (list1.Count() < 4)
            {
                for(int i = 0; i < 4 - count1; i++)
                {
                    list1.Add(0);
                }
            }
            list1.Sort();
            list1.Reverse();
            var list2 = dlist2.Distinct().ToList();
            int count2 = list2.Count();
            if (list2.Count() < 4)
            {
                for (int i = 0; i < 4 - count2; i++)
                {
                    list2.Add(0);
                }
            }
            list2.Sort();
            list2.Reverse();
            var list3 = dlist3.Distinct().ToList();
            int count3 = list3.Count();
            if (list3.Count() < 4)
            {
                for (int i = 0; i < 4 - count3; i++)
                {
                    list3.Add(0);
                }
            }
            list3.Sort();
            list3.Reverse();
            var list4 = dlist4.Distinct().ToList();
            int count4 = list4.Count();
            if (list4.Count() < 4)
            {
                for (int i = 0; i < 4 - count4; i++)
                {
                    list4.Add(0);
                }
            }
            list4.Sort();
            list4.Reverse();
            List<List<double>> sortlist = new List<List<double>>();
            sortlist.Add(list1);
            sortlist.Add(list2);
            sortlist.Add(list3);
            sortlist.Add(list4);
            sortlist.Add(doublelist1);
            sortlist.Add(doublelist2);
            sortlist.Add(doublelist3);
            sortlist.Add(doublelist4);
            string[] strarr = new string[4] { "基础负载", "二级负载", "三级负载", "四级负载" };
            ViewBag.Sort =JsonConvert.SerializeObject(sortlist);
            ViewBag.str = JsonConvert.SerializeObject(strarr);
            ViewBag.time = JsonConvert.SerializeObject(time);
            ViewBag.upi = JsonConvert.SerializeObject(upi);
            return View();
        }
        /// <summary>
        /// 压力设定界面实时详细记录表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult PressDesignJson(int limit,int page)
        {
            var res = pressSer.PressDesignJson(limit,page);

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 压力设定记录表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult PressDesignConJson(int limit,int page)
        {
            var res = pressconSer.PressDesignConJson(limit, page);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 压力设定实时记录表6S刷新
        /// </summary>
        /// <returns></returns>
        public ActionResult SixSecond()
        {
            DateTime now = DateTime.Now;
            //获取id最大的一条
            AP_PressDesign_Conclusion res =pressconSer.Find(new object[] { pressconSer.QueryWhere().Max(x => x.Id) });
            //获取数据库时间
            DateTime checktime = DateTime.Parse("" + res.Time);
            string restime = checktime.ToString("yyyy/MM/dd HH:mm:ss");
            string rescon = res.Conclusion;
            TimeCheck second = new TimeCheck();
            //获得秒差
            var check = second.CheckTicks(now, checktime);
            //小于6秒说明有更新
            if (check[3] < 6)
            {
                //上一条时间
                DateTime beforetime = Convert.ToDateTime(pressconSer.Find(new object[] { res.Id - 1 }).Time);
                DateTime fixtime = DateTime.Parse("2019-06-27 00:00:00");
                int dvalue = (checktime - fixtime).Days;
                DateTime currtime = checktime;
                beforetime = currtime.AddHours(-5);
                double resupi =Math.Round(factoryHisSer.PressDesignUPISer(currtime, beforetime, 5)*1000,2);//单位转换
                string[] resarr = rescon.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
                List<double> doublelist1 = new List<double>();
                List<double> doublelist2 = new List<double>();
                List<double> doublelist3 = new List<double>();
                List<double> doublelist4 = new List<double>();
                for (int i = 0; i < 7; i++)
                {
                    if (i < 6)
                    {
                        doublelist1.Add(Convert.ToDouble(resarr[i]));
                        doublelist2.Add(Convert.ToDouble(resarr[i + 6]));
                        doublelist3.Add(Convert.ToDouble(resarr[i + 12]));
                        doublelist4.Add(Convert.ToDouble(resarr[i + 18]));
                    }
                    else
                    {
                        doublelist4.Add(Convert.ToDouble(resarr[i + 18]));
                    }
                }
                List<double> dlist1 = doublelist1.Where(x => x.ToString() != "0").ToList();
                List<double> dlist2 = doublelist2.Where(x => x.ToString() != "0").ToList();
                List<double> dlist3 = doublelist3.Where(x => x.ToString() != "0").ToList();
                List<double> dlist4 = doublelist4.Where(x => x.ToString() != "0").ToList();
                var list1 = dlist1.Distinct().ToList();
                int count1 = list1.Count();
                if (list1.Count() < 4)
                {
                    for (int i = 0; i < 4 - count1; i++)
                    {
                        list1.Add(0);
                    }
                }
                list1.Sort();
                list1.Reverse();
                var list2 = dlist2.Distinct().ToList();
                int count2 = list2.Count();
                if (list2.Count() < 4)
                {
                    for (int i = 0; i < 4 - count2; i++)
                    {
                        list2.Add(0);
                    }
                }
                list2.Sort();
                list2.Reverse();
                var list3 = dlist3.Distinct().ToList();
                int count3 = list3.Count();
                if (list3.Count() < 4)
                {
                    for (int i = 0; i < 4 - count3; i++)
                    {
                        list3.Add(0);
                    }
                }
                list3.Sort();
                list3.Reverse();
                var list4 = dlist4.Distinct().ToList();
                int count4 = list4.Count();
                if (list4.Count() < 4)
                {
                    for (int i = 0; i < 4 - count4; i++)
                    {
                        list4.Add(0);
                    }
                }
                list4.Sort();
                list4.Reverse();
                List<List<double>> sortlist = new List<List<double>>();
                sortlist.Add(list1);
                sortlist.Add(list2);
                sortlist.Add(list3);
                sortlist.Add(list4);
                sortlist.Add(doublelist1);
                sortlist.Add(doublelist2);
                sortlist.Add(doublelist3);
                sortlist.Add(doublelist4);
                string[] strarr = new string[4] { "基础负载", "二级负载", "三级负载", "四级负载" };
                return Json(new { mess="有",value=sortlist,str=strarr,time=restime,upi=resupi}, JsonRequestBehavior.AllowGet);
            }
            //大于6秒没有查到更新
            else
            {
                return Json(new { mess = "无", value = "",str="" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PressDesignConList(int limit=3,int curr=1)
        {
            //获取数据
            List<Model.AP_PressDesign_Conclusion> res = pressconSer.PressDesignConJson(limit, curr);
            //总数
            int count = pressconSer.AllCount();
            //负载
            string[] strarr = new string[4] { "基础负载", "二级负载", "三级负载", "四级负载" };
            //时间集合
            List<string> timelist = new List<string>();
            List<double> upilist = new List<double>();
            List<List<double>> doubleGetlist = new List<List<double>>();
            List<List<double>> doubleSetlist = new List<List<double>>();
            #region 循环处理
            foreach (var item in res)
            {
                //将获得的压力值分组
                string[] resarr = item.Conclusion.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
                //当前时间
                string time = DateTime.Parse("" + item.Time).ToString("yyyy-MM-dd HH:mm:ss");
                //获取Upi平均值
                //获取上一行的时间
                DateTime currenttime = Convert.ToDateTime(item.Time);
                DateTime beforetime =Convert.ToDateTime(pressconSer.Find(new object[] { item.Id - 1 }).Time);
                //获取两个时间段的upi平均值
                TimeCheck ddtime = new TimeCheck();
                List<long> checktime= ddtime.CheckTicks(currenttime, beforetime);
                double  upires=Math.Round(factoryHisSer.PressDesignUPISer(currenttime, beforetime.AddHours(-4),(int)checktime[1])*1000,2);
                timelist.Add(time);
                upilist.Add(upires);
                //空压站1压力集合
                List<double> doublelist1 = new List<double>();
                //空压站2压力集合
                List<double> doublelist2 = new List<double>();
                //空压站3压力集合
                List<double> doublelist3 = new List<double>();
                //空压站4压力集合
                List<double> doublelist4 = new List<double>();
                //将获取值加入四组中
                for (int i = 0; i < 7; i++)
                {
                    if (i < 6)
                    {
                        doublelist1.Add(Convert.ToDouble(resarr[i]));
                        doublelist2.Add(Convert.ToDouble(resarr[i + 6]));
                        doublelist3.Add(Convert.ToDouble(resarr[i + 12]));
                        doublelist4.Add(Convert.ToDouble(resarr[i + 18]));
                    }
                    else
                    {
                        doublelist4.Add(Convert.ToDouble(resarr[i + 18]));
                    }
                }
                #region 四个空压站去重分组
                List<double> list1 = doublelist1.Distinct().ToList();
                int count1 = list1.Count();
                if (list1.Count() < 4)
                {
                    for (int i = 0; i < 4 - count1; i++)
                    {
                        list1.Add(0);
                    }
                }
                list1.Sort();
                list1.Reverse();
                var list2 = doublelist2.Distinct().ToList();
                int count2 = list2.Count();
                if (list2.Count() < 4)
                {
                    for (int i = 0; i < 4 - count2; i++)
                    {
                        list2.Add(0);
                    }
                }
                list2.Sort();
                list2.Reverse();
                var list3 = doublelist3.Distinct().ToList();
                int count3 = list3.Count();
                if (list3.Count() < 4)
                {
                    for (int i = 0; i < 4 - count3; i++)
                    {
                        list3.Add(0);
                    }
                }
                list3.Sort();
                list3.Reverse();
                var list4 = doublelist4.Distinct().ToList();
                int count4 = list4.Count();
                if (list4.Count() < 4)
                {
                    for (int i = 0; i < 4 - count4; i++)
                    {
                        list4.Add(0);
                    }
                }
                list4.Sort();
                list4.Reverse();
                #endregion
                #region 四个空压站压力设定值集合
                //四个空压站压力设定值集合
                List<double> fourpressSetlist = new List<double>();
                for(var i = 0; i < 16; i++)
                {
                    if (i < 4)
                    {
                        fourpressSetlist.Add(list1[i]);
                    }
                    else if (i < 8 && i >= 4)
                    {
                        fourpressSetlist.Add(list2[i - 4]);
                    }
                    else if (i < 12 && i >= 8)
                    {
                        fourpressSetlist.Add(list3[i - 8]);
                    }
                    else
                    {
                        fourpressSetlist.Add(list4[i - 12]); ;
                    }
                }
                #endregion
                #region 四个空压站压力获取值集合
                //四个空压站压力获取值集合
                List<double> fourpressGetlist = new List<double>();
                for (var i = 0; i < 25; i++)
                {
                    if (i < 6)
                    {
                        fourpressGetlist.Add(doublelist1[i]);
                    }
                    else if (i < 12 && i >= 6)
                    {
                        fourpressGetlist.Add(doublelist2[i-6]);
                    }
                    else if (i < 18 && i >= 12)
                    {
                        fourpressGetlist.Add(doublelist3[i-12]);
                    }
                    else
                    {
                        fourpressGetlist.Add(doublelist4[i-18]);
                    }
                }
                #endregion
                doubleGetlist.Add(fourpressGetlist);
                doubleSetlist.Add(fourpressSetlist);
            }
            #endregion
            Model.ViewObject.PressDesign press = new Model.ViewObject.PressDesign()
            {
                ListGet = doubleGetlist,
                ListSet = doubleSetlist,
                pressarr = strarr,
                Time = timelist,
                upiarr = upilist
            };
            var intarr = new
            {
                Count = count,
                Curr = curr,
                Limit = limit
            };
            ViewBag.Count = JsonConvert.SerializeObject(intarr);
            return View(press);
        }

        public ActionResult CentriStateSender(string tag,string tagvalue)
        {
            //SocketHelper.GetInstance();
            //SocketHelper.InitSocket();
            //SocketHelper.SendMessage(tagvalue.ToString());
            Thread.Sleep(2000);
            string ip = Session[Keys.userIP].ToString();
            int a = 5;
            string message = "{'IP':'192.168.14.132','SimTest_F001_PressDesign_S001_E001_Press':'23','SimTest_F001_PressDesign_S001_E002_Press2':'214'}";
            string mess = "{'IP':'"+ip+"','" + tag + "':'" + tagvalue + "'}";
            SocketHelper.GetInstance();
            SocketHelper.InitSocket();
            SocketHelper.SendMessage(mess.ToString());
            if (a>4)
            {
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("NO", JsonRequestBehavior.AllowGet);
            }
            //记录操作
        }
    }
}