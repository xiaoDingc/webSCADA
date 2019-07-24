using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Model.BussionObject
{
    class Custom_BO
    {

    }
    public class CustomShow_BO {
        public int boID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("账号"), Required(ErrorMessage = "不能为空")]
        [StringLength(25,MinimumLength =6)]    
        public string boName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("客户类型"), Required(ErrorMessage = "不能为空")]
        [StringLength(2, MinimumLength = 1)]
        public string boType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("地址"), Required(ErrorMessage = "不能为空")]
        [StringLength(50, MinimumLength = 2)]
        public string boAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("座机号"), Required(ErrorMessage = "不能为空")]
        [StringLength(12,MinimumLength =9)]
        public string boTelephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("公司邮箱"), Required(ErrorMessage = "不能为空")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "格式错误")]
        public string boCompanyMail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("联系人"), Required(ErrorMessage = "不能为空")]
        [StringLength(5, MinimumLength = 2)]
        public string boContacts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("联系人手机号"), Required(ErrorMessage = "不能为空")]
        //[RegularExpression(@"^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(17[0,3,5-8])|(18[0-9])|166|198|199|(147))\\d{8}$", ErrorMessage = "基本格式错误")]
        public string boContactsCellone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("联系人邮箱"), Required(ErrorMessage = "不能为空")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "格式错误")]
        public string boContactsMail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string boRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bostate { get; set; }

    }
    public class CuostomShowADD_BO {
        
            /// <summary>
            /// 
            /// </summary>
            public int addID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addType { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addAddress { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addTelephone { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addCompanyMail { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addContacts { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addContactsCellone { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addContactsMail { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addRemark { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string addstate { get; set; }
        }


   
}
