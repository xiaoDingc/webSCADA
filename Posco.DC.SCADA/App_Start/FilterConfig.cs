// <copyright file="FilterConfig.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Posco.DC.SCADA
{
    using System.Web;
    using System.Web.Mvc;
    using Posco.DC.WebHelper.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new CheckLoginAttribute());
        }
    }
}
