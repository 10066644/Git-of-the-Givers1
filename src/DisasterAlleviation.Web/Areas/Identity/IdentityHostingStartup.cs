using DisasterAlleviation.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

[assembly: HostingStartup(typeof(DisasterAlleviation.Web.Areas.Identity.IdentityHostingStartup))]
namespace DisasterAlleviation.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=app.db"));
                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}
