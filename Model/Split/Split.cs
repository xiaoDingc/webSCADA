// <copyright file="Split.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Model.Split
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Split
    {
    }

    public class AuthoritySplitPage
    {
        public AuthoritySplitPage()
        {
            UserList = new List<SB_User>();
            RoelList = new List<List<string>>();
        }

        public List<SB_User> UserList { get; set; }

        public List<List<string>> RoelList { get; set; }
       
    }

    public class ShowAll
    {
        public List<AuthoritySplitPage> Authorityshow { get; set; }

        public Model.PageSplitInfo PageSplit { get; set; }
    }

   public class Splitpage
    {
        public List<SB_User> UserList { get; set; }

        public List<List<string>> RoleList { get; set; }

        public Model.PageSplitInfo PageSplit { get; set; }

        public int RoleId { get; set; }

        public int PageSize { get; set; }
    }
}
