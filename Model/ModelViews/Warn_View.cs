using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelViews
{
    class Warn_View
    {
    }
    public class WarnWarn_View
    {
        public Model.SB_WarnLog Warn { get; set; }
        public Model.BB_Area Area { get; set; }
        public Model.BB_Factory Fac { get; set; }
        public Model.BB_Instrument Ins { get; set; }
        public Model.BB_Station Sta { get; set; }
    }
}
