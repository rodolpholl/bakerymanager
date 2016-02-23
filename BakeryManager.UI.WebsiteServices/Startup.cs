using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BakeryManager.UI.WebsiteServices.Startup))]

namespace BakeryManager.UI.WebsiteServices
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
           
        }
    }
}
