using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelViews
{
    class EfficiencyAnalysis_view
    {
    }
   
    
  public class CenStatic_View
    {
        public decimal? Upi { get; set; }
        public decimal? Loss { get; set; }
        public decimal? MP { get; set; }
        public decimal? MQ { get; set; }
        public decimal? Epower { get; set; }

    }
    public class DateTime_View
    {
        public string Time1 { get; set; }
        public string Time2 { get; set; }
    }


}
