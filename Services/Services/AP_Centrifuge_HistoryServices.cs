//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Services
{
    using System;
    using System.Collections.Generic;

    using Model;
    using Base;
    using IServices;
    using IRepository;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Text.RegularExpressions;
    using CommonHelper;

    /// <summary>
    /// 负责每个数据表的业务逻辑操作的约定
    /// </summary>
    public partial class AP_Centrifuge_HistoryServices : BaseServices<AP_Centrifuge_History>, IAP_Centrifuge_HistoryServices
    {
        IAP_Centrifuge_HistoryRepository dal;
        private long startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks;
        private TimeCheck check;
        #region 定义构造函数，接收autofac将数据仓储层的具体实现类的对象注入到此类中
        public AP_Centrifuge_HistoryServices(IAP_Centrifuge_HistoryRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
            check = new TimeCheck();
        }
        #endregion

        #region 针对此表的特殊操作写在此处
        #region 统计分析空压机
        public object StaMainpq(DateTime currtime, DateTime begtime,string sid)
        {
            var checktime = check.CheckTicks(currtime, begtime);
            var query = dal.QueryWhere(d=>d.DateTime >= begtime && d.DateTime <= currtime);
            string sql = "select * from AP_Centrifuge_History where DateTime>='" + begtime + "'and DateTime<='" + currtime + "'and StationID='" + sid + "'and Id%100<=25 and Id%100>0";
            
            if (checktime[1] <=24)
            {
                //var res = dal.QueryNoParams(sql).Select(s => new
                //{
                //    mainp = s.Outlet_P,
                //    mainq = s.ePower,
                //    staid = s.EquipID
                //}).GroupBy(g => g.staid);
                var res = dal.QueryWhere(d => d.StationID == sid && d.DateTime >= begtime && d.DateTime <= currtime && d.Id % 100 <= 25 && d.Id % 100 > 0).Select(s => new
                {
                    mainp = s.Outlet_P,
                    mainq = s.ePower,
                    staid = s.EquipID
                }).GroupBy(g => g.staid).ToList();
                //var res = dal.QueryWhere(d => d.StationID == sid && d.DateTime >= begtime && d.DateTime <= currtime).Select(s => new
                //{
                //    mainp = s.Outlet_P,
                //    mainq = s.ePower,
                //    staid = s.EquipID
                //}).ToList();//看个数
                //var rres= dal.QueryWhere(d => d.StationID == sid && d.DateTime >= begtime && d.DateTime <= currtime&&d.Id%50<=25&&d.Id%50>0).Select(s => new
                //{
                //    mainp = s.Outlet_P,
                //    mainq = s.ePower,
                //    staid = s.EquipID
                //}).ToList();//看个数
                return res;
            }
            else
            {
                int b = (int)checktime[1] / 12 * 25*2;
                var res = dal.QueryWhere(d =>d.StationID==sid && d.DateTime >= begtime && d.DateTime <= currtime && d.Id % b <= 25 && d.Id % b > 0).Select(s => new
                {
                    mainp = s.Outlet_P,
                    mainq = s.ePower,
                    staid = s.EquipID
                }).GroupBy(g => g.staid).ToList();
                return res;
            }
        }
        /// <summary>
        /// 空压机放散率打开度
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="sidcid"></param>
        /// <returns></returns>
        public object CentriBovLoss(DateTime currtime, DateTime begtime, string sidcid)
        {
            //判断时间
            List<long> time = check.CheckTicks(currtime, begtime);
            int b = 0;
            if (time[1] <= 24)
            {
                b = 25 * 2;
            }
            else
            {
                b = (int)time[1] / 12 * 25;
            }
            var query = dal.QueryWhere(d => d.DateTime >= begtime && d.DateTime <= currtime);
            string sid = sidcid.Substring(0, 4);
            string cid = sidcid.Substring(4, 4);
            var res = query.Where(x => x.StationID.Equals(sid) && x.EquipID.Equals(cid) && x.Id % b <= 25 && x.Id % b > 0).Select(s => new
            {
                bov = s.DiscgargeOpen,
                loss = s.LossRatio
            }).ToList();
            return res;
        }
        /// <summary>
        /// 统计分析，空压机产气量功率
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="sidcid"></param>
        /// <returns></returns>
        public object CentriProduceEpower(DateTime currtime, DateTime begtime, string sidcid)
        {
            //判断时间
            List<long> time = check.CheckTicks(currtime, begtime);
            int b = 0;
            if (time[1] <= 24)
            {
                b = 25 * 2;
            }
            else
            {
                b = (int)time[1] / 12 * 25;
            }
            var query = dal.QueryWhere(d => d.DateTime >= begtime && d.DateTime <= currtime);
            string sid = sidcid.Substring(0, 4);
            string cid = sidcid.Substring(4, 4);
            var res = query.Where(x => x.StationID.Equals(sid) && x.EquipID.Equals(cid) && x.Id % b <= 25 && x.Id % b > 0).Select(s => new
            {
                q = s.Q,
                e = s.ePower,
                s.DateTime,
                s.Id
            }).ToList();
            return res;
        }
        /// <summary>
        /// 统计分析，空压机单耗放散率
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="sidcid"></param>
        /// <returns></returns>
        public object CentriUpiLoss(DateTime currtime, DateTime begtime, string sidcid)
        {
            //判断时间
            List<long> time = check.CheckTicks(currtime, begtime);
            int b = 0;
            if (time[1] <= 24)
            {
                b = 25 * 2;
            }
            else
            {
                b = (int)time[1] / 12 * 25;
            }
            var query = dal.QueryWhere(d => d.DateTime >= begtime && d.DateTime <= currtime);
            string sid = sidcid.Substring(0, 4);
            string cid = sidcid.Substring(4, 4);
            var res = query.Where(x => x.StationID.Equals(sid) && x.EquipID.Equals(cid) && x.Id % b <= 25 && x.Id % b> 0).Select(s => new
            {
                upi = s.UPI,
                loss = s.LossRatio
            }).ToList();
            return res;
        }
        /// <summary>
        /// 统计分析，空压机压力功率图
        /// </summary>
        /// <param name="currtime"></param>
        /// <param name="begtime"></param>
        /// <param name="sidcid"></param>
        /// <returns></returns>
        public object CentriPEpower(DateTime currtime, DateTime begtime, string sidcid)
        {
            //判断时间
            List<long> time = check.CheckTicks(currtime, begtime);
            int b = 0;
            if (time[1] <= 24)
            {
                b = 25 * 2;
            }
            else
            {
                b = (int)time[1] / 12 * 25;
            }
            var query = dal.QueryWhere(d => d.DateTime >= begtime && d.DateTime <= currtime);
            string sid = sidcid.Substring(0, 4);
            string cid = sidcid.Substring(4, 4);
            var res = query.Where(x => x.StationID.Equals(sid) && x.EquipID.Equals(cid) && x.Id % b <= 25 && x.Id % b > 0).Select(s => new
            {
                p = s.Outlet_P,
                e = s.ePower
            }).ToList();
            return res;
        }
        #endregion

        public List<Model.Stack> StackSer(DateTime currtime, DateTime begtime)
        {
            var res = dal.QueryWhere(s => s.DateTime >= begtime && s.DateTime <= currtime).GroupBy(g => new { g.StationID, g.EquipID }).Select(f => new
            {
                staid = f.Key.StationID,
                equid = f.Key.EquipID,
                upi = f.Sum(u => u.UPI)
            });

            List<Model.Stack> stackList = new List<Model.Stack>();
            int count = 1;
            // Regex.Replace(item.equipId, "[A-Z0]", "") + "#"
            foreach (var item in res)
            {
                Model.Stack stack = new Model.Stack()
                {
                    Number = item.upi,
                    Str = count.ToString() + "#"

                };
                count++;
                stackList.Add(stack);
            }
            return stackList;
        }
        public List<int> SwitchSer(DateTime currtime, DateTime begtime)
        {
            List<int> arr = new List<int>();
            var res = dal.QuerySplitPage(c => c.DateTime >= begtime && c.DateTime <= currtime, or => or.OrderByDescending(o => o.Id), 25, 1).Select(f => new
            {
                runTime = f.Run,
                id = f.Id,
                stationId = f.StationID,
                equId = f.EquipID

            }).OrderBy(f => f.id).ToList();
            int count = 0;
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;
            int countadd = 0;
            foreach (var item in res)
            {
                if (countadd < 6&&item.runTime>0)
                {
                    count++;
                    count1++;
                }
                if(countadd>5&&countadd<12&&item.runTime>0){
                    count++;
                    count2++;
                }
                if (countadd > 11 && countadd < 18 && item.runTime > 0)
                {
                    count++;
                    count3++;
                }
                if(countadd>17&&item.runTime>0)
                {
                    count++;
                    count4++;
                }
                countadd++;
                arr.Add(Convert.ToInt32(item.runTime));
            }
            arr.Add(count);
            arr.Add(count1);
            arr.Add(count2);
            arr.Add(count3);
            arr.Add(count4);
            return arr;
        }
        public List<List<Model.ViewObject.CenSwitchStack_VO>> SwitchStackSer(DateTime currtime, DateTime begtime)
        {
            Random rr = new Random();

            var res = dal.QuerySplitPage(d => d.DateTime >= begtime && d.DateTime <= currtime, or => or.OrderByDescending(o => o.Id), 25, 1).Select(s => new
            {
                run = s.Run,
                id = s.Id,
                stationId = s.StationID,
                equId = s.EquipID,
                upi = s.UPI,
                time = s.DateTime
            }).OrderBy(o => o.id).ToList().Select(ss => new
            {
                upii = rr.Next(0, 100) / 100.0,
                equId = ss.equId,
                time = ss.time,
                run = ss.run,
                id = ss.id,
                stationId = ss.stationId
            }).OrderByDescending(o => o.upii).GroupBy(g => g.stationId).OrderBy(gg => gg.Key).ToList();
            List<Model.ViewObject.CenSwitchStack_VO> upiarr = new List<Model.ViewObject.CenSwitchStack_VO>();
            foreach (var it in res)
            {
                foreach (var i in it)
                {
                    Model.ViewObject.CenSwitchStack_VO model = new Model.ViewObject.CenSwitchStack_VO()
                    {
                        UPI = i.upii,
                        Equid = Convert.ToInt32(Regex.Replace(i.equId, "[A-Z0]", "")),
                        Run = Convert.ToInt32(i.run),
                        Time = i.time.ToString()
                    };
                    upiarr.Add(model);
                }
            }
            List<List<Model.ViewObject.CenSwitchStack_VO>> all = new List<List<Model.ViewObject.CenSwitchStack_VO>>();
            for (int i = 0; i < 7; i++)
            {
                List<Model.ViewObject.CenSwitchStack_VO> test = new List<Model.ViewObject.CenSwitchStack_VO>();
                if (i < 6)
                {
                    test.Add(upiarr[i]);
                    test.Add(upiarr[i + 6 * 1]);
                    test.Add(upiarr[i + 6 * 2]);
                    test.Add(upiarr[i + 6 * 3]);
                }
                else
                {
                    Model.ViewObject.CenSwitchStack_VO model1 = new Model.ViewObject.CenSwitchStack_VO()
                    {
                        UPI = 0,
                        Equid = 0,
                        Run = 0,
                        Time = ""
                    };
                    test.Add(model1);
                    test.Add(model1);
                    test.Add(model1);
                    test.Add(upiarr[i + 6 * 3]);
                }
                all.Add(test);
            }
            return all;
        }
        public object TrendCentri(DateTime currtime, DateTime begtime,List<string> censtrlist)
        {
            var query = dal.QueryWhere(d => d.DateTime >= begtime && d.DateTime <= currtime);
            if (censtrlist.Count < 2)
            {
                string str = censtrlist[0];
                string stastr = str.Substring(0, 4);
                string censtr = str.Substring(4, 4);
                var res = query.Where(x => x.StationID.Equals(stastr) && x.EquipID.Equals(censtr)).ToList().Select(s => new
                {
                    time = (DateTime.Parse("" + s.DateTime).AddHours(8).Ticks - startTime) / 10000,
                    flow = s.Q,
                    epower = s.ePower,
                    press = s.Outlet_P,
                    A = s.C,
                    upi = s.UPI,
                    dre = s.DRE,
                    bor = s.LossRatio,
                    igv = s.InletIOpen,
                    bov = s.DiscgargeOpen
                }).OrderBy(o=>o.time).ToList();
                return res;
            }
           else
            {
                //扩展类where 此句是调用此类的必要语句
                var where = ExtendWhereClass.False<Model.AP_Centrifuge_History>();
                //循环加载or语句
                foreach (var item in censtrlist)
                {
                    string stastr = item.Substring(0, 4);
                    string censtr = item.Substring(4, 4);
                 //var re12s= ExtendWhereClass.Or<AP_Centrifuge_History>(x => x.StationID.Equals(stastr),x=>x.EquipID.Equals(censtr));
                    //拼接的lambda表达式（or）
                    where = where.Or(x => x.StationID.Equals(stastr) && x.EquipID.Equals(censtr));
                }
                //将lambda放入查询where语句中查询结果
                var res = query.Where(where.Compile()).ToList().Select(s => new
                 {
                     time = (DateTime.Parse("" + s.DateTime).AddHours(8).Ticks - startTime) / 10000,
                     flow = s.Q,
                     epower = s.ePower,
                     press = s.Outlet_P,
                     A = s.C,
                     upi = s.UPI,
                     dre = s.DRE,
                     bor = s.LossRatio,
                     igv = s.InletIOpen,
                     bov = s.DiscgargeOpen,
                     sta = s.StationID,
                     cen = s.EquipID
                 }).OrderBy(o => o.time).GroupBy(g => new { g.sta, g.cen }).ToList();
                return res;
            }
        }
        public object TrendCentriAjax(DateTime currtime, DateTime begtime, List<string> censtrlist)
        {
            var query = dal.QueryWhere(d => d.DateTime >= begtime && d.DateTime <= currtime);
            if (censtrlist.Count<2)
            {
                string str = censtrlist[0];
                string stastr = str.Substring(0, 4);
                string censtr = str.Substring(4, 4);
                var res = query.Where(x => x.StationID.Equals(stastr) && x.EquipID.Equals(censtr)).ToList().Select(s => new
                {
                    time = (DateTime.Parse("" + s.DateTime).AddHours(8).Ticks - startTime) / 10000,
                    flow = s.Q,
                    epower = s.ePower,
                    press = s.Outlet_P,
                    A = s.C,
                    upi = s.UPI,
                    dre = s.DRE,
                    bor = s.LossRatio,
                    igv = s.InletIOpen,
                    bov = s.DiscgargeOpen
                }).FirstOrDefault();
                return res;
            }
            else
            {
                //扩展类where 此句是调用此类的必要语句
                var where = ExtendWhereClass.False<Model.AP_Centrifuge_History>();
                //循环加载or语句
                foreach (var item in censtrlist)
                {
                    string stastr = item.Substring(0, 4);
                    string censtr = item.Substring(4, 4);
                    //拼接的lambda表达式（or）
                    where = where.Or(x => x.StationID.Equals(stastr) && x.EquipID.Equals(censtr));
                }
                //将lambda放入查询where语句中查询结果
                var res = query.Where(where.Compile()).ToList().Select(s => new
                {
                    time = (DateTime.Parse("" + s.DateTime).AddHours(8).Ticks - startTime) / 10000,
                    flow = s.Q,
                    epower = s.ePower,
                    press = s.Outlet_P,
                    A = s.C,
                    upi = s.UPI,
                    dre = s.DRE,
                    bor = s.LossRatio,
                    igv = s.InletIOpen,
                    bov = s.DiscgargeOpen,
                    sta = s.StationID,
                    cen = s.EquipID
                }).GroupBy(g => new { g.sta, g.cen }).Select(s=>s.FirstOrDefault()).ToList();
                return res;
            }
        }
        public object CentrifugePePowerPage(DateTime currtime,DateTime begtime)
        {
           var checktime= check.CheckTicks(currtime, begtime);
            var query = dal.QueryWhere(d => d.DateTime >= begtime && d.DateTime <= currtime);
            if (checktime[3] / 6 < 288)
            {
                query = query.OrderByDescending(x => x.Id).Skip(0).Take(3600);
            }
            else
            {
                int SSkip = (int)checktime[3] / 6 / 144*25;
                query = query.Where(x => x.Id % SSkip <= 25 && x.Id % SSkip > 0).OrderByDescending(x => x.Id).Skip(0).Take(3600);
            }
            var res = query.Select(s => new
            {
                sta=s.StationID,
                eq=s.EquipID,
                p = s.Outlet_P,
                e = s.ePower
            }).GroupBy(g=>new { g.sta,g.eq}).ToList();
            return res;
        }
        #endregion

    } 
}
