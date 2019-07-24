using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BussionObject
{
    class Centrifuge_BO
    {
    }
    public class CentrifugeShow_BO {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>  
        [DisplayName("编号"), Required(ErrorMessage = "不能为空")]
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("设备类型"), Required(ErrorMessage = "不能为空")]
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("额定排气量(N㎥/h)")]
        public string Rated_Q { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("设定压力(㎏/㎠)")]
        public string DesignPressure { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("额定电压(V)")]
        public string Rated_V { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("额定电流(A)")]
        public string Rated_A { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("额定功率(KW)")]
        public string Rated_Power { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("露点(℃)")]
        public string Dewpoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("制造商")]
        public string Manufacture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("投入运行年份")]
        public string RunYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("空压站")]
        public Nullable<int> StationID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("工厂")]
        public Nullable<int> FactoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("客户")]
        public Nullable<int> ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("状态")]
        public string Valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("出厂日期")]
        public Nullable<System.DateTime> DateOfProductionTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("出厂编号")]
        public string SerialNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("排气法兰尺寸(DN)")]
        public Nullable<int> ExFlangeSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("外形尺寸(mm*mm*mm)")]
        public Nullable<int> ShapeSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("制冷剂类型")]
        public string Refrigerantype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("吸附剂类型")]
        public string AdsorbentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[DisplayName("滤芯数")]
        //public Nullable<int> FilterNum { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //[DisplayName("再生耗气率")]
        //public Nullable<decimal> GasConsumptionRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [DisplayName("一级中冷器功率")]
        public Nullable<decimal> PrimaryIntercoolerPower { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        [DisplayName("二级中冷器功率")]
        public Nullable<decimal> SecondaryIntercoolerPower { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        [DisplayName("三级中冷器功率")]
        public Nullable<decimal> ThirdintercoolerPower { get; set; }

    }

}
