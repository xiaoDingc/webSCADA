// <copyright file="LoginInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Model.ModelViews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LoginInfo
    {
        [DisplayName("账号"), Required(ErrorMessage = "账号非空")]
        public string ULoginName { get; set; }

        [DisplayName("密码"), Required(ErrorMessage = "密码非空")]
        public string ULoginPWD { get; set; }

    }

    public class SysRoleAdd
    {
        public string Name { get; set; }

        public string Valid { get; set; }
    }
}
