using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BussionObject
{
    class MeasureMeter_BO
    {
    }
    public class MeasureMeterShow_BO
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("仪表编号"), Required(ErrorMessage = "不能为空")]
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("仪表类型"), Required(ErrorMessage = "不能为空")]
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("仪表种类"), Required(ErrorMessage = "不能为空")]
        public string Variety { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("测量范围"), Required(ErrorMessage = "不能为空")]
        public string MeasurementRange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("投入运行年份"), Required(ErrorMessage = "不能为空")]
        public string RunYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("校准时间"), Required(ErrorMessage = "不能为空")]
        public Nullable<System.DateTime> CalibrationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("制造商"), Required(ErrorMessage = "不能为空")]
        public string Manufacture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("安装位置"), Required(ErrorMessage = "不能为空")]
        public string Location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注"), Required(ErrorMessage = "不能为空")]
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("设备"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> InstrumentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("用气区域"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> AreaID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("工厂"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> FactoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("客户"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("状态"), Required(ErrorMessage = "不能为空")]
        public string Valid { get; set; }
    }
}
