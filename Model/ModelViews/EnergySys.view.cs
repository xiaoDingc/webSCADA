using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelViews
{
    class EnergySys
    {
    }
    public class ShowView
    {
        public string str { get; set; }
        public string time { get; set; }
        public string time1 { get; set; }
        public decimal? power { get; set; }
        public decimal? upi { get; set; }
    }
    public class EnergyformView {
        public List<Model.AP_Station_Statistics> stasta { get; set; }
        public List<Model.AP_Factory_Statistics> facsta { get; set; }
        public int num { get; set; }
    }
    public class EnergyformChildView
    {
        public List<Model.AP_Factory_History> fachis { get; set; }
        public List<Model.AP_Station_History> stahis { get; set; }
        public PageSplitInfo PageSplit { get; set; }
        public string time1 { get; set; }
        public string time2 { get; set; }
        public int NUM { get; set; }
    }

}
