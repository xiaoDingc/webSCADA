using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IServices;
using Posco.DC.WebHelper;

namespace Posco.DC.SCADA.Controllers
{
    [SkipCheckLogin]
    public class AlarmController : Controller
    {
        private IBB_StationServices staSer;
        // GET: Alarm
        public AlarmController(IBB_StationServices staser)
        {
            staSer = staser;
        }
        public ActionResult Index()
        {
          
            return View();
        }
    }
}