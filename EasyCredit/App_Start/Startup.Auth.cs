using System;
using System.Threading.Tasks;

using Autofac;

using EasyCredit.Infrastructure;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using Owin;

using EasyCredit.Models.Identity;


namespace EasyCredit
{
    public partial class Startup
    {
        // For more information on configuring authentication, please 
        // visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app, ContainerBuilder builder)
        {
            // Enable the application to use a cookie to store information for the signed in user and to use 
            // a cookie to temporarily store information about a user logging in with a third party 
            // login provider 
            // Configure the sign in cookie
            Func<CookieValidateIdentityContext, Task> validateIdentityCallback
                = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, Guid>(
                    getUserIdCallback: identity => identity.GetUserGuidId(),
                    validateInterval: TimeSpan.FromMinutes(15),
                    regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = TimeSpan.FromMinutes(15),
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add 
                    // an external login to your account.  
                    OnValidateIdentity = validateIdentityCallback
                }
            });


            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //// Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromSeconds(30));

            //// Enables the application to remember the second login verification factor such as phone or email.
            //// Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            //// This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
    }
}