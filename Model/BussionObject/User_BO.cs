using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BussionObject
{
    class User_BO
    {
    }
    public class UserShow_BO
    {
        /// <summary>
        /// 
        /// </summary>
        
        public System.Guid boID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("账号"), Required(ErrorMessage = "不能为空")]
        //[StringLength(15, MinimumLength = 4)]
        public string boAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("密码"), Required(ErrorMessage = "不能为空")]
        public string boPassWord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("姓名"), Required(ErrorMessage = "不能为空")]
        public string boName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("用户类型"), Required(ErrorMessage = "不能为空")]
        public string boUserType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("性别"), Required(ErrorMessage = "不能为空")]
        public string boSex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("身份证"), Required(ErrorMessage = "不能为空")]
        public string boIDNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("手机号码"), Required(ErrorMessage = "不能为空")]
        public string boCellphone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("邮箱"), Required(ErrorMessage = "不能为空")]
        public string boEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("所属客户"), Required(ErrorMessage = "不能为空")]
        public Nullable<int> boClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注")]
        public string boRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("状态"), Required(ErrorMessage = "不能为空")]
        public string boState { get; set; }
    }
    public class ADDUserShow_BO
    {
        

      
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("账号"), Required(ErrorMessage = "不能为空")]
        //[StringLength(15, MinimumLength = 4)]
        public string boAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("密码"), Required(ErrorMessage = "不能为空")]
        public string boPassWord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("姓名"), Required(ErrorMessage = "不能为空")]
        public string boName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("用户类型"), Required(ErrorMessage = "不能为空")]
        public string boUserType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("性别"), Required(ErrorMessage = "不能为空")]
        public string boSex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("身份证"), Required(ErrorMessage = "不能为空")]
        public string boIDNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("手机号码"), Required(ErrorMessage = "不能为空")]
        public string boCellphone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("邮箱"), Required(ErrorMessage = "不能为空")]
        public string boEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("所属客户")]
        public Nullable<int> boClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注"), Required(ErrorMessage = "不能为空")]
        public string boRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("状态"), Required(ErrorMessage = "不能为空")]
        public string boState { get; set; }
    }
}
