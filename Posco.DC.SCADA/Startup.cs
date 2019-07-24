// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Posco.DC.SCADA.Startup))]
namespace Posco.DC.SCADA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
