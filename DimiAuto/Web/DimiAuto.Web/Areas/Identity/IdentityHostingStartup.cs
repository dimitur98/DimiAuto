using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DimiAuto.Web.Areas.Identity.IdentityHostingStartup))]

namespace DimiAuto.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
