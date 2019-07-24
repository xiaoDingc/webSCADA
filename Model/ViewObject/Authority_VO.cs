// <copyright file="Authority_VO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Model.VO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Authority_VO
    {
    }

    public class AuthorityPage_VO{
      public  List<Model.SB_SysRole> SysRole { get; set; }

     public List<Model.ModelViews.MainPage_View> viewList { get; set; }

        }
}
