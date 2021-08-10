using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZorgApp2.Areas.Identity.Data;
using ZorgApp2.Data;

[assembly: HostingStartup(typeof(ZorgApp2.Areas.Identity.IdentityHostingStartup))]
namespace ZorgApp2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ZorgApp2Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ZorgApp2ContextConnection")));

                services.AddDefaultIdentity<ZorgApp2User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ZorgApp2Context>();
            });
        }
    }
}