using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BussionObject
{
    class Station_BO
    {
    }
    public class StationShow_BO
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("空压站编码"), Required(ErrorMessage = "不能为空")]
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("名称"), Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("介绍"), Required(ErrorMessage = "不能为空")]
        public string Introduction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("纬度"), Required(ErrorMessage = "不能为空")]
        public Nullable<decimal> Latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("经度"), Required(ErrorMessage = "不能为空")]
        public Nullable<decimal> Longtitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注"), Required(ErrorMessage = "不能为空")]
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("所属工厂"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> FactoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("所属客户"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("状态"), Required(ErrorMessage = "不能为空")]
        public string Valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("主管道尺寸"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> GasMainSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("连接方式"), Required(ErrorMessage = "不能为空")]
        public string DeviceBindMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("空压站面积"), Required(ErrorMessage = "不能为空")]
        public Nullable<decimal> StationArea { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("临近用气区域"), Required(ErrorMessage = "不能为空")]
        public Nullable<decimal> NearGasArea { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("集控系统"), Required(ErrorMessage = "不能为空")]
        public string StationControlSystem { get; set; }
    }
}
