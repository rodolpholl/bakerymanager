using BakeryManager.BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace BakeryManager.BackOffice
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /*protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {

                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var tipoUsuario = WebHelpers.ObtertipoUsuario(HttpContext.Current.User.Identity.Name);

                if (authCookie != null)
                {
                    //Extract the forms authentication cookie
                    var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    HttpContext.Current.User = new SecurityPrincipalModel(HttpContext.Current.User,
                        new SecurityIdentityModel(new FormsIdentity(authTicket), new UserIdentityModel()
                        {
                            Login = HttpContext.Current.User.Identity.Name,
                            TipoUsuario = tipoUsuario,
                            ExpiraEm = authTicket.Expiration
                        }));

                }

            }

        }*/
    }
}
