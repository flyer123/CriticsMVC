using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Test_Google_Api.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;


[assembly: OwinStartup(typeof(Test_Google_Api.Startup))]

namespace Test_Google_Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}