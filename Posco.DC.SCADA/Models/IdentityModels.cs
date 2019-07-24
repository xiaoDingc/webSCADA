// <copyright file="IdentityModels.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Posco.DC.SCADA.Models
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Model.SB_User> SB_User { get; set; }

        public System.Data.Entity.DbSet<Model.AP_Centrifuge_Real> AP_Centrifuge_Real { get; set; }

        public System.Data.Entity.DbSet<Model.SB_Client> SB_Client { get; set; }
    }
}