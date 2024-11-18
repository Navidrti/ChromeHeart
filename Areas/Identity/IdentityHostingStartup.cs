using System;
using ChromeHeart.Areas.Identity.Data;
using ChromeHeart.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ChromeHeart.Areas.Identity.IdentityHostingStartup))]
namespace ChromeHeart.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DbWebFinal>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DbWebFinalConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>().AddEntityFrameworkStores<DbWebFinal>();
                services.AddAuthorization(x =>
                {
                    x.AddPolicy("AdminPolicy", p => p.RequireRole("Admin"));
                });
            });
        }
    }
}
