using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BakeryManager.UI.DeliciaDiPane.Startup))]
namespace BakeryManager.UI.DeliciaDiPane
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            return;
        }
    }
}
