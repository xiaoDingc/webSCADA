using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewObject
{
    class List_VO
    {
    }
    /// <summary>
    /// 空压机能效开关图集合
    /// </summary>
    public class CenSwitchStack_VO
    {
       public double UPI { get; set; }
        public int Equid { get; set; }
        public int Run { get; set; }
        public string Time { get; set; }
    }
    public class FactoryList_VO
    {
        public Model.BB_Factory factory { get; set; }
        public string ClientName { get; set; }
        public string VliadName { get; set; }
        public int Num { get; set; }
    }
   
   
    public class AreaList_VO
    {
        public Model.BB_Area area { get; set; }
        public string ClientName { get; set; }
        public string VliadName { get; set; }
        public int Num { get; set; }
        public string FactoryName { get; set; }

    }
  
    /// <summary>
    /// 大数据界面流量压力功率list
    /// </summary>
    public class Supply_View
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? usef { get; set; }
        public decimal? main { get; set; }
        public decimal? press { get; set; }
        public string dtime { get; set; }
    }
    /// <summary>
    /// 大数据界面实际必要台数list
    /// </summary>
    public class SupplyStackSumpowerpq_VO
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 实际开机台数
        /// </summary>
        public int Runnum { get; set; }
        /// <summary>
        /// 需求必要台数
        /// </summary>
        public decimal Mustnum { get; set; }
        /// <summary>
        /// 功率
        /// </summary>
        public decimal Power { get; set; }
        /// <summary>
        /// 压力
        /// </summary>
        public decimal Press { get; set; }
        /// <summary>
        /// 流量
        /// </summary>
        public decimal ProduceStation { get; set; }
        /// <summary>
        /// 真实时间
        /// </summary>
        public string dtime { get; set; }
    }
    public class RoleUser_VO
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string AllRole { get; set; }
    }
    /// <summary>
    /// 开关机提醒趋势分析界面
    /// </summary>
    public class EnergyAnalysysOnOff
    {
        /// <summary>
        /// 空压站号
        /// </summary>
        public int sta { get; set; }
        /// <summary>
        /// 空压站可关台数
        /// </summary>
        public int cen { get; set; }
    }
    /// <summary>
    /// 性别
    /// </summary>
    public class Sex
    {
        /// <summary>
        /// 数据库值
        /// </summary>
        public string numstring { get; set; }
        /// <summary>
        /// 显示值
        /// </summary>
        public string showstring { get; set; }
        public Sex(string Num,string Show)
        {
            numstring = Num;
            showstring = Show;
        }
       
    }
    /// <summary>
    /// 压力设定界面list
    /// </summary>
    public class PressDesign
    {
        /// <summary>
        /// 数据获取集合
        /// </summary>
        public List<List<double>> ListGet { get; set; }
        /// <summary>
        /// 数据设定集合
        /// </summary>
        public List<List<double>> ListSet { get; set; }
        /// <summary>
        /// 时间集合
        /// </summary>
        public List<string> Time { get; set; }
        /// <summary>
        /// 负载名数组
        /// </summary>
        public string[] pressarr { get; set; }
        /// <summary>
        /// upi集合
        /// </summary>
        public List<double> upiarr { get; set; }
    }
}