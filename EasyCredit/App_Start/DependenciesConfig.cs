using System;
using System.Data.Entity;
using System.Web;

using Autofac;
using Autofac.Integration.Mvc;

using EasyCredit.Contexts;
using EasyCredit.Models.Identity;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;


namespace EasyCredit
{
    public class DependenciesConfig
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //            // OPTIONAL: Register model binders that require DI.
            //            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            //            builder.RegisterModelBinderProvider();
            
            // This includes:
            // HttpContextBase
            // HttpRequestBase
            // HttpResponseBase
            // HttpServerUtilityBase
            // HttpSessionStateBase
            // HttpApplicationStateBase
            // HttpBrowserCapabilitiesBase
            // HttpFileCollectionBase
            // RequestContext
            // HttpCachePolicyBase
            // VirtualPathProvider
            // UrlHelper
            builder.RegisterModule<AutofacWebTypesModule>();
            
            //            // OPTIONAL: Enable property injection in view pages.
            //            builder.RegisterSource(new ViewRegistrationSource());
            //
            //            // OPTIONAL: Enable property injection into action filters.
            //            builder.RegisterFilterProvider();
            //
            //            // OPTIONAL: Enable action method parameter injection (RARE).
            //            builder.InjectActionInvoker();

            builder
                .RegisterType<ApplicationDbContext>()
                .AsSelf()
                .As<DbContext>()
                .InstancePerRequest();

            builder
                .RegisterType<UserStore<
                    ApplicationUser,
                    ApplicationRole,
                    Guid,
                    ApplicationUserLogin,
                    ApplicationUserRole,
                    ApplicationUserClaim>>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .Register(c => new IdentityFactoryOptions<ApplicationUserManager>
                {
                    DataProtectionProvider = new DpapiDataProtectionProvider("EasyCredit")
                });

            builder
                .RegisterType<ApplicationUserManager>()
                .As<UserManager<ApplicationUser, Guid>, ApplicationUserManager>()
                .InstancePerRequest();

            builder
                .Register(context => new ApplicationSignInManager(
                    context.Resolve<ApplicationUserManager>(),
                    context.Resolve<HttpRequestBase>().GetOwinContext().Authentication))
                .As<SignInManager<ApplicationUser, Guid>, ApplicationSignInManager>()
                .InstancePerRequest();
        }
    }
}
