using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelViews
{
    class Home_View
    {
    }
    public class AddUserLayer_View
    {
        public List<SB_User> userList;
        public int? num;
    }
    public class MainPage_View
    {
        public List<Model.SB_Page> childPage { get; set; }
       public Model.SB_Page Page { get; set; }
        public int mark { get; set; }
    }
  public class Instrument_View
    {
        public List<Model.BB_Instrument> centri { get; set; }
        public List<Model.BB_Instrument> drys { get; set; }
        public List<Model.BB_Instrument> fliter { get; set; }
        public List<Model.BB_Instrument> colddrys { get; set; }
    }
}
