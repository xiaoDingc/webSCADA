using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using IServices;
using Newtonsoft.Json;
using CommonHelper;

namespace Posco.DC.SCADA.Controllers
{
    public class EnergyFormController : Controller
    {
        private IBB_StationServices stationSer;
        private IAP_Station_RealServices station_realSer;
        private IAP_Station_StatisticsServices station_statistSer;
        private IAP_Factory_StatisticsServices factory_statistSer;
        private IAP_Factory_HistoryServices factory_hisSer;
        private IAP_Station_HistoryServices station_hisSer;
        public EnergyFormController(IBB_StationServices StationSer, IAP_Station_RealServices Station_realSer, IAP_Station_StatisticsServices Station_statistSer
            , IAP_Factory_StatisticsServices Factory_statistSer, IAP_Factory_HistoryServices Factory_hisSer, IAP_Station_HistoryServices Station_hisSer)
        {
            stationSer = StationSer;
            station_realSer = Station_realSer;
            station_statistSer = Station_statistSer;
            factory_statistSer = Factory_statistSer;
            factory_hisSer = Factory_hisSer;
            station_hisSer = Station_hisSer;
        }
        // GET: EnergyForm
        public ActionResult Index()
        {
            string[] str1 = new string[] { "1#空压站汇总","供气量", "1#空压机用电量", "2#空压机用电量", "3#空压机用电量", "4#空压机用电量", "5#空压机用电量", "6#空压机用电量", "1#变压器", "2#变压器", "1#高压进线", "2#高压进线", "1#低压用电量", "2#低压用电量", "工业水补水量", "旁滤装置用水量", "强制排污量", "1#空压站月度供气总量", "1#空压站月度耗电量", "1#空压站电耗", "1#空压站水耗", "单日用电量", "单日电耗", "单日高压用电量", "单日高压电耗", ""};
            string[] str2 = new string[] { "2#空压站汇总", "供气量", "1#空压机用电量", "2#空压机用电量", "3#空压机用电量", "4#空压机用电量", "5#空压机用电量", "6#空压机用电量", "1#变压器", "2#变压器", "1#高压进线", "2#高压进线", "1#低压用电量", "2#低压用电量", "工业水补水量", "旁滤装置用水量", "强制排污量", "2#空压站月度供气总量", "2#空压站月度耗电量", "2#空压站电耗", "2#空压站水耗", "单日用电量", "单日电耗", "单日高压用电量", "单日高压电耗", "" };
            string[] str3 = new string[] { "3#空压站汇总", "供气量", "1#空压机用电量", "2#空压机用电量", "3#空压机用电量", "4#空压机用电量", "5#空压机用电量", "6#空压机用电量", "1#变压器", "2#变压器", "1#高压进线", "2#高压进线", "1#低压用电量", "2#低压用电量", "工业水补水量", "旁滤装置用水量", "强制排污量", "3#空压站月度供气总量", "3#空压站月度耗电量", "3#空压站电耗", "3#空压站水耗", "单日用电量", "单日电耗", "单日高压用电量", "单日高压电耗", "" };
            string[] str4 = new string[] { "4#空压站汇总", "供气量", "1#空压机用电量", "2#空压机用电量", "3#空压机用电量", "4#空压机用电量", "5#空压机用电量", "6#空压机用电量", "7#空压机用电量", "1#变压器", "2#变压器", "1#高压进线", "2#高压进线", "1#低压用电量", "2#低压用电量", "工业水补水量", "旁滤装置用水量", "强制排污量", "4#空压站月度供气总量", "4#空压站月度耗电量", "4#空压站电耗", "4#空压站水耗", "单日用电量", "单日电耗", "单日高压用电量", "单日高压电耗", "" };
            List<string[]> strlist = new List<string[]>();
            strlist.Add(str1);
            strlist.Add(str2);
            strlist.Add(str3);
            strlist.Add(str4);
            return View(strlist);
        }
         public ActionResult HistroyForm(int limit,int page)
        {
            return Json("0", JsonRequestBehavior.AllowGet);
        }
    }
}