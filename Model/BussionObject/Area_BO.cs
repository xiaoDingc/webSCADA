using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BussionObject
{
    class Area_BO
    {
    }
    public class AreaShow_BO{
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("用气区域编码"), Required(ErrorMessage = "不能为空")]
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("名称"), Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("简介"), Required(ErrorMessage = "不能为空")]
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
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("入口管道尺寸"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> InputSize { get; set; }
    }
}
