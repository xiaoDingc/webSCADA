using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BussionObject
{
    class Factory_BO
    {
    }
    public class FactoryShow_BO{
        /// <summary>
        /// 
        /// </summary>
        
        public int boID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("代码"), Required(ErrorMessage = "不能为空")]
        public string bocode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("工厂名称"), Required(ErrorMessage = "不能为空")]
        public string boName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("工厂介绍"), Required(ErrorMessage = "不能为空")]
        public string boIntroduction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("产品类型"), Required(ErrorMessage = "不能为空")]
        public string boProduct { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("年产量"), Required(ErrorMessage = "不能为空")]
        public string boAnnualOutput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("纬度"), Required(ErrorMessage = "不能为空")]
        public Nullable<decimal> boLatitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("经度"), Required(ErrorMessage = "不能为空")]
        public Nullable<decimal> boLongtitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("海拔"), Required(ErrorMessage = "不能为空")]
        public Nullable<decimal> boAltitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("官网尺寸"), Required(ErrorMessage = "不能为空")]
        public Nullable<decimal> boSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注"), Required(ErrorMessage = "不能为空")]
        public string boRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("所属客户"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> boClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("状态"), Required(ErrorMessage = "不能为空")]
        public string boValid { get; set; }

    }
}
